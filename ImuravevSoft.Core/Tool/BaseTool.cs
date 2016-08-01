using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImuravevSoft.Core.Tool
{
    public partial class BaseTool : UserControl
    {
        private ProgressBar progressBar1;
        private readonly List<BaseData> usedData = new List<BaseData>();
        public List<BaseData> UsedData
        {
            get
            {
                return usedData;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseTool
            // 
            this.Name = "BaseTool";
            this.Size = new System.Drawing.Size(788, 213);
            this.ResumeLayout(false);

        }

    }
}
