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

        private Polyhedron camera_figure = new Polyhedron();
        private Point3d coords = new Point3d(0, 0, 100);
        private List<Edge> lines = new List < Edge >{new Edge(new Point3d(0, 0, 0), new Point3d(100, 0, 0)),
        new Edge(new Point3d(0, 0, 0), new Point3d(0, 100, 0)),
        new Edge(new Point3d(0, 0, 0), new Point3d(0, 0, 100))
        };

        public CameraMode mode = CameraMode.Simple;

        public Camera(Polyhedron polyhedron)
        {
            camera_figure = polyhedron;
        }

        private void show_lines(Graphics g)
        {
            List<Edge> perspective_lines = new List<Edge>();
            foreach (var edge in lines)
            {
                Edge perspective_edge = new Edge(edge);
                perspective_edge.Apply(Transformation.ProjectionTransform(Projection.PERSPECTIVE));
                perspective_lines.Add(perspective_edge);
            }
                
            g.DrawLine(Pens.Blue, perspective_lines[0].P1.toPointF(Projection.PERSPECTIVE), perspective_lines[0].P2.toPointF(Projection.PERSPECTIVE));
            g.DrawLine(Pens.Red, perspective_lines[1].P1.toPointF(Projection.PERSPECTIVE), perspective_lines[1].P2.toPointF(Projection.PERSPECTIVE));
            g.DrawLine(Pens.Green, perspective_lines[2].P1.toPointF(Projection.PERSPECTIVE), perspective_lines[2].P2.toPointF(Projection.PERSPECTIVE));
        }
        public void show(Graphics g, Pen pen = null, bool normal = false)
        {
            switch (mode)
            {
                case CameraMode.Simple:
                    show_lines(g);
                    camera_figure.show(g, Projection.PERSPECTIVE, pen);
                    break;
                case CameraMode.Clipping:
                    camera_figure.show(g, Projection.PERSPECTIVE, pen, true, coords);
                    break;
                case CameraMode.Buffer:
                    break;
            }

        }

        public void fiqureApply(Transformation t)
        {
            camera_figure.Apply(t);
        }

        public void Apply(Transformation t)
        {
            Transformation transformation = Transformation.Translate(-coords.X, -coords.Y, -coords.Z);
            transformation *= t;
            transformation *= Transformation.Translate(coords.X, coords.Y, coords.Z);
            camera_figure.Apply(transformation);
            foreach (var edge in lines)
                edge.Apply(transformation);
        }

        internal void setMode(CameraMode mode)
        {
            this.mode = mode;
        }
    }
}
