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

namespace Lab2_color_spaces
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private static ColorMatrix redColor = new ColorMatrix(new float[][] {
            new float [] {1, 0, 0, 0, 0},
            new float [] {0, 0, 0, 0, 0 },
            new float [] {0, 0, 0, 0, 0 },
            new float [] {0, 0, 0, 1, 0 },
            new float [] {0, 0, 0, 0, 1 }
        });
        private static ColorMatrix greenColor = new ColorMatrix(new float[][] {
            new float [] {0, 0, 0, 0, 0},
            new float [] {0, 1, 0, 0, 0 },
            new float [] {0, 0, 0, 0, 0 },
            new float [] {0, 0, 0, 1, 0 },
            new float [] {0, 0, 0, 0, 1 }
        });
        private static ColorMatrix blueColor = new ColorMatrix(new float[][] {
            new float [] {0, 0, 0, 0, 0},
            new float [] {0, 0, 0, 0, 0 },
            new float [] {0, 0, 1, 0, 0 },
            new float [] {0, 0, 0, 1, 0 },
            new float [] {0, 0, 0, 0, 1 }
        });

        private static Bitmap ConvertColor(Bitmap pic, ColorMatrix transform) {
            var color = new Bitmap(pic.Width, pic.Height);
            var attr = new ImageAttributes();
            attr.SetColorMatrix(transform);
            var graph = Graphics.FromImage(color);
            graph.DrawImage(pic, new Rectangle(0, 0, pic.Width, pic.Height), 0, 0, pic.Width, pic.Height, GraphicsUnit.Pixel, attr);
            graph.Dispose();
            return color;
        }

        private static Bitmap Histogram(Bitmap img, char cl)
        {
            var color = new int[256];
            var histogram = new Bitmap(256, 256);
            var graph = Graphics.FromImage(histogram);
            int max = int.MinValue;
            for (int row = 0; row < img.Height; ++row)
                for (int col = 0; col < img.Width; ++col)
                {
                    var x = img.GetPixel(col, row);
                    if (cl == 'R')
                    {
                        ++color[x.R];
                        if (color[x.R] > max)
                            max = color[x.R];
                    }
                    if (cl == 'G')
                    {
                        ++color[x.G];
                        if (color[x.G] > max)
                            max = color[x.G];
                    }
                    if (cl == 'B')
                    {
                        ++color[x.B];
                        if (color[x.B] > max)
                            max = color[x.B];
                    }
                }
            Pen pen = new Pen(Color.Black);

            if (cl == 'R') pen.Color = Color.Red;
            if (cl == 'G') pen.Color = Color.Green;
            if (cl == 'B') pen.Color = Color.Blue;

            for (int i = 0; i < 255; ++i)
            {
                graph.DrawLine(pen, i, 255 - (int)(256 * color[i] / (double)max),
                i + 1, 255 - (int)(256 * color[i + 1] / (double)max));
            }
            graph.Dispose();
            return histogram;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            // диалог для выбора файла
            var dialog = new OpenFileDialog();
            // фильтр форматов файлов
            dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            // если в диалоге была нажата кнопка ОК
            if (DialogResult.OK != dialog.ShowDialog()) return;
            Bitmap bitmap;
            try
            {
                // загружаем изображение
                bitmap = new Bitmap(dialog.FileName);
            }
            catch // в случае ошибки выводим MessageBox
            {
                MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            pictureBox1.Image = bitmap;
            var R = ConvertColor(bitmap, redColor);
            var G = ConvertColor(bitmap, greenColor);
            var B = ConvertColor(bitmap, blueColor);
            pictureBox2.Image = R;
            pictureBox3.Image = G;
            pictureBox4.Image = B;
            pictureBox6.Image = Histogram(R, 'R');
            pictureBox5.Image = Histogram(G, 'G');
            pictureBox7.Image = Histogram(B, 'B');
        }
    }
}

