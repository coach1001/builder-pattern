using System.Threading.Tasks;
using CoreDuiWebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using CoreDuiWebApi.Authentication.DbUserEf;

namespace CoreDuiWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/security")]    
    public class SecurityController : ControllerBase
    {
        private readonly IAuthenticationService<DbUserClient> _authDbService;
        private readonly IAuthenticationService<LdapUser> _authLdapService;              

        public SecurityController(
            IAuthenticationService<DbUserClient> authDbService,
            IAuthenticationService<LdapUser> authLdapService)
        {
            _authDbService = authDbService;
            _authLdapService = authLdapService;            
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            if(registerUser.Password != registerUser.ConfirmPassword)
            {
                return BadRequest(new { message = "Passwords do not match." });
            }

            var registerUserResult = await _authDbService.RegisterUser(registerUser);
            
            if(registerUserResult.UserCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = registerUserResult.Reason });
            }
            
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            var ldapUser = await _authLdapService.Login(credentials.UserName, credentials.Password);                        
            if (ldapUser != null)
            {
                return Ok(ldapUser);
            }
            var dbUser = await _authDbService.Login(credentials.UserName, credentials.Password);
            if (dbUser != null)
            {
                return Ok(dbUser);
            }
            return Unauthorized();            
        }

        [Route("secure-test")]
        [HttpGet]
        public IActionResult SecureTest()
        {
            return Ok();
        }

    }
}