using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImuravevSoft.Core.Tool
{
    public abstract class BaseTool:UserControl
    {
        private readonly List<BaseData> usedData = new List<BaseData>();
        public List<BaseData> UsedData
        {
            get
            {
                return usedData;
            }
        }

    }
}
