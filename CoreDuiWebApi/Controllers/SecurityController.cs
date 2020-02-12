using System.Threading.Tasks;
using CoreDuiWebApi.Authentication;
using CoreDuiWebApi.Email.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreDuiWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/security")]    
    public class SecurityController : ControllerBase
    {
        private readonly IAuthenticationService<DbUserClient> _authDbService;
        private readonly IAuthenticationService<LdapUser> _authLdapService;
        private readonly IEmailTemplates _emailTemplates;
        private readonly AppConfig _appConfig;

        public SecurityController(
            IAuthenticationService<DbUserClient> authDbService,
            IAuthenticationService<LdapUser> authLdapService,
            IEmailTemplates emailTemplates,
            IOptions<AppConfig> appConfig)
        {
            _authDbService = authDbService;
            _authLdapService = authLdapService;
            _emailTemplates = emailTemplates;
            _appConfig = appConfig.Value;
        }

        [AllowAnonymous]
        [Route("validate-account/{accountId}")]
        [HttpGet]
        public IActionResult ValidateAccount(string accountId, [FromQuery] string validationToken)
        {
            return Redirect(
                $"{_appConfig.UiHost}:{_appConfig.UiPort}/{_appConfig.UiAccountValidationSuccessPath}"
            );
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
                await _emailTemplates.BuildAndSendValidateEmail(
                    registerUserResult.EmailAddress,
                    registerUserResult.UserId.ToString(),
                    registerUserResult.ValidationToken.ToString());
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