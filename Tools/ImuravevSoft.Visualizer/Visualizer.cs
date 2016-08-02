using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.GraphData;
using System;

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
