using System;
using System.IO;
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
            
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            pictureBox1.Image = new Bitmap(projectDirectory + "\\img\\fruits.jpg");
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
        
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) 
            {
                GrayForm gf = new GrayForm();
                gf.pic1 = this.pictureBox1;
                gf.Show();
            }
            else MessageBox.Show("Не выбран файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

