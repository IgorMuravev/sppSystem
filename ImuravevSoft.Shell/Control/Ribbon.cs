using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImuravevSoft.Core.Tool;
using System.IO;
using System.Reflection;
using ImuravevSoft.Core.Attributes;

namespace ImuravevSoft.Shell.Control
{
    public partial class Ribbon : UserControl
    {
        private readonly Dictionary<Type, List<BaseMenu>> typesMenu = new Dictionary<Type, List<BaseMenu>>();
        private readonly Dictionary<BaseMenu, TabPage> menuPages = new Dictionary<BaseMenu, TabPage>();

        private void OnToolChanged(object sender, EventArgs e)
        {
            var tool = Main.Shell.OpenedTools.ActiveTool;
            if (tool == null)
            {
                for (int i = tabControl1.TabCount - 1; i > 1; i--)
                    tabControl1.TabPages.RemoveAt(i);
                return;
            }
            if (typesMenu.ContainsKey(tool.GetType()))
            {
                foreach (var m in typesMenu[tool.GetType()])
                {
                    if (m.IsVisible)
                    {
                        if (!tabControl1.TabPages.Contains(menuPages[m]))
                            tabControl1.TabPages.Add(menuPages[m]);
                    }
                    else
                        tabControl1.TabPages.Remove(menuPages[m]);

                }
            }

        }
        public ToolsPanel Tools
        {
            get
            {
                return tabControl1.TabPages[1].Controls[0] as ToolsPanel;
            }
        }
        public Ribbon()
        {
            InitializeComponent();
        }

        public void Init()
        {
            typesMenu.Clear();
            menuPages.Clear();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
            var files = Directory.GetFiles(path, "ImuravevSoft.*.dll");
            foreach (var f in files)
            {
                var asm = Assembly.LoadFile(f);
                var asmTypes = asm.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseMenu)));
                foreach (var t in asmTypes)
                {
                    var attr = t.GetCustomAttributes(typeof(ToolMenuAttribute), false) as ToolMenuAttribute[];
                    var instance = Activator.CreateInstance(t) as BaseMenu;
                    var page = new TabPage(instance.Title);
                    page.Controls.Add(instance);
                    instance.Dock = DockStyle.Fill;
                    menuPages.Add(instance, page);
                    foreach (var m in attr)
                    {
                        if (typesMenu.ContainsKey(m.ToolType))
                            typesMenu[m.ToolType].Add(instance);
                        else
                        {
                            typesMenu.Add(m.ToolType, new List<BaseMenu>() { instance });
                        }


                    }

                }
            }
            Main.Shell.OpenedTools.ToolChanged += OnToolChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.Shell.LoadFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main.Shell.SaveFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main.Shell.Exit();
        }


    }
}
