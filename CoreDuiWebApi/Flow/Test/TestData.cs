using System;
using System.Collections.Generic;
using CoreDui.Attributes;
using CoreDui.Definitions;
using CoreDui.JsonSerializers.Collection;
using Newtonsoft.Json;

namespace CoreDuiWebApi.Flow.Test
{
    public class TestData
    {
        public PersonalDetails PersonalDetails { get; set; }
    }

    public class PersonalDetails
    {
        public IndividualDetails MainMember { get; set; }
        public string LastName { get; set; }
        public bool HasSpouse { get; set; }
        public IndividualDetails Spouse { get; set; }
        public bool? HasChildren { get; set; }
        
        [RequiredIf(nameof(HasChildren), true)]
        [CollectionRange(1, 3)]
        [JsonConverter(typeof(BaseCollectionConverter))]
        public ICollection<Child> Children { get; set; }
    }

    public class Child : BaseCollectionModel
    {
        public bool isBiologicalChild { get; set; }
        public IndividualDetails Details { get; set; }
    }

    public class IndividualDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SelectOption Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

}
