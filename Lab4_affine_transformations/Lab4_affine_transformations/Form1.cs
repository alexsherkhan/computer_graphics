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

        private bool shouldStartNewPolygon = true;
        private bool shouldStartNewEdge = true;
        private Point2D edgeFirstPoint;

        private bool shouldShowDistance = false;
        private Edge previouslySelectedEdge;

        private MouseEventArgs args;
        private Primitive SelectedPrim;
      

        private Primitive SelectedPrimitive
        {
            get
            {
                if (null == SelectedPrim) return null;
                var p = SelectedPrim;

                if (p is Edge) previouslySelectedEdge = (Edge)p;
                if (p is Point2D && shouldShowDistance)
                {
                    MessageBox.Show("Расстояние от отрезка до точки: " +
                        previouslySelectedEdge.Distance((Point2D)p));
                }
                if (!(p is Edge)) shouldShowDistance = false;
                return p;
              
                
            }
            set
            {
                Redraw();
            }
        }

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
            pictureBox1.Invalidate();
            points.Clear();
            edges.Clear();
            polygons.Clear();
            shouldStartNewPolygon = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            args = (MouseEventArgs)e;
            Point2D p = Point2D.FromPoint(args.Location);
            if (selectedPr.Checked)
            {

                foreach (var item in points)
                {
                    if(item.X - p.X > -5 && item.X - p.X < 5) SelectedPrim = (Primitive)item;
                }
                foreach (var item in edges)
                {
                    if (item.Distance(p) >-5 && item.Distance(p) < 5) SelectedPrim = (Primitive)item;
                }
                foreach (var item in polygons)
                {
                    for (int i = 0; i <= item.Points.Count-1; i++)
                    {
                        Edge line;
                        if (i == item.Points.Count - 1) line = new Edge(item.Points[i], item.Points[0]);
                        else line = new Edge(item.Points[i], item.Points[i + 1]);
                        if (line.Distance(p) > -5 && line.Distance(p) < 5) SelectedPrim = (Primitive)item;
                    }
                }

                if (SelectedPrimitive is Point2D)
                    lastPoint = (Point2D)SelectedPrimitive;
                else if (SelectedPrimitive is Polygon)
                    lastPolygon = (Polygon)SelectedPrimitive;
                else if (SelectedPrimitive is Edge)
                {
                    previousEdge = lastEdge;
                    lastEdge = (Edge)SelectedPrimitive;
                }
            }
                

            if (rbPoint.Checked)
            {
                SelectedPrim = (Primitive)p;
                points.Add(p);
            }
            else if (rbEdge.Checked)
            {
                if (shouldStartNewEdge)
                {
                    edgeFirstPoint = p;
                    shouldStartNewEdge = false;
                }
                else
                {
                    Edge edge = new Edge(edgeFirstPoint, p);
                    SelectedPrim = (Primitive)edge;
                    edges.Add(edge);
                    shouldStartNewEdge = true;
                }
            }
            else if (rbPolygon.Checked)
            {
                if (shouldStartNewPolygon)
                {
                    Polygon polygon = new Polygon();
                    SelectedPrim = (Primitive)polygon;
                    polygons.Add(polygon);
                    shouldStartNewPolygon = false;
                }
                polygons[polygons.Count - 1].Points.Add(p);
            }
            Redraw();
        }

        private void Redraw()
        {
            graphics.Clear(Color.White);
            if (!shouldStartNewEdge) edgeFirstPoint.Draw(graphics, false);
            points.ForEach((p) => p.Draw(graphics, p == SelectedPrimitive));
            edges.ForEach((e) => e.Draw(graphics, e == SelectedPrimitive));
            polygons.ForEach((p) => p.Draw(graphics, p == SelectedPrimitive));
            pictureBox1.Invalidate();
        }

        private void newPolygon_Click(object sender, EventArgs e)
        {
            shouldStartNewPolygon = true;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox2.Text);
            int y = int.Parse(textBox3.Text);
            Transformation shift = Transformation.Translate(x, y);
            SelectedPrimitive.Apply(shift);
            Redraw();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            PointF center;
            if (comboBox1.SelectedIndex == 0)
            {
                center = new PointF(lastPoint.X, lastPoint.Y);
            }
            else
            {
                center = ((Polygon)SelectedPrimitive).Center();
            }
            var moveToCenter = Transformation.Translate(-center.X, -center.Y);
            Transformation rotate = Transformation.Rotate((float)(Math.PI / 180 * int.Parse(textBox1.Text)));
            var moveBack = Transformation.Translate(center.X, center.Y);
            SelectedPrimitive.Apply(moveToCenter * rotate * moveBack);
            Redraw();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PointF center;
            if (comboBox2.SelectedIndex == 0)
            {
                center = new PointF(lastPoint.X, lastPoint.Y);
            }
            else
            {
                center = ((Polygon)SelectedPrimitive).Center();
            }
            float scaleX = float.Parse(comboBox3.Text)/ 100;
            float scaleY = float.Parse(comboBox4.Text) / 100;
            var moveToCenter = Transformation.Translate(-center.X, -center.Y);
            Transformation rotate = Transformation.Scale(scaleX,scaleY);
            var moveBack = Transformation.Translate(center.X, center.Y);
            SelectedPrimitive.Apply(moveToCenter * rotate * moveBack);
            Redraw();
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

        // определить принадлежность точки полигону методом лучей
        private bool even_od_rule(Polygon pol, Point2D p2D)
        {

            for (int i = 0; i < pol.Points.Count - 1; i++)
                graphics.DrawLine(new Pen(Color.Red, 3), pol.Points[i].X, pol.Points[i].Y, pol.Points[i + 1].X, pol.Points[i + 1].Y);

            graphics.DrawLine(new Pen(Color.Red, 3), pol.Points[0].X, pol.Points[0].Y, pol.Points[pol.Points.Count - 1].X, pol.Points[pol.Points.Count - 1].Y);

            int avg_x = (int)(pol.Points[1].X + pol.Points[0].X) / 2;
            int avg_y = (int)(pol.Points[1].Y + pol.Points[0].Y) / 2;

            Point2D p_avg = Point2D.FromPoint(new Point(avg_x, avg_y));

            float rX = p2D.X - p_avg.X;
            float rY = p2D.Y - p_avg.Y;

            pictureBox1.Refresh();

            var e = new Edge(p2D, p_avg);
            float A, B, C;
            lineEquation(e, out A, out B, out C); //Прямая, проходящая через заданую точку и середину первого ребра

            int count_point = 1;

            for (int i = 1; i < pol.Points.Count; ++i)
            {

                var e1 = new Edge(pol.Points[i], pol.Points[(i + 1) % pol.Points.Count]);


                float A1, B1, C1, xRes, yRes;
                lineEquation((Edge)e1, out A1, out B1, out C1);
                Point2D pr = findPoint(A1, A, B1, B, C1, C, out xRes, out yRes);

                if ((pr.X >= Math.Min(e1.A.X, e1.B.X)) && (pr.X <= Math.Max(e1.A.X, e1.B.X)) &&
                    (pr.Y >= Math.Min(e1.A.Y, e1.B.Y)) && (pr.Y <= Math.Max(e1.A.Y, e1.B.Y)) &&
                    (p2D.X - pr.X) * rX > 0 && (p2D.Y - pr.Y) * rY > 0)
                {
                    graphics.DrawLine(new Pen(Color.Red, 1), new Point((int)p2D.X, (int)p2D.Y), new Point((int)pr.X, (int)pr.Y));
                    pictureBox1.Refresh();
                    ++count_point;
                }
            }

            return count_point % 2 == 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lastPolygon == null || lastPoint == null)
                MessageBox.Show("Не выбраны точка и многоугольник");
            else
            {
                if (even_od_rule(lastPolygon,lastPoint))
                    MessageBox.Show("Точка принадлежит многоугольнику");
                else
                    MessageBox.Show("Точка НЕ принадлежит многоугольнику");
            }
        }


        private void button9_Click(object sender, EventArgs e)
        {

            if (lastEdge == null || lastPoint == null)
                MessageBox.Show("Не выбраны точка и отрезок");
            else
            {
                if (lastEdge.Distance(lastPoint) < 0) MessageBox.Show("Точка слева от отрезка");
                else MessageBox.Show("Точка справа от отрезка"); ;
            }
        }
    }

}
