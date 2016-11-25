using System;
using System.Collections.Generic;

namespace ImuravevSoft.GraphData
{
    [Serializable]
    public class Vertex : VertexPoint
    {
        public HashSet<string> Markers { get; private set; }
        public string Caption { get; set; }
        public Vertex(VertexPoint p, string caption) : base(p.X, p.Y)
        {
            Caption = caption;
            Markers = new HashSet<string>();
        }

    }
}
