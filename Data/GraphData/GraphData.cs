using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ImuravevSoft.Core.Attributes;

namespace ImuravevSoft.GraphData
{
    [DataTree("Графы", "29712F03-947C-41D2-9A21-A7DFEA178448")]
    public class GraphData : BaseData
    {
        public GraphData() : base()
        {
            Name = "Граф";
        }

        public override void Load(BinaryReader reader)
        {
            Name = reader.ReadString();
        }

        public override void Save(BinaryWriter writer)
        {
            writer.Write(Name);
        }
    }
}
