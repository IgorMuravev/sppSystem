using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImuravevSoft.FejerTool.RareField
{
    public class RfVector
    {
        public int Lenght { get; set; }

        public double[] Values;
        public int[] Pos;

        public RfVector()
        { }
        public RfVector(double[] v)
        {
            int noZero = 0;
            for (int i = 0; i < v.Length; i++)
                if (v[i] != 0) noZero++;

            Lenght = noZero;
            Values = new double[Lenght];
            Pos = new int[Lenght];
            int j = 0;
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i] != 0)
                {
                    Values[j] = v[i];
                    Pos[j++] = i;
                }
            }

        }
    }
}
