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
using ImuravevSoft.LinearProblem;

namespace ImuravevSoft.FejerTool
{
    [Tool("Минимальный путь v2", "Поиск минимального пути в графе с помощью фейеровских итерационных методов")]
    [ReqData(typeof(Graph))]
    [ReqData(typeof(LinearProblemData))]
    public partial class MinPathByFejer : BaseTool
    {
        private class DataContainer
        {
            public Vector c;
            public Vector b;
            public Matrix A;


            public bool IsGood;
            public void FromGraph(Graph g, string startMark, string endMark)
            {
                IsGood = false;
                using (var ms = new MemoryStream(1024))
                {
                    var wr = new StreamWriter(ms, Encoding.UTF8);
                    g.Export(wr);
                    wr.Flush();
                    var reader = new StreamReader(ms, Encoding.UTF8, true);

                    ms.Position = 0;

                    c = Vector.FromStream(reader);
                    A = Matrix.FromStream(reader);

                    wr.Dispose();
                    reader.Dispose();
                }


                b = new Vector(A.RowCount);
                bool findStart = false;
                bool findEnd = false;

                for (int i = 0; i < g.VertexCount; i++)
                {
                    var v = g.Vertexes[i];
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
                if (!(findEnd && findStart))
                    IsGood = false;
                else
                    IsGood = true;
            }

            public void FromLinearProblem(LinearProblemData p)
            {
                IsGood = false;
                c = new Vector(p.c.Length);
                for (int i = 0; i < c.Length; i++)
                    c[i] = p.c[i];

                b = new Vector(p.b.Length);
                for (int i = 0; i < b.Length; i++)
                    b[i] = p.b[i];

                A = new Matrix(p.A.GetLength(0), p.A.GetLength(1));
                for (int i = 0; i < A.RowCount; i++)
                    for (int j = 0; j < A.ColCount; j++)
                        A[i, j] = p.A[i, j];

                IsGood = true;
            }
        }
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


            foreach (var data in UsedData)
            {

                tbDialog.AppendText("Обработка данных " + data.Name);
                tbDialog.AppendText("\n");
                Vector b, c;
                Matrix a;

                var container = new DataContainer();
                if (data is Graph)
                    container.FromGraph(data as Graph, startMark, endMark);
                else if (data is LinearProblemData)
                    container.FromLinearProblem(data as LinearProblemData);
                else
                {
                    tbDialog.AppendText("Плохие данные:  " + data.Name);
                    tbDialog.AppendText("\n");
                    continue;
                }

                if (container.IsGood)
                {
                    a = container.A;
                    b = container.b;
                    c = container.c;
                }
                else
                {
                    tbDialog.AppendText("Плохие данные:  " + data.Name);
                    tbDialog.AppendText("\n");
                    continue;
                }


                if (cbAutoRelax.Checked)
                    relax = a.RowCount;

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
                    writer = new StreamWriter(path + "\\" + data.Name);

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


                        if (Math.Abs(Math.Sqrt(pred1X.SqrNorm()) - Math.Sqrt(predX.SqrNorm())) < eps) break;

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

                        if (Math.Abs(Math.Sqrt(x.SqrNorm()) - Math.Sqrt(predX.SqrNorm())) < eps) break;

                    }
                    timer.Stop();
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
                tbDialog.AppendText("Число ребер - " + x.Count(t => t > 0.5));
                tbDialog.AppendText("\n");
                tbDialog.AppendText("Стоимость пути: " + (x * c).ToString());
                tbDialog.AppendText("\n");
                tbDialog.AppendText(x.ToString());
                tbDialog.AppendText("\n");
                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] > border)
                        x[i] = 1.0;
                    else
                        x[i] = 0.0;
                }

                if (data is Graph)
                {
                    var graph = data as Graph;
                    for (int i = 0; i < graph.EdgeCount; i++)
                    {
                        if (x[i] > 0.5)
                        {
                            graph.Edges[i].Markers.Add(startMark);
                        }
                    }
                }
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Graph g in UsedData.OfType<Graph>())
            {
                foreach (var ed in g.Edges)
                {
                    ed.Markers.Clear();
                }
            }
        }
    }
}
