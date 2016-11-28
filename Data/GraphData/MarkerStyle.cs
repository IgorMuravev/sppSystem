using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImuravevSoft.GraphData
{
    [Serializable]
    public class MarkerStyle
    {
        public Color Color { get; set; }
        public int Width { get; set; }
        public double Depth { get; set; }
    }
}
