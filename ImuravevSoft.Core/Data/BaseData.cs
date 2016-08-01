using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImuravevSoft.Core.Data
{
    /// <summary>
    /// Базовый класс данных
    /// </summary>
    public abstract class BaseData
    {
        private Guid id;
        protected Guid typeId;
        public string Name { get; set; }
        public Guid Id
        {
            get
            {
                return id;
            }
        }
        public BaseData()
        {
            id = Guid.NewGuid();

        }

        public abstract void Save(BinaryWriter writer);
        public abstract void Load(BinaryReader reader);
    }
}
