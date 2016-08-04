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
    public class Graph : BaseData
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
            Border = new Rectangle((int)xmin, (int)ymin, (int)(xmax - xmin), (int)(ymax - ymin));
        }
        public Rectangle Border { get; private set; }
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
    }




}
