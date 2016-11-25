using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fLib
{
    internal static class fMath
    {
        public static Vector fi1(Vector x, Matrix a, Vector b,double relaxCoeff)
        {
            var result = new Vector(a.ColCount);
            for (int i = 0; i < a.RowCount; i++)
            {
                var row = a.GetRow(i);
                var coeff = row * x - b[i];
                row = coeff * row;
                result += row;
            }
            return x - relaxCoeff * result;
        }
        public static Vector fi2(Vector u, Matrix a, Vector c, double relaxCoeff)
        {
            var result = new Vector(a.RowCount);
            for (int i = 0; i < a.ColCount; i++)
            {
                var col = a.GetCol(i);
                var coeff = col * u - c[i];
                coeff = coeff > 0 ? coeff : 0.0;

                col = coeff * col;
                result += col;
            }
            return u - relaxCoeff * result;
        }

        public static Tuple<Vector, Vector> Fi(Tuple<Vector, Vector> xu, Vector b, Vector c, Vector ccoeff, Vector bcoeff)
        {
            var cx = c * xu.Item1;
            var bu = b * xu.Item2;
            var sub = cx - bu;

            var forX = sub * ccoeff;
            var forU = sub * bcoeff;

            var nx = xu.Item1 - forX;
            var nu = xu.Item2 + forU;
            return new Tuple<Vector, Vector>(nx, nu);

        }
        

    }
}
