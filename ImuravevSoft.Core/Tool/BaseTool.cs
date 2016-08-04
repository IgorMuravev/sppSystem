﻿using ImuravevSoft.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
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

        public event EventHandler AfterUseData = null;
        public event EventHandler AfterUnuseData = null;
        public event EventHandler BeforeUseData = null;
        public event EventHandler BeforeUnuseData = null;

        public virtual void ShowInToolTabs()
        { }

        public virtual void UnuseData(BaseData data)
        {
            UnuseData(new[] { data });
        }
        public virtual void UnuseData(BaseData[] datas)
        {
            if (BeforeUnuseData != null)
                BeforeUnuseData(null, EventArgs.Empty);

            var used = new List<BaseData>();
            foreach (var data in UsedData)
            {
                if (!datas.Contains(data))
                    used.Add(data);
            }
            usedData = used;

            if (AfterUnuseData != null)
                AfterUnuseData(null, EventArgs.Empty);
        }
        public virtual void UseData(BaseData data)
        {
            UseData(new[] { data });
        }
        public virtual void UseData(BaseData[] datas)
        {
            if (BeforeUseData != null)
                BeforeUseData(null, EventArgs.Empty);

            UsedData.AddRange(datas);

            if (AfterUseData != null)
                AfterUseData(null, EventArgs.Empty);
        }

        public virtual void LoadTool(BinaryReader reader)
        { }

        public virtual void SaveTool(BinaryWriter writer)
        {

        }

    }
}
