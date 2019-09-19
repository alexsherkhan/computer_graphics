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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            trackBar2.Minimum = 0;
            trackBar2.Maximum = 360;
            trackBar3.Minimum = 0;
            trackBar3.Maximum = 100;
            trackBar4.Minimum = 0;
            trackBar4.Maximum = 100;
        }
        // Ч/Б
        private void hsv(PictureBox pictureBox1, PictureBox pictureBox2)
        {
            if (pictureBox1.Image != null)
            {
                if (trackBar2.Value == 0 && trackBar3.Value == 0 && trackBar4.Value == 0)
                {
                    Bitmap input = new Bitmap(pictureBox1.Image);
                    Bitmap output = new Bitmap(input.Width, input.Height);
                    float sumH = 0;
                    float sumS = 0;
                    float sumV = 0;
                    for (int j = 0; j < input.Height; j++)
                        for (int i = 0; i < input.Width; i++)
                        {
                            UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                            float R = (float)((pixel & 0x00FF0000) >> 16) / 255;
                            float G = (float)((pixel & 0x0000FF00) >> 8) / 255;
                            float B = (float)(pixel & 0x000000FF) / 255;
                            float H = 0;
                            float S = 0;
                            float V = 0;
                            RGBtoHSV(R, G, B, ref H, ref S, ref V);
                            Color color = ColorFromHSV(H, S, V);
                            sumH += H;
                            sumS += S;
                            sumV += V;
                            // добавляем его в Bitmap нового изображения
                            output.SetPixel(i, j, color);
                        }
                    pictureBox2.Image = output;
                    trackBar2.Value = (int)(((sumH * 100) / 360) / (input.Height * input.Width));
                    trackBar3.Value = (int)(sumS * 100 / (input.Height * input.Width));
                    trackBar4.Value = (int)(sumV * 100 / (input.Height * input.Width));
                }
                else
                {
                    Bitmap input = new Bitmap(pictureBox1.Image);
                    Bitmap output = new Bitmap(input.Width, input.Height);
                    for (int j = 0; j < input.Height; j++)
                        for (int i = 0; i < input.Width; i++)
                        {
                            UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                            float R = (float)((pixel & 0x00FF0000) >> 16) / 255;
                            float G = (float)((pixel & 0x0000FF00) >> 8) / 255;
                            float B = (float)(pixel & 0x000000FF) / 255;
                            float H = 0;
                            float S = 0;
                            float V = 0;
                            RGBtoHSV(R, G, B, ref H, ref S, ref V);
                            H *= (float)trackBar2.Value / 100;
                            S *= (float)trackBar3.Value / 100;
                            V *= (float)trackBar4.Value / 100;
                            Color color = ColorFromHSV(H, S, V);
                            // добавляем его в Bitmap нового изображения
                            output.SetPixel(i, j, color);
                        }
                    pictureBox2.Image = output;
                }
            }
        }
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
        void RGBtoHSV(float fR, float fG, float fB, ref float fH, ref float fS,ref float fV)
        {
            float fCMax = Math.Max(Math.Max(fR, fG), fB);
            float fCMin = Math.Min(Math.Min(fR, fG), fB);
            float fDelta = fCMax - fCMin;

            if (fDelta > 0)
            {
                if (fCMax == fR)
                {
                    if (fG >= fB) {
                        fH = 60 * ((fG - fB) / fDelta);
                    }
                    else
                    {
                        fH = 60 * ((fG - fB) / fDelta) + 360;
                    }
                }
                else if (fCMax == fG)
                {
                    fH = 60 * ((fB - fR) / fDelta) + 120;
                }
                else if (fCMax == fB)
                {
                    fH = 60 * ((fR - fG) / fDelta) + 240;
                }

                if (fCMax > 0)
                {
                    fS = fDelta / fCMax;
                }
                else
                {
                    fS = 0;
                }

                fV = fCMax;
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
                    Image newImage = Image.FromFile(ofd.FileName);
                    pictureBox1.Image = new Bitmap(newImage, pictureBox1.Size);
                }
                catch // в случае ошибки выводим MessageBox
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            hsv(pictureBox1, pictureBox3);
        }
    }
}
