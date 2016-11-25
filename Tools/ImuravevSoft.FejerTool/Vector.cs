using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fLib
{
    internal class Vector: IEnumerable<double>
    {
        private double[] values;

        public int Length { get { return values.Length; } }
        public Vector(int size)
        {
            values = new double[size];
        }
        public double this[int i]
        {
            get
            {
                return values[i];
            }
            set
            {
                values[i] = value;
            }
        }

        public double SqrNorm()
        {
            var res = 0.0;
            for (int i = 0; i < Length; i++)
                res += values[i] * values[i];
            return res;
        }

        public static double operator *(Vector v1, Vector v2)
        {
            var summ = 0.0;
            for (int i = 0; i < v1.Length; i++)
                summ += v1[i] * v2[i];
            return summ;
        }

        public static Vector operator *(double a, Vector v)
        {
            var r = new Vector(v.Length);
            for (int i = 0; i < v.Length; i++)
                r[i] = a * v[i];
            return r;
        }
        public static Vector operator *(Vector v, double a)
        {
            var r = new Vector(v.Length);
            for (int i = 0; i < v.Length; i++)
                r[i] = a * v[i];
            return r;
        }

        public static Vector operator !(Vector v)
        {
            var r = new Vector(v.Length);
            for (int i = 0; i < v.Length; i++)
                r[i] = v[i] > 0 ? v[i] : 0;
            return r;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            var summ = new Vector(v1.Length);
            for (int i = 0; i < v1.Length; i++)
                summ[i] = v1[i] + v2[i];
            return summ;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            var sub = new Vector(v1.Length);
            for (int i = 0; i < v1.Length; i++)
                sub[i] = v1[i] - v2[i];
            return sub;
        }

        public override string ToString()
        {
            return "[ "+ String.Join(" ; ", values) + " ]";
        }

        public static Vector FromStream(StreamReader reader)
        {
            var vals = reader.ReadLine().Trim().Split(new[] { ';' },StringSplitOptions.RemoveEmptyEntries);
            var values = new Vector(vals.Length);
            for (int i = 0; i < vals.Length; i++)
                values[i] = Convert.ToDouble(vals[i]);
            return values;
        }
        public static double Distance(Vector v1, Vector v2)
        {
            var res = 0.0;
            for (int i = 0; i < v1.Length; i++)
                res += (v1[i] - v2[i]) * (v1[i] - v2[i]);

            return Math.Sqrt(res);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<double>)values).GetEnumerator();
        }
    }
}
