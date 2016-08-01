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
       

        public abstract void Save(BinaryWriter writer);
        public abstract void Load(BinaryReader reader);
    }
}
