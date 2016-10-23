using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImuravevSoft.Core.Attributes
{
    /// <summary>
    /// Описывает какие инструменты могут использовть какие данные
    /// </summary>
    public class ProxyAttribute:Attribute
    {
        public Type DataType { get; set; }
        public Type InstrumentType { get; set; }
    }
}
