using System;
using System.IO;

namespace ImuravevSoft.Core.Data
{
    /// <summary>
    /// Базовый класс данных
    /// </summary>
    public abstract class BaseData
    {
        private Guid id;
        public string Name { get; set; }
        public Guid Id
        {
            get
            {
                return id;
            }
        }
        public bool IsSelected { get; set; }
        public BaseData()
        {
            id = Guid.NewGuid();

        }


        public virtual void Save(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(id.ToString());
        }
        public virtual void Load(BinaryReader reader)
        {
            Name = reader.ReadString();
            id = new Guid(reader.ReadString());
        }
    }
}
