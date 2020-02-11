using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreDuiWebApi.Authentication.DbUserEf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CoreDuiWebApi.Authentication
{
    public class DbAuthenticationService : IAuthenticationService<DbUserClient>
    {        
        private readonly JwtConfig _jwtConfig;
        private readonly DbUserContext _context;

        public DbAuthenticationService(IOptions<JwtConfig> jwtConfig, DbUserContext context)
        {            
            _jwtConfig = jwtConfig.Value;
            _context = context;
        }

        public async Task<DbUserClient> Login(string emailAddress, string password)
        {
            var dbUser = await _context.DbUsers.Where(x => x.EmailAddress == emailAddress).ToListAsync();
            if (dbUser != null && dbUser.Count > 0)
            {
                if (SaltedHash.VerifyPassword(password, dbUser[0].Hash, dbUser[0].Salt))
                {
                    return CreateToken(dbUser[0]);
                }                
            }
            return null;
        }

        public DbUserClient CreateToken(DbUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.SymmetricKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.Name, user.Id.ToString()),
                            new Claim("id", user.Id.ToString()),                            
                            new Claim("provider", "EMAIL_PASSWORD"),
                            new Claim("account_enabled", user.AccountEnabled.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var dbUserClient = new DbUserClient
            {
                Id = user.Id,
                EmailAddress = user.EmailAddress,
                ProviderType = user.ProviderType,
                Name = user.Name,
                DisplayName = user.DisplayName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccountEnabled = user.AccountEnabled,
                Token = tokenHandler.WriteToken(token)
        };            
            return dbUserClient;
        }

        public DbUserClient CreateToken(DbUserClient user)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(RegisterUser user)
        {
            var saltedHash = SaltedHash.Generate(64, user.Password);
            var dbUserToCreate = new DbUser
            {
                ProviderId = null,
                ProviderType = "EMAIL_PASSWORD",
                EmailAddress = user.EmailAddress,
                AccountEnabled = false,
                ConfirmEmailToken = Guid.NewGuid(),
                ConfirmEmailTokenExpiresAt = DateTime.UtcNow.AddMinutes(30),
                Hash = saltedHash.Hash,
                Salt = saltedHash.Salt
            };
            _context.DbUsers.Add(dbUserToCreate);
            await _context.SaveChangesAsync();
        }
    }
}
