using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Core.Attributes;
using ImuravevSoft.GraphData;

namespace ImuravevSoft.Visualizer
{
    [Tool("Визуализатор графов","Позволяет отображать графы на плоскости")]
    [ReqData(typeof(Graph))]
    public partial class Visualizer : BaseTool
    {
        private void UsedData(object sender, EventArgs e)
        {

        }
        public Visualizer()
        {
            InitializeComponent();
            AfterUseData += UsedData;
        }
    }
}
