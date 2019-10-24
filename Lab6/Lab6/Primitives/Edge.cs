using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace Lab6.Primitives
{
    public class Edge
    {
        public Point3d P1 { get; set; }
        public Point3d P2 { get; set; }

        public Edge(Point3d pt1, Point3d pt2)
        {
            P1 = new Point3d(pt1);
            P2 = new Point3d(pt2);
        }

        public Edge(string s)
        {
            var arr = s.Split(' ');
            P1 = new Point3d(float.Parse(arr[0], CultureInfo.InvariantCulture),
                float.Parse(arr[1], CultureInfo.InvariantCulture),
                float.Parse(arr[2], CultureInfo.InvariantCulture));
            P2 = new Point3d(float.Parse(arr[3], CultureInfo.InvariantCulture),
                float.Parse(arr[4], CultureInfo.InvariantCulture),
                float.Parse(arr[5], CultureInfo.InvariantCulture));
        }
        public void Apply(Transformation t)
        {
            P1.Apply(t);
            P2.Apply(t);
        }

       
    }


}
