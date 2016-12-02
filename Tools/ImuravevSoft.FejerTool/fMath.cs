using ImuravevSoft.FejerTool.RareField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fLib
{
    internal static class fMath
    {
        public static Vector fi1(Vector x, Matrix a, Vector b, double relaxCoeff)
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

    internal static class fMathF
    {
        public static void fi1(ref double[] x, ref double[][] rows, ref double[] b, double relaxCoeff)
        {
            var result = new double[x.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                var row = VectorOperations.Clone(ref rows[i]);
                var coeff = VectorOperations.ScalarMult(ref row, ref x) - b[i];
                VectorOperations.Mult(ref row, coeff);
                VectorOperations.Add(ref result, ref row);
            }
            VectorOperations.Mult(ref result, relaxCoeff);
            VectorOperations.Sub(ref x, ref result);
        }
        public static void fi2(ref double[] u, ref double[][] cols, ref double[] c, double relaxCoeff)
        {
            var result = new double[u.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                var col = VectorOperations.Clone(ref cols[i]);
                var coeff = VectorOperations.ScalarMult(ref col, ref u) - c[i];
                coeff = coeff > 0 ? coeff : 0.0;

                VectorOperations.Mult(ref col, coeff);
                VectorOperations.Add(ref result, ref col);
            }
            VectorOperations.Mult(ref result, relaxCoeff);
            VectorOperations.Sub(ref u, ref result);
        }
        public static void FiX(ref double[] x,ref double[] ccoeff, double sub)
        {
            var forX = VectorOperations.Clone(ref ccoeff);
            VectorOperations.Mult(ref forX, sub);
            VectorOperations.Sub(ref x, ref forX);
        }
        public static void FiU(ref double[] u, ref double[] bcoeff, double sub)
        {
            var forU = VectorOperations.Clone(ref bcoeff);
            VectorOperations.Mult(ref forU, sub);
            VectorOperations.Add(ref u, ref forU);
        }


    }

    internal static class fMathFRarefield
    {
        public static void fi1(ref double[] x, ref RfVector[] rows, ref double[] b, double relaxCoeff)
        {
            var result = new double[x.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                var row = VectorOperations.Clone(ref rows[i]);
                var coeff = VectorOperations.ScalarMult(ref x, ref row) - b[i];
                VectorOperations.Mult(ref row, coeff);
                VectorOperations.Add(ref result, ref row);
            }
            VectorOperations.Mult(ref result, relaxCoeff);
            VectorOperations.Sub(ref x, ref result);
        }
        public static void fi2(ref double[] u, ref RfVector[] cols, ref double[] c, double relaxCoeff)
        {
            var result = new double[u.Length];
            for (int i = 0; i < cols.Length; i++)
            {
                var col = VectorOperations.Clone(ref cols[i]);
                var coeff = VectorOperations.ScalarMult( ref u, ref col) - c[i];
                coeff = coeff > 0 ? coeff : 0.0;

                VectorOperations.Mult(ref col, coeff);
                VectorOperations.Add(ref result, ref col);
            }
            VectorOperations.Mult(ref result, relaxCoeff);
            VectorOperations.Sub(ref u, ref result);
        }

        public static void FiX(ref double[] x, ref double[] ccoeff, double sub)
        {
            var forX = VectorOperations.Clone(ref ccoeff);
            VectorOperations.Mult(ref forX, sub);
            VectorOperations.Sub(ref x, ref forX);
        }

        public static void FiU(ref double[] u, ref double[] bcoeff, double sub)
        {
            var forU = VectorOperations.Clone(ref bcoeff);
            VectorOperations.Mult(ref forU,sub);
            VectorOperations.Add(ref u, ref forU);
        }


    }

}
