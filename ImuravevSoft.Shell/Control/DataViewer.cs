using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImuravevSoft.Core.Data;

namespace ImuravevSoft.Shell.Control
{
    public partial class DataViewer : UserControl
    {
        private readonly Dictionary<Guid, BaseData> datas = new Dictionary<Guid, BaseData>();
        public DataViewer()
        {
            InitializeComponent();
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
                dataListView.Items.Add(range[i].Name);
                datas.Add(range[i].Id, range[i]);
            }
        }
    }
}
