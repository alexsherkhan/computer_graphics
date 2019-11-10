using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
using static Lab6.Primitives.Face_Affines;

namespace Lab6.Primitives
{
    // многогранник
    public class Polyhedron
    {
        public const int MODE_POL = 0;
        public const int MODE_ROT = 1;
        public List<Face> Faces { get; set; } = null;
        public Point3d Center { get; set; } = new Point3d(0, 0, 0);
        public float Cube_size { get; set; }

        private Dictionary<Point3d, int> point_to_ind = null;

        public Polyhedron(List<Face> fs = null)
        {
            if (fs != null)
            {
                Faces = fs.Select(face => new Face(face)).ToList();
                find_center();
            }
        }

        public Polyhedron(Polyhedron polyhedron)
        {
            Faces = polyhedron.Faces.Select(face => new Face(face)).ToList();
            Center = new Point3d(polyhedron.Center);
            Cube_size = polyhedron.Cube_size;
        }

        //Load from file 
        public Polyhedron(string s, int mode = MODE_POL)
        {
            Faces = new List<Face>();
            List<Point3d> Points = new List<Point3d>();
            
            switch (mode)
            {
                case MODE_POL:
                    var arr1 = s.Split('\n');
                    //int faces_cnt = int.Parse(arr1[0], CultureInfo.InvariantCulture);
                    for (int i = 0; i < arr1.Length-1; ++i)
                    {
                        if (string.IsNullOrEmpty(arr1[i]))
                            continue;
                        if (arr1[i][0] == 'v')
                        {
                            Point3d p = new Point3d(arr1[i]);
                            Points.Add(p);
                        }

                        if (arr1[i][0] == 'f')
                        {
                            Face f = new Face(arr1[i], Points);
                            Faces.Add(f);
                        }
                    }
                    find_center();
                    break;
                case MODE_ROT:
                    break;
                default: break;
            }
        }

        public string save_obj()
        {
            string res = "";
            point_to_ind = new Dictionary<Point3d, int>(new Point3dComparer());
            int ind = 1;
            foreach (Face f in Faces)
                foreach (Point3d p in f.Points)
                    if (!point_to_ind.ContainsKey(p))
                    {
                        point_to_ind[p] = ind;
                        ++ind;
                    }

            foreach (var k in point_to_ind)
            {
                res += "v " + k.Key.to_string() + "\n";
            }

            res += "# " + point_to_ind.Count.ToString() + " vertices\n";

            foreach (Face f in Faces)
            {
                if (f.Points.Count == 3)
                    res += "f " + point_to_ind[f.Points[0]].ToString() + " " +
                                  point_to_ind[f.Points[1]].ToString() + " " +
                                  point_to_ind[f.Points[2]].ToString() + "\n";

                if (f.Points.Count == 4)
                    res += "f " + point_to_ind[f.Points[0]].ToString() + " " +
                                  point_to_ind[f.Points[1]].ToString() + " " +
                                  point_to_ind[f.Points[2]].ToString() + " " +
                                  point_to_ind[f.Points[3]].ToString() + "\n";

                if (f.Points.Count > 4)
                    res += "f " + point_to_ind[f.Points[0]].ToString() + " " +
                                  point_to_ind[f.Points[1]].ToString() + " " +
                                  point_to_ind[f.Points[2]].ToString() + " " +
                                  point_to_ind[f.Points[3]].ToString() + " " +
                                  point_to_ind[f.Points[4]].ToString() + "\n";

            }

            res += "# " + Faces.Count.ToString() + " faces";
            return res;
        }


        private void find_center()
        {
            Center.X = 0;
            Center.Y = 0;
            Center.Z = 0;
            foreach (Face f in Faces)
            {
                Center.X += f.Center.X;
                Center.Y += f.Center.Y;
                Center.Z += f.Center.Z;
            }
            Center.X /= Faces.Count;
            Center.Y /= Faces.Count;
            Center.Z /= Faces.Count;
        }

        public void show(Graphics g, Projection pr = 0, Pen pen = null)
        {
            var figure = new Polyhedron(this);
            figure.Apply(Transformation.ProjectionTransform(pr));
            foreach (Face f in figure.Faces)
                //if (f.IsVisible)
                f.show(g, pr, pen);
        }

        public void Apply(Transformation t)
        {
            foreach (var face in Faces)
                face.Apply(t);
        }
        /* ------ Figures ------- */

