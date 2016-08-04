using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.GraphData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using ImuravevSoft.Shell;

namespace ImuravevSoft.Visualizer
{
    [Tool("Визуализатор графов", "Позволяет отображать графы на плоскости")]
    [ReqData(typeof(Graph))]
    public partial class Visualizer : BaseTool
    {
        private bool canMoving = false;
        private float dx = 0;
        private float dy = 0;
        private Point prevPoint;

        public Graph Graph { get; private set; }
        public float ZoomScale { get; private set; }
        private void AUsedData(object sender, EventArgs e)
        {
            Graph = UsedData.OfType<Graph>().FirstOrDefault();
            dx = 0;
            dy = 0;
            ZoomScale = 1;
            DrawTo();
        }
        private void BUsedData(object sender, EventArgs e)
        {
            var g = UsedData.OfType<Graph>().ToArray();
            UnuseData(g);
        }

        private void AUnusedData(object sender, EventArgs e)
        {
            if (Graph != null && !UsedData.Contains(Graph))
                Graph = null;
        }

        public override void ShowInToolTabs()
        {
            DrawTo();
        }

        public override void LoadTool(BinaryReader reader)
        {
            if (reader.ReadBoolean())
                Graph = Main.Shell.DataManager.DataById(new Guid(reader.ReadString())) as Graph;
        }

        public override void SaveTool(BinaryWriter writer)
        {
            writer.Write(Graph != null);
            if (Graph != null)
                writer.Write(Graph.Id.ToString());
        }

        private void DrawTo()
        {
            if (Graph != null)
            {
                var bmp = new Bitmap(Width, Height);
                float kx = ZoomScale * Width / Graph.Border.Width;
                float ky = ZoomScale * Height / Graph.Border.Height;

                var f = new Func<VertexPoint, VertexPoint>(p =>
                {
                    return new VertexPoint(kx * (p.X - Graph.Border.X) + dx, Height - ky * (p.Y - Graph.Border.Y) + dy);
                });

                var points = new Dictionary<Vertex, VertexPoint>();
                using (var g = Graphics.FromImage(bmp))
                {
                    foreach (var v in Graph.Vertexes)
                    {
                        var p = f(v);
                        points.Add(v, p);
                        g.FillEllipse(Brushes.Blue, (float)p.X - 3f, (float)p.Y - 3f, 6f, 6f);
                    }

                    foreach (var e in Graph.Edges)
                    {
                        var p1 = points[e.V1];
                        var p2 = points[e.V2];
                        g.DrawLine(Pens.Black, (float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y);
                    }

                }
                pictureBox1.Image = bmp;

            }
        }
        public Visualizer()
        {

            InitializeComponent();
            prevPoint = new Point(0, 0);
            ZoomScale = 1;
            pictureBox1.MouseWheel += Visualizer_MouseWheel;
            AfterUseData += AUsedData;
            BeforeUseData += BUsedData;
            AfterUnuseData += AUnusedData;

        }

        private void Visualizer_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var adding = trackBar1.Value + e.Delta / 10;
            if (adding > trackBar1.Maximum)
                trackBar1.Value = trackBar1.Maximum;
            else if (adding < trackBar1.Minimum)
                trackBar1.Value = trackBar1.Minimum;
            else
                trackBar1.Value = adding;
        }

        private void Visualizer_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                canMoving = true;
                prevPoint = e.Location;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                pictureBox1.Focus();
            }
        }

        private void Visualizer_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            canMoving = false;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            ZoomScale = trackBar1.Value / 100f;
            DrawTo();
        }

        private void Visualizer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (canMoving)
            {
                dx += (e.X - prevPoint.X) * 0.7f;
                dy += (e.Y - prevPoint.Y) * 0.7f;
                prevPoint = e.Location;
                DrawTo();
            }
            if (!pictureBox1.Focused)
                pictureBox1.Focus();
        }

        private void Visualizer_SizeChanged(object sender, EventArgs e)
        {
            DrawTo();
        }

    }
}
