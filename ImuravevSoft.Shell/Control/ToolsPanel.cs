using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Core.Attributes;
using System.Resources;

namespace ImuravevSoft.Shell.Control
{
    public partial class ToolsPanel : UserControl
    {
        public void LoadTools()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
            var files = Directory.GetFiles(path, "ImuravevSoft.*.dll");
            foreach (var f in files)
            {
                var asm = Assembly.LoadFile(f);
                var asmTypes = asm.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseTool)));
                foreach (var t in asmTypes)
                {
                    var attr = t.GetCustomAttributes(typeof(ToolAttribute), false).FirstOrDefault() as ToolAttribute;
                    if (t != default(object))
                    {
                   
                        var r = new ResourceManager(t);
                        var icon =  r.GetObject("TypeIcon") as Icon;
                        var btn = new Button();
                        btn.Text = attr.Name;
                        btn.Height = Height;
                        btn.TextAlign = ContentAlignment.BottomCenter;
                        btn.ImageAlign = ContentAlignment.TopCenter;
                        if (icon != null)
                        {
                            btn.Image = ResizeBitmap(icon.ToBitmap(), 32, 32);
                        }
                        toolTip1.SetToolTip(btn, attr.Desc);
                        btn.Click += (sender, e) => {
                            if (Main.Shell != null)
                                Main.Shell.OpenedTools.AddTool(Activator.CreateInstance(t) as BaseTool);
                        };
                        Controls.Add(btn);
                    }
                }
            }
        }

        private static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }
        public ToolsPanel()
        {
            InitializeComponent();
        }
    }
}
