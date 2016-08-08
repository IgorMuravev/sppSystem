using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImuravevSoft.Core.Tool
{
    public partial class BaseMenu : UserControl
    {
        public string Title { get; set; }
        public BaseMenu()
        {
            InitializeComponent();
            Title = "Unnamed";
        }
        public virtual bool IsVisible
        {
            get
            {
                return false;
            }
        }
    }
}
