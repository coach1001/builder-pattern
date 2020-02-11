using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Authentication
{
    public class HashSalt
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
