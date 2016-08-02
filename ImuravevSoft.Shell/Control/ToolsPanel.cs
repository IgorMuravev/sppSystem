﻿using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace ImuravevSoft.Shell.Control
{
    public partial class ToolsPanel : UserControl
    {
        public void LoadTools()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
            var files = Directory.GetFiles(path, "ImuravevSoft.*.dll");
            var left = 0;
            foreach (var f in files)
            {
                var asm = Assembly.LoadFile(f);
                var asmTypes = asm.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseTool)));
                foreach (var t in asmTypes)
                {
                    var attr = t.GetCustomAttributes(typeof(ToolAttribute), false).FirstOrDefault() as ToolAttribute;
                    if (t != default(object))
                    {


                        var btn = new Button();
                        btn.Text = attr.Name;
                        btn.Height = Height;
                        btn.TextAlign = ContentAlignment.BottomCenter;
                        btn.ImageAlign = ContentAlignment.TopCenter;
                        btn.Left = left;
                        left += btn.Width;
                        try
                        {
                            var r = new ResourceManager(t);
                            var icon = r.GetObject("TypeIcon") as Icon;
                            if (icon != null)
                            {
                                btn.Image = ResizeBitmap(icon.ToBitmap(), 32, 32);
                            }
                        }
                        catch
                        {
                            Main.Shell.MessageList.Echo(String.Format("Иконка инструмента '{0}' не найдена", attr.Name), MsgType.Warning);
                        }
                        toolTip1.SetToolTip(btn, attr.Desc);
                        btn.Click += (sender, e) =>
                        {
                            try
                            {
                                if (Main.Shell != null)
                                    Main.Shell.OpenedTools.AddTool(Activator.CreateInstance(t) as BaseTool);
                            }
                            catch (Exception ex)
                            {
                                Main.Shell.MessageList.Echo(ex.ToString(), MsgType.Error);
                            }
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