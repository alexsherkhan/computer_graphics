//2. Выделить из полноцветного изображения один 
//из каналов R, G, B  и вывести результат. Построить гистограмму по цветам.
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

        private static ColorMatrix redColour = new ColorMatrix(new float[][] {
            new float [] {1, 0, 0, 0, 0},
            new float [] {0, 0, 0, 0, 0 },
            new float [] {0, 0, 0, 0, 0 },
            new float [] {0, 0, 0, 1, 0 },
            new float [] {0, 0, 0, 0, 1 }
        });
        private static ColorMatrix greenColour = new ColorMatrix(new float[][] {
            new float [] {0, 0, 0, 0, 0},
            new float [] {1, 0, 0, 0, 0 },
            new float [] {0, 0, 0, 0, 0 },
            new float [] {0, 0, 0, 1, 0 },
            new float [] {0, 0, 0, 0, 1 }
        });
        private static ColorMatrix blueColour = new ColorMatrix(new float[][] {
            new float [] {0, 0, 0, 0, 0},
            new float [] {0, 0, 0, 0, 0 },
            new float [] {1, 0, 0, 0, 0 },
            new float [] {0, 0, 0, 1, 0 },
            new float [] {0, 0, 0, 0, 1 }
        });
       
       // private static Bitmap ConvertColor(Bitmap pic, ColorMatrix transform) {
            //return;
      //  }
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
                    pictureBox2.Image = new Bitmap(ofd.FileName);
                }
                catch // в случае ошибки выводим MessageBox
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

    }
}

