using System;

namespace ImuravevSoft.Core.Attributes
{
    public class DataTreeAttribute : Attribute
    {

        public DataTreeAttribute(string v)
        {
            Name = v;
        }
        public string Name { get; set; }
    }
}
