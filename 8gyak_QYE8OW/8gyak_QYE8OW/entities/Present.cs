using _8gyak_QYE8OW.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8gyak_QYE8OW.entities
{
    public class Present : Toy
    {
        public SolidBrush Ribbon { get; private set; }
        public SolidBrush Box { get; private set; }

        public Present(Color ribbon, Color box)
        {
            Ribbon = new SolidBrush(ribbon);
            Box = new SolidBrush(box);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(Box, 0, 0, 50, 50);
            g.FillRectangle(Ribbon, 20, 0, 10, 50);
            g.FillRectangle(Ribbon, 0, 20, 50, 10);
        }
    }
}
