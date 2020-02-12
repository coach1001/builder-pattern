using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Authentication
{
    public class RegisterUserResult
    {
        public bool UserCreated { get; set; }
        public string Reason { get; set; }
    }
}
