using System;

namespace ImuravevSoft.GraphData
{
    [Serializable]
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Distance(Point p)
        {
            return Math.Sqrt((p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y));
        }

        public double Distance2(Point p)
        {
            return (p.X - X) * (p.X - X) + (p.Y - Y) * (p.Y - Y);
        }

        public static double Distance(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public static double operator *(Point a, Point b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
        public static Point operator *(double a, Point b)
        {
            return new Point(a * b.X, a * b.Y);
        }


    }
}
