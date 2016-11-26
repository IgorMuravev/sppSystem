using ImuravevSoft.GraphData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImuravevSoft.Visualizer.Classes
{
    public class GraphGen
    {

        private static List<DelaunayTriangulator.Vertex> Cast(List<Vertex> v)
        {
            return v.Select(x => new DelaunayTriangulator.Vertex((float)x.X, (float)x.Y)).ToList();
        }
        private static double sepFunc(Location loc, double x, int k)
        {
            if (k > 1)
            {
                return loc.Height * Math.Abs(Math.Sin(x * 0.5 * Math.PI / loc.Width * (k - 1)));
            }
            else
                return -1;
        }
        private static readonly Random random = new Random();
        private static double RandomY(double x, double nextBorder, Location loc, int k, double dy)
        {
            var r = GetNormRandom();
            var border = sepFunc(loc, x, k);
            if (border > nextBorder)
            {
                var buf = border;
                border = nextBorder;
                nextBorder = buf;
            }
            return border + (nextBorder - border - dy) * r;

        }
        public static List<Vertex> GetRandomVertexes(Location location, int vertexCount)
        {
            var result = new List<Vertex>(vertexCount);
            for (int i = 0; i < vertexCount; i++)
            {
                result.Add(new Vertex(location.GetRandomPoint(), i.ToString()));
            }
            return result;
        }

        public static List<Edge> GetrandomEdges(List<Vertex> vertexes, double incDistance, double maxDistance)
        {
            var B = new List<Vertex>(vertexes.Count);
            var A = vertexes.ToList();
            var rnd = new Random();
            var result = new List<Edge>(vertexes.Count);
            var a = A[rnd.Next(0, A.Count)];
            A.Remove(a);
            B.Add(a);
            while (A.Count > 0)
            {
                a = A[rnd.Next(0, A.Count)];
                var b = B[rnd.Next(0, B.Count)];
                double d = a.Distance(b);
                double p = (d < incDistance ? 1 : incDistance / d / d);
                if (p > rnd.NextDouble() && d < maxDistance)
                {
                    A.Remove(a);
                    B.Add(a);
                    result.Add(new Edge(a, b, VertexPoint.Distance(a, b)));
                }
            }
            return result;

        }

        private static double GetNormRandom()
        {
            var sum = 0.0;
            for (int i = 0; i < 8; i++)
                sum += random.NextDouble();
            return sum / 8;
        }
        public static Graph GetRandomGraph(Location location, int vertexCount)
        {
            var vertex = GetRandomVertexes(location, vertexCount);
            return new Graph(vertex, GetrandomEdges(vertex, 0.02 * location.Width, 0.3 * location.Width));
        }

        private static VertexPoint[] GetPoints(Location loc, int k, int count = 100)
        {
            double h = loc.Width / count;
            var array = new VertexPoint[count];
            for (int i = 0; i < count; i++)
            {
                var y = sepFunc(loc, loc.X + h * i, k);
                array[i] = new VertexPoint(loc.X + h * i, y);
            }
            return array;
        }

        private static double DistanceToRoots(VertexPoint v, VertexPoint[] roots, out int root)
        {
            double min = Double.PositiveInfinity;
            root = -1;
            for (int i = 0; i < roots.Length; i++)
            {
                var distance = v.Distance(roots[i]);
                if (distance < min)
                {
                    min = distance;
                    root = i;
                }
            }
            return min;
        }


        private static List<int> Mins(List<Vertex> vertexes, VertexPoint[] roots, int mins)
        {
            var res = new List<int>();
            var min = new List<double>();
            for (int i = 0; i < mins; i++)
            {
                res.Add(-1);
                min.Add(double.PositiveInfinity);
            }

            for (int k = 0; k < vertexes.Count; k++)
            {
                var v = vertexes[k];
                int u = 0;
                double val = DistanceToRoots(v, roots, out u);

                for (int i = 0; i < min.Count; i++)
                {
                    if (val < min[i])
                    {
                        for (int j = min.Count - 1; j > i; j--)
                        {
                            min[j] = min[j - 1];
                            res[j] = res[j - 1];

                        }
                        min[i] = val;
                        res[i] = k;
                        break;
                    }

                }
            }
            return res;

        }
        private static double Rotate(Vertex a, Vertex b, Vertex c)
        {
            return (b.X - a.X) * (c.Y - b.Y) - (b.Y - a.Y) * (c.X - b.X);
        }
        private static List<Vertex> Jarvis(List<Vertex> points)
        {
            var result = new List<Vertex>();
            var used = new bool[points.Count + 1];
            var point = points[0];
            int index = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < point.X)
                {
                    point = points[i];
                    index = i;
                }
            }
            used[index] = true;
            result.Add(point);
            points.Add(point);
            while (true)
            {
                index = -1;
                for (int i = 0; i < points.Count; i++)
                {
                    if (used[i]) continue;
                    if (index < 0) index = i;
                    if (Rotate(point, points[index], points[i]) < 0)
                        index = i;
                }
                if (index == points.Count - 1)
                    break;

                result.Add(points[index]);
                used[index] = true;
                point = points[index];
            }
            points.RemoveAt(points.Count - 1);
            return result;
        }

        public static Graph GetPlanarZoneGraph(Location location, int vertexCount, int zoneCount)
        {

            var triangulator = new DelaunayTriangulator.Triangulator();
            Graph[] g = new Graph[zoneCount];
            List<Edge> edges = new List<Edge>();
            List<Vertex> vertexes = new List<Vertex>();
            double[] roots_one = new double[zoneCount / 2 + zoneCount % 2 + 1];
            double[] roots_zero = new double[zoneCount / 2 + 1];
            double c = 2 * location.Width / (zoneCount - 1);
            roots_one[0] = 0;
            roots_one[roots_one.Length - 1] = location.Width;
            for (int i = 1; i < roots_one.Length - 1; i++)
                roots_one[i] = (2 * location.Width * i - location.Width) / (zoneCount - 1);

            for (int i = 0; i < roots_zero.Length - 1; i++)
                roots_zero[i] = c * i;
            roots_zero[roots_zero.Length - 1] = location.Width;

            var roots = GetPoints(location, zoneCount);
            var addedRoots = new Dictionary<VertexPoint, Vertex>();
            int number = 0;
            for (int i = 0; i < roots_one.Length - 1; i++)
            {
                double prevroot = roots_one[i];
                double nextroot = roots_one[i + 1];
                var list = new List<Vertex>();
                for (int j = 0; j < vertexCount; j++)
                {
                    var r = GetNormRandom();
                    var x = prevroot + (nextroot - prevroot) * r;
                    var y = RandomY(x, location.Height, location, zoneCount, 0.05 * location.Height);
                    var vertex = new Vertex(new VertexPoint(x + location.X, y + location.Y), number.ToString());
                    number++;
                    list.Add(vertex);
                }
                vertexes.AddRange(list);
                var triangles = triangulator.Triangulation(Cast(list));
                foreach (var t in triangles)
                {
                    edges.Add(new Edge(list[t.a], list[t.b], VertexPoint.Distance(list[t.a], list[t.b])));
                    edges.Add(new Edge(list[t.b], list[t.a], VertexPoint.Distance(list[t.a], list[t.b])));
                    edges.Add(new Edge(list[t.b], list[t.c], VertexPoint.Distance(list[t.b], list[t.c])));
                    edges.Add(new Edge(list[t.c], list[t.b], VertexPoint.Distance(list[t.b], list[t.c])));
                    edges.Add(new Edge(list[t.a], list[t.c], VertexPoint.Distance(list[t.a], list[t.c])));
                    edges.Add(new Edge(list[t.c], list[t.a], VertexPoint.Distance(list[t.a], list[t.c])));
                }


            }
            for (int i = 0; i < roots_zero.Length - 1; i++)
            {
                double prevroot = roots_zero[i];
                double nextroot = roots_zero[i + 1];
                var list = new List<Vertex>();
                for (int j = 0; j < vertexCount; j++)
                {
                    var r = GetNormRandom();
                    var x = prevroot + (nextroot - prevroot) * r;
                    var y = RandomY(x, 0, location, zoneCount, 0.05 * location.Height);
                    var vertex = new Vertex(new VertexPoint(x + location.X, y + location.Y), number.ToString());
                    number++;
                    list.Add(vertex);
                }

                vertexes.AddRange(list);
                var triangles = triangulator.Triangulation(Cast(list));
                foreach (var t in triangles)
                {
                    edges.Add(new Edge(list[t.a], list[t.b], VertexPoint.Distance(list[t.a], list[t.b])));
                    edges.Add(new Edge(list[t.b], list[t.a], VertexPoint.Distance(list[t.a], list[t.b])));
                    edges.Add(new Edge(list[t.b], list[t.c], VertexPoint.Distance(list[t.b], list[t.c])));
                    edges.Add(new Edge(list[t.c], list[t.b], VertexPoint.Distance(list[t.b], list[t.c])));
                    edges.Add(new Edge(list[t.a], list[t.c], VertexPoint.Distance(list[t.a], list[t.c])));
                    edges.Add(new Edge(list[t.c], list[t.a], VertexPoint.Distance(list[t.a], list[t.c])));
                }

            }
            edges = edges.Distinct(new EdgeComparer()).ToList();
            return new Graph(vertexes, edges);
        }


        internal class EdgeComparer : IEqualityComparer<Edge>
        {
            bool IEqualityComparer<Edge>.Equals(Edge x, Edge y)
            {
                return x.V1 == y.V1 && x.V2 == y.V2;
            }

            int IEqualityComparer<Edge>.GetHashCode(Edge obj)
            {
                return obj.V1.GetHashCode() + obj.V2.GetHashCode();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="vertexCount"></param>
        /// <param name="zoneCount"></param>
        /// <returns></returns>
        public static Graph GetRandomZoneGraph(Location location, int vertexCount, int zoneCount)
        {
            Graph[] g = new Graph[zoneCount];
            List<Edge> edges = new List<Edge>();
            List<Vertex> vertexes = new List<Vertex>();
            double[] roots_one = new double[zoneCount / 2 + zoneCount % 2 + 1];
            double[] roots_zero = new double[zoneCount / 2 + 1];
            double c = 2 * location.Width / (zoneCount - 1);
            roots_one[0] = 0;
            roots_one[roots_one.Length - 1] = location.Width;
            for (int i = 1; i < roots_one.Length - 1; i++)
                roots_one[i] = (2 * location.Width * i - location.Width) / (zoneCount - 1);

            for (int i = 0; i < roots_zero.Length - 1; i++)
                roots_zero[i] = c * i;
            roots_zero[roots_zero.Length - 1] = location.Width;
            int n = 3;
            Random r = new Random();
            Location[] l = new Location[n];
            List<Vertex> _l = new List<Vertex>();
            for (int i = 0; i < roots_one.Length - 1; i++)
            {
                double prevroot = roots_one[i];
                double nextroot = roots_one[i + 1];
                double dw = (nextroot - prevroot) / n;
                for (int j = 0; j < n; j++)
                {
                    double xleft = prevroot + dw * j;
                    double xright = prevroot + dw * (j + 1);
                    double yXleft = sepFunc(location, xleft, zoneCount);
                    double yXright = sepFunc(location, xright, zoneCount);
                    double y = yXleft < yXright ? yXright : yXleft;
                    l[j] = new Location()
                    {
                        X = xleft,
                        Width = dw,
                        Y = y,
                        Height = location.Height - y

                    };
                }
                _l = new List<Vertex>();
                for (int j = 0; j < vertexCount; j++)
                {
                    int z = r.Next(0, n);
                    var p = l[z].GetRandomPoint();
                    _l.Add(new Vertex(p, ""));
                }
                edges.AddRange(GetrandomEdges(_l, 0.2 * location.Width, 0.6 * location.Width));
                vertexes.AddRange(_l);
            }

            for (int i = 0; i < roots_zero.Length - 1; i++)
            {
                double prevroot = roots_zero[i];
                double nextroot = roots_zero[i + 1];
                double dw = (nextroot - prevroot) / n;
                for (int j = 0; j < n; j++)
                {
                    double xleft = prevroot + dw * j;
                    double xright = prevroot + dw * (j + 1);
                    double yXleft = sepFunc(location, xleft, zoneCount);
                    double yXright = sepFunc(location, xright, zoneCount);
                    double y = yXleft > yXright ? yXright : yXleft;
                    l[j] = new Location()
                    {
                        X = xleft,
                        Width = dw,
                        Y = 0,
                        Height = y
                    };
                }
                _l = new List<Vertex>();
                for (int j = 0; j < vertexCount; j++)
                {
                    int z = r.Next(0, n);
                    var p = l[z].GetRandomPoint();
                    _l.Add(new Vertex(p, ""));
                }

                edges.AddRange(GetrandomEdges(_l, 0.2 * location.Width, 0.6 * location.Width));
                vertexes.AddRange(_l);
            }

            return new Graph(vertexes, edges);

        }
    }
}
