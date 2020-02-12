using System.Threading.Tasks;
using CoreDuiWebApi.Authentication.DbUserEf;

namespace CoreDuiWebApi.Authentication
{
    public interface IAuthenticationService<TResult>
    {
        Task<TResult> Login(string uid, string password);
        TResult CreateToken(TResult user);
        TResult CreateToken(DbUser user);
        Task<RegisterUserResult> RegisterUser(RegisterUser user);
    }
}
