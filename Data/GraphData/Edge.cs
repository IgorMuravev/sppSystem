using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImuravevSoft.GraphData
{
    [Serializable]
    public class Edge
    {
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }
        public double Weight { get; set; }
        public Edge(Vertex v1, Vertex v2, double W = 1)
        {
            V1 = v1;
            V2 = v2;
            Weight = W;
        }
    }
}
