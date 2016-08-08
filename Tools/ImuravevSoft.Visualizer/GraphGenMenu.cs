using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Visualizer.Classes;
using ImuravevSoft.Shell;
using ImuravevSoft.Core.Attributes;

namespace ImuravevSoft.Visualizer
{
    [ToolMenu(typeof(Visualizer))]
    public partial class GraphGenMenu : BaseMenu
    {
        public GraphGenMenu()
        {
            InitializeComponent();
            Graph = null;
            Title = "Генерация графа";
        }

        public override bool IsVisible
        {
            get
            {
                var tool = Main.Shell.OpenedTools.ActiveTool as Visualizer;
                if (tool == null) return false;

                return true;
            }
        }

        private GraphData.Graph Graph;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var x = Convert.ToInt32(numericUpDown1.Value);
                var y = Convert.ToInt32(numericUpDown2.Value);
                var w = Convert.ToInt32(numericUpDown3.Value);
                var h = Convert.ToInt32(numericUpDown4.Value); 
                Graph = GraphGen.GetPlanarZoneGraph(new Location() { X = x, Y = y, Width = w, Height = h }, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            }
            catch (Exception ex)
            {
                Main.Shell.MessageList.Echo(ex.ToString(), Shell.Control.MsgType.Error);
            }
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
