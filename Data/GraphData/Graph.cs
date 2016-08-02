﻿using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Data;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ImuravevSoft.GraphData
{
    [DataTree("Графы", "29712F03-947C-41D2-9A21-A7DFEA178448")]
    public class Graph : BaseData
    {
        public List<Vertex> Vertexes { get; private set; }
        public List<Edge> Edges { get; private set; }
        public int VertexCount { get { return Vertexes.Count; } }
        public int EdgeCount { get { return Edges.Count; } }
        public Graph() : base()
        {
            Name = "Граф";
            Vertexes = new List<Vertex>();
            Edges = new List<Edge>();
        }
        public Graph(List<Vertex> vs, List<Edge> es) : base()
        {
            Vertexes = vs;
            Edges = es;
        }
        public void Save(string fileName)
        {
            Stream FileStream = File.Create(fileName);

            FileStream.Close();
        }
        public static Graph Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                Stream FileStream = File.OpenRead(fileName);

            }
            return null;
        }
        public override void Load(BinaryReader reader)
        {
            Name = reader.ReadString();
            BinaryFormatter deserializer = new BinaryFormatter();
            Vertexes = (List<Vertex>)deserializer.Deserialize(reader.BaseStream);
            Edges = (List<Edge>)deserializer.Deserialize(reader.BaseStream);
        }
        public override void Save(BinaryWriter writer)
        {
            writer.Write(Name);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(writer.BaseStream, Vertexes);
            serializer.Serialize(writer.BaseStream, Edges);
        }
    }


}
