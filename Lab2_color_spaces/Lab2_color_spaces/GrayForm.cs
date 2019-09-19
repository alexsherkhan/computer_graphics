using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_color_spaces
{
    public partial class GrayForm : Form
    {
        public PictureBox pic1;

        public GrayForm()
        {
            InitializeComponent();
        }

        // Ч/Б
        private void gray(PictureBox pictureBox1, PictureBox pictureBox2, bool average = true)
        {
            if (pictureBox1.Image != null) // если изображение в pictureBox1 имеется
            {
                // создаём Bitmap из изображения, находящегося в pictureBox1
                Bitmap input = new Bitmap(pictureBox1.Image);
                // создаём Bitmap для черно-белого изображения
                Bitmap output = new Bitmap(input.Width, input.Height);
                // перебираем в циклах все пиксели исходного изображения
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        // получаем (i, j) пиксель
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        // получаем компоненты цветов пикселя
                        float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                        float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                        float B = (float)(pixel & 0x000000FF); // синий

                        // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
                        if (average)          //R = G = B = (R + G + B) / 3.0f;
                            R = G = B = 0.299f * R + 0.587f * G + 0.114f * B;
                        else
                            R = G = B = 0.2126f * R + 0.7152f * G + 0.0722f * B;

                        // собираем новый пиксель по частям (по каналам)
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        // добавляем его в Bitmap нового изображения
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                // выводим черно-белый Bitmap в pictureBox2
                pictureBox2.Image = output;

            }
        }

        private void difference_gray(PictureBox pictureBox1, PictureBox pictureBox2, PictureBox pictureBox3)
        {
            if (pictureBox1.Image != null && pictureBox2.Image != null) // если изображение в pictureBox1 имеется
            {
                // создаём Bitmap из изображения, находящегося в pictureBox1 и pictureBox1
                Bitmap input1 = new Bitmap(pictureBox1.Image);
                Bitmap input2 = new Bitmap(pictureBox2.Image);

                // создаём Bitmap для черно-белого изображения
                Bitmap output = new Bitmap(input1.Width, input1.Height);

                // перебираем в циклах все пиксели исходного изображения
                for (int j = 0; j < input1.Height; j++)
                    for (int i = 0; i < input1.Width; i++)
                    {
                        // получаем (i, j) пиксель
                        UInt32 pixel1 = (UInt32)(input1.GetPixel(i, j).ToArgb());
                        UInt32 pixel2 = (UInt32)(input2.GetPixel(i, j).ToArgb());

                        // получаем компоненты цветов пикселя
                        float R1 = (float)((pixel1 & 0x00FF0000) >> 16); // красный
                        float G1 = (float)((pixel1 & 0x0000FF00) >> 8); // зеленый
                        float B1 = (float)(pixel1 & 0x000000FF); // синий

                        float R2 = (float)((pixel2 & 0x00FF0000) >> 16); // красный
                        float G2 = (float)((pixel2 & 0x0000FF00) >> 8); // зеленый
                        float B2 = (float)(pixel2 & 0x000000FF); // синий

                        R1 -= R2;
                        G1 -= G2;
                        B1 -= B2;

                        // собираем новый пиксель по частям (по каналам)
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R1 << 16) | ((UInt32)G1 << 8) | ((UInt32)B1);
                        // добавляем его в Bitmap нового изображения
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                // выводим черно-белый Bitmap в pictureBox2
                pictureBox3.Image = output;

            }
        }

        public Image CalculateBarChart(Image image, char cl)
        {
            Bitmap barChart = null;
            if (image != null)
            {
                // определяем размеры гистограммы. В идеале, ширина должны быть кратна 768 - 
                // по пикселю на каждый столбик каждого из каналов
                int width = 768, height = 600;
                // получаем битмап из изображения
                Bitmap bmp = new Bitmap(image);
                // создаем саму гистограмму
                barChart = new Bitmap(width, height);
                // создаем массивы, в котором будут содержаться количества повторений для каждого из значений каналов.
                // индекс соответствует значению канала
                int[] R = new int[256];
                int[] G = new int[256];
                int[] B = new int[256];
                int i, j;
                Color color;
                // собираем статистику для изображения
                for (i = 0; i < bmp.Width; ++i)
                    for (j = 0; j < bmp.Height; ++j)
                    {
                        color = bmp.GetPixel(i, j);
                        ++R[color.R];
                        ++G[color.G];
                        ++B[color.B];
                    }
                // находим самый высокий столбец, чтобы корректно масштабировать гистограмму по высоте
                int max = 0;
                for (i = 0; i < 256; ++i)
                {
                    if (R[i] > max)
                        max = R[i];
                    if (G[i] > max)
                        max = G[i];
                    if (B[i] > max)
                        max = B[i];
                }
                // определяем коэффициент масштабирования по высоте
                double point = (double)max / height;
                // отрисовываем столбец за столбцом нашу гистограмму с учетом масштаба
                for (i = 0; i < width - 3; ++i)
                {
                    if ((cl == 'R') || (cl == 'A'))
                    {
                        for (j = height - 1; j > height - R[i / 3] / point; --j)
                        {
                            if (cl == 'R') barChart.SetPixel(i, j, Color.Red);
                            else barChart.SetPixel(i, j, Color.Black);
                        }
                        if (cl == 'R') ++i;
                    }

                    if ((cl == 'G') || (cl == 'A'))
                    {
                        for (j = height - 1; j > height - G[i / 3] / point; --j)
                        {
                            if (cl == 'G') barChart.SetPixel(i, j, Color.Green);
                            else barChart.SetPixel(i, j, Color.Black);
                        }
                        if (cl == 'G') ++i;
                    }

                    if ((cl == 'B') || (cl == 'A'))
                    {
                        for (j = height - 1; j > height - B[i / 3] / point; --j)
                        {
                            if (cl == 'B') barChart.SetPixel(i, j, Color.Blue);
                            else barChart.SetPixel(i, j, Color.Black);
                        }
                        if (cl == 'B') ++i;
                    }
                }
            }
            else
                barChart = new Bitmap(1, 1);
            return barChart;
        }

        private void GrayForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = pic1.Image;
            gray(pictureBox1, pictureBox2);
            gray(pictureBox1, pictureBox3,false);
            difference_gray(pictureBox2, pictureBox3, pictureBox4);
            pictureBox5.Image = CalculateBarChart(pictureBox2.Image, 'A');
            pictureBox6.Image = CalculateBarChart(pictureBox3.Image, 'A');
        }
    }
}
