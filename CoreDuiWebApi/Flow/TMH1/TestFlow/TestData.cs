using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Definitions;

namespace CoreDuiWebApi.Flow.TMH1.TestFlow
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
        public bool HasChildren { get; set; }
        public ICollection<Child> Children { get; set; }
    }

    public class Child
    {
        public bool isBiologicalChild { get; set; }
        public IndividualDetails Details { get; set; }
    }

    public class IndividualDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SelectOption Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

}
