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
            public List<double> Normal { get; set; }
            public bool IsVisible { get; set; } = true;
            public Face(Face face)
            {
                Points = face.Points.Select(pt => new Point3d(pt.X, pt.Y, pt.Z, pt.TextureCoordinates)).ToList();
                Center = new Point3d(face.Center);
                if (Normal != null)
                    Normal = new List<double>(face.Normal);
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

            //Load from file
            public Face(string s, List<Point3d> pp)
            {
                Points = new List<Point3d>();

                var arr = s.Split(' ');
                //int points_cnt = int.Parse(arr[0], CultureInfo.InvariantCulture);
                for (int i = 1; i < arr.Length; ++i)
                {
                    if (string.IsNullOrEmpty(arr[i]))
                        continue;
                    Point3d p = pp[int.Parse(arr[i], CultureInfo.InvariantCulture)-1]; 
                    Points.Add(p);
                }
                find_center();
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
                   // pen = Pens.Red;
                   // this.find_center();
                   // this.find_normal(new Point3d(0, 0, 0));
                   // List<double> CQ = new List<double> { Points[1].X - 0, Points[1].Y - 0, Points[1].Z - 0};
                   // g.DrawLine(pen, new Point3d((float)Center.X, (float)Center.Y, (float)Center.Z).toPointF(pr), new Point3d((float)Normal[0], (float)Normal[1], (float)Normal[2]).toPointF(pr));
                }
                 else if (Points.Count == 1)
                     g.DrawRectangle(pen, (float)Points[0].X, (float)Points[0].Y, 1, 1);

             }

            public void find_normal(Point3d p_center, Point3d camera = null)
            {
                Point3d Q = Points[1], R = Points[2], S = Points[0];
                List<double> QR = new List<double> { R.X - Q.X, R.Y - Q.Y, R.Z - Q.Z };
                List<double> QS = new List<double> { S.X - Q.X, S.Y - Q.Y, S.Z - Q.Z };


                Normal = new List<double> { QR[1] * QS[2] - QR[2] * QS[1],
                                       -(QR[0] * QS[2] - QR[2] * QS[0]),
                                       QR[0] * QS[1] - QR[1] * QS[0] }; 

                List<double> CQ = new List<double> { Q.X - p_center.X, Q.Y - p_center.Y, Q.Z - p_center.Z };
                if (Point3d.mul_matrix(Normal, 1, 3, CQ, 3, 1)[0] > 1E-6)
                {
                    Normal[0] *= -1;
                    Normal[1] *= -1;
                    Normal[2] *= -1;
                }

                Point3d E = new Point3d(0, 0, 100);
                
                if (camera != null)
                E = camera;
                

                List<double> CE = new List<double> { E.X - Center.X, E.Y - Center.Y, E.Z - Center.Z };

                double dot_product = Point3d.mul_matrix(Normal, 1, 3, CE, 3, 1)[0];
                IsVisible = Math.Abs(dot_product) < 1E-6 || dot_product < 0;


            }

        }

        

    }
}
