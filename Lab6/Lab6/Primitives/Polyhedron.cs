using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
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

        private Dictionary<Point3d, List<int>> map = null;
        private Dictionary<Point3d, List<double>> point_to_normal = null;
        private Dictionary<Point3d, double> point_to_intensive = null;

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
                    var arr2 = s.Split('\n');
                    int cnt_breaks = int.Parse(arr2[0], CultureInfo.InvariantCulture);
                    Edge rot_line = new Edge(arr2[1]);
                    int cnt_points = int.Parse(arr2[2], CultureInfo.InvariantCulture);
                    var arr3 = arr2[3].Split(' ');
                    List<Point3d> pts = new List<Point3d>();
                    for (int i = 0; i < 3 * cnt_points; i += 3)
                        pts.Add(new Point3d(
                            float.Parse(arr3[i], CultureInfo.InvariantCulture),
                            float.Parse(arr3[i + 1], CultureInfo.InvariantCulture),
                            float.Parse(arr3[i + 2], CultureInfo.InvariantCulture)));
                   make_rotation_figure(cnt_breaks, rot_line, pts);

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

        public void show(Graphics g, Projection pr = 0, Pen pen = null, bool normal = false, Point3d camera = null)
        {
            var figure = new Polyhedron(this);
            figure.Apply(Transformation.ProjectionTransform(pr));
            foreach (Face f in figure.Faces)
            {
                if(normal)
                    if(camera == null)
                        f.find_normal(this.Center);
                    else
                        f.find_normal(this.Center, camera);
                if (f.IsVisible)
                    f.show(g, pr, pen);
            }

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
                    new Point3d(pts[i * 2]),
                    new Point3d(pts[i * 2 + 1])
                }));
            }

            find_center();
        }

        private void make_rotation_figure(int cnt_breaks, Edge rot_line, List<Point3d> pts)
        {
             double angle = 360.0 / cnt_breaks;
             angle = angle / 180.0 * Math.PI;

            double Ax = rot_line.P1.X, Ay = rot_line.P1.Y, Az = rot_line.P1.Z;

            foreach (var p in pts)
                p.Apply(Transformation.Translate(-Ax, -Ay, -Az));

            List<Point3d> new_pts = new List<Point3d>();
            foreach (var p in pts)
                new_pts.Add(new Point3d(p.X, p.Y, p.Z));


            for (int i = 0; i < cnt_breaks; ++i)
            {
                foreach (var np in new_pts)
                    np.Apply(Transformation.RotateY(angle));
                for (int j = 1; j < pts.Count; ++j)
                {
                    Face f = new Face(new List<Point3d>(){ new Point3d(pts[j - 1]), new Point3d(new_pts[j - 1]),
                        new Point3d(new_pts[j]), new Point3d(pts[j])});
                    Faces.Add(f);
                }
                foreach (var p in pts)
                    p.Apply(Transformation.RotateY(angle));
            }

            find_center();
        }

        private int[] Interpolate(int i0, int d0, int i1, int d1)
        {
            if (i0 == i1)
            {
                return new int[] { d0 };
            }
            int[] values = new int[i1 - i0 + 1];
            float a = (float)(d1 - d0) / (i1 - i0);
            float d = d0;
            int ind = 0;
            for (int i = i0; i <= i1; ++i)
            {
                values[ind] = (int)(d + 0.5);
                d = d + a;
                ++ind;
            }
            return values;
        }

        private void DrawFilledTriangle(Edge camera, Point3d P0, Point3d P1, Point3d P2, int[] buff, int width, int height, int[] colors, int color)
        {
            PointF p0 = P0.toPointF(0);
            PointF p1 = P1.toPointF(0);
            PointF p2 = P2.toPointF(0);

            // y0 <= y1 <= y2
            int y0 = (int)p0.Y; int x0 = (int)p0.X; int z0 = (int)P0.Z;
            int y1 = (int)p1.Y; int x1 = (int)p1.X; int z1 = (int)P1.Z;
            int y2 = (int)p2.Y; int x2 = (int)p2.X; int z2 = (int)P2.Z;

            var x01 = Interpolate(y0, x0, y1, x1);
            var x12 = Interpolate(y1, x1, y2, x2);
            var x02 = Interpolate(y0, x0, y2, x2);

            var h01 = Interpolate(y0, z0, y1, z1);
            var h12 = Interpolate(y1, z1, y2, z2);
            var h02 = Interpolate(y0, z0, y2, z2);

            // Конкатенация коротких сторон
            int[] x012 = x01.Take(x01.Length - 1).Concat(x12).ToArray();
            int[] h012 = h01.Take(h01.Length - 1).Concat(h12).ToArray();

            // Определяем, какая из сторон левая и правая
            int m = x012.Length / 2;
            int[] x_left, x_right, h_left, h_right;
            if (x02[m] < x012[m])
            {
                x_left = x02;
                x_right = x012;

                h_left = h02;
                h_right = h012;
            }
            else
            {
                x_left = x012;
                x_right = x02;


                h_left = h012;
                h_right = h02;
            }

            Face f = new Face(new List<Point3d>() { P0, P1, P2 });

            // Отрисовка горизонтальных отрезков
            for (int y = y0; y <= y2; ++y)
            {
                int x_l = x_left[y - y0];
                int x_r = x_right[y - y0];
                int[] h_segment;

                // interpolation
                if (x_l > x_r)
                {
                    continue;
                    //h_segment = Interpolate(x_r, h_left[y - y0], x_l, h_right[y - y0]); // костыль
                }
                else
                    h_segment = Interpolate(x_l, h_left[y - y0], x_r, h_right[y - y0]);
                for (int x = x_l; x <= x_r; ++x)
                {
                    int z = h_segment[x - x_l];
                    // i, j, z - координаты в пространстве, в пикчербоксе x, y
                    //int xx = (x + width / 2) % width;
                    //int yy = (-y + height / 2) % height;
                    int xx = x + width / 2;
                    int yy = -y + height / 2;
                    if (xx < 0 || xx > width || yy < 0 || yy > height || (xx * height + yy) < 0 || (xx * height + yy) > (buff.Length - 1))
                        continue;
                    if (z > buff[xx * height + yy])
                    {
                        buff[xx * height + yy] = (int)(z + 0.5);
                        colors[xx * height + yy] = color;
                    }
                }
            }
        }

        private void magic(Edge camera, Point3d P0, Point3d P1, Point3d P2, int[] buff, int width, int height, int[] colors, int color)
        {
            // сортируем p0, p1, p2: y0 <= y1 <= y2
            PointF p0 = P0.toPointF(0);
            PointF p1 = P1.toPointF(0);
            PointF p2 = P2.toPointF(0);

            if (p1.Y < p0.Y)
            {
                Point3d tmpp = new Point3d(P0);
                P0.X = P1.X; P0.Y = P1.Y; P0.Z = P1.Z;
                P1.X = tmpp.X; P1.Y = tmpp.Y; P1.Z = tmpp.Z;
                PointF tmppp = new PointF(p0.X, p0.Y);
                p0.X = p1.X; p0.Y = p1.Y;
                p1.X = tmppp.X; p1.Y = tmppp.Y;
            }
            if (p2.Y < p0.Y)
            {
                Point3d tmpp = new Point3d(P0);
                P0.X = P2.X; P0.Y = P2.Y; P0.Z = P2.Z;
                P2.X = tmpp.X; P2.Y = tmpp.Y; P2.Z = tmpp.Z;
                PointF tmppp = new PointF(p0.X, p0.Y);
                p0.X = p2.X; p0.Y = p2.Y;
                p2.X = tmppp.X; p2.Y = tmppp.Y;
            }
            if (p2.Y < p1.Y)
            {
                Point3d tmpp = new Point3d(P1);
                P1.X = P2.X; P1.Y = P2.Y; P1.Z = P2.Z;
                P2.X = tmpp.X; P2.Y = tmpp.Y; P2.Z = tmpp.Z;
                PointF tmppp = new PointF(p1.X, p1.Y);
                p1.X = p2.X; p1.Y = p2.Y;
                p2.X = tmppp.X; p2.Y = tmppp.Y;
            }

            DrawFilledTriangle(camera, P0, P1, P2, buff, width, height, colors, color);
        }


        public void calc_z_buff(Edge camera, int width, int height, out int[] buf, out int[] colors)
        {
            buf = new int[width * height];
            for (int i = 0; i < width * height; ++i)
                buf[i] = int.MinValue;
            colors = new int[width * height];
            for (int i = 0; i < width * height; ++i)
                colors[i] = 255;

            Random r = new Random();
            int color = 0;
            foreach (var f in Faces)
            {

                color = (color + 30) % 255;
                // треугольник
                Point3d P0 = new Point3d(f.Points[0]);
                Point3d P1 = new Point3d(f.Points[1]);
                Point3d P2 = new Point3d(f.Points[2]);
                magic(camera, P0, P1, P2, buf, width, height, colors, color);
                // 4
                if (f.Points.Count > 3)
                {
                    P0 = new Point3d(f.Points[2]);
                    P1 = new Point3d(f.Points[3]);
                    P2 = new Point3d(f.Points[0]);
                    magic(camera, P0, P1, P2, buf, width, height, colors, color);
                }
                // 5  убейте додекаэдр,пожалуйста
                if (f.Points.Count > 4)
                {
                    P0 = new Point3d(f.Points[3]);
                    P1 = new Point3d(f.Points[4]);
                    P2 = new Point3d(f.Points[0]);
                    magic(camera, P0, P1, P2, buf, width, height, colors, color);
                }
            }

            int min_v = int.MaxValue;
            int max_v = 0;
            for (int i = 0; i < width * height; ++i)
            {
                if (buf[i] != int.MinValue && buf[i] < min_v)
                    min_v = buf[i];
                if (buf[i] > max_v)
                    max_v = buf[i];
            }
            if (min_v < 0)
            {
                min_v = -min_v;
                max_v += min_v;
                for (int i = 0; i < width * height; ++i)
                    if (buf[i] != int.MinValue)
                        buf[i] = (buf[i] + min_v) % int.MaxValue;
            }
            for (int i = 0; i < width * height; ++i)
                if (buf[i] == int.MinValue)
                    buf[i] = 255;
                else if (max_v != 0) buf[i] = buf[i] * 225 / max_v;

        }

        private void create_map(Point3d camera, Point3d light)
        {
            map = new Dictionary<Point3d, List<int>>(new Point3dComparer());
            point_to_normal = new Dictionary<Point3d, List<double>>(new Point3dComparer());
            point_to_intensive = new Dictionary<Point3d, double>(new Point3dComparer());
            for (int i = 0; i < Faces.Count; ++i)
            {
                Faces[i].find_normal(Center, camera);
                var n = Faces[i].Normal;
                foreach (var p in Faces[i].Points)
                {
                    if (!map.ContainsKey(p))
                        map[p] = new List<int>();
                    map[p].Add(i);
                    if (!point_to_normal.ContainsKey(p))
                        point_to_normal[p] = new List<double>() { 0, 0, 0 };
                    point_to_normal[p][0] += n[0];
                    point_to_normal[p][1] += n[1];
                    point_to_normal[p][2] += n[2];
                }
            }
            double max = 0;
            foreach (var el in map)
            {
                var p = el.Key;
                var lenght = (float)Math.Sqrt(point_to_normal[p][0] * point_to_normal[p][0] + point_to_normal[p][1] * point_to_normal[p][1] + point_to_normal[p][2] * point_to_normal[p][2]);
                point_to_normal[p][0] /= lenght;
                point_to_normal[p][1] /= lenght;
                point_to_normal[p][2] /= lenght;

                List<double> to_light = new List<double>() { -light.X + p.X, -light.Y + p.Y, -light.Z + p.Z };
                lenght = (float)Math.Sqrt(to_light[0] * to_light[0] + to_light[1] * to_light[1] + to_light[2] * to_light[2]);
                to_light[0] /= lenght; to_light[1] /= lenght; to_light[2] /= lenght;

                //ka - свойство материала воспринимать фоновое освещение, ia - мощность фонового освещения
                double ka = 1; double ia = 0.7f;
                double Ia = ka * ia;
                //kd - свойство материала воспринимать рассеянное освещение, id - мощность рассеянного освещения
                double kd = 0.7f; double id = 1f;
                double Id = kd * id * (point_to_normal[p][0] * to_light[0] + point_to_normal[p][1] * to_light[1] + point_to_normal[p][2] * to_light[2]);
                point_to_intensive[p] = Ia + Id;
                if (point_to_intensive[p] > max)
                    max = point_to_intensive[p];
            }
            //может ли быть больше 1?
            if (max != 0)
                foreach (var el in point_to_normal)
                {
                    point_to_intensive[el.Key] /= max;
                    if (point_to_intensive[el.Key] < 0)
                        point_to_intensive[el.Key] = 0;
                }

        }

        private void G_DrawFilledTriangle(Point3d camera, Point3d P0, Point3d P1, Point3d P2, int[] buff, int width, int height, float[] colors, double c_P0, double c_P1, double c_P2)
        {
            PointF p0 = P0.toPointF(Projection.PERSPECTIVE);
            PointF p1 = P1.toPointF(Projection.PERSPECTIVE);
            PointF p2 = P2.toPointF(Projection.PERSPECTIVE);

            //y0 <= y1 <= y2
            int y0 = (int)p0.Y; int x0 = (int)p0.X; int z0 = (int)P0.Z;
            int y1 = (int)p1.Y; int x1 = (int)p1.X; int z1 = (int)P1.Z;
            int y2 = (int)p2.Y; int x2 = (int)p2.X; int z2 = (int)P2.Z;

            var x01 = Interpolate(y0, x0, y1, x1);
            var x12 = Interpolate(y1, x1, y2, x2);
            var x02 = Interpolate(y0, x0, y2, x2);

            var h01 = Interpolate(y0, z0, y1, z1);
            var h12 = Interpolate(y1, z1, y2, z2);
            var h02 = Interpolate(y0, z0, y2, z2);

            var c01 = Interpolate(y0, (int)(c_P0 * 100), y1, (int)(c_P1 * 100));
            var c12 = Interpolate(y1, (int)(c_P1 * 100), y2, (int)(c_P2 * 100));
            var c02 = Interpolate(y0, (int)(c_P0 * 100), y2, (int)(c_P2 * 100));
            // Конкатенация коротких сторон
            int[] x012 = x01.Take(x01.Length - 1).Concat(x12).ToArray();
            int[] h012 = h01.Take(h01.Length - 1).Concat(h12).ToArray();
            int[] c012 = c01.Take(c01.Length - 1).Concat(c12).ToArray();

            // Определяем, какая из сторон левая и правая
            int m = x012.Length / 2;
            int[] x_left, x_right, h_left, h_right, c_left, c_right;
            if (x02[m] < x012[m])
            {
                x_left = x02;
                x_right = x012;

                h_left = h02;
                h_right = h012;

                c_left = c02;
                c_right = c012;
            }
            else
            {
                x_left = x012;
                x_right = x02;

                h_left = h012;
                h_right = h02;

                c_left = c012;
                c_right = c02;
            }

            // Отрисовка горизонтальных отрезков
            for (int y = y0; y <= y2; ++y)
            {
                int x_l = x_left[y - y0];
                int x_r = x_right[y - y0];
                int[] h_segment;
                int[] c_segment;
                // interpolation
                if (x_l > x_r)
                    continue;
                h_segment = Interpolate(x_l, h_left[y - y0], x_r, h_right[y - y0]);
                c_segment = Interpolate(x_l, c_left[y - y0], x_r, c_right[y - y0]);
                for (int x = x_l; x <= x_r; ++x)
                {
                    int z = h_segment[x - x_l];
                    float color = c_segment[x - x_l] / 100f;
                    // i, j, z - координаты в пространстве, в пикчербоксе x, y
                    //int xx = (x + width / 2) % width;
                    //int yy = (-y + height / 2) % height;
                    int xx = x + width / 2;
                    int yy = -y + height / 2;
                    if (xx < 0 || xx > width || yy < 0 || yy > height || (xx * height + yy) < 0 || (xx * height + yy) > (buff.Length - 1))
                        continue;
                    if (z > buff[xx * height + yy])
                    {
                        buff[xx * height + yy] = (int)(z + 0.5);
                        colors[xx * height + yy] = color;
                    }
                }
            }
        }

        private void G_magic(Point3d camera, Point3d P0, Point3d P1, Point3d P2, int[] buff, int width, int height, float[] colors, double c_P0, double c_P1, double c_P2)
        {
            // сортируем p0, p1, p2: y0 <= y1 <= y2
            PointF p0 = P0.toPointF(Projection.PERSPECTIVE);
            PointF p1 = P1.toPointF(Projection.PERSPECTIVE);
            PointF p2 = P2.toPointF(Projection.PERSPECTIVE);

            if (p1.Y < p0.Y)
            {
                Point3d tmpp = new Point3d(P0);
                P0.X = P1.X; P0.Y = P1.Y; P0.Z = P1.Z;
                P1.X = tmpp.X; P1.Y = tmpp.Y; P1.Z = tmpp.Z;
                PointF tmppp = new PointF(p0.X, p0.Y);
                p0.X = p1.X; p0.Y = p1.Y;
                p1.X = tmppp.X; p1.Y = tmppp.Y;
                var tmpc = c_P1;
                c_P1 = c_P0;
                c_P0 = tmpc;
            }
            if (p2.Y < p0.Y)
            {
                Point3d tmpp = new Point3d(P0);
                P0.X = P2.X; P0.Y = P2.Y; P0.Z = P2.Z;
                P2.X = tmpp.X; P2.Y = tmpp.Y; P2.Z = tmpp.Z;
                PointF tmppp = new PointF(p0.X, p0.Y);
                p0.X = p2.X; p0.Y = p2.Y;
                p2.X = tmppp.X; p2.Y = tmppp.Y;
                var tmpc = c_P2;
                c_P2 = c_P0;
                c_P0 = tmpc;
            }
            if (p2.Y < p1.Y)
            {
                Point3d tmpp = new Point3d(P1);
                P1.X = P2.X; P1.Y = P2.Y; P1.Z = P2.Z;
                P2.X = tmpp.X; P2.Y = tmpp.Y; P2.Z = tmpp.Z;
                PointF tmppp = new PointF(p1.X, p1.Y);
                p1.X = p2.X; p1.Y = p2.Y;
                p2.X = tmppp.X; p2.Y = tmppp.Y;
                var tmpc = c_P1;
                c_P1 = c_P2;
                c_P2 = tmpc;
            }

            G_DrawFilledTriangle(camera, P0, P1, P2, buff, width, height, colors, c_P0, c_P1, c_P2);
        }

        public void calc_gouraud(Point3d camera, int width, int height, out float[] intensive, Point3d light)
        {
            int[] buf = new int[width * height];
            for (int i = 0; i < width * height; ++i)
                buf[i] = int.MinValue;
            intensive = new float[width * height];
            for (int i = 0; i < width * height; ++i)
                intensive[i] = 0;

            create_map(camera, light);
            foreach (var f in Faces)
            {
                // треугольник
                Point3d P0 = new Point3d(f.Points[0]);
                Point3d P1 = new Point3d(f.Points[1]);
                Point3d P2 = new Point3d(f.Points[2]);
                double i_p0 = point_to_intensive[P0], i_p1 = point_to_intensive[P1], i_p2 = point_to_intensive[P2];
                G_magic(camera, P0, P1, P2, buf, width, height, intensive, i_p0, i_p1, i_p2);
                // 4
                if (f.Points.Count > 3)
                {
                    P0 = new Point3d(f.Points[2]);
                    P1 = new Point3d(f.Points[3]);
                    P2 = new Point3d(f.Points[0]);
                    i_p0 = point_to_intensive[P0]; i_p1 = point_to_intensive[P1]; i_p2 = point_to_intensive[P2];
                    G_magic(camera, P0, P1, P2, buf, width, height, intensive, i_p0, i_p1, i_p2);
                }
                // 5
                if (f.Points.Count > 4)
                {
                    P0 = new Point3d(f.Points[3]);
                    P1 = new Point3d(f.Points[4]);
                    P2 = new Point3d(f.Points[0]);
                    i_p0 = point_to_intensive[P0]; i_p1 = point_to_intensive[P1]; i_p2 = point_to_intensive[P2];
                    G_magic(camera, P0, P1, P2, buf, width, height, intensive, i_p0, i_p1, i_p2);
                }
            }

            SortedSet<float> test = new SortedSet<float>();
            for (int i = 0; i < width * height; ++i)
                test.Add(intensive[i]);

           // int max = 0;
        }


        /// <summary>
        /// Linear interpolation from i0 to i1
        /// </summary>
        /// <param name="i0">First independent variable</param>
        /// <param name="d0">First dependent variable</param>
        /// <param name="i1">Last independent variable</param>
        /// <param name="d1">Last dependent variable</param>
        private static double[] Interpolate(int i0, double d0, int i1, double d1)
        {
            if (i0 == i1)
                return new double[] { d0 };
            double[] values = new double[i1 - i0 + 1];
            double a = (d1 - d0) / (i1 - i0);
            double d = d0;

            int ind = 0;
            for (int i = i0; i <= i1; ++i)
            {
                values[ind++] = d;
                d += a;
            }

            return values;
        }


        /// <summary>
        /// Сортировка вершин треугольника по Y от min до max
        /// </summary>
        private static Point3d[] SortTriangleVertices(Point3d P0, Point3d P1, Point3d P2)
        {
            Point3d p0 = new Point3d(P0)
            {

                X = P0.toPointF(Projection.PERSPECTIVE).X,
                Y = P0.toPointF(Projection.PERSPECTIVE).Y
            },
            p1 = new Point3d(P1)
            {
                X = P1.toPointF(Projection.PERSPECTIVE).X,
                Y = P1.toPointF(Projection.PERSPECTIVE).Y
            },
            p2 = new Point3d(P2)
            {
                X = P2.toPointF(Projection.PERSPECTIVE).X,
                Y = P2.toPointF(Projection.PERSPECTIVE).Y
            };
            Point3d[] points = new Point3d[3] { p0, p1, p2 };

            if (points[1].Y < points[0].Y)
            {
                points[0] = p1;
                points[1] = p0;
            }
            if (points[2].Y < points[0].Y)
            {
                points[2] = points[0];
                points[0] = p2;
            }
            if (points[2].Y < points[1].Y)
            {
                Point3d temp = points[1];
                points[1] = points[2];
                points[2] = temp;
            }

            return points;
        }

        private static void DrawTexture(Point3d P0, Point3d P1, Point3d P2, Bitmap bmp, BitmapData bmpData, byte[] rgbValues, Bitmap texture, BitmapData bmpDataTexture, byte[] rgbValuesTexture)
        {
            // Отсортируйте точки так, чтобы y0 <= y1 <= y2
            var points = SortTriangleVertices(P0, P1, P2);
            Point3d SortedP0 = points[0], SortedP1 = points[1], SortedP2 = points[2];

            // Вычислите координаты x и U, V текстурных координат ребер треугольника
            var x01 = Interpolate((int)SortedP0.Y, SortedP0.X, (int)SortedP1.Y, SortedP1.X);
            var u01 = Interpolate((int)SortedP0.Y, SortedP0.TextureCoordinates.X, (int)SortedP1.Y, SortedP1.TextureCoordinates.X);
            var v01 = Interpolate((int)SortedP0.Y, SortedP0.TextureCoordinates.Y, (int)SortedP1.Y, SortedP1.TextureCoordinates.Y);
            var x12 = Interpolate((int)SortedP1.Y, SortedP1.X, (int)SortedP2.Y, SortedP2.X);
            var u12 = Interpolate((int)SortedP1.Y, SortedP1.TextureCoordinates.X, (int)SortedP2.Y, SortedP2.TextureCoordinates.X);
            var v12 = Interpolate((int)SortedP1.Y, SortedP1.TextureCoordinates.Y, (int)SortedP2.Y, SortedP2.TextureCoordinates.Y);
            var x02 = Interpolate((int)SortedP0.Y, SortedP0.X, (int)SortedP2.Y, SortedP2.X);
            var u02 = Interpolate((int)SortedP0.Y, SortedP0.TextureCoordinates.X, (int)SortedP2.Y, SortedP2.TextureCoordinates.X);
            var v02 = Interpolate((int)SortedP0.Y, SortedP0.TextureCoordinates.Y, (int)SortedP2.Y, SortedP2.TextureCoordinates.Y);

            // Concatenate the short sides
            x01 = x01.Take(x01.Length - 1).ToArray(); // remove last element, it's the first in x12
            var x012 = x01.Concat(x12).ToArray();
            u01 = u01.Take(u01.Length - 1).ToArray(); // remove last element, it's the first in u12
            var u012 = u01.Concat(u12).ToArray();
            v01 = v01.Take(v01.Length - 1).ToArray(); // remove last element, it's the first in v12
            var v012 = v01.Concat(v12).ToArray();

            // Determine which is left and which is right
            int m = x012.Length / 2;
            double[] x_left, x_right, u_left, u_right, v_left, v_right;
            if (x02[m] < x012[m])
            {
                x_left = x02;
                x_right = x012;
                u_left = u02;
                u_right = u012;
                v_left = v02;
                v_right = v012;
            }
            else
            {
                x_left = x012;
                x_right = x02;
                u_left = u012;
                u_right = u02;
                v_left = v012;
                v_right = v02;
            }

            // Рисует горизонтальные сегменты
            for (int y = (int)SortedP0.Y; y < (int)SortedP2.Y; ++y)
            {
                int screen_y = -y + bmp.Height / 2;
                if (screen_y < 0)
                    break;
                if (bmp.Height <= screen_y)
                    continue;

                var x_l = x_left[y - (int)SortedP0.Y];
                var x_r = x_right[y - (int)SortedP0.Y];

                var u_segment = Interpolate((int)x_l, u_left[y - (int)SortedP0.Y], (int)x_r, u_right[y - (int)SortedP0.Y]);
                var v_segment = Interpolate((int)x_l, v_left[y - (int)SortedP0.Y], (int)x_r, v_right[y - (int)SortedP0.Y]);
                for (int x = (int)x_l; x < (int)x_r; ++x)
                {
                    int screen_x = x + bmp.Width / 2;
                    if (screen_x < 0)
                        continue;
                    if (bmp.Width <= screen_x)
                        break;

                    int texture_u = (int)(u_segment[x - (int)x_l] * (texture.Width - 1));
                    int texture_v = (int)(v_segment[x - (int)x_l] * (texture.Height - 1));

                    rgbValues[screen_x * 3 + screen_y * bmpData.Stride] = rgbValuesTexture[texture_u * 3 + texture_v * bmpDataTexture.Stride];
                    rgbValues[screen_x * 3 + 1 + screen_y * bmpData.Stride] = rgbValuesTexture[texture_u * 3 + 1 + texture_v * bmpDataTexture.Stride];
                    rgbValues[screen_x * 3 + 2 + screen_y * bmpData.Stride] = rgbValuesTexture[texture_u * 3 + 2 + texture_v * bmpDataTexture.Stride];
                }
            }
        }

        public void ApplyTexture(Bitmap bmp, BitmapData bmpData, byte[] rgbValues, Bitmap texture, BitmapData bmpDataTexture, byte[] rgbValuesTexture)
        {
            foreach (var f in Faces)
            {
                f.find_normal(Center);
                if (!f.IsVisible)
                    continue;

                // 3 vertices
                Point3d P0 = new Point3d(f.Points[0]);
                Point3d P1 = new Point3d(f.Points[1]);
                Point3d P2 = new Point3d(f.Points[2]);
                DrawTexture(P0, P1, P2, bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);

                // 4 vertices
                if (f.Points.Count == 4)
                {
                    P0 = new Point3d(f.Points[2]);
                    P1 = new Point3d(f.Points[3]);
                    P2 = new Point3d(f.Points[0]);
                    DrawTexture(P0, P1, P2, bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
                }
            }
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
