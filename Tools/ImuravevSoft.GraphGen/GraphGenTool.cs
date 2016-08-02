using ImuravevSoft.Core.Attributes;
using ImuravevSoft.Core.Tool;
using ImuravevSoft.Shell;
using System;
using ImuravevSoft.Core.Data;
using System.Linq;

namespace ImuravevSoft.GraphGen
{
    [Tool("Генератор графов", "Позволяет генерировать планарный граф с различным числом вершин")]
    [ReqData(typeof(GraphData.GraphData))]
    public partial class GraphGenTool : BaseTool
    {
        public GraphGenTool()
        {
            InitializeComponent();
            OnUseData += UseData;
        }

        public GraphData.GraphData Graph { get; private set; }
        public void UseData(object sender, EventArgs e)
        {
            var g = UsedData.OfType<GraphData.GraphData>().FirstOrDefault();
            if (g != null)
            {
                Graph = g;
                label1.Text = Graph.Name;
            }
        }


    }
}
