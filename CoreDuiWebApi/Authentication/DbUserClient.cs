using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Authentication
{
    public class DbUserClient
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        [Required]
        public string ProviderType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public bool AccountEnabled { get; set; }
        public ICollection<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
