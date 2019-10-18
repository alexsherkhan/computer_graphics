using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_L_systems
{
    class Store
    {
        public int x, y;
        public int angle;
        public System.Drawing.Color color;
        public double length;
        public int width;

        public Store(int x, int y, int angle, System.Drawing.Color c, double length, int width)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.color = c;
            this.length = length;
            this.width = width;
        }
    }
}
