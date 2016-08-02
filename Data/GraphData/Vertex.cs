using System;
namespace ImuravevSoft.GraphData
{
    [Serializable]
    public class Vertex : Point
    {
        public string Caption { get; set; }
        public Vertex(Point p, string caption) : base(p.X, p.Y)
        {
            Caption = caption;
        }

    }
}
