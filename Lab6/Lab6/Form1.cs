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
using static Lab6.Primitives.Face_Affines;

namespace Lab6
{
    public enum Axis { AXIS_X, AXIS_Y, AXIS_Z, OTHER };
    public enum Projection { PERSPECTIVE = 0, ISOMETRIC, ORTHOGR_X, ORTHOGR_Y, ORTHOGR_Z };
    public enum CameraMode { Simple, Clipping, Buffer, Guro}
    public enum Reflect { X, Y, Z};
    public delegate float Function(float x, float y);

    public partial class Form1 : Form
    {
        Color fill_color = Color.MediumVioletRed;
        Pen new_fig = Pens.Black;
        Pen old_fig = Pens.LightGray;
        Graphics g, camera_g;
        Projection pr = 0;
        Polyhedron figure = null;
        Axis line_mode = 0;
        Camera camera = null;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            g = pictureBox1.CreateGraphics();
            g.TranslateTransform(pictureBox1.ClientSize.Width / 2, pictureBox1.ClientSize.Height / 2);
            g.ScaleTransform(1, -1);
            pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            camera_g = pictureBox3.CreateGraphics();
            camera_g.TranslateTransform(pictureBox3.ClientSize.Width / 2, pictureBox3.ClientSize.Height / 2);
            camera_g.ScaleTransform(1, -1);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            colorDialog1.FullOpen = true;
            colorDialog1.Color = fill_color;
            label_color.BackColor = fill_color;
        }

