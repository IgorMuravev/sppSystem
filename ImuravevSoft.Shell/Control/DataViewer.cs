using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImuravevSoft.Core.Data;
using System.IO;
using System.Reflection;
using ImuravevSoft.Core.Attributes;

namespace ImuravevSoft.Shell.Control
{
    public partial class DataViewer : UserControl
    {
        private readonly Dictionary<Guid, BaseData> datas = new Dictionary<Guid, BaseData>();
        private readonly Dictionary<Type, TreeNode> types = new Dictionary<Type, TreeNode>();
        private readonly Dictionary<Guid, Type> guidTypes = new Dictionary<Guid, Type>();

        public TreeNode TreeNodeByType(Type t)
        {
            if (types.ContainsKey(t))
                return types[t];
            else
                return null;
        }
        public void LoadDataTypes()
        {
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

                        types.Add(t, node);
                        guidTypes.Add(attr.TypeGuid, t);
                    }
                }
            }
            treeView1.Nodes.AddRange(types.Values.ToArray());

        }
        public DataViewer()
        {
            InitializeComponent();
        }
        public Dictionary<Guid, Type> TypesGuid
        {
            get
            {
                return guidTypes;
            }
        }
        public BaseData DataById(Guid Id)
        {
            if (datas.ContainsKey(Id))
                return datas[Id];
            else
                return null;
        }
        public BaseData[] AllDatas()
        {
            return datas.Values.ToArray();
        }
        public void AddData(BaseData data)
        {
            AddData(new[] { data });
        }
        public void AddData(BaseData[] range)
        {
            for (int i = 0; i < range.Length; i++)
            {
                datas.Add(range[i].Id, range[i]);
                var rootNode = types[range[i].GetType()];
                var node = new TreeNode(range[i].Name)
                {
                    Tag = range[i].Id
                };
                rootNode.Nodes.Add(node);
            }
        }

        public IEnumerable<BaseData> Selected
        {
            get
            {
                return datas.Values.Where(x => x.IsSelected);
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var node = treeView1.GetNodeAt(e.X, e.Y);
                if (!types.ContainsValue(node))
                {
                    var contextMenu = new ContextMenuStrip();
                    contextMenu.Items.Add("Переименовать");
                    contextMenu.Items[0].Click += DataViewer_Click;
                    contextMenu.Show(treeView1, new Point(e.X, e.Y));
                    treeView1.SelectedNode = node;
                }
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
    }
}
