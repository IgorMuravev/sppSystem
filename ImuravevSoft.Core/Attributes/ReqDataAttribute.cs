using System;

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
