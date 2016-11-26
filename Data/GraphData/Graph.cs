
using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImuravevSoft.GraphData
{
    [DataTree("Графы")]
    public class Graph : BaseData, IDrawable
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

        public Dictionary<string, MarkerStyle> Markers { get; private set; }
        public Graph() : base()
        {
            Name = "Граф";
            Vertexes = new List<Vertex>();
            Markers = new Dictionary<string, MarkerStyle>();
            Edges = new List<Edge>();
            setBorder();
        }
        public Graph(List<Vertex> vs, List<Edge> es) : base()
        {

            Vertexes = vs;
            Edges = es;
            Markers = new Dictionary<string, MarkerStyle>();
            setBorder();
        }
        public override void Load(BinaryReader reader)
        {
            base.Load(reader);
            BinaryFormatter deserializer = new BinaryFormatter();
            var result = deserializer.Deserialize(reader.BaseStream) as GraphStruct;
            Vertexes = result.Vertexes;
            Edges = result.Edges;
            Markers = deserializer.Deserialize(reader.BaseStream) as Dictionary<string, MarkerStyle>;
            setBorder();
        }
        public override void Save(BinaryWriter writer)
        {
            base.Save(writer);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(writer.BaseStream, new GraphStruct() { Vertexes = this.Vertexes, Edges = this.Edges });
            serializer.Serialize(writer.BaseStream, Markers);
        }

        void IDrawable.Draw(Graphics g, Func<PointF, PointF> transform)
        {
            var points = new Dictionary<Vertex, PointF>();
            var sortedStyles = Markers.OrderBy(x => x.Value.Depth).Select(x => x.Key).ToList();
            var font = new Font("Arial", 8f);

            foreach (var v in Vertexes)
            {
                var p = transform(new PointF((float)v.X, (float)v.Y));
                points.Add(v, p);
                var style = false;
                if (v.Markers.Count > 0)
                {
                    foreach (var s in sortedStyles)
                    {
                        if (v.Markers.Contains(s))
                        {
                            g.FillEllipse(new SolidBrush(Markers[s].Color), p.X - Markers[s].Width / 2, p.Y - Markers[s].Width / 2, Markers[s].Width, Markers[s].Width);
                            style = true;
                            break;
                        }
                    }
                    if (!style)
                        g.FillEllipse(Brushes.Blue, p.X - 3f, p.Y - 3f, 6f, 6f);
                }
                else
                    g.FillEllipse(Brushes.Blue, p.X - 3f, p.Y - 3f, 6f, 6f);

                g.DrawString(v.Caption, font, Brushes.Black, p.X + 10, p.Y - 8);
            }

            foreach (var e in Edges)
            {
                var p1 = points[e.V1];
                var p2 = points[e.V2];

                var style = false;
                if (e.Markers.Count > 0)
                {
                    foreach (var s in sortedStyles)
                    {
                        if (e.Markers.Contains(s))
                        {
                            g.DrawLine(new Pen(Markers[s].Color, Markers[s].Width), p1.X, p1.Y, p2.X, p2.Y);
                            style = true;
                            break;
                        }
                    }
                    if (!style)
                        g.DrawLine(Pens.Black, p1.X, p1.Y, p2.X, p2.Y);
                }
                else
                    g.DrawLine(Pens.Black, p1.X, p1.Y, p2.X, p2.Y);


                var centerX = (p1.X + p2.X) / 2;
                var centerY =(p1.Y + p2.Y) / 2;

                g.DrawString(e.Weight.ToString("f3"), font, Brushes.Black, centerX, centerY);



            }
        }

        RectangleF IDrawable.GetBorder()
        {
            return Border;
        }

        public void Export(string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine(String.Join(";", Edges.Select(x => x.Weight)));
                writer.WriteLine(String.Format("{0};{1}", VertexCount, EdgeCount));
                foreach (var vertex in Vertexes)
                {
                    var row = new int[EdgeCount];
                    for (int i = 0; i < EdgeCount; i++)
                    {
                        if (vertex == Edges[i].V1) row[i] = 100;
                        if (vertex == Edges[i].V2) row[i] = -100;
                    }
                    writer.WriteLine(String.Join(";", row));
                }
            }
        }

        public void Export(StreamWriter writer)
        {

            writer.WriteLine(String.Join(";", Edges.Select(x => (int)(100 * x.Weight))));
            writer.WriteLine(String.Format("{0};{1}", VertexCount, EdgeCount));
            foreach (var vertex in Vertexes)
            {
                var row = new int[EdgeCount];
                for (int i = 0; i < EdgeCount; i++)
                {
                    if (vertex == Edges[i].V1) row[i] = 1;
                    if (vertex == Edges[i].V2) row[i] = -1;
                }
                writer.WriteLine(String.Join(";", row));
            }
        }


        public void Import(string filename)
        {
            throw new NotImplementedException();
        }
    }




}
