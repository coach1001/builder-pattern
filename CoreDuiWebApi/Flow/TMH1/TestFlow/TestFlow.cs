using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDui.Builders;
using CoreDui.Definitions;
using CoreDui.Enums;

namespace CoreDuiWebApi.Flow.TMH1.TestFlow
{
    public static class TestFlow
    {        
        public static void RegisterFlow(IModuleBuilder moduleBuilder)
        {
            var flow = moduleBuilder
                .WithFlow<TestData, TestContext>("test-flow")
                    .RequiresAuthorization()
                    .WithStep(m => m.PersonalDetails)
                        .Next("Next")
                        .GridConfig("1fr")
                        .AddGroup(m => m.MainMember, "Main Member")
                            .GridConfig("4fr 4fr 2fr")
                            .AddControl(m => m.FirstName, ControlType.Text, "First Name").End()
                            .AddControl(m => m.LastName, ControlType.Text, "Last Name").End()
                            .AddControl(m => m.Gender, ControlType.Select, "Gender")
                                .WithOptions(Constants.Lookups.Gender)
                            .End()
                            .AddControl(m => m.DateOfBirth, ControlType.DateTime).End()
                        .End()
                        .AddControl(m => m.HasSpouse, ControlType.Boolean, "Do you have a spouse?")
                        .End()
                        .AddGroup(m => m.Spouse, "Spouse")
                            .GridConfig("4fr 4fr 2fr")
                            .WithReactivity(m => m.HasSpouse == false, ReactivityType.ClearWhen)
                            .WithReactivity(m => m.HasSpouse == true, ReactivityType.VisibleWhen)
                            .AddControl(m => m.FirstName, ControlType.Text, "First Name").End()
                            .AddControl(m => m.LastName, ControlType.Text, "Last Name").End()
                            .AddControl(m => m.Gender, ControlType.Select, "Gender")
                                .WithOptions(Constants.Lookups.Gender)
                            .End()
                            .AddControl(m => m.DateOfBirth, ControlType.DateTime).End()
                        .End()
                        .AddControl(m => m.HasChildren, ControlType.Boolean, "Do you have children?")
                        .End()
                        .AddArray(m => m.Children, "Children")
                            .GridConfig("1fr 4fr")                            
                            .WithReactivity(m => m.HasChildren ==  false, ReactivityType.ClearWhen)
                            .WithReactivity(m => m.HasChildren == true, ReactivityType.VisibleWhen)
                            .AddControl(m => m.isBiologicalChild, ControlType.Boolean, "Biological child?")
                            .End()
                            .AddGroup(m => m.Details)
                                .PositionConfig("2/5")
                                .GridConfig("4fr 4fr 2fr")
                                .AddControl(m => m.FirstName, ControlType.Text, "First Name").End()
                                .AddControl(m => m.LastName, ControlType.Text, "Last Name").End()
                                .AddControl(m => m.Gender, ControlType.Select, "Gender")
                                   .WithOptions(Constants.Lookups.Gender)
                                .End()
                            .End()
                        .End()
                    .End();
            moduleBuilder.AddFlowToModule("", "lab-calculator", "tmh1", flow.Flow);
        }
    }
}
