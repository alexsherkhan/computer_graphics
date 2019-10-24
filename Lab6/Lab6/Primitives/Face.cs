using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace Lab6.Primitives
{
     public class Face_Affines
    {
        public class Face
        {
            public List<Point3d> Points { get; }
            public Point3d Center { get; set; } = new Point3d(0, 0, 0);
            public List<float> Normal { get; set; }
            public bool IsVisible { get; set; }
            public Face(Face face)
            {
                Points = face.Points.Select(pt => new Point3d(pt.X, pt.Y, pt.Z)).ToList();
                Center = new Point3d(face.Center);
                if (Normal != null)
                    Normal = new List<float>(face.Normal);
                IsVisible = face.IsVisible;
            }

            public Face(List<Point3d> pts = null)
            {
                if (pts != null)
                {
                    Points = new List<Point3d>(pts);
                    find_center();
                }
            }


            private void find_center()
            {
                Center.X = 0;
                Center.Y = 0;
                Center.Z = 0;
                foreach (Point3d p in Points)
                {
                    Center.X += p.X;
                    Center.Y += p.Y;
                    Center.Z += p.Z;
                }
                Center.X /= Points.Count;
                Center.Y /= Points.Count;
                Center.Z /= Points.Count;
            }

            public void Apply(Transformation t)
            {
                foreach (var point in Points)
                    point.Apply(t);
            }
            public void show(Graphics g, Projection pr = 0, Pen pen = null, float k = 1000)
             {
                 if (pen == null)
                     pen = Pens.Black;

                 if (Points.Count > 1)
                 {
                    for (int i = 0; i < Points.Count; ++i)
                    {
                        g.DrawLine(pen, Points[i].toPointF(pr), Points[(i + 1) % Points.Count].toPointF(pr));
                    }
                 }
                 else if (Points.Count == 1)
                     g.DrawRectangle(pen, (float)Points[0].X, (float)Points[0].Y, 1, 1);
             }


        }
    }
}
