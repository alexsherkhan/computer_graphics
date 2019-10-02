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

                   // if (item.Points.Contains(p) || item.Points.Contains(new Point2D (p.X+1,p.Y+1)) 
                      //  || item.Points.Contains(new Point2D(p.X - 1, p.Y - 1)))
                     //   SelectedPrim = (Primitive)item;
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
    }

}
