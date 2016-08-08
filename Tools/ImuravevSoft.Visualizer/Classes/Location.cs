using ImuravevSoft.GraphData;
using System;


namespace ImuravevSoft.Visualizer.Classes
{
    public struct Location
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;


        public static Random rnd = new Random();
        public VertexPoint GetRandomPoint()
        {
            return new VertexPoint(Width * rnd.NextDouble() + X, Height * rnd.NextDouble() + Y);
        }
    }
}
