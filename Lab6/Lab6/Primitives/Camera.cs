using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Primitives
{
    public class Camera
    {
        public Edge view = new Edge(new Point3d(0, 0, 300), new Point3d(0, 0, 250));
        Polyhedron small_cube = new Polyhedron();
        public Edge rot_line { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Camera(int w, int h)
        {
            int camera_half_size = 5;
            small_cube.make_hexahedron(camera_half_size);
            small_cube.Apply(Transformation.Translate(view.P1.X, view.P1.Y, view.P1.Z));
            set_rot_line();
            Width = w;
            Height = h;
        }

        public void set_rot_line(Axis a = Axis.AXIS_X)
        {
            Point3d p1, p2;
            p1 = new Point3d(view.P1);
            switch (a)
            {
                case Axis.AXIS_Y:
                    p2 = new Point3d(p1.X, p1.Y + 10, p1.Z);
                    break;
                case Axis.AXIS_Z:
                    p2 = new Point3d(p1.X, p1.Y, p1.Z + 10);
                    break;
                default:
                    p2 = new Point3d(p1.X + 10, p1.Y, p1.Z);
                    break;
            }
            rot_line = new Edge(p1, p2);
        }

        public void show(Graphics g, Projection pr = 0, int x = 0, int y = 0, int z = 0, Pen pen = null)
        {
            pen = Pens.Red;

            small_cube.Apply(Transformation.Translate(x, y, z));
            small_cube.show(g, pr, pen);
            small_cube.Apply(Transformation.Translate(- x, -y, -z));
        }

        public void translate(float x, float y, float z)
        {
            view.Apply(Transformation.Translate(x, y, z));
            small_cube.Apply(Transformation.Translate(x, y, z));
            rot_line.Apply(Transformation.Translate(x, y, z));
        }


    }
}
