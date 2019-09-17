//2. Выделить из полноцветного изображения один 
//из каналов R, G, B  и вывести результат. Построить гистограмму по цветам.
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
    public partial class Form1 : Form
    {

        public Form1()
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
       

        private void button1_Click(object sender, EventArgs e)
        {
            // диалог для выбора файла
            OpenFileDialog ofd = new OpenFileDialog();
            // фильтр форматов файлов
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            // если в диалоге была нажата кнопка ОК
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // загружаем изображение
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch // в случае ошибки выводим MessageBox
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gray(pictureBox1, pictureBox5);
            gray(pictureBox1, pictureBox6,false);
            difference_gray(pictureBox5, pictureBox6, pictureBox7);
        }
    }
}

