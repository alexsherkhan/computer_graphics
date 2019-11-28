using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private PictureBox pictureBox;

        private TextBox light_x;
        private TextBox light_y;
        private TextBox light_z;
        public Color fill_color;

        public Bitmap bmp, texture;
        public BitmapData bmpData, bmpDataTexture; // for picturebox and texture
        public byte[] rgbValues, rgbValuesTexture; // for picturebox and texture
        public IntPtr ptr; // pointer to the rgbValues
        public int bytes; // length of rgbValues

        public PictureBox picture;

        public CameraMode mode = CameraMode.Simple;

        public Camera(Polyhedron polyhedron, PictureBox pictureBox, Color fill_color, TextBox light_x = null, TextBox light_y = null, TextBox light_z = null)
        {
            camera_figure = polyhedron;
            this.pictureBox = pictureBox;
            this.light_x = light_x;
            this.light_y = light_y;
            this.light_z = light_z;
            this.fill_color = fill_color;
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
                    //show_lines(g);
                    camera_figure.show(g, Projection.PERSPECTIVE, pen);
                    break;
                case CameraMode.Clipping:
                    camera_figure.show(g, Projection.PERSPECTIVE, pen, true, coords);
                    break;
                case CameraMode.Buffer:
                    show_z_buff();
                    break;
                case CameraMode.Guro:
                    show_gouraud();
                    break;
                case CameraMode.Texture:
                    show_texture();
                    break;
            }

        }

        public void fiqureApply(Transformation t)
        {
            camera_figure.Apply(t);
        }

        private void show_z_buff()
        {
            int[] buff = new int[pictureBox.Width * pictureBox.Height];
            int[] colors = new int[pictureBox.Width * pictureBox.Height];

            camera_figure.calc_z_buff(new Edge(new Point3d(0, 0, 100), new Point3d(0, 0, 250)), pictureBox.Width, pictureBox.Height, out buff, out colors);
            Bitmap bmp = pictureBox.Image as Bitmap;


            for (int i = 0; i < pictureBox.Width; ++i)
                for (int j = 0; j < pictureBox.Height; ++j)
                {
                    Color c = Color.FromArgb(buff[i * pictureBox.Height + j], buff[i * pictureBox.Height + j], buff[i * pictureBox.Height + j]);
                    bmp.SetPixel(i, j, c);
                }

            pictureBox.Refresh();
        }

        private void show_gouraud()
        {
            float[] intensive = new float[pictureBox.Width * pictureBox.Height];

            camera_figure.calc_gouraud(coords, pictureBox.Width, pictureBox.Height, out intensive, new Point3d(int.Parse(light_x.Text), int.Parse(light_y.Text), int.Parse(light_z.Text)));
            Bitmap bmp = pictureBox.Image as Bitmap;
       

            for (int i = 0; i < pictureBox.Width; ++i)
                for (int j = 0; j < pictureBox.Height; ++j)
                {
                    Color c;
                    if (intensive[i * pictureBox.Height + j] < 1E-6f)
                        c = Color.Black;
                    else
                    {
                        float intsv = intensive[i * pictureBox.Height + j];
                        if (intsv > 1)
                            intsv = 1;
                        c = Color.FromArgb((int)(fill_color.R * intsv) % 256, (int)(fill_color.G * intsv) % 256, (int)(fill_color.B * intsv) % 256);
                    }
                    bmp.SetPixel(i, j, c);
                }

            pictureBox.Refresh();
        }

        private byte[] getRGBValues(out Bitmap bmp, out BitmapData bmpData,
            out IntPtr ptr, out int bytes)
        {
            bmp = new Bitmap(picture.ClientSize.Width, picture.ClientSize.Height, PixelFormat.Format24bppRgb);

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            bmpData =
                bmp.LockBits(rect, ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgb_values = new byte[bytes];

            // Create rgb array with background color
            for (int i = 0; i < bytes - 3; i += 3)
            {
                rgb_values[i] = picture.BackColor.R;
                rgb_values[i + 1] = picture.BackColor.G;
                rgb_values[i + 2] = picture.BackColor.B;
            }

            return rgb_values;
        }

        private void show_texture()
        {
            if (bmp != null)
                bmp.Dispose();
            rgbValues = getRGBValues(out bmp, out bmpData, out ptr, out bytes);
            camera_figure.ApplyTexture(bmp, bmpData, rgbValues, texture, bmpDataTexture, rgbValuesTexture);
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            picture.Image = bmp;
        }

        public void Apply(Transformation t)
        {
            double old_x = camera_figure.Center.X, old_y = camera_figure.Center.Y, old_z = camera_figure.Center.Z;
            Transformation transformation = Transformation.Translate(-old_x, -old_y, -old_z);
            transformation *= t;
            transformation *= Transformation.Translate(old_x, old_y, old_z);
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
