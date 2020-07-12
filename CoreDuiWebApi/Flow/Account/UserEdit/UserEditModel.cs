using System;
using System.Collections.Generic;
using CoreDui.Attributes;
using CoreDui.Definitions;
using CoreDui.JsonSerializers.Collection;
using Newtonsoft.Json;

namespace CoreDuiWebApi.Flow.Account.UserEdit
{
    public class UserEditModel
    {        
        public UserGrid UserGrid { get; set; } 
        public User UserEdit { get; set; }
    }

    public class UserGrid
    {
        [JsonConverter(typeof(BaseCollectionConverter))]
        public ICollection<User> Users { get; set; }
    }

    public class User : BaseCollectionModel
    {
        public Guid Id { get; set; }
        public string ProviderId { get; set; }
        public string ProviderType { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool AccountEnabled { get; set; }
    }
}
