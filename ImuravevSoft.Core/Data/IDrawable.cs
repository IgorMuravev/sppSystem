using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImuravevSoft.Core.Data
{
    public interface IDrawable
    {
        void Draw(Graphics g, Func<PointF, PointF> transform);

        RectangleF GetBorder();
    }
}
