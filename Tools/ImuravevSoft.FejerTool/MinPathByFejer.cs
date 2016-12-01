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
using System.Threading.Tasks;
using ImuravevSoft.FejerTool.RareField;

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

            FejerParams[] scripts = null;
            if (cbUseScript.Checked)
                scripts = ScriptParser.Parse(tbScript.Text);
            else
                scripts = new FejerParams[] { new FejerParams()
                { Autorelax = cbAutoRelax.Checked,
                  IsNewMethod = cbModMethod.Checked,
                  EdgeBorder = border,
                  Eps = eps,
                  Log = cbLogs.Checked,
                  Relax = relax,
                  Logfile = tbLogs.Text,
                } };

            btnClear.PerformClick();
            btnStart.Enabled = false;
            var task = Task.Factory.StartNew(() =>
            {
                foreach (var p in scripts)
                {
                    foreach (var data in UsedData)
                    {
                        Invoke(new Action(() =>
                        {
                            tbDialog.AppendText("Обработка данных " + data.Name);
                            tbDialog.AppendText("\n");
                        }));

                        Vector b, c;
                        Matrix a;

                        var container = new DataContainer();
                        if (data is Graph)
                            container.FromGraph(data as Graph, startMark, endMark);
                        else if (data is LinearProblemData)
                            container.FromLinearProblem(data as LinearProblemData);
                        else
                        {
                            Invoke(new Action(() =>
                            {
                                tbDialog.AppendText("Плохие данные:  " + data.Name);
                                tbDialog.AppendText("\n");
                            }));

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
                            Invoke(new Action(() =>
                            {
                                tbDialog.AppendText("Плохие данные:  " + data.Name);
                                tbDialog.AppendText("\n");
                            }));

                            continue;
                        }

                        p.A = a;
                        p.b = b;
                        p.c = c;

                        var result = FejerCalc.Computing(p);

                        if (data is Graph)
                        {
                            var graph = data as Graph;
                            for (int i = 0; i < graph.EdgeCount; i++)
                            {
                                if (result.x[i] > 0.5)
                                {
                                    graph.Edges[i].Markers.Add(startMark);
                                }
                            }
                        }
                        Invoke(new Action(() =>
                        {
                            tbDialog.AppendText("Расчет закончен ");
                            tbDialog.AppendText("\n");
                            tbDialog.AppendText("Число итераций: " + result.Iterations.ToString());
                            tbDialog.AppendText("\n");
                            tbDialog.AppendText("Затрачено времени(мс): " + result.Ms);
                            tbDialog.AppendText("\n");
                            tbDialog.AppendText("Результат");
                            tbDialog.AppendText("\n");
                            tbDialog.AppendText("Число ребер - " + result.x.Count(t => t > 0.5));
                            tbDialog.AppendText("\n");
                            tbDialog.AppendText("Стоимость пути: " + (result.x * c).ToString());
                            tbDialog.AppendText("\n");
                            tbDialog.AppendText(result.x.ToString());
                            tbDialog.AppendText("\n");
                            tbDialog.AppendText("----------------------------------------------------------");
                            tbDialog.AppendText("\n");
                        }));

                    }
                }
                Invoke(new Action(() =>
                {
                    btnStart.Enabled = true;
                }));
            });

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
        private void cbUseScript_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = !cbUseScript.Checked;
        }

    }

    internal class FejerCalc
    {
        private static FejerResult StdMethodF(FejerParams p)
        {
            var x = new double[p.A.ColCount];
            var u = new double[p.A.RowCount];
            var relax = 0.0;

            #region быстрые доступы к строке и стоолбцу
            double[][] rows = new double[p.A.RowCount][];
            for (int i = 0; i < p.A.RowCount; i++)
            {
                rows[i] = new double[p.A.ColCount];
                for (int j = 0; j < p.A.ColCount; j++)
                    rows[i][j] = p.A[i, j];
            }
            double[][] cols = new double[p.A.ColCount][];
            for (int i = 0; i < p.A.ColCount; i++)
            {
                cols[i] = new double[p.A.RowCount];
                for (int j = 0; j < p.A.RowCount; j++)
                    cols[i][j] = p.A[j, i];
            }
            #endregion

            #region расчет доп коэффов

            var b = new double[p.b.Length];
            for (int i = 0; i < p.b.Length; i++)
                b[i] = p.b[i];
            var c = new double[p.c.Length];
            for (int i = 0; i < p.c.Length; i++)
                c[i] = p.c[i];


            if (p.Autorelax)
                relax = rows.Length;
            else
                relax = p.Relax;


            var coeff = relax / MatrixOperations.GetNorm(ref rows);
            var normSum = 1 / (VectorOperations.GetSqrNorm(ref b) + VectorOperations.GetSqrNorm(ref c));

            var ccoeff = VectorOperations.Clone(ref c);
            VectorOperations.Mult(ref ccoeff, normSum);
            var bcoeff = VectorOperations.Clone(ref b);
            VectorOperations.Mult(ref bcoeff, normSum);
            #endregion
            int it = 0;
            StreamWriter writer = null;
            if (p.Log)
                writer = new StreamWriter(p.Logfile);
            var timer = new Stopwatch();
            timer.Start();

            while (true)
            {

                var predX = VectorOperations.Clone(ref x);
                var predY = VectorOperations.Clone(ref u);
                var t1 = Task.Factory.StartNew(() =>
                {
                    fMathF.fi1(ref x, ref rows, ref b, coeff);
                });
                var t2 = Task.Factory.StartNew(() =>
                {
                    fMathF.fi2(ref u, ref cols, ref c, coeff);
                });

                Task.WaitAll(t1, t2);

                var cx = VectorOperations.ScalarMult(ref c, ref x);
                var bu = VectorOperations.ScalarMult(ref b, ref u);
                var sub = cx - bu;


                t1 = Task.Factory.StartNew(() =>
                {
                    fMathF.FiX(ref x, ref ccoeff, sub);
                    VectorOperations.Pos(ref x);
                });

                t2 = Task.Factory.StartNew(() =>
                {
                    fMathF.FiU(ref u, ref bcoeff, sub);
                });

                Task.WaitAll(t1, t2);


                it++;
                if (writer != null)
                    writer.WriteLine(String.Join(";", x));

                if (Math.Abs(Math.Sqrt(VectorOperations.GetSqrNorm(ref x)) - Math.Sqrt(VectorOperations.GetSqrNorm(ref predX))) < p.Eps) break;

            }
            timer.Stop();
            if (p.Log)
                writer.Dispose();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > p.EdgeBorder)
                    x[i] = 1.0;
                else
                    x[i] = 0.0;
            }
            return new FejerResult()
            {
                Iterations = it,
                Ms = timer.ElapsedMilliseconds,
                x = new Vector(x),
                u = new Vector(u)
            };
        }
        private static FejerResult StdMethod(FejerParams p)
        {
            var x = new Vector(p.A.ColCount);
            var u = new Vector(p.A.RowCount);
            var relax = 0.0;

            if (p.Autorelax)
                relax = p.A.RowCount;
            else
                relax = p.Relax;

            var coeff = relax / p.A.GetNorm();

            var normSum = 1 / (p.b.SqrNorm() + p.c.SqrNorm());

            var ccoeff = normSum * p.c;
            var bcoeff = normSum * p.b;
            int it = 0;

            StreamWriter writer = null;
            if (p.Log)
                writer = new StreamWriter(p.Logfile);
            var timer = new Stopwatch();
            timer.Start();

            while (true)
            {

                var predX = x;
                var predY = u;

                x = fMath.fi1(x, p.A, p.b, coeff);
                u = fMath.fi2(u, p.A, p.c, coeff);

                var result = fMath.Fi(new Tuple<Vector, Vector>(x, u), p.b, p.c, ccoeff, bcoeff);
                x = !result.Item1;
                u = result.Item2;


                it++;
                if (writer != null)
                    writer.WriteLine(String.Join(";", x));

                if (Math.Abs(Math.Sqrt(x.SqrNorm()) - Math.Sqrt(predX.SqrNorm())) < p.Eps) break;

            }
            timer.Stop();
            if (p.Log)
                writer.Dispose();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > p.EdgeBorder)
                    x[i] = 1.0;
                else
                    x[i] = 0.0;
            }
            return new FejerResult()
            {
                Iterations = it,
                Ms = timer.ElapsedMilliseconds,
                x = x,
                u = u
            };
        }
        private static FejerResult ModMethod(FejerParams p)
        {
            var x = new Vector(p.A.ColCount);
            var u = new Vector(p.A.RowCount);
            var relax = 0.0;

            if (p.Autorelax)
                relax = p.A.RowCount;
            else
                relax = p.Relax;

            var coeff = relax / p.A.GetNorm();

            var normSum = 1 / (p.b.SqrNorm() + p.c.SqrNorm());

            var ccoeff = normSum * p.c;
            var bcoeff = normSum * p.b;
            int it = 0;

            StreamWriter writer = null;
            if (p.Log)
                writer = new StreamWriter(p.Logfile);
            var timer = new Stopwatch();
            timer.Start();

            while (true)
            {
                x = fMath.fi1(x, p.A, p.b, coeff);
                u = fMath.fi2(u, p.A, p.c, coeff);

                var result = fMath.Fi(new Tuple<Vector, Vector>(x, u), p.b, p.c, ccoeff, bcoeff);
                x = !result.Item1;
                u = result.Item2;



                var pred2x = x;
                var pred2Y = u;

                x = fMath.fi1(x, p.A, p.b, coeff);
                u = fMath.fi2(u, p.A, p.c, coeff);

                result = fMath.Fi(new Tuple<Vector, Vector>(x, u), p.b, p.c, ccoeff, bcoeff);
                x = !result.Item1;
                u = result.Item2;



                var pred1X = x;
                var pred1Y = u;

                x = fMath.fi1(x, p.A, p.b, coeff);
                u = fMath.fi2(u, p.A, p.c, coeff);

                result = fMath.Fi(new Tuple<Vector, Vector>(x, u), p.b, p.c, ccoeff, bcoeff);
                x = !result.Item1;
                u = result.Item2;



                var predX = x;
                var predY = u;


                if (Math.Abs(Math.Sqrt(pred1X.SqrNorm()) - Math.Sqrt(predX.SqrNorm())) < p.Eps) break;

                var gradX = predX - pred2x;

                var grad = gradX;
                var len = Math.Sqrt(grad.SqrNorm());
                var normCoeff = 1d / len;
                grad = normCoeff * grad;

                var gradY = predY - pred2Y;

                x = !(pred2x + 2 * gradX);
                u = pred2Y + 2 * gradY;

                if (writer != null)
                    writer.WriteLine(String.Join(";", x));

                it++;
            }
            timer.Stop();
            if (p.Log)
                writer.Dispose();


            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > p.EdgeBorder)
                    x[i] = 1.0;
                else
                    x[i] = 0.0;
            }

            return new FejerResult()
            {
                Iterations = it,
                Ms = timer.ElapsedMilliseconds,
                x = x,
                u = u
            };
        }
        private static FejerResult ModMethodF(FejerParams p)
        {
            var x = new double[p.A.ColCount];
            var u = new double[p.A.RowCount];
            var relax = 0.0;

            #region быстрые доступы к строке и стоолбцу
            double[][] rows = new double[p.A.RowCount][];
            for (int i = 0; i < p.A.RowCount; i++)
            {
                rows[i] = new double[p.A.ColCount];
                for (int j = 0; j < p.A.ColCount; j++)
                    rows[i][j] = p.A[i, j];
            }
            double[][] cols = new double[p.A.ColCount][];
            for (int i = 0; i < p.A.ColCount; i++)
            {
                cols[i] = new double[p.A.RowCount];
                for (int j = 0; j < p.A.RowCount; j++)
                    cols[i][j] = p.A[j, i];
            }
            #endregion

            #region расчет доп коэффов

            var b = new double[p.b.Length];
            for (int i = 0; i < p.b.Length; i++)
                b[i] = p.b[i];
            var c = new double[p.c.Length];
            for (int i = 0; i < p.c.Length; i++)
                c[i] = p.c[i];


            if (p.Autorelax)
                relax = rows.Length;
            else
                relax = p.Relax;


            var coeff = relax / MatrixOperations.GetNorm(ref rows);
            var normSum = 1 / (VectorOperations.GetSqrNorm(ref b) + VectorOperations.GetSqrNorm(ref c));

            var ccoeff = VectorOperations.Clone(ref c);
            VectorOperations.Mult(ref ccoeff, normSum);
            var bcoeff = VectorOperations.Clone(ref b);
            VectorOperations.Mult(ref bcoeff, normSum);
            #endregion
            int it = 0;
            StreamWriter writer = null;
            if (p.Log)
                writer = new StreamWriter(p.Logfile);
            var timer = new Stopwatch();
            timer.Start();

            while (true)
            {
                var t1 = Task.Factory.StartNew(() =>
                {
                    fMathF.fi1(ref x, ref rows, ref b, coeff);
                });
                var t2 = Task.Factory.StartNew(() =>
                {
                    fMathF.fi2(ref u, ref cols, ref c, coeff);
                });

                Task.WaitAll(t1, t2);

                var cx = VectorOperations.ScalarMult(ref c, ref x);
                var bu = VectorOperations.ScalarMult(ref b, ref u);
                var sub = cx - bu;


                t1 = Task.Factory.StartNew(() =>
                {
                    fMathF.FiX(ref x, ref ccoeff, sub);
                    VectorOperations.Pos(ref x);
                });

                t2 = Task.Factory.StartNew(() =>
                {
                    fMathF.FiU(ref u, ref bcoeff, sub);
                });

                Task.WaitAll(t1, t2);


                var pred2x = VectorOperations.Clone(ref x);
                var pred2Y = VectorOperations.Clone(ref u);

                t1 = Task.Factory.StartNew(() =>
                {
                    fMathF.fi1(ref x, ref rows, ref b, coeff);
                });
                t2 = Task.Factory.StartNew(() =>
                {
                    fMathF.fi2(ref u, ref cols, ref c, coeff);
                });

                Task.WaitAll(t1, t2);

                cx = VectorOperations.ScalarMult(ref c, ref x);
                bu = VectorOperations.ScalarMult(ref b, ref u);
                sub = cx - bu;


                t1 = Task.Factory.StartNew(() =>
                {
                    fMathF.FiX(ref x, ref ccoeff, sub);
                    VectorOperations.Pos(ref x);
                });

                t2 = Task.Factory.StartNew(() =>
                {
                    fMathF.FiU(ref u, ref bcoeff, sub);
                });

                Task.WaitAll(t1, t2);


                var pred1X = VectorOperations.Clone(ref x);
                var pred1Y = VectorOperations.Clone(ref u);

                t1 = Task.Factory.StartNew(() =>
                {
                    fMathF.fi1(ref x, ref rows, ref b, coeff);
                });
                t2 = Task.Factory.StartNew(() =>
                {
                    fMathF.fi2(ref u, ref cols, ref c, coeff);
                });

                Task.WaitAll(t1, t2);

                cx = VectorOperations.ScalarMult(ref c, ref x);
                bu = VectorOperations.ScalarMult(ref b, ref u);
                sub = cx - bu;


                t1 = Task.Factory.StartNew(() =>
                {
                    fMathF.FiX(ref x, ref ccoeff, sub);
                    VectorOperations.Pos(ref x);
                });

                t2 = Task.Factory.StartNew(() =>
                {
                    fMathF.FiU(ref u, ref bcoeff, sub);
                });

                Task.WaitAll(t1, t2);



                var predX = VectorOperations.Clone(ref x);
                var predY = VectorOperations.Clone(ref u);


                if (Math.Abs(Math.Sqrt(VectorOperations.GetSqrNorm(ref pred1X)) - Math.Sqrt(VectorOperations.GetSqrNorm(ref predX))) < p.Eps) break;

                VectorOperations.Sub(ref predX, ref pred2x);
                VectorOperations.Sub(ref predY, ref pred2Y);

                VectorOperations.Mult(ref predX, 2);
                VectorOperations.Mult(ref predY, 2);

                VectorOperations.Add(ref x, ref predX);
                VectorOperations.Pos(ref x);
                VectorOperations.Add(ref u, ref predY);

                if (writer != null)
                    writer.WriteLine(String.Join(";", x));

                it++;
            }
            timer.Stop();
            if (p.Log)
                writer.Dispose();


            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > p.EdgeBorder)
                    x[i] = 1.0;
                else
                    x[i] = 0.0;
            }

            return new FejerResult()
            {
                Iterations = it,
                Ms = timer.ElapsedMilliseconds,
                x = new Vector(x),
                u = new Vector(u)
            };
        }
        public static FejerResult Computing(FejerParams p)
        {
            return p.IsNewMethod ? ModMethodFRarefield(p) : StdMethodFRarefield(p);
        }
        private static FejerResult StdMethodFRarefield(FejerParams p)
        {
            var x = new double[p.A.ColCount];
            var u = new double[p.A.RowCount];
            var relax = 0.0;

            #region быстрые доступы к строке и стоолбцу
            RfVector[] rows = new RfVector[p.A.RowCount];
            for (int i = 0; i < p.A.RowCount; i++)
            {
                var br = new double[p.A.ColCount];
                for (int j = 0; j < p.A.ColCount; j++)
                    br[j] = p.A[i, j];
                rows[i] = new RfVector(br);
            }
            RfVector[] cols = new RfVector[p.A.ColCount];
            for (int i = 0; i < p.A.ColCount; i++)
            {
                var bc = new double[p.A.RowCount];
                for (int j = 0; j < p.A.RowCount; j++)
                    bc[j] = p.A[j, i];

                cols[i] = new RfVector(bc);
            };

            
            #endregion

            #region расчет доп коэффов

            var b = new double[p.b.Length];
            for (int i = 0; i < p.b.Length; i++)
                b[i] = p.b[i];
            var c = new double[p.c.Length];
            for (int i = 0; i < p.c.Length; i++)
                c[i] = p.c[i];


            if (p.Autorelax)
                relax = p.A.RowCount;
            else
                relax = p.Relax;


            var coeff = relax / p.A.GetNorm();
            var normSum = 1 / (VectorOperations.GetSqrNorm(ref b) + VectorOperations.GetSqrNorm(ref c));

            var ccoeff = VectorOperations.Clone(ref c);
            VectorOperations.Mult(ref ccoeff, normSum);
            var bcoeff = VectorOperations.Clone(ref b);
            VectorOperations.Mult(ref bcoeff, normSum);
            #endregion
            int it = 0;
            StreamWriter writer = null;
            if (p.Log)
                writer = new StreamWriter(p.Logfile);
            var timer = new Stopwatch();
            timer.Start();

            while (true)
            {

                var predX = VectorOperations.Clone(ref x);
                var predY = VectorOperations.Clone(ref u);
                var t1 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.fi1(ref x, ref rows, ref b, coeff);
                });
                var t2 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.fi2(ref u, ref cols, ref c, coeff);
                });

                Task.WaitAll(t1, t2);

                var cx = VectorOperations.ScalarMult(ref c, ref x);
                var bu = VectorOperations.ScalarMult(ref b, ref u);
                var sub = cx - bu;


                t1 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.FiX(ref x, ref ccoeff, sub);
                    VectorOperations.Pos(ref x);
                });

                t2 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.FiU(ref u, ref bcoeff, sub);
                });

                Task.WaitAll(t1, t2);


                it++;
                if (writer != null)
                    writer.WriteLine(String.Join(";", x));

                if (Math.Abs(Math.Sqrt(VectorOperations.GetSqrNorm(ref x)) - Math.Sqrt(VectorOperations.GetSqrNorm(ref predX))) < p.Eps) break;

            }
            timer.Stop();
            if (p.Log)
                writer.Dispose();

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > p.EdgeBorder)
                    x[i] = 1.0;
                else
                    x[i] = 0.0;
            }
            return new FejerResult()
            {
                Iterations = it,
                Ms = timer.ElapsedMilliseconds,
                x = new Vector(x),
                u = new Vector(u)
            };
        }
        private static FejerResult ModMethodFRarefield(FejerParams p)
        {
            var x = new double[p.A.ColCount];
            var u = new double[p.A.RowCount];
            var relax = 0.0;

            #region быстрые доступы к строке и стоолбцу
            RfVector[] rows = new RfVector[p.A.RowCount];
            for (int i = 0; i < p.A.RowCount; i++)
            {
                var br = new double[p.A.ColCount];
                for (int j = 0; j < p.A.ColCount; j++)
                    br[j] = p.A[i, j];
                rows[i] = new RfVector(br);
            }
            RfVector[] cols = new RfVector[p.A.ColCount];
            for (int i = 0; i < p.A.ColCount; i++)
            {
                var bc = new double[p.A.RowCount];
                for (int j = 0; j < p.A.RowCount; j++)
                    bc[j] = p.A[j, i];

                cols[i] = new RfVector(bc);
            };


            #endregion

            #region расчет доп коэффов

            var b = new double[p.b.Length];
            for (int i = 0; i < p.b.Length; i++)
                b[i] = p.b[i];
            var c = new double[p.c.Length];
            for (int i = 0; i < p.c.Length; i++)
                c[i] = p.c[i];


            if (p.Autorelax)
                relax = p.A.RowCount;
            else
                relax = p.Relax;


            var coeff = relax / p.A.GetNorm();
            var normSum = 1 / (VectorOperations.GetSqrNorm(ref b) + VectorOperations.GetSqrNorm(ref c));

            var ccoeff = VectorOperations.Clone(ref c);
            VectorOperations.Mult(ref ccoeff, normSum);
            var bcoeff = VectorOperations.Clone(ref b);
            VectorOperations.Mult(ref bcoeff, normSum);
            #endregion
            int it = 0;
            StreamWriter writer = null;
            if (p.Log)
                writer = new StreamWriter(p.Logfile);
            var timer = new Stopwatch();
            timer.Start();
            while (true)
            {
                var t1 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.fi1(ref x, ref rows, ref b, coeff);
                });
                var t2 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.fi2(ref u, ref cols, ref c, coeff);
                });

                Task.WaitAll(t1, t2);

                var cx = VectorOperations.ScalarMult(ref c, ref x);
                var bu = VectorOperations.ScalarMult(ref b, ref u);
                var sub = cx - bu;


                t1 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.FiX(ref x, ref ccoeff, sub);
                    VectorOperations.Pos(ref x);
                });

                t2 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.FiU(ref u, ref bcoeff, sub);
                });

                Task.WaitAll(t1, t2);


                var pred2x = VectorOperations.Clone(ref x);
                var pred2Y = VectorOperations.Clone(ref u);

                t1 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.fi1(ref x, ref rows, ref b, coeff);
                });
                t2 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.fi2(ref u, ref cols, ref c, coeff);
                });

                Task.WaitAll(t1, t2);

                cx = VectorOperations.ScalarMult(ref c, ref x);
                bu = VectorOperations.ScalarMult(ref b, ref u);
                sub = cx - bu;


                t1 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.FiX(ref x, ref ccoeff, sub);
                    VectorOperations.Pos(ref x);
                });

                t2 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.FiU(ref u, ref bcoeff, sub);
                });

                Task.WaitAll(t1, t2);


                var pred1X = VectorOperations.Clone(ref x);
                var pred1Y = VectorOperations.Clone(ref u);

                t1 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.fi1(ref x, ref rows, ref b, coeff);
                });
                t2 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.fi2(ref u, ref cols, ref c, coeff);
                });

                Task.WaitAll(t1, t2);

                cx = VectorOperations.ScalarMult(ref c, ref x);
                bu = VectorOperations.ScalarMult(ref b, ref u);
                sub = cx - bu;


                t1 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.FiX(ref x, ref ccoeff, sub);
                    VectorOperations.Pos(ref x);
                });

                t2 = Task.Factory.StartNew(() =>
                {
                    fMathFRarefield.FiU(ref u, ref bcoeff, sub);
                });

                Task.WaitAll(t1, t2);



                var predX = VectorOperations.Clone(ref x);
                var predY = VectorOperations.Clone(ref u);


                if (Math.Abs(Math.Sqrt(VectorOperations.GetSqrNorm(ref pred1X)) - Math.Sqrt(VectorOperations.GetSqrNorm(ref predX))) < p.Eps) break;

                VectorOperations.Sub(ref predX, ref pred2x);
                VectorOperations.Sub(ref predY, ref pred2Y);

                VectorOperations.Mult(ref predX, 2);
                VectorOperations.Mult(ref predY, 2);

                VectorOperations.Add(ref x, ref predX);
                VectorOperations.Pos(ref x);
                VectorOperations.Add(ref u, ref predY);

                if (writer != null)
                    writer.WriteLine(String.Join(";", x));

                it++;
            }
            timer.Stop();
            if (p.Log)
                writer.Dispose();


            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > p.EdgeBorder)
                    x[i] = 1.0;
                else
                    x[i] = 0.0;
            }

            return new FejerResult()
            {
                Iterations = it,
                Ms = timer.ElapsedMilliseconds,
                x = new Vector(x),
                u = new Vector(u)
            };
        }
    }
    internal class FejerParams
    {
        public double Relax;
        public bool Autorelax;
        public double Eps;
        public double EdgeBorder;

        public bool IsNewMethod;
        public bool Log;
        public string Logfile;

        public Matrix A;
        public Vector c;
        public Vector b;

    }
    internal class FejerResult
    {
        public Vector x;
        public Vector u;

        public long Iterations;
        public long Ms;
    }

}
