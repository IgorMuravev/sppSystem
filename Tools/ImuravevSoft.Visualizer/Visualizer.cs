using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.GraphData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using ImuravevSoft.Shell;
using ImuravevSoft.Core.Data;

namespace ImuravevSoft.Visualizer
{
    [Tool("Визуализатор графов", "Позволяет отображать графы на плоскости")]
    [ReqData(typeof(IDrawable))]
    public partial class Visualizer : BaseTool
    {
        private bool canMoving = false;
        private float dx = 0;
        private float dy = 0;
        private Point prevPoint;
        private RectangleF worldRect;

        public float ZoomScale { get; private set; }
        private void AUsedData(object sender, AfterUseDataArgs e)
        {
            var border = (e.Data as IDrawable).GetBorder();
            var newx = border.X < worldRect.X ? border.X : worldRect.X;
            var newy = border.Y < worldRect.Y ? border.Y : worldRect.Y;
            var newwidth = border.Right > worldRect.Right ? border.Right : worldRect.Right;
            var newheigth = border.Bottom > worldRect.Bottom ? border.Bottom : worldRect.Bottom;
            worldRect = new RectangleF(newx, newy, newwidth - newx, newheigth - newy);
            DrawTo();
        }

        public override void ShowInToolTabs()
        {
            DrawTo();
        }

        private void DrawTo()
        {
            var bmp = new Bitmap(Width, Height);
            float kx = ZoomScale * Width / worldRect.Width;
            float ky = ZoomScale * Height / worldRect.Height;
            
            var f = new Func<PointF, PointF>(p =>
            {
                return new PointF(kx * (p.X - worldRect.X) + dx, Height - ky * (p.Y - worldRect.Y) + dy);
            });
            using (var g = Graphics.FromImage(bmp))
            {
                foreach (var d in UsedData)
                {

                    var draw = d as IDrawable;
                    if (draw != null)
                        draw.Draw(g, f);
                }
            }
            pictureBox1.Image = bmp;
        }
        public Visualizer()
        {

            InitializeComponent();
            prevPoint = new Point(0, 0);
            ZoomScale = 1;
            pictureBox1.MouseWheel += Visualizer_MouseWheel;
            AfterUseData += AUsedData;
            AfterUnuseData += Visualizer_AfterUnuseData;

        }

        private void Visualizer_AfterUnuseData(object sender, AfterUnuseDataArgs e)
        {
            DrawTo();
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
