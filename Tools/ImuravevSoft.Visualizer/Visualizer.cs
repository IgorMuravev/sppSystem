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

namespace ImuravevSoft.Visualizer
{
    [Tool("Визуализатор графов","Позволяет отображать графы на плоскости")]
    [ReqData(typeof(GraphData.GraphData))]
    public partial class Visualizer : BaseTool
    {
        public Visualizer()
        {
            InitializeComponent();
        }
    }
}
