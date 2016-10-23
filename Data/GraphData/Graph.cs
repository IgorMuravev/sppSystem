using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImuravevSoft.GraphData
{
    [DataTree("Графы")]
    public class Graph : BaseData,IDrawable
    {
        [Serializable]
        internal class GraphStruct
        {
            public List<Vertex> Vertexes { get; set; }
            public List<Edge> Edges { get; set; }
        }
        private void setBorder()
        {
            double xmin = Double.PositiveInfinity;
            double xmax = Double.NegativeInfinity;
            double ymin = Double.PositiveInfinity;
            double ymax = Double.NegativeInfinity;
            foreach (var v in Vertexes)
            {
                if (v.X > xmax) xmax = v.X;
                if (v.X < xmin) xmin = v.X;
                if (v.Y > ymax) ymax = v.Y;
                if (v.Y < ymin) ymin = v.Y;
            }
            Border = new RectangleF((float)xmin, (float)ymin, (float)(xmax - xmin), (float)(ymax - ymin));
        }
        public RectangleF Border { get; private set; }
        public List<Vertex> Vertexes { get; private set; }
        public List<Edge> Edges { get; private set; }
        public int VertexCount { get { return Vertexes.Count; } }
        public int EdgeCount { get { return Edges.Count; } }
        public Graph() : base()
        {
            Name = "Граф";
            Vertexes = new List<Vertex>();
            Edges = new List<Edge>();
            setBorder();
        }
        public Graph(List<Vertex> vs, List<Edge> es) : base()
        {
            Vertexes = vs;
            Edges = es;
            setBorder();
        }
        public override void Load(BinaryReader reader)
        {
            base.Load(reader);
            BinaryFormatter deserializer = new BinaryFormatter();
            var result = deserializer.Deserialize(reader.BaseStream) as GraphStruct;
            Vertexes = result.Vertexes;
            Edges = result.Edges;
            setBorder();
        }
        public override void Save(BinaryWriter writer)
        {
            base.Save(writer);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(writer.BaseStream, new GraphStruct() { Vertexes = this.Vertexes, Edges = this.Edges });
        }

        void IDrawable.Draw(Graphics g, Func<PointF, PointF> transform)
        {
            var points = new Dictionary<Vertex, PointF>();
            foreach (var v in Vertexes)
            {
                var p = transform(new PointF((float)v.X, (float)v.Y));
                points.Add(v, p);
                g.FillEllipse(Brushes.Blue, p.X - 3f, p.Y - 3f, 6f, 6f);
            }

            foreach (var e in Edges)
            {
                var p1 = points[e.V1];
                var p2 = points[e.V2];
                g.DrawLine(Pens.Black, p1.X, p1.Y, p2.X, p2.Y);
            }
        }

        RectangleF IDrawable.GetBorder()
        {
            return Border;
        }
    }




}
