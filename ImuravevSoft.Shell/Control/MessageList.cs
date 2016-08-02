using System;
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
            var item = new ListViewItem(new[] {"", DateTime.Now.ToString(), msg });
            item.ImageIndex = (int)type;
            listView1.Items.Add(item);
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