        public void make_hexahedron(float cube_half_size = 50)
        {
            Face f = new Face(
                new List<Point3d>
                {
                    new Point3d(-cube_half_size, cube_half_size, cube_half_size),
                    new Point3d(cube_half_size, cube_half_size, cube_half_size),
                    new Point3d(cube_half_size, -cube_half_size, cube_half_size),
                    new Point3d(-cube_half_size, -cube_half_size, cube_half_size)
                }
            );


            Faces = new List<Face> { f }; // front face


            List<Point3d> l1 = new List<Point3d>();
            // back face
            foreach (var point in f.Points)
            {
                l1.Add(new Point3d(point.X, point.Y, point.Z - 2 * cube_half_size));
            }
            Face f1 = new Face(
                    new List<Point3d>
                    {
                        new Point3d(-cube_half_size, cube_half_size, -cube_half_size),
                        new Point3d(-cube_half_size, -cube_half_size, -cube_half_size),
                        new Point3d(cube_half_size, -cube_half_size, -cube_half_size),
                        new Point3d(cube_half_size, cube_half_size, -cube_half_size)
                    });

            Faces.Add(f1);

            List<Point3d> l2 = new List<Point3d>
            {
                new Point3d(f.Points[2]),
                new Point3d(f1.Points[2]),
                new Point3d(f1.Points[1]),
                new Point3d(f.Points[3]),
            };
            Face f2 = new Face(l2);
            Faces.Add(f2);

            List<Point3d> l3 = new List<Point3d>
            {
                new Point3d(f1.Points[0]),
                new Point3d(f1.Points[3]),
                new Point3d(f.Points[1]),
                new Point3d(f.Points[0]),
            };
            Face f3 = new Face(l3);
            Faces.Add(f3);

            List<Point3d> l4 = new List<Point3d>
            {
                new Point3d(f1.Points[0]),
                new Point3d(f.Points[0]),
                new Point3d(f.Points[3]),
                new Point3d(f1.Points[1])
            };
            Face f4 = new Face(l4);
            Faces.Add(f4);

            List<Point3d> l5 = new List<Point3d>
            {
                new Point3d(f1.Points[3]),
                new Point3d(f1.Points[2]),
                new Point3d(f.Points[2]),
                new Point3d(f.Points[1])
            };
            Face f5 = new Face(l5);
            Faces.Add(f5);

            Cube_size = 2 * cube_half_size;
            find_center();
        }

