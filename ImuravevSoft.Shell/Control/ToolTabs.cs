using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Core.Attributes;

namespace ImuravevSoft.Shell.Control
{
    public partial class ToolTabs : UserControl
    {
        public ToolTabs()
        {
            InitializeComponent();
            
        }

        public void AddTool(BaseTool tool)
        {
            var attr = tool.GetType().GetCustomAttributes(typeof(ToolAttribute), false).FirstOrDefault() as ToolAttribute;
            if (attr != default(ToolAttribute))
            {
                tabControl1.TabPages.Add(attr.Name);
                var page = tabControl1.TabPages[tabControl1.TabPages.Count - 1];
                page.Controls.Add(tool);
                tool.Dock = DockStyle.Fill;
            }
            var attr1 = tool.GetType().GetCustomAttributes(typeof(ReqDataAttribute), false) as ReqDataAttribute[];
            if (attr1 != null)
            {
                foreach (var data in attr1)
                {
                    var node = Main.Shell.DataManager.TreeNodeByType(data.Data);
                    if (node != null)
                    {
                        for (int i = 0; i < node.Nodes.Count; i++)
                            node.Checked = true;
                    }
                }
            }

        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle r = tabControl1.GetTabRect(this.tabControl1.SelectedIndex);
            Rectangle closeButton = new Rectangle(r.Right - 12, r.Top + 4, 10, 15);
            if (closeButton.Contains(e.Location))
                this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
           
            e.Graphics.DrawString("x", e.Font, Brushes.Black, e.Bounds.Right - 12, e.Bounds.Top + 4);
            e.Graphics.DrawRectangle(Pens.Black, e.Bounds.Right - 12, e.Bounds.Top + 4, 10, 15);
            e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left, e.Bounds.Top + 4);
            e.DrawFocusRectangle();

        }
    }
}
