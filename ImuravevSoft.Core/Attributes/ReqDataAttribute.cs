using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImuravevSoft.Core.Attributes
{
    public class ReqDataAttribute : Attribute
    {
        public Type Data { get; set; }

        public ReqDataAttribute(Type data)
        {
            Data = data;
        }
    }
}
