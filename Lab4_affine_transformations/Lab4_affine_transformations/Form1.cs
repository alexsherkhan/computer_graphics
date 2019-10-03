using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab4_affine_transformations.Primitives;

namespace Lab4_affine_transformations
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private List<Point2D> points = new List<Point2D>();
        private List<Edge> edges = new List<Edge>();
        private List<Polygon> polygons = new List<Polygon>();

        private Edge lastEdge;
        private Edge previousEdge;
        private Polygon lastPolygon;
        private Point2D lastPoint;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(2048, 2048);
            graphics = Graphics.FromImage(pictureBox1.Image);
            graphics.Clear(Color.White);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
        }

        // Уравнение прямой, проходящей через две заданные точки (x1,y1) и (x2,y2):
        // (y1 - y2)x + (x2 - x1)y + (x1y2 - x2y1) = 0
        private void lineEquation(Edge e, out float A, out float B, out float C)
        {
            A = e.A.Y - e.B.Y;
            B = e.B.X - e.A.X;
            C = e.A.X * e.B.Y - e.B.X * e.A.Y;
        }

        // Зная уравнения прямых : A1x + B1y + C1 = 0 и A2x + B2y + C2 = 0, 
        // получим точку пересечения по формуле Крамера:
        // x = - (C1B2 - C2B1)/(A1B2 - A2B1)
        // y = - (A1C2 - A2C1)/(A1B2 - A2B1)
        private Point2D findPoint(float A1, float A2, float B1, float B2, float C1, float C2, out float x, out float y)
        {
            var denom = A1 * B2 - A2 * B1;
            x = -1 * (C1 * B2 - C2 * B1) / denom;
            y = -1 * (A1 * C2 - A2 * C1) / denom;
            return (new Point2D(x, y));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (edges.Count < 2)
                MessageBox.Show("Нарисуйте хотя бы два ребра");
            else
            {

                var e1 = previousEdge;
                var e2 = lastEdge;

                if (e1 == null || e2 == null)
                    MessageBox.Show("Выберите два ребра");
                else
                {
                    float A1, B1, C1, A2, B2, C2, xRes, yRes;
                    lineEquation((Edge)e1, out A1, out B1, out C1);
                    lineEquation(e2, out A2, out B2, out C2);
                    var p = findPoint(A1, A2, B1, B2, C1, C2, out xRes, out yRes);

                    if ((p.X >= Math.Min(e1.A.X, e1.B.X)) && (p.X <= Math.Max(e1.A.X, e1.B.X)) &&
                        (p.X >= Math.Min(e2.A.X, e2.B.X)) && (p.X <= Math.Max(e2.A.X, e2.B.X)) &&
                        (p.Y >= Math.Min(e1.A.Y, e1.B.Y)) && (p.Y <= Math.Max(e1.A.Y, e1.B.Y)) &&
                        (p.Y >= Math.Min(e2.A.Y, e2.B.Y)) && (p.Y <= Math.Max(e2.A.Y, e2.B.Y)))
                    {
                        graphics.FillEllipse(new SolidBrush(Color.Yellow), p.X - 3, p.Y - 3, 6, 6);
                        pictureBox1.Refresh();
                    }
                    else
                        MessageBox.Show("Ребра не имеют общей точки");
                }
            }

        }

        // arccos( ( PVi-1 * PVi ) / |PVi-1| * |PVi| ) * sign ( det (PVi-1 PVi) )
        private double degreeBetweenEdges(Edge e1, Edge e2)
        {
            // находим координаты векторов по начальной и конечной точке
            var xE1 = e1.B.X - e1.A.X;
            var yE1 = e1.B.Y - e1.A.Y;

            var xE2 = e2.B.X - e2.A.X;
            var yE2 = e2.B.Y - e2.A.Y;

            // находим длины векторов 
            var lenE1 = Math.Sqrt(xE1 * xE1 + yE1 * yE1);
            var lenE2 = Math.Sqrt(xE2 * xE2 + yE2 * yE2);

            // Находим скалярное произведение PVi-1 * PVi
            var scalarProd = xE1 * xE2 + yE1 * yE2;

            return Math.Acos(scalarProd / (lenE1 * lenE2)) * Math.Sign(xE1 * yE2 - yE1 * xE2);
        }

        // определить принадлежность точки полигону методом углов
        private void button3_Click(object sender, EventArgs e)
        {
            if (polygons.Count == 0)
                MessageBox.Show("Нарисуйте хотя бы один многоугольник");
            else if (points.Count == 0)
                MessageBox.Show("Нарисуйте хотя бы одну точку");
            else
            {
                var p = lastPoint;
                var poly = lastPolygon;

                if (p == null || poly == null)
                    MessageBox.Show("Выберите точку и многоугольник");
                else
                {
                    if (poly.Points.Count < 3)
                        MessageBox.Show("Составьте полигон верно");
                    else
                    {
                        double sumDegree = 0;

                        for (int i = 0; i < poly.Points.Count - 1; ++i)
                            sumDegree += degreeBetweenEdges(new Edge(p, poly.Points[i]), new Edge(p, poly.Points[i + 1]));

                        sumDegree += degreeBetweenEdges(new Edge(p, poly.Points[poly.Points.Count - 1]), new Edge(p, poly.Points[0]));

                        var eps = 0.0001;
                        if (Math.Abs(sumDegree - 0) < eps)
                            MessageBox.Show("Точка лежит снаружи многоугольника");
                        else
                            MessageBox.Show("Точка лежит внутри многоугольника");
                    }
                }

            }
        }
    }
}
