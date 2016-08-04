using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ImuravevSoft.Shell.Control
{
    public partial class ToolTabs : UserControl
    {
        public ToolTabs()
        {
            InitializeComponent();

        }
        private readonly Dictionary<TabPage, BaseTool> openedTools = new Dictionary<TabPage, BaseTool>();
        public List<BaseTool> OpenedTools
        {
            get
            {
                return openedTools.Values.ToList();
            }
        }
        public void AddTool(BaseTool tool)
        {
            var attr = tool.GetType().GetCustomAttributes(typeof(ToolAttribute), false).FirstOrDefault() as ToolAttribute;
            if (attr != default(ToolAttribute))
            {
                var page = new TabPage(attr.Name);
                page.Controls.Add(tool);
                tool.Dock = DockStyle.Fill;
                openedTools.Add(page, tool);
                tabControl1.TabPages.Add(page);
            }


        }
        public BaseTool ActiveTool
        {
            get
            {
                if (tabControl1.SelectedTab == null) return null;

                if (openedTools.ContainsKey(tabControl1.SelectedTab))
                    return openedTools[tabControl1.SelectedTab];
                else
                    return null;
            }
        }
        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Rectangle r = tabControl1.GetTabRect(this.tabControl1.SelectedIndex);
                Rectangle closeButton = new Rectangle(r.Right - 10, r.Top, 10, r.Height);
                if (closeButton.Contains(e.Location))
                    this.tabControl1.TabPages.Remove(this.tabControl1.SelectedTab);
            }
            if (e.Button == MouseButtons.Right)
            {
                Rectangle r = tabControl1.GetTabRect(this.tabControl1.SelectedIndex);
                if (r.Contains(e.Location))
                    contextMenuStrip1.Show(this,e.Location);
            }
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.DrawString("x", e.Font, Brushes.LightGray, e.Bounds.Right - 10, e.Bounds.Top);
            e.Graphics.DrawString(this.tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left, e.Bounds.Top + 4);
       

        }

        public event EventHandler ToolChanged = null;
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (ToolChanged != null)
                ToolChanged(null, EventArgs.Empty);
            if (ActiveTool != null)
                ActiveTool.ShowInToolTabs();
        }
        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            tabControl1.SelectTab(tabControl1.TabCount - 1);
            if (tabControl1.TabCount == 1)
                tabControl1_Selected(sender, null);
        }

        private void закрытьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Controls[0].Dispose();
            }
            tabControl1.TabPages.Clear();
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tab = tabControl1.SelectedTab;
            tab.Controls[0].Dispose();
            tabControl1.TabPages.Remove(tab);
        }

        private void закрытьВсеКромеЭтоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selTab = tabControl1.SelectedTab;
            var toRemove = new List<TabPage>();
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (selTab != tab)
                {
                    tab.Controls[0].Dispose();
                    toRemove.Add(tab);
                }
            }
            for (int i = 0; i < toRemove.Count; i++)
                tabControl1.TabPages.Remove(toRemove[i]);
        }
    }
}
