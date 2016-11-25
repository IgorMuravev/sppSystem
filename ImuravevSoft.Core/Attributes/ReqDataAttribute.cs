using System;

namespace ImuravevSoft.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ReqDataAttribute : Attribute
    {
        public Type Data { get; set; }

        public ReqDataAttribute(Type data)
        {
            Data = data;
        }
    }
}
