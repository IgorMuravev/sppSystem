using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Shell;
using System;
using ImuravevSoft.Core.Data;
using System.Linq;
using ImuravevSoft.GraphData;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImuravevSoft.GraphGen
{
    [Tool("Генератор графов", "Позволяет генерировать планарный граф с различным числом вершин")]
    [ReqData(typeof(Graph))]
    public partial class GraphGenTool : BaseTool
    {
        private PointF Translate(PointF p)
        {
            return new PointF() { X = p.X, Y = panel1.Height - p.Y };
        }
        public GraphGenTool()
        {
            InitializeComponent();
            AfterUseData += AUseData;
            BeforeUseData += BUseData;
        }


        public Graph Graph { get; private set; }
        public void BUseData(object sender, EventArgs e)
        {
            var g = UsedData.OfType<Graph>().ToArray();
            UnuseData(g);
        }

        public void AUseData(object sender, EventArgs e)
        {
            Graph = UsedData.OfType<Graph>().FirstOrDefault();
            DrawGraph();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graph = GraphGen.GetPlanarZoneGraph(new Location() { X = 0, Y = 0, Width = panel1.Width, Height = panel1.Height }, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            DrawGraph();
        }
        public void Draw(Graph d,Graphics g, Func<PointF, PointF> func = null)
        {

            var points = new Dictionary<Vertex, PointF>();
            foreach (var v in d.Vertexes)
            {
                PointF p = new PointF((float)v.X - 3f, (float)v.Y - 3f);
                if (func != null)
                    p = func(p);
                points.Add(v, p);
                g.FillEllipse(Brushes.Blue, p.X, p.Y, 6f, 6f);
            }
            foreach (var edge in d.Edges)
            {
                var p1 = points[edge.V1];
                var p2 = points[edge.V2];
                g.DrawLine(Pens.Black, p1, p2);
            }
        }

        public void DrawGraph()
        {
            if (Graph == null) return;
            var bmp = new Bitmap(panel1.Width, panel1.Height);

            using (var g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, new RectangleF(new PointF(0, 0), bmp.Size));
                Draw(Graph,g, Translate);
            }
            using (var g = panel1.CreateGraphics())
                g.DrawImage(bmp, 0, 0);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Graph == null) return;
            if (MessageBox.Show("Сохранить полученный граф в дерево данных?", "Подтвердите", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Graph.Name = "Сгенерированный граф";
                Main.Shell.DataManager.AddData(Graph);
            }
        }
    }
}
