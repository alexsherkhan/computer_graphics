using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab5_L_systems
{
    public class MyPoint
    {
        public int x, y;
        public int a, b;
        public int angle;
        public Color c;
        private int width;

        public MyPoint(int x, int y, int a, int b, int angle, Color c, int width)
        {
            this.x = x;
            this.y = y;
            this.a = a;
            this.b = b;
            this.angle = angle;
            this.c = c;
            this.width = width;
        }

        public void Draw(Graphics graphics, int offsetX, int offsetY, float ScaleX, float ScaleY)
        {
            if (ScaleX == 0.0f)
            {
                ScaleX += 0.01f;
            }
            if (ScaleY == 0.0f)
            {
                ScaleY += 0.01f;
            }
            graphics.DrawLine(new Pen(c, width), (x + offsetX) / ScaleX, (y + offsetY) / ScaleY, (a + offsetX) / ScaleX, (b + offsetY) / ScaleY);
        }

        public void Draw(Graphics graphics, float offsetX, float offsetY, float ScaleX, float ScaleY)
        {
            if (ScaleX == 0.0f)
            {
                ScaleX += 0.01f;
            }
            if (ScaleY == 0.0f)
            {
                ScaleY += 0.01f;
            }
            graphics.DrawLine(new Pen(c, width), (x + offsetX) / ScaleX, (y + offsetY) / ScaleY, (a + offsetX) / ScaleX, (b + offsetY) / ScaleY);
        }

        public double Dist()
        {
            return Math.Sqrt(x * x + y * y);
        }
    }
}
