using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImuravevSoft.Core.Attributes
{
    public class ToolAttribute : Attribute
    {
        public string Name { get; set; }
        public string Desc { get; set; }

        public ToolAttribute(string n, string d)
        {
            Name = n;
            Desc = d;
        }
    }
}