        public void make_tetrahedron(Polyhedron cube = null)
        {
            if (cube == null)
            {
                cube = new Polyhedron();
                cube.make_hexahedron();
            }
            Face f0 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[0].Points[0]),
                    new Point3d(cube.Faces[1].Points[1]),
                    new Point3d(cube.Faces[1].Points[3])
                }
            );

            Face f1 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[1].Points[3]),
                    new Point3d(cube.Faces[1].Points[1]),
                    new Point3d(cube.Faces[0].Points[2])
                }
            );

            Face f2 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[0].Points[2]),
                    new Point3d(cube.Faces[1].Points[1]),
                    new Point3d(cube.Faces[0].Points[0])
                }
            );

            Face f3 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[0].Points[2]),
                    new Point3d(cube.Faces[0].Points[0]),
                    new Point3d(cube.Faces[1].Points[3])
                }
            );

            Faces = new List<Face> { f0, f1, f2, f3 };
            find_center();
        }

        public void make_octahedron(Polyhedron cube = null)
        {
            if (cube == null)
            {
                cube = new Polyhedron();
                cube.make_hexahedron();
            }

            // up
            Face f0 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[2].Center),
                    new Point3d(cube.Faces[1].Center),
                    new Point3d(cube.Faces[4].Center)
                }
            );

            Face f1 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[2].Center),
                    new Point3d(cube.Faces[1].Center),
                    new Point3d(cube.Faces[5].Center)
                }
            );

            Face f2 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[2].Center),
                    new Point3d(cube.Faces[5].Center),
                    new Point3d(cube.Faces[0].Center)
                }
            );

            Face f3 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[2].Center),
                    new Point3d(cube.Faces[0].Center),
                    new Point3d(cube.Faces[4].Center)
                }
            );

            // down
            Face f4 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[3].Center),
                    new Point3d(cube.Faces[1].Center),
                    new Point3d(cube.Faces[4].Center)
                }
            );

            Face f5 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[3].Center),
                    new Point3d(cube.Faces[1].Center),
                    new Point3d(cube.Faces[5].Center)
                }
            );

            Face f6 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[3].Center),
                    new Point3d(cube.Faces[5].Center),
                    new Point3d(cube.Faces[0].Center)
                }
            );

            Face f7 = new Face(
                new List<Point3d>
                {
                    new Point3d(cube.Faces[3].Center),
                    new Point3d(cube.Faces[0].Center),
                    new Point3d(cube.Faces[4].Center)
                }
            );

            Faces = new List<Face> { f0, f1, f2, f3, f4, f5, f6, f7 };
            find_center();
        }

        public void make_icosahedron()
        {
            Faces = new List<Face>();

            float size = 100;

            float r1 = size * (float)Math.Sqrt(3) / 4;   // половина высоты правильного треугольника - для высоты цилиндра
            float r = size * (3 + (float)Math.Sqrt(5)) / (4 * (float)Math.Sqrt(3)); // радиус вписанной сферы - для правильных пятиугольников

            Point3d up_center = new Point3d(0, -r1, 0);  // центр верхней окружности
            Point3d down_center = new Point3d(0, r1, 0); // центр нижней окружности

            // up
            double a = Math.PI / 2;
            List<Point3d> up_points = new List<Point3d>();
            for (int i = 0; i < 5; ++i)
            {
                up_points.Add(new Point3d(up_center.X + r * (float)Math.Cos(a), up_center.Y, up_center.Z - r * (float)Math.Sin(a)));
                a += 2 * Math.PI / 5;
            }

            // down
            a = Math.PI / 2 - Math.PI / 5;
            List<Point3d> down_points = new List<Point3d>();
            for (int i = 0; i < 5; ++i)
            {
                down_points.Add(new Point3d(down_center.X + r * (float)Math.Cos(a), down_center.Y, down_center.Z - r * (float)Math.Sin(a)));
                a += 2 * Math.PI / 5;
            }

            var R = Math.Sqrt(2 * (5 + Math.Sqrt(5))) * size / 4; // радиус описанной сферы - для пирамидок над цилиндром

            Point3d p_up = new Point3d(up_center.X, (float)(-R), up_center.Z);
            Point3d p_down = new Point3d(down_center.X, (float)R, down_center.Z);

            // upper faces
            for (int i = 0; i < 5; ++i)
            {
                Faces.Add(
                    new Face(new List<Point3d>
                    {
                        new Point3d(p_up),
                        new Point3d(up_points[i]),
                        new Point3d(up_points[(i+1) % 5]),
                    })
                    );
            }

            // lower faces
            for (int i = 0; i < 5; ++i)
            {
                Faces.Add(
                    new Face(new List<Point3d>
                    {
                        new Point3d(p_down),
                        new Point3d(down_points[i]),
                        new Point3d(down_points[(i+1) % 5]),
                    })
                    );
            }

            // vertical
            for (int i = 0; i < 5; ++i)
            {
                // triangle \/
                Faces.Add(
                    new Face(new List<Point3d>
                    {
                        new Point3d(up_points[i]),
                        new Point3d(up_points[(i+1) % 5]),
                        new Point3d(down_points[(i+1) % 5])
                    })
                    );

                // triangle /\
                Faces.Add(
                    new Face(new List<Point3d>
                    {
                        new Point3d(up_points[i]),
                        new Point3d(down_points[i]),
                        new Point3d(down_points[(i+1) % 5])
                    })
                    );
            }

            find_center();
        }

        public void make_dodecahedron()
        {
            Faces = new List<Face>();
            Polyhedron ik = new Polyhedron();
            ik.make_icosahedron();

            List<Point3d> pts = new List<Point3d>();
            foreach (Face f in ik.Faces)
            {
                pts.Add(f.Center);
            }

            Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[0]),
                new Point3d(pts[1]),
                new Point3d(pts[2]),
                new Point3d(pts[3]),
                new Point3d(pts[4])
            }));

            Faces.Add(new Face(new List<Point3d>
            {
                new Point3d(pts[5]),
                new Point3d(pts[6]),
                new Point3d(pts[7]),
                new Point3d(pts[8]),
                new Point3d(pts[9])
            }));

            for (int i = 0; i < 5; ++i)
            {
                Faces.Add(new Face(new List<Point3d>
                {
                    new Point3d(pts[i]),
                    new Point3d(pts[(i + 1) % 5]),
                    new Point3d(pts[(i == 4) ? 10 : 2*i + 12]),
                    new Point3d(pts[(i == 4) ? 11 : 2*i + 13]),
                    new Point3d(pts[2*i + 10])
                }));
            }

            for (int i = 5; i < 10; ++i)
            {
                int val = (i + 1 == 10) ? 5 : i + 1;
                Faces.Add(new Face(new List<Point3d>
                {
                    new Point3d(pts[i]),
                    new Point3d(pts[val]),
                    new Point3d(pts[val * 2 + 1]),
                    new Point3d(pts[val * 2 - 2]),
                    new Point3d(pts[val * 2 - 1])
                }));
            }

            find_center();
        }
    }

    public sealed class Point3dComparer : IEqualityComparer<Point3d>
    {
        public bool Equals(Point3d x, Point3d y)
        {
            return x.X.Equals(y.X) && x.Y.Equals(y.Y) && x.Z.Equals(y.Z);
        }

        public int GetHashCode(Point3d obj)
        {
            return obj.X.GetHashCode() + obj.Y.GetHashCode() + obj.Z.GetHashCode();
        }
    }

    public sealed class PointComparer : IComparer<PointF>
    {
        public int Compare(PointF p1, PointF p2)
        {
            if (p1.X.Equals(p2.X))
                return p1.Y.CompareTo(p2.Y);
            else return p1.X.CompareTo(p2.X);
        }
    }
    public sealed class ReverseFloatComparer : IComparer<float>
    {
        public int Compare(float x, float y)
        {
            return y.CompareTo(x);
        }
    }
}
