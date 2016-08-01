using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImuravevSoft.Core.Attributes
{
    public class DataTreeAttribute : Attribute
    {

        public DataTreeAttribute(string v, string g)
        {
            Name = v;
            TypeGuid = new Guid(g);
        }

        public string Name { get; set; }
        public Guid TypeGuid { get; set; }
    }
}
