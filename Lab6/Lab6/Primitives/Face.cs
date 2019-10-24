using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;

namespace Lab6.Primitives
{
     class Face_Affines
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

            public Face(string s)
            {
                Points = new List<Point3d>();

                var arr = s.Split(' ');
                //int points_cnt = int.Parse(arr[0], CultureInfo.InvariantCulture);
                for (int i = 1; i < arr.Length; i += 3)
                {
                    if (string.IsNullOrEmpty(arr[i]))
                        continue;
                    float x = float.Parse(arr[i], CultureInfo.InvariantCulture);
                    float y = float.Parse(arr[i + 1], CultureInfo.InvariantCulture);
                    float z = float.Parse(arr[i + 2], CultureInfo.InvariantCulture);
                    Point3d p = new Point3d(x, y, z);
                    Points.Add(p);
                }
                find_center();
            }

            public string to_string()
            {
                string res = "";
                res += Points.Count.ToString(CultureInfo.InvariantCulture) + " ";
                foreach (var f in Points)
                {
                    res += f.to_string() + " ";
                }

                return res;
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

           
            public void reflectX()
            {
                Center.X = -Center.X;
                if (Points != null)
                    foreach (var p in Points)
                        p.reflectX();
            }
            public void reflectY()
            {
                Center.Y = -Center.Y;
                if (Points != null)
                    foreach (var p in Points)
                        p.reflectY();
            }
            public void reflectZ()
            {
                Center.Z = -Center.Z;
                if (Points != null)
                    foreach (var p in Points)
                        p.reflectZ();
            }

           
            public List<PointF> make_perspective(float k = 1000, float z_camera = 1000)
            {
                List<PointF> res = new List<PointF>();

                foreach (Point3d p in Points)
                {
                    res.Add(p.make_perspective(k));
                }
                return res;
            }

           
            public List<PointF> make_isometric()
            {
                List<PointF> res = new List<PointF>();

                foreach (Point3d p in Points)
                    res.Add(p.make_isometric());

                return res;
            }

            
            public List<PointF> make_orthographic(Axis a)
            {
                List<PointF> res = new List<PointF>();

                foreach (Point3d p in Points)
                    res.Add(p.make_orthographic(a));

                return res;
            }

            public void show(Graphics g, Projection pr = 0, Pen pen = null, Edge camera = null, float k = 1000)
            {
                if (pen == null)
                    pen = Pens.Black;

                List<PointF> pts;

                switch (pr)
                {
                    case Projection.ISOMETRIC:
                        pts = make_isometric();
                        break;
                    case Projection.ORTHOGR_X:
                        pts = make_orthographic(Axis.AXIS_X);
                        break;
                    case Projection.ORTHOGR_Y:
                        pts = make_orthographic(Axis.AXIS_Y);
                        break;
                    case Projection.ORTHOGR_Z:
                        pts = make_orthographic(Axis.AXIS_Z);
                        break;
                    default:
                        if (camera != null)
                            pts = make_perspective(k, camera.P1.Z);
                        else pts = make_perspective(k);
                        break;
                }

                if (pts.Count > 1)
                {
                    g.DrawLines(pen, pts.ToArray());
                    g.DrawLine(pen, pts[0], pts[pts.Count - 1]);
                }
                else if (pts.Count == 1)
                    g.DrawRectangle(pen, pts[0].X, pts[0].Y, 1, 1);
            }

            
        }
    }
}
