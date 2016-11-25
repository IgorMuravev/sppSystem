using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.GraphData;
using ImuravevSoft.Core.Attributes;
using fLib;
using System.IO;
using System.Diagnostics;

namespace ImuravevSoft.FejerTool
{
    [Tool("Минимальный путь v2", "Поиск минимального пути в графе с помощью фейеровских итерационных методов")]
    [ReqData(typeof(Graph))]
    public partial class MinPathByFejer : BaseTool
    {
        public MinPathByFejer()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tbDialog.Clear();
            var eps = Convert.ToDouble(tbEps.Text);
            var relax = 0.0;
            if (!cbAutoRelax.Checked)
                relax = Convert.ToDouble(tbRelax.Text);
            var path = tbLogs.Text;
            var marks = tbMarkers.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var startMark = marks[0];
            var endMark = marks[1];
            var border = Convert.ToDouble(tbHasEdge.Text);

            var grafs = UsedData.OfType<Graph>();
            foreach (var graph in grafs)
            {

                tbDialog.AppendText("Обработка графа " + graph.Name);
                tbDialog.AppendText("\n");
                Vector b, c;
                Matrix a;

                if (cbAutoRelax.Checked)
                    relax = graph.EdgeCount;

               
                using (var ms = new MemoryStream(1024))
                {
                    var wr = new StreamWriter(ms, Encoding.UTF8);
                    graph.Export(wr);
                    wr.Flush();
                    var reader = new StreamReader(ms, Encoding.UTF8, true);
           
                    ms.Position = 0;
                
                    c = Vector.FromStream(reader);
                    a = Matrix.FromStream(reader);

                    wr.Dispose();
                    reader.Dispose();
                }

                b = new Vector(a.RowCount);
                bool findStart = false;
                bool findEnd = false;
                tbDialog.AppendText("Поиск стартовой и конечной вершин.. ");
                tbDialog.AppendText("\n");
                for (int i = 0; i < graph.VertexCount; i++)
                {
                    var v = graph.Vertexes[i];
                    if (v.Markers.Contains(startMark))
                    {
                        b[i] = 1;
                        findStart = true;
                    }
                    if (v.Markers.Contains(endMark))
                    {
                        b[i] = -1;
                        findEnd = true;
                    }

                    if (findEnd && findStart) break;
                }
                if (!findStart)
                {
                    tbDialog.AppendText("Не найдена стартовая вершина.. ");
                    tbDialog.AppendText("\n");
                }
                if (!findEnd)
                {
                    tbDialog.AppendText("Не найдена конечная вершина.. ");
                    tbDialog.AppendText("\n");
                }
                if (!(findEnd && findStart))
                {
                    tbDialog.AppendText("Невозможно продолжать.. ");
                    tbDialog.AppendText("\n");
                    tbDialog.AppendText("----------------------------------------------------------");
                    tbDialog.AppendText("\n");
                    continue;
                }

                var x = new Vector(a.ColCount);
                var u = new Vector(a.RowCount);

                var coeff = relax / a.GetNorm();

                var normSum = 1 / (b.SqrNorm() + c.SqrNorm());

                var ccoeff = normSum * c;
                var bcoeff = normSum * b;
                int it = 0;
                tbDialog.AppendText("Расчет начат");
                tbDialog.AppendText("\n");

                StreamWriter writer = null;
                if (cbLogs.Checked)
                    writer = new StreamWriter(path + "\\" + graph.Name);

                Stopwatch timer = new Stopwatch();
                if (cbModMethod.Checked)
                {
                    timer.Start();
                    while (true)
                    {

                        // берем 3 точки
                        x = fMath.fi1(x, a, b, coeff);
                        u = fMath.fi2(u, a, c, coeff);

                        var result = fMath.Fi(new Tuple<Vector, Vector>(x, u), b, c, ccoeff, bcoeff);
                        x = !result.Item1;
                        u = result.Item2;



                        var pred2x = x;
                        var pred2Y = u;

                        x = fMath.fi1(x, a, b, coeff);
                        u = fMath.fi2(u, a, c, coeff);

                        result = fMath.Fi(new Tuple<Vector, Vector>(x, u), b, c, ccoeff, bcoeff);
                        x = !result.Item1;
                        u = result.Item2;



                        var pred1X = x;
                        var pred1Y = u;

                        x = fMath.fi1(x, a, b, coeff);
                        u = fMath.fi2(u, a, c, coeff);

                        result = fMath.Fi(new Tuple<Vector, Vector>(x, u), b, c, ccoeff, bcoeff);
                        x = !result.Item1;
                        u = result.Item2;



                        var predX = x;
                        var predY = u;

                        if (Math.Abs(predX.SqrNorm() - pred1X.SqrNorm()) < eps) break;

                        // берем вектор из 1 в 3

                        var gradX = predX - pred2x;

                        var grad = gradX;
                        var len = Math.Sqrt(grad.SqrNorm());
                        var normCoeff = 1d / len;
                        grad = normCoeff * grad;
                        //   Console.WriteLine(!grad);



                        var gradY = predY - pred2Y;

                        x = !(pred2x + 2 * gradX);
                        u = pred2Y + 2 * gradY;

                        if (writer != null)
                            writer.WriteLine(String.Join(";", x));

                        it++;
                    }
                    timer.Stop();
                }
                else
                {
                    timer.Start();

                    while (true)
                    {

                        var predX = x;
                        var predY = u;

                        x = fMath.fi1(x, a, b, coeff);
                        u = fMath.fi2(u, a, c, coeff);

                        var result = fMath.Fi(new Tuple<Vector, Vector>(x, u), b, c, ccoeff, bcoeff);
                        x = !result.Item1;
                        u = result.Item2;


                        it++;
                        if (writer != null)
                            writer.WriteLine(String.Join(";", x));

                        if (Math.Abs(predX.SqrNorm() - x.SqrNorm()) < eps) break;
                    }
                    timer.Stop();
                }
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] > border)
                        x[i] = 1.0;
                    else
                        x[i] = 0.0;
                }

                for (int i = 0; i < graph.EdgeCount; i++)
                {
                    if (x[i] > 0.5)
                    {
                        graph.Edges[i].Markers.Add(startMark);
                    }
                }
                if (cbLogs.Checked)
                    writer.Dispose();

                tbDialog.AppendText("Расчет закончен ");
                tbDialog.AppendText("\n");
                tbDialog.AppendText("Число итераций: " + it.ToString());
                tbDialog.AppendText("\n");
                tbDialog.AppendText("Затрачено времени(мс): " + timer.ElapsedMilliseconds);
                tbDialog.AppendText("\n");
                tbDialog.AppendText("Результат");
                tbDialog.AppendText("\n");
                tbDialog.AppendText("Стоимость пути: " + (x * c).ToString());
                tbDialog.AppendText("\n");
                tbDialog.AppendText(x.ToString());
                tbDialog.AppendText("\n");
                tbDialog.AppendText("----------------------------------------------------------");
                tbDialog.AppendText("\n");
            }
        }

        private void btnSelDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbLogs.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
