using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GraphData
{
    public class GraphData : BaseData
    {
        public GraphData() : base()
        {
            Name = "Граф";
        }

        public override void Load(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Save(BinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
