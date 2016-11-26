using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Shell;
using ImuravevSoft.GraphData;

namespace ImuravevSoft.Visualizer
{
    [ToolMenu(typeof(Visualizer))]
    public partial class MarkerMenu : BaseMenu
    {
        private Visualizer tool;
        public MarkerMenu()
        {
            InitializeComponent();
            Title = "Маркеры вершин";
        }
        public override bool IsVisible
        {
            get
            {
                tool = Main.Shell.OpenedTools.ActiveTool as Visualizer;
                return (tool != null);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var split = textBox2.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var name = textBox1.Text.Trim();
            var n = new MarkerStyle()
            {
                Color = lblColor.BackColor,
                Width = Convert.ToInt32(textBox3.Text),
                Depth = 1
            };

            foreach (Graph g in tool.UsedData.Where(x => x is Graph))
            {

                g.Markers[name] = n;
                foreach (var vertex in split)
                {
                    foreach (var v in g.Vertexes)
                    {
                        if (v.Caption == vertex)
                            v.Markers.Add(name);
                    }
                }
            }
        }

        private void MarkerMenu_MouseClick(object sender, MouseEventArgs e)
        {
            var split = textBox2.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var name = textBox1.Text.Trim();
            foreach (Graph g in tool.UsedData.Where(x => x is Graph))
            {

                foreach (var vertex in split)
                {
                    foreach (var v in g.Vertexes)
                    {
                        if (v.Caption == vertex)
                            v.Markers.Remove(name);
                    }
                }
            }
        }

        private void btnCLear_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text.Trim();
            foreach (Graph g in tool.UsedData.Where(x => x is Graph))
            {

                if (g.Markers.ContainsKey(name))
                    g.Markers.Remove(name);

                foreach (var v in g.Vertexes)
                {
                    if (v.Markers.Contains(name))
                        v.Markers.Remove(name);
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            foreach (Graph g in tool.UsedData.Where(x => x is Graph))
            {

                g.Markers.Clear();

                foreach (var v in g.Vertexes)
                {
                    v.Markers.Clear();
                }

                foreach (var ed in g.Edges)
                {
                    ed.Markers.Clear();
                }
            }
        }
    }
}
