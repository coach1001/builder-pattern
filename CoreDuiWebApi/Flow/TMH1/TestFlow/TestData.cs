using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDuiWebApi.Flow.TMH1.TestFlow
{
    public class TestData
    {
        public TestStepData TestStep { get; set; }
    }

    public class TestStepData
    {
        public string Title { get; set; }
        public NestedObject NestedObject { get; set; }
        public ICollection<NestedArrayObject> NestedArray { get; set; }
    }

    public class NestedObject
    {
        public string NestedObjectProp { get; set; }
        public DeepNestedObject DeepNestedObject { get; set; }
    }

    public class DeepNestedObject
    {
        public string DeepNestedObjectProp { get; set; }        
    }


    public class NestedArrayObject
    {
        public string NestedArrayObjectProp1 { get; set; }
        public string NestedArrayObjectProp2 { get; set; }
        /*public ICollection<DeepNestedArrayObject> DeepNestedArray { get; set; }*/
    }

    public class DeepNestedArrayObject
    {
        public string DeepNestedArrayObjectProp { get; set; }
    }



}
