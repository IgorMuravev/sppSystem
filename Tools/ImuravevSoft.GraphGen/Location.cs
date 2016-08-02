using ImuravevSoft.GraphData;
using System;


namespace ImuravevSoft.GraphGen
{
    public struct Location
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;


        public static Random rnd = new Random();
        public Point GetRandomPoint()
        {
            return new Point(Width * rnd.NextDouble() + X, Height * rnd.NextDouble() + Y);
        }
    }
}
