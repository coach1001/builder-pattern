using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreDuiWebApi.Authentication.DbUserEf;
using CoreDuiWebApi.Authentication.DbUserRoleEf;
using CoreDuiWebApi.Flow.TMH1.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoreDuiWebApi.Authentication
{
    public class LdapAuthenticationService : IAuthenticationService<LdapUser>
    {
        private readonly LdapConfig _ldapConfig;
        private readonly JwtConfig _jwtConfig;
        private readonly DbLabCalcContext _context;

        public LdapAuthenticationService(IOptions<LdapConfig> ldapConfig, IOptions<JwtConfig> jwtConfig, DbLabCalcContext context)
        {
            _ldapConfig = ldapConfig.Value;
            _jwtConfig = jwtConfig.Value;
            _context = context;
        }

        public async Task<LdapUser> Login(string userName, string password)
        {
            using (var ldap = new LdapConnection(new LdapDirectoryIdentifier(_ldapConfig.Server, _ldapConfig.Port)))
            {
                ldap.SessionOptions.ProtocolVersion = 3;
                ldap.AuthType = AuthType.Basic;
                ldap.Bind(new NetworkCredential(_ldapConfig.BindDn, _ldapConfig.BindPassword));
                var dn = GetDn(ldap, userName);                
                try
                {
                    ldap.Bind(new NetworkCredential(dn, password));
                    var user = GetUser(ldap, dn);
                    var dbUser = await _context.DbUsers.Where(x => x.ProviderId == user.Uid).ToListAsync();
                    if (dbUser == null || dbUser.Count <= 0)
                    {
                        var dbUserToCreate = new DbUser
                        {
                            ProviderId = user.Uid,
                            ProviderType = "LDAP",
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            DisplayName = user.DisplayName,
                            AccountEnabled = true,
                            Roles = new List<DbUserRole>()
                            {
                                { new DbUserRole { Role = Roles.User } }
                            }
                        };
                        _context.DbUsers.Add(dbUserToCreate);
                        await _context.SaveChangesAsync();
                        user.Id = dbUserToCreate.Id;
                    }
                    else
                    {
                        user.Id = dbUser[0].Id;                        
                        user.Roles = dbUser[0].Roles.Select(m => m.Role).ToList();
                    }
                    user = CreateToken(user);
                    return user;
                }
                catch (LdapException)
                {
                    return null;
                }
            }
        }

        private string GetDn(LdapConnection ldap, string userName)
        {
            var request = new SearchRequest(_ldapConfig.SearchDn, string.Format(CultureInfo.InvariantCulture, _ldapConfig.Filter, userName), SearchScope.Subtree);
            var response = (SearchResponse)ldap.SendRequest(request);
            if (response.Entries.Count == 1)
            {
                return response.Entries[0].DistinguishedName;
            }
            return null;
        }

        private LdapUser GetUser(LdapConnection ldap, string dn)
        {
            var request = new SearchRequest(dn, "(objectclass=*)", SearchScope.Base);
            var response = (SearchResponse)ldap.SendRequest(request);
            if (response.Entries.Count == 1)
            {
                return new LdapUser(response.Entries[0]);
            }
            return null;
        }

        public LdapUser CreateToken(LdapUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.SymmetricKey);            
            var tokenDescriptor = new SecurityTokenDescriptor
            {                
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.Name, user.Id.ToString()),
                            new Claim("id", user.Id.ToString()),
                            new Claim("provider_id", user.Uid),
                            new Claim("provider", "LDAP"),
                            new Claim("account_enabled", true.ToString())                            
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            foreach (var role in user.Roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public LdapUser CreateToken(DbUser user)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterUserResult> RegisterUser(RegisterUser user)
        {
            throw new NotImplementedException();
        }
    }
}
