using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ImuravevSoft.LinearProblem
{
    [DataTree("Задача линейного программирования")]
    public class LinearProblemData : BaseData
    {
        [Serializable]
        private class SerializableContext
        {
            public double[,] A;
            public double[] b;
            public double[] c;
        }

        public double[,] A;
        public double[] b;
        public double[] c;

        public override void Load(BinaryReader reader)
        {
            base.Load(reader);
            BinaryFormatter deserializer = new BinaryFormatter();
            var result = deserializer.Deserialize(reader.BaseStream) as SerializableContext;
            A = result.A;
            b = result.b;
            c = result.c;
        }

        public override void Save(BinaryWriter writer)
        {
            base.Save(writer);

            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(writer.BaseStream, new SerializableContext() { A = this.A, b = this.b, c = this.c });
        }

        public override bool Import()
        {
            var fileopendialog = new OpenFileDialog();
            if (fileopendialog.ShowDialog() == DialogResult.OK)
            {
                Name = Path.GetFileName(fileopendialog.FileName);

                using (var reader = new StreamReader(fileopendialog.FileName))
                {
                    var vals = reader.ReadLine().Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var values = new double[vals.Length];
                    for (int i = 0; i < vals.Length; i++)
                        values[i] = Convert.ToDouble(vals[i]);
                    c = values;

                    vals = reader.ReadLine().Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    values = new double[vals.Length];
                    for (int i = 0; i < vals.Length; i++)
                        values[i] = Convert.ToDouble(vals[i]);
                    b = values;

                    var dims = reader.ReadLine().Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var matrix = new double[Convert.ToInt32(dims[0]), Convert.ToInt32(dims[1])];

                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        var cols = reader.ReadLine().Trim().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int j = 0; j < cols.Length; j++)
                            matrix[i, j] = Convert.ToDouble(cols[j]);
                    }
                    A = matrix;
                }

                return true;
            }
            return false;
        }
    }
}
