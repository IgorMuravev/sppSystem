using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ImuravevSoft.GraphData;
using System.IO;
using ImuravevSoft.Shell;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImuravevSoft.GraphMarkerData
{
    [DataTree("Маркеры графов")]
    public class GraphMarker : BaseData, IDrawable
    {
        private Guid ownerId;
        private Graph owner;
        public Graph Owner
        {
            get
            {
                if (owner == null && ownerId != Guid.Empty)
                {
                    owner = Main.Shell.DataManager.DataById(ownerId) as Graph;
                }
                return owner;
            }
        }

        public List<Vertex> MarkeredVertex { get; private set; }
        public List<Edge> MarkeredEdges { get; private set; }

        public GraphMarker(Graph parent, List<Vertex> v, List<Edge> e) : base()
        {
            ownerId = parent.Id;
            owner = parent;

            MarkeredVertex = v;
            MarkeredEdges = e;
        }
        public override void Load(BinaryReader reader)
        {
            base.Load(reader);
            ownerId = Guid.Parse(reader.ReadString());

            BinaryFormatter deserializer = new BinaryFormatter();
            var result = deserializer.Deserialize(reader.BaseStream) as GraphStruct;
            MarkeredVertex = result.Vertexes;
            MarkeredEdges = result.Edges;

        }

        public override void Save(BinaryWriter writer)
        {
            base.Save(writer);
            writer.Write(Owner == null ? Guid.Empty.ToString() : Owner.Id.ToString());


            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(writer.BaseStream, new GraphStruct() { Vertexes = this.MarkeredVertex, Edges = this.MarkeredEdges });

        }
        void IDrawable.Draw(Graphics g, Func<PointF, PointF> transform)
        {

            var points = new Dictionary<Vertex, PointF>();
            foreach (var v in MarkeredVertex)
            {
                var p = transform(new PointF((float)v.X, (float)v.Y));
                points.Add(v, p);
                g.FillEllipse(Brushes.Red, p.X - 3f, p.Y - 3f, 6f, 6f);
            }
     
            foreach (var edges in MarkeredEdges)
            {
                var p1 = points[edges.V1];
                var p2 = points[edges.V2];
                g.DrawLine(Pens.Red, p1.X, p1.Y, p2.X, p2.Y);
            }
        }

        RectangleF IDrawable.GetBorder()
        {
            if (Owner != null)
              return   (Owner as IDrawable).GetBorder();

            return new RectangleF();
        }

        private class GraphStruct
        {
            public List<Edge> Edges { get; set; }
            public List<Vertex> Vertexes { get; set; }
        }
    }
}
