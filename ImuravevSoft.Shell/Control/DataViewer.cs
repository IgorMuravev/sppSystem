using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Data;
using ImuravevSoft.Core.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ImuravevSoft.Shell.Control
{
    public partial class DataViewer : UserControl
    {
        /// <summary>
        /// По гуиду данные
        /// </summary>
        private readonly Dictionary<Guid, BaseData> guidData = new Dictionary<Guid, BaseData>();
        /// <summary>
        /// Под данным тринод
        /// </summary>
        private readonly Dictionary<BaseData, TreeNode> dataNode = new Dictionary<BaseData, TreeNode>();
        /// <summary>
        /// По типу можем узнать корневой узел
        /// </summary>
        private readonly Dictionary<Type, TreeNode> typeNode = new Dictionary<Type, TreeNode>();
        /// <summary>
        /// ПО гуиду узнает тип
        /// </summary>
        private readonly Dictionary<Guid, Type> guidType = new Dictionary<Guid, Type>();
        /// <summary>
        /// Разрешенные данные к использованию
        /// </summary>
        private readonly List<BaseData> usedData = new List<BaseData>();

        private BaseTool tool = null;
        private void OnToolChanged(object sender, EventArgs s)
        {
            usedData.Clear();
            tool = Main.Shell.OpenedTools.ActiveTool;
            if (tool == null) return;
            var reqData = tool.GetType().GetCustomAttributes(typeof(ReqDataAttribute), false) as ReqDataAttribute[];
            foreach (var req in reqData)
            {
                var rootType = typeNode[req.Data];
                foreach (TreeNode child in rootType.Nodes)
                    usedData.Add(guidData[(Guid)child.Tag]);
            }
            treeView1.Invalidate();
        }
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var node = treeView1.GetNodeAt(e.X, e.Y);
                if (!typeNode.ContainsValue(node))
                {
                    treeView1.SelectedNode = node;
                    var contextMenu = new ContextMenuStrip();
                    contextMenu.Items.Add("Переименовать");
                    contextMenu.Items.Add("Удалить");
                    contextMenu.Items[0].Click += DataViewer_Click;
                    contextMenu.Items[1].Click += DataViewer_Remove;
                    contextMenu.Show(treeView1, new Point(e.X, e.Y));
                    treeView1.SelectedNode = node;
                }
            }
        }

        private void DataViewer_Remove(object sender, EventArgs e)
        {
         
            var node = treeView1.SelectedNode;
            if (MessageBox.Show(String.Format("Вы действительно хотите удалить '{0}' ?", node.Text), "Подтвердите", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                RemoveData(guidData[(Guid)node.Tag]);
            }
        }
        private void DataViewer_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (!node.IsEditing)
            {
                node.BeginEdit();
            }
        }
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null && e.Label.Length > 0)
            {
                var data = DataById((Guid)e.Node.Tag);
                if (data != null)
                {
                    data.Name = e.Label;
                }
                e.Node.EndEdit(true);
            }
        }
        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.Graphics.DrawString(e.Node.Text, treeView1.Font, Brushes.Black, e.Bounds);
            if (tool == null) return;
            if (!typeNode.ContainsValue(e.Node))
            {
                var data = guidData[(Guid)e.Node.Tag];
                if (usedData.Contains(data))
                {
                    if (tool.UsedData.Contains(data))
                        e.Graphics.DrawImage(Properties.Resources.check, treeView1.Width - e.Bounds.Height - 5, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                    else
                        e.Graphics.DrawImage(Properties.Resources.uncheck, treeView1.Width - e.Bounds.Height - 5, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                }
            }
        }
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tool == null) return;
            var node = treeView1.GetNodeAt(e.X, e.Y);
            if (node != null && !typeNode.ContainsValue(node))
            {
                var data = guidData[(Guid)node.Tag];
                if (node.Bounds.Contains(e.Location) && usedData.Contains(data))
                    if (tool.UsedData.Contains(data))
                        tool.UnuseData(data);
                    else
                        tool.UseData(data);
            }
            Refresh();
        }


        public void Init()
        {
            Main.Shell.OpenedTools.ToolChanged += OnToolChanged;

            treeView1.Nodes.Clear();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
            var files = Directory.GetFiles(path, "ImuravevSoft.*.dll");
            foreach (var f in files)
            {
                var asm = Assembly.LoadFile(f);
                var asmTypes = asm.GetTypes().Where(x => x.IsSubclassOf(typeof(BaseData)));
                foreach (var t in asmTypes)
                {
                    var attr = t.GetCustomAttributes(typeof(DataTreeAttribute), false).FirstOrDefault() as DataTreeAttribute;
                    if (t != default(object))
                    {
                        var node = new TreeNode(attr.Name);
                        typeNode.Add(t, node);
                        guidType.Add(attr.TypeGuid, t);
                    }
                }
            }
            treeView1.Nodes.AddRange(typeNode.Values.ToArray());
        

        }
        public DataViewer()
        {
            InitializeComponent();

        }
        public Dictionary<Guid, Type> TypesGuid
        {
            get
            {
                return guidType;
            }
        }
        public BaseData DataById(Guid Id)
        {
            if (guidData.ContainsKey(Id))
                return guidData[Id];
            else
                return null;
        }
        public BaseData[] AllDatas()
        {
            return guidData.Values.ToArray();
        }
        public void AddData(BaseData data)
        {
            AddData(new[] { data });
        }
        public void AddData(BaseData[] range)
        {
            for (int i = 0; i < range.Length; i++)
            {
                guidData.Add(range[i].Id, range[i]);
                var rootNode = typeNode[range[i].GetType()];
                var node = new TreeNode(range[i].Name)
                {
                    Tag = range[i].Id
                };
                dataNode.Add(range[i], node);
                rootNode.Nodes.Add(node);
                rootNode.Expand();
            }
            OnToolChanged(null, EventArgs.Empty);
        }

        public void RemoveData(BaseData data)
        {
            RemoveData(new[] { data });
        }
        public void RemoveData(BaseData[] range)
        {
            for (int i = 0; i < range.Length; i++)
            {
                
                var node = dataNode[range[i]];
                var guid = (Guid)node.Tag;
                var data = guidData[guid];
                foreach (var tool in Main.Shell.OpenedTools.OpenedTools)
                {
                    if (tool.UsedData.Contains(data))
                        tool.UnuseData(data);
                }
                guidData.Remove(guid);
                node.Remove();
            }
            OnToolChanged(null, EventArgs.Empty);
        }
    }
}
