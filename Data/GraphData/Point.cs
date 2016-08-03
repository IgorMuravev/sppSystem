using System;

namespace ImuravevSoft.GraphData
{
    [Serializable]
    public class VertexPoint
    {
        public double X { get; private set; }
        public double Y { get;  private set; }

        public VertexPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Distance(VertexPoint p)
        {
            return Math.Sqrt((p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y));
        }

        public double Distance2(VertexPoint p)
        {
            return (p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y);
        }

        public static double Distance(VertexPoint a, VertexPoint b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public static VertexPoint operator +(VertexPoint a, VertexPoint b)
        {
            return new VertexPoint(a.X + b.X, a.Y + b.Y);
        }
        public static VertexPoint operator -(VertexPoint a, VertexPoint b)
        {
            return new VertexPoint(a.X - b.X, a.Y - b.Y);
        }

        public static double operator *(VertexPoint a, VertexPoint b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static VertexPoint operator *(double a, VertexPoint b)
        {
            return new VertexPoint(a * b.X, a * b.Y);
        }


    }
}
