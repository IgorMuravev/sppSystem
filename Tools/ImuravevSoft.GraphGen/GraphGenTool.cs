using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImuravevSoft.GraphGen
{
    [Tool("Генератор графов", "Позволяет генерировать планарный граф с различным числом вершин")]
    [ReqData(typeof(GraphData.GraphData))]
    public partial class GraphGenTool : BaseTool
    {
        public GraphGenTool()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main.Shell.MessageList.Echo("НАГЕНЕРИЛ НАХУЙ");
        }
    }
}
