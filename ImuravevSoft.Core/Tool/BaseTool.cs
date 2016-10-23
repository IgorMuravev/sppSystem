using ImuravevSoft.Core.Data;
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
        private List<BaseMenu> menus = new List<BaseMenu>();
        public List<BaseData> UsedData
        {
            get
            {
                return usedData;
            }
        }
        public List<BaseMenu> Menus
        {
            get { return menus; }
        }


        public event EventHandler<AfterUseDataArgs> AfterUseData = null;
        public event EventHandler<AfterUnuseDataArgs> AfterUnuseData = null;
        public event EventHandler<BeforeUseDataArgs> BeforeUseData = null;
        public event EventHandler<BeforeUnuseDataArgs> BeforeUnuseData = null;

        public virtual void ShowInToolTabs()
        { }

        public virtual void UnuseData(BaseData data)
        {
            UnuseData(new[] { data });
        }
        public virtual void UnuseData(BaseData[] datas)
        {


            var used = new List<BaseData>();
            foreach (var data in UsedData)
            {
                if (BeforeUnuseData != null)
                    BeforeUnuseData(this, new BeforeUnuseDataArgs(data));
                if (!datas.Contains(data))
                    used.Add(data);

                if (AfterUnuseData != null)
                    AfterUnuseData(null, new AfterUnuseDataArgs(data));
            }
            usedData = used;

        }
        public virtual void UseData(BaseData data)
        {
            UseData(new[] { data });
        }
        public virtual void UseData(BaseData[] datas)
        {

            foreach (var data in datas)
            {
                if (BeforeUseData != null)
                    BeforeUseData(null, new BeforeUseDataArgs(data));
                UsedData.Add(data);
                if (AfterUseData != null)
                    AfterUseData(null, new AfterUseDataArgs(data));
            }
        }



        // TO-DO
        public void LoadTool(BinaryReader reader, Dictionary<Guid, BaseData> d)
        {
            int count = reader.ReadInt32();
            var dataInUse = new BaseData[count];
            for (int i = 0; i < count; i++)
            {
                var data = d[(new Guid(reader.ReadString()))];
                dataInUse[i] = data;
            }
            UseData(dataInUse);
        }

        public void SaveTool(BinaryWriter writer)
        {
            writer.Write(usedData.Count);
            foreach (var d in usedData)
            {
                writer.Write(d.Id.ToString());
            }
        }

    }

    public abstract class BaseDataArgs : EventArgs
    {
        public readonly BaseData Data;

        public BaseDataArgs(BaseData d)
        {
            Data = d;
        }
    }

    public class AfterUseDataArgs : BaseDataArgs
    {
        public AfterUseDataArgs(BaseData d) : base(d) { }
    }
    public class AfterUnuseDataArgs : BaseDataArgs
    {
        public AfterUnuseDataArgs(BaseData d) : base(d) { }
    }
    public class BeforeUseDataArgs : BaseDataArgs
    {
        public BeforeUseDataArgs(BaseData d) : base(d) { }
    }
    public class BeforeUnuseDataArgs : BaseDataArgs
    {
        public BeforeUnuseDataArgs(BaseData d) : base(d) { }
    }
}