        private void Button_cube_Click(object sender, EventArgs e)
        {
            createFigure(0);
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
                Transformation transformation = Transformation.Translate(-old_x, -old_y, -old_z);
                transformation *= Transformation.Translate(Double.Parse(trans_x.Text), Double.Parse(trans_y.Text), Double.Parse(trans_z.Text));
                transformation *= Transformation.Scale(Double.Parse(scaling_x.Text), Double.Parse(scaling_y.Text), Double.Parse(scaling_z.Text));
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

                        transformation *= Transformation.Translate(-Ax, -Ay, Az)
                                   * Transformation.RotateX(rotX)
                                   * Transformation.RotateY(rotY)
                                   * Transformation.RotateZ(rotZ)
                                   * Transformation.Translate(Ax, Ay, Az);
                        break;
                    default:
                        transformation *= Transformation.RotateX(rotX)
                                    * Transformation.RotateY(rotY)
                                    * Transformation.RotateZ(rotZ);
                        break;
                           
                }
                transformation *= Transformation.Translate(old_x, old_y, old_z);
                figure.Apply(transformation);
                camera.fiqureApply(transformation);
                clearScenes();
                figure.show(g, pr, new_fig);
                camera.show(camera_g, old_fig);
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
            createFigure(4);
        }

        private void createFigure(int figureType)
        {
            clearScenes();
            figure = new Polyhedron();
            switch (figureType)
            {
                case 0:
                    figure.make_hexahedron();
                    break;
                case 1:
                    figure.make_tetrahedron();
                    break;
                case 2:
                    figure.make_octahedron();
                    break;
                case 3:
                    figure.make_dodecahedron();
                    break;
                case 4:
                    figure.make_icosahedron();
                    break;
            }
            CameraMode oldMode = camera == null ? CameraMode.Simple : camera.mode;
            camera= new Camera(new Polyhedron(figure), pictureBox3,fill_color, light_x, light_y, light_z);
            camera.setMode(oldMode);
            camera.Apply(Transformation.Identity());
            figure.show(g, pr);
            camera.show(camera_g, old_fig);
        }

        private void createFigureFromFile(string fileText, int mode = 0)
        {
            clearScenes();
            figure = new Polyhedron(fileText, mode);
            CameraMode oldMode = camera == null ? CameraMode.Simple : camera.mode;
            camera = new Camera(new Polyhedron(figure), pictureBox3, fill_color, light_x, light_y, light_z);
            camera.setMode(oldMode);
            camera.Apply(Transformation.Identity());
            figure.show(g, pr);
            camera.show(camera_g, old_fig);
        }

        private void Button_dodecaeder_Click(object sender, EventArgs e)
        {
            createFigure(3);
        }

        private void Button_octaeder_Click(object sender, EventArgs e)
        {
            createFigure(2);
        }

        private void Button_tetraeder_Click(object sender, EventArgs e)
        {
            createFigure(1);
        }

        private void clearScenes()
        {
            g.Clear(Color.White);
            camera_g.Clear(Color.White);
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

            createFigureFromFile(fileText);
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

            createFigureFromFile(fileText, Polyhedron.MODE_ROT);

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
            camera_g.Clear(Color.White);
            figure.show(g, pr, new_fig);
         }

        private void Camera_translation_Click(object sender, EventArgs e)
        {
            camera.Apply(Transformation.Translate(-Double.Parse(trans_x_camera.Text),
                -Double.Parse(trans_y_camera.Text),
                -Double.Parse(trans_z_camera.Text)));
            camera_g.Clear(Color.White);
            camera.show(camera_g, old_fig);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            camera_g.Clear(Color.White);
            if (radioButton1.Checked)
            {
                camera.setMode(CameraMode.Clipping);
            }
            camera.show(camera_g, null, true);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

            var f = form2.f;
            float x0 = form2.X0;
            float x1 = form2.X1;
            float y0 = form2.Y0;
            float y1 = form2.Y1;
            int cnt_of_breaks = form2.Cnt_of_breaks;

            form2.Dispose();

            ReverseFloatComparer fcmp = new ReverseFloatComparer();

            float dx = (Math.Abs(x0) + Math.Abs(x1)) / cnt_of_breaks;
            float dy = (Math.Abs(y0) + Math.Abs(y1)) / cnt_of_breaks;

            List<Face> faces = new List<Face>();
            List<Point3d> pts0 = new List<Point3d>();
            List<Point3d> pts1 = new List<Point3d>();

            for (float x = x0; x < x1; x += dx)
            {
                for (float y = y0; y < y1; y += dy)
                {
                    float z = f(x, y);
                    pts1.Add(new Point3d(x, y, z));
                }
                if (pts0.Count != 0)
                    for (int i = 1; i < pts0.Count; ++i)
                    {
                        faces.Add(new Face(new List<Point3d>() {
                            new Point3d(pts0[i - 1]), new Point3d(pts1[i - 1]),
                            new Point3d(pts1[i]), new Point3d(pts0[i])
                        }));
                    }
                pts0.Clear();
                pts0 = pts1;
                pts1 = new List<Point3d>();
            }

            g.Clear(Color.White);
            figure = new Polyhedron(faces);
            figure.Apply(Transformation.Scale(5, 5, 5));
            figure.show(g, pr, new_fig);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            
            double rotX = -Double.Parse(camera_x_rotate.Text) / 180 * Math.PI;
            double rotY = -Double.Parse(camera_y_rotate.Text) / 180 * Math.PI;
            double rotZ = -Double.Parse(camera_z_rotate.Text) / 180 * Math.PI;
            camera.Apply(Transformation.RotateX(rotX)
                                * Transformation.RotateY(rotY)
                                * Transformation.RotateZ(rotZ)
                                );
            camera_g.Clear(Color.White);
            camera.show(camera_g);
        }

    
        private void radioButton2_Checked(object sender, EventArgs e)
        {
            if (figure != null)
            {
                if (radioButton2.Checked)
                {
                    camera.setMode(CameraMode.Buffer);
                }
            }

            camera.show(camera_g);


        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (figure != null)
            {
                if (radioButton3.Checked)
                {
                    camera.setMode(CameraMode.Guro);
                 
                }
            }
            camera.show(camera_g);
        }

        private void label_color_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            fill_color = colorDialog1.Color;
            label_color.BackColor = fill_color;
            if (fill_color.ToArgb() == Color.Black.ToArgb())
                label_color.ForeColor = Color.White;
            else label_color.ForeColor = Color.Black;
            if (radioButton3.Checked)
            {
                camera.setMode(CameraMode.Guro);
            }
            camera.fill_color = fill_color;
            camera.show(camera_g);

        }

        private void button_exec_camera_Click(object sender, EventArgs e)
        {
            camera.show(camera_g);
        }

        private void camera_y_Click(object sender, EventArgs e)
        {}
    }
}
