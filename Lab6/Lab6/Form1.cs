using Lab6.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public enum Axis { AXIS_X, AXIS_Y, AXIS_Z, OTHER };
    public enum Projection { PERSPECTIVE = 0, ISOMETRIC, ORTHOGR_X, ORTHOGR_Y, ORTHOGR_Z };

    public enum Reflect { X, Y, Z};
    public delegate float Function(float x, float y);

    public partial class Form1 : Form
    {
        Color fill_color = Color.MediumVioletRed;
        Pen new_fig = Pens.Black;
        Pen old_fig = Pens.LightGray;
        Graphics g;
        Projection pr = 0;
        Polyhedron figure = null;
        Axis line_mode = 0;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
            g.ScaleTransform(1, -1);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void Button_cube_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_hexahedron();
            figure.show(g, pr);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pr = (Projection)comboBox1.SelectedIndex;
            g.Clear(Color.White);
            if (figure != null)
                figure.show(g, pr);
        }

        private void Button_exec_Click(object sender, EventArgs e)
        {
            if (figure == null)
            {
                MessageBox.Show("Сначала создайте фигуру", "Нет фигуры", MessageBoxButtons.OK);
            }
            else
            {
                double old_x = figure.Center.X, old_y = figure.Center.Y, old_z = figure.Center.Z;
                figure.Apply(Transformation.Translate(-old_x, -old_y, -old_z));
                figure.Apply(Transformation.Translate(Double.Parse(trans_x.Text), Double.Parse(trans_y.Text), Double.Parse(trans_z.Text)));
                figure.Apply(Transformation.Scale(Double.Parse(scaling_x.Text), Double.Parse(scaling_y.Text), Double.Parse(scaling_z.Text)));
                double rotX = Double.Parse(angle_x.Text) / 180 * Math.PI;
                double rotY = Double.Parse(angle_y.Text) /  180 * Math.PI;
                double rotZ = Double.Parse(angle_z.Text) / 180 * Math.PI;
                switch (line_mode)
                {
                    case Axis.OTHER:
                        Edge rot_line = new Edge(
                                                    new Point3d(
                                                        int.Parse(rot_line_x1.Text),
                                                        int.Parse(rot_line_y1.Text),
                                                        int.Parse(rot_line_z1.Text)),
                                                    new Point3d(
                                                        int.Parse(rot_line_x2.Text),
                                                        int.Parse(rot_line_y2.Text),
                                                        int.Parse(rot_line_z2.Text))
                                                    );
                        double Ax = (rot_line.P1.X + rot_line.P2.X) / 2, 
                            Ay = (rot_line.P1.Y + rot_line.P2.Y) / 2, 
                            Az = (rot_line.P1.Z + rot_line.P2.Z) / 2;

                        figure.Apply(Transformation.Translate(-Ax, -Ay, Az) 
                                   * Transformation.RotateX(rotX)
                                   * Transformation.RotateY(rotY)
                                   * Transformation.RotateZ(rotZ)
                                   * Transformation.Translate(Ax, Ay, Az)
                                   );
                        break;
                    default:
                        figure.Apply(Transformation.RotateX(rotX)
                                    * Transformation.RotateY(rotY)
                                    * Transformation.RotateZ(rotZ)
                                    );
                        break;
                           
                }
                figure.Apply(Transformation.Translate(old_x, old_y, old_z));
                g.Clear(Color.White);
                figure.show(g, pr, new_fig);
            }
        }


        private void Button_refl_x_Click(object sender, EventArgs e)
        {
            reflect(Reflect.X);
        }

        private void Button_refl_y_Click(object sender, EventArgs e)
        {
            reflect(Reflect.Y);
        }

        private void Button_refl_z_Click(object sender, EventArgs e)
        {
            reflect(Reflect.Z);
        }

        private void reflect(Reflect refl)
        {
            if (figure == null)
            {
                MessageBox.Show("Сначала создайте фигуру", "Нет фигуры", MessageBoxButtons.OK);
            }
            else
            {
                switch (refl)
                {
                    case Reflect.X:
                        figure.Apply(Transformation.ReflectX());
                        break;
                    case Reflect.Y:
                        figure.Apply(Transformation.ReflectY());
                        break;
                    case Reflect.Z:
                        figure.Apply(Transformation.ReflectZ());
                        break;
                }
                g.Clear(Color.White);
                figure.show(g, pr, new_fig);
            }

        }
        private void Button_ikosaeder_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_icosahedron();
            figure.show(g, pr);
        }

        private void Button_dodecaeder_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_dodecahedron();
            figure.show(g, pr);
        }

        private void Button_octaeder_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_octahedron();
            figure.show(g, pr);
        }

        private void Button_tetraeder_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            figure = new Polyhedron();
            figure.make_tetrahedron();
            figure.show(g, pr);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            line_mode = (Axis)comboBox2.SelectedIndex;
            if (line_mode == Axis.OTHER)
            {
                rot_line_x1.Enabled = true;
                rot_line_y1.Enabled = true;
                rot_line_z1.Enabled = true;
                rot_line_x2.Enabled = true;
                rot_line_y2.Enabled = true;
                rot_line_z2.Enabled = true;
            }
            else
            {
                rot_line_x1.Enabled = false;
                rot_line_y1.Enabled = false;
                rot_line_z1.Enabled = false;
                rot_line_x2.Enabled = false;
                rot_line_y2.Enabled = false;
                rot_line_z2.Enabled = false;
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // open_file_dialog
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);

            figure = new Polyhedron(fileText);
            g.Clear(Color.White);
            figure.show(g, pr);
     
            label10.Text = Math.Round(figure.Center.X).ToString() + ", " + Math.Round(figure.Center.Y).ToString() + ", " + Math.Round(figure.Center.Z).ToString();
        }

        // save_file_dialog
        private void button2_Click(object sender, EventArgs e)
        {
             if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            string text = "";
            if (figure != null)
                text = figure.save_obj();
            System.IO.File.WriteAllText(filename, text);
        }

        // rotation_figure
        private void button33_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);

            figure = new Polyhedron(fileText, Polyhedron.MODE_ROT);
            g.Clear(Color.White);
            figure.show(g, pr);
         

           // label10.Text = figure.Center.X.ToString() + ", " + figure.Center.Y.ToString() + ", " + figure.Center.Z.ToString();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            foreach (var c in Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = c as TextBox;
                    if (t.Name == "scaling_x" || t.Name == "scaling_y" || t.Name == "scaling_z" || t.Name == "rot_line_x2" ||
                            t.Name == "rot_line_y2" || t.Name == "rot_line_z2")
                        t.Text = "1";
                    else t.Text = "0";

                }
            }
            
            g.Clear(Color.White);
            figure.show(g, pr, new_fig);
        }
    }
}
