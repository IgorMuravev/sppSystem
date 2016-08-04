using ImuravevSoft.Core.Data;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Shell.Control;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ImuravevSoft.Shell
{
    public partial class Main : Form
    {
        public static Main Shell;

        public Main()
        {
            InitializeComponent();
            DataManager = dataViewer1;
            MessageList = messageList1;
            Tools = toolsPanel1;
            OpenedTools = toolTabs1;
            Main.Shell = this;

            DataManager.Init();
            Tools.LoadTools();

            MessageList.Echo("Программа запущена", MsgType.Info);
        }

        public readonly DataViewer DataManager;
        public readonly MessageList MessageList;
        public readonly ToolsPanel Tools;
        public readonly ToolTabs OpenedTools;

        public void SaveData(string fileName)
        {
            using (var writer = new BinaryWriter(File.OpenWrite(fileName)))
            {
                var data = DataManager.AllDatas();
                writer.Write(data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    var name = data[i].GetType().FullName + "," + data[i].GetType().Assembly.GetName().Name;
                    writer.Write(name);
                    data[i].Save(writer);

                }
                var tools = OpenedTools.OpenedTools;
                writer.Write(tools.Count);
                for (int i = 0; i < tools.Count; i++)
                {
                    var name = tools[i].GetType().FullName + "," + tools[i].GetType().Assembly.GetName().Name;
                    writer.Write(name);
                    tools[i].SaveTool(writer);
                }
            }
        }

        public void LoadData(string fileName)
        {
            var loadedData = new List<BaseData>();
            using (var reader = new BinaryReader(File.OpenRead(fileName)))
            {
                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    var name = reader.ReadString();
                    var type = Type.GetType(name);
                    var instance = Activator.CreateInstance(type) as BaseData;
                    instance.Load(reader);
                    loadedData.Add(instance);
                }
                DataManager.AddData(loadedData.ToArray());
                count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    var name = reader.ReadString();
                    var type = Type.GetType(name);
                    var instance = Activator.CreateInstance(type) as BaseTool;
                    instance.LoadTool(reader);
                    OpenedTools.AddTool(instance);
                }
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadData(openFileDialog1.FileName);
                MessageList.Echo("Данные загружены");
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveData(saveFileDialog1.FileName);
                MessageList.Echo("Данные сохранены!");
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить изменения перед выходом?", "Сохранение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                сохранитьToolStripMenuItem_Click(null, EventArgs.Empty);
            }
            Close();
        }
    }
}
