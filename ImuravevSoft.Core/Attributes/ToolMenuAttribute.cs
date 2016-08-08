using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImuravevSoft.Core.Attributes
{
    public class ToolMenuAttribute:Attribute
    {
        public Type ToolType { get; set; }

        public ToolMenuAttribute(Type t)
        {
            ToolType = t;
        }
    }
}
