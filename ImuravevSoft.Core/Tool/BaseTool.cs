using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ImuravevSoft.Core.Tool
{
    public partial class BaseTool : UserControl
    {
        private List<BaseData> usedData = new List<BaseData>();
        public List<BaseData> UsedData
        {
            get
            {
                return usedData;
            }
        }

        public EventHandler OnUseData = null;
        public EventHandler OnUnuseData = null;

        public virtual void UnuseData(BaseData data)
        {
            UnuseData(new[] { data });
        }
        public virtual void UnuseData(BaseData[] datas)
        {
            var used = new List<BaseData>();
            foreach (var data in UsedData)
            {
                if (!datas.Contains(data))
                    used.Add(data);
            }
            usedData = used;
            if (OnUnuseData != null)
                OnUnuseData(null, EventArgs.Empty);
        }
        public virtual void UseData(BaseData data)
        {
            UseData(new[] { data });
        }
        public virtual void UseData(BaseData[] datas)
        {
            UsedData.AddRange(datas);
            if (OnUseData != null)
                OnUseData(null, EventArgs.Empty);
        }

    }
}
