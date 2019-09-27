using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_raster_algorithms
{
    public partial class Form1 : Form
    {
        private Bitmap image;
        private Graphics g;
        private Color paletteColor;
        private Color curPixel;
        private int xG, yG;
        private List<Point> bordersList = new List<Point>();
        private bool[,] used;
        public Form1()
        {
            InitializeComponent();
            paletteColor = Color.Black;
        }

        private void borderAllocation()
        {
            //Image image = pictureBox1.Image;
            Color color = colorDialog1.Color;
            used = new bool[image.Width, image.Height];
            bool flag = false;
            for (int x = 0; x < image.Width; ++x)
            {
                for (int y = 0; y < image.Height; ++y)
                {
                    Color currentPixel = image.GetPixel(x, y);
                    if (checkPixel(image.GetPixel(x, y), color) && !checkPixel(image.GetPixel(x, y + 1), color))
                    {
                        if (!used[x, y])
                        {
                            findBorder(x, y, color);
                            drawBorder();
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag){
                  break;
}
            }
            
        }


        private bool checkPixel(Color pixelColor, Color standardColor)
        {
            return Math.Abs(pixelColor.R - standardColor.R) < 20 && Math.Abs(pixelColor.A - standardColor.A) < 20 && Math.Abs(pixelColor.B - standardColor.B) < 20 && Math.Abs(pixelColor.G - standardColor.G) < 20;
        }


        private void findBorder(int x, int y, Color color)
        {
            Stack<Point> queue = new Stack<Point>();
            List<Point> tmp = new List<Point>();
            tmp.Add(new Point(x, y));
            queue.Push(new Point(x, y));
            while (queue.Count > 0)
            {
                Point currentPoint = queue.Pop();
                /*if (tmp.First().Equals(currentPoint)){
                    break;
                }*/
                if (used[currentPoint.X, currentPoint.Y])
                {
                    continue;
                }
                used[currentPoint.X, currentPoint.Y] = true;
                Point prevPoint = tmp.ElementAt(tmp.Count - 1);
                if (currentPoint.X < image.Width - 1)
                {
                    Point nextPoint = new Point(currentPoint.X + 1, currentPoint.Y);
                    int Xdiff = currentPoint.X - nextPoint.X;
                    int Ydiff = currentPoint.Y - nextPoint.Y;
                    Point inPoint = new Point(nextPoint.X - Ydiff, nextPoint.Y - Xdiff);
                    if (checkPixel(image.GetPixel(nextPoint.X, nextPoint.Y), color) && !checkPixel(image.GetPixel(inPoint.X, inPoint.Y), color))
                    {
                        queue.Push(new Point(nextPoint.X + 1, nextPoint.Y));
                        tmp.Add(new Point(nextPoint.X + 1, nextPoint.Y));
                        continue;
                    }
                }
                if (currentPoint.Y > 0 && currentPoint.X < image.Width - 1)
                {
                    Point nextPoint = new Point(currentPoint.X + 1, currentPoint.Y - 1);
                    int Xdiff = currentPoint.X - nextPoint.X;
                    int Ydiff = currentPoint.Y - nextPoint.Y;
                    Point inPoint = new Point(nextPoint.X - Ydiff, nextPoint.Y - Xdiff);
                    if (checkPixel(image.GetPixel(nextPoint.X, nextPoint.Y), color) && !checkPixel(image.GetPixel(inPoint.X, inPoint.Y), color))
                    {
                        queue.Push(new Point(nextPoint.X, nextPoint.Y));
                        tmp.Add(new Point(nextPoint.X, nextPoint.Y));
                        continue;

                    }
                }
                if (currentPoint.Y > 0 )
                {
                    Point nextPoint = new Point(currentPoint.X, currentPoint.Y - 1);
                    int Xdiff = currentPoint.X - nextPoint.X;
                    int Ydiff = currentPoint.Y - nextPoint.Y;
                    Point inPoint = new Point(nextPoint.X - Ydiff, nextPoint.Y - Xdiff);
                    if (checkPixel(image.GetPixel(nextPoint.X, nextPoint.Y), color) && !checkPixel(image.GetPixel(inPoint.X, inPoint.Y), color))
                    {
                        queue.Push(new Point(nextPoint.X, nextPoint.Y));
                        tmp.Add(new Point(nextPoint.X, nextPoint.Y));
                        continue;

                    }
                }
                if (currentPoint.Y > 0 && currentPoint.X > 0)
                {
                    Point nextPoint = new Point(currentPoint.X - 1, currentPoint.Y - 1);
                    int Xdiff = currentPoint.X - nextPoint.X;
                    int Ydiff = currentPoint.Y - nextPoint.Y;
                    Point inPoint = new Point(nextPoint.X - Ydiff, nextPoint.Y - Xdiff);
                    if (checkPixel(image.GetPixel(nextPoint.X, nextPoint.Y), color) && !checkPixel(image.GetPixel(inPoint.X, inPoint.Y), color))
                    {
                        queue.Push(new Point(nextPoint.X, nextPoint.Y));
                        tmp.Add(new Point(nextPoint.X, nextPoint.Y));
                        continue;

                    }
                }
                if (currentPoint.X > 0)
                {
                    Point nextPoint = new Point(currentPoint.X - 1, currentPoint.Y);
                    int Xdiff = currentPoint.X - nextPoint.X;
                    int Ydiff = currentPoint.Y - nextPoint.Y;
                    Point inPoint = new Point(nextPoint.X - Ydiff, nextPoint.Y - Xdiff);
                    if (checkPixel(image.GetPixel(nextPoint.X, nextPoint.Y), color) && !checkPixel(image.GetPixel(inPoint.X, inPoint.Y), color))
                    {
                        queue.Push(new Point(nextPoint.X, nextPoint.Y));
                        tmp.Add(new Point(nextPoint.X, nextPoint.Y));
                        continue;

                    }
                }
                if (currentPoint.Y < image.Height - 1 && currentPoint.X > 0)
                {
                    Point nextPoint = new Point(currentPoint.X + 1, currentPoint.Y - 1);
                    int Xdiff = currentPoint.X - nextPoint.X;
                    int Ydiff = currentPoint.Y - nextPoint.Y;
                    Point inPoint = new Point(nextPoint.X - Ydiff, nextPoint.Y - Xdiff);
                    if (checkPixel(image.GetPixel(nextPoint.X, nextPoint.Y), color) && !checkPixel(image.GetPixel(inPoint.X, inPoint.Y), color))
                    {
                        queue.Push(new Point(nextPoint.X, nextPoint.Y));
                        tmp.Add(new Point(nextPoint.X, nextPoint.Y));
                        continue;

                    }
                }
                if (currentPoint.Y < image.Height - 1)
                {
                    Point nextPoint = new Point(currentPoint.X, currentPoint.Y + 1);
                    int Xdiff = currentPoint.X - nextPoint.X;
                    int Ydiff = currentPoint.Y - nextPoint.Y;
                    Point inPoint = new Point(nextPoint.X - Ydiff, nextPoint.Y - Xdiff);
                    if (checkPixel(image.GetPixel(nextPoint.X, nextPoint.Y), color) && checkPixel(image.GetPixel(inPoint.X, inPoint.Y), color))
                    {
                        queue.Push(new Point(nextPoint.X, nextPoint.Y));
                        tmp.Add(new Point(nextPoint.X, nextPoint.Y));
                        continue;

                    }
                }
                if (currentPoint.Y < image.Height - 1 && currentPoint.X < image.Width - 1)
                {
                    Point nextPoint = new Point(currentPoint.X + 1, currentPoint.Y + 1);
                    int Xdiff = currentPoint.X - nextPoint.X;
                    int Ydiff = currentPoint.Y - nextPoint.Y;
                    Point inPoint = new Point(nextPoint.X - Ydiff, nextPoint.Y - Xdiff);
                    if (checkPixel(image.GetPixel(nextPoint.X, nextPoint.Y), color) && !checkPixel(image.GetPixel(inPoint.X, inPoint.Y), color))
                    {
                        queue.Push(new Point(nextPoint.X, nextPoint.Y));
                        tmp.Add(new Point(nextPoint.X, nextPoint.Y));
                        continue;

                    }
                }

            }
            bordersList.InsertRange(bordersList.Count, tmp);
        }

        private void drawBorder()
        {
            foreach (Point i in bordersList)
            {
                image.SetPixel(i.X, i.Y, Color.Red);
            }
            pictureBox1.Image = image;
        }
        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(openDialog.FileName);
                    pictureBox1.Image = image;
                    g = Graphics.FromImage(pictureBox1.Image);
                    pictureBox1.Invalidate();
                    Form1.ActiveForm.Width = image.Width + 70;
                    Form1.ActiveForm.Height = image.Height + menuStrip1.Height + panel1.Height;
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void paletteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                paletteColor = colorDialog1.Color;
            }
        }

        private void paint(int x, int y, Color curColor, Color newColor)
        {
            if (image.GetPixel(x, y) == newColor || image.GetPixel(x, y) != curColor)
                return;

            int leftX = x - 1, rightX = x + 1;

            Color pix;

            // Цвет правого пикселя
            if (rightX != image.Width)
            {
                pix = image.GetPixel(rightX, y);

                while (rightX < image.Width && pix == curColor)
                {
                    rightX++;
                    if (rightX < image.Width)
                        pix = image.GetPixel(rightX, y);
                }
            }


            // Цвет левого пикселя
            if (leftX != -1)
            {
                pix = image.GetPixel(leftX, y);

                while (leftX >= 0 && pix == curColor)
                {
                    leftX--;
                    if (leftX >= 0)
                        pix = image.GetPixel(leftX, y);
                }
            }

            rightX--;
            leftX++;

            Pen pen = new Pen(newColor);
            g.DrawLine(pen, leftX, y, rightX, y);

            for (int i = leftX; i < rightX; ++i)
            {
                if (y < image.Height - 1)
                    paint(i, y + 1, curColor, newColor);

                if (y > 0)
                    paint(i, y - 1, curColor, newColor);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                xG = e.Location.X;
                yG = e.Location.Y;
                curPixel = (pictureBox1.Image as Bitmap).GetPixel(e.Location.X, e.Location.Y);
                label2.Text = yG.ToString();
            }
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            xG = e.Location.X;
            yG = e.Location.Y;
        }

        private void pictureBox1_DrawLine(MouseEventArgs e)
        {
            g.DrawLine(new Pen(paletteColor, trackBar1.Value), new Point(xG, yG), e.Location);
            xG = e.Location.X;
            yG = e.Location.Y;
            pictureBox1.Refresh();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(image);
            g.FillRectangle(Brushes.White, 0, 0, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                paint(xG, yG, curPixel, paletteColor);
                pictureBox1.Refresh();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            borderAllocation();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null && e.Button == MouseButtons.Left)
                pictureBox1_DrawLine(e);
        }
    }

}
