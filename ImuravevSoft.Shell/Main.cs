using ImuravevSoft.Core.Data;
using ImuravevSoft.Shell.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImuravevSoft.Shell
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            DataManager = dataViewer1;
            MessageList = messageList1;

            MessageList.Echo("Программа запущена", MsgType.Info);
        }

        public readonly DataViewer DataManager;
        public readonly MessageList MessageList;

        public void SaveData(string fileName)
        {
            using (var writer = new BinaryWriter(File.OpenWrite(fileName)))
            {
                var data = DataManager.AllDatas();
                for (int i = 0; i < data.Length; i++)
                {
                    writer.Write(data.GetType().GUID.ToString());
                    data[i].Save(writer);
                }
            }
        }

        public void LoadData(string fileName)
        {
            var loadedData = new List<BaseData>();
            using (var reader = new BinaryReader(File.OpenRead(fileName)))
            {
                var guid = new Guid(reader.ReadString());
                var type = Type.GetTypeFromCLSID(guid);
                var instance = Activator.CreateInstance(type) as BaseData;
                instance.Load(reader);
                loadedData.Add(instance);
            }
            DataManager.AddData(loadedData.ToArray());
        }
    }
}
