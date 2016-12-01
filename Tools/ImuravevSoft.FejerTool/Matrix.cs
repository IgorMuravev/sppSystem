using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fLib
{
    internal class Matrix
    {
        private double[,] values;

        public Matrix(int r, int c)
        {
            values = new double[r, c];
        }

        public double this[int r, int c]
        {
            get
            {
                return values[r, c];
            }
            set
            {
                values[r, c] = value;
            }
        }
        public int RowCount { get { return values.GetLength(0); } }
        public int ColCount { get { return values.GetLength(1); } }
        public Vector GetRow(int rowNumber)
        {
            var res = new Vector(ColCount);
            for (int i = 0; i < ColCount; i++)
                res[i] = values[rowNumber, i];
            return res;
        }
        public Vector GetCol(int colNumber)
        {
            var res = new Vector(RowCount);
            for (int i = 0; i < RowCount; i++)
                res[i] = values[i, colNumber];
            return res;
        }
        public double GetNorm()
        {
            var result = 0.0;

            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColCount; j++)
                    result += values[i, j] * values[i, j];

            return result;
        }

        public static Matrix FromStream(StreamReader reader)
        {
            var dims = reader.ReadLine().Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var matrix = new Matrix(Convert.ToInt32(dims[0]), Convert.ToInt32(dims[1]));

            for (int i = 0; i < matrix.RowCount; i++)
            {
                var cols = reader.ReadLine().Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < cols.Length; j++)
                    matrix[i, j] = Convert.ToDouble(cols[j]);
            }
            return matrix;
        }

  
    }
    internal class MatrixOperations
    {
        public static double GetNorm(ref double[][] rows)
        {
            var result = 0.0;

            for (int i = 0; i < rows.Length; i++)
                for (int j = 0; j < rows[i].Length; j++)
                    result += rows[i][j] * rows[i][j];
            return result;
        }
    }
}
