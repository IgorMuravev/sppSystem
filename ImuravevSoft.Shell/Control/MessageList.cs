using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImuravevSoft.Shell.Control
{
    public partial class MessageList : UserControl
    {
        private readonly int typeMsgColWidth = 50;
        private readonly int dateTimeMsgColWidth = 150;

        private readonly int typeColIndex = 0;
        private readonly int dateColIndex = 1;
        private readonly int msgColIndex = 2;

        public MessageList()
        {
            InitializeComponent();
        }
        public void Echo(string msg, MsgType type = MsgType.Info)
        {
            listView1.Items.Add(new ListViewItem(new[] { type.ToString(), DateTime.Now.ToString(), msg }));
        }

        public void Clear()
        {
            listView1.Items.Clear();
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            var w = listView1.Width - typeMsgColWidth - dateTimeMsgColWidth - 10;
            listView1.Columns[msgColIndex].Width = w;
        }

    }

    public enum MsgType { Info, Warning, Error }
}
