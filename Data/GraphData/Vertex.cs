using System;

namespace ImuravevSoft.GraphData
{
    [Serializable]
    public class Vertex : VertexPoint
    {
        public string Caption { get; set; }
        public Vertex(VertexPoint p, string caption) : base(p.X, p.Y)
        {
            Caption = caption;
        }

    }
}
