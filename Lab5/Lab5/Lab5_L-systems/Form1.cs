using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_L_systems
{
    public partial class Form1 : Form
    {
        Fractal fractal;

        Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                numericUpDown1.Value = 0;
                fractal = new Fractal(ofd.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fractal == null)
            {
                MessageBox.Show("Загрузите описание фрактала из Меню.");
                return;
            }
            fractal.Reset();
            for (int i = 0; i < (int)numericUpDown1.Value; ++i)
                fractal.Iterate();
            fractal.ParseSourse();
            float ScaleX = (fractal.maxX - fractal.minX) / ((pictureBox1.Width - 20) * 1.0f);
            float ScaleY = (fractal.maxY - fractal.minY) / ((pictureBox1.Height - 20) * 1.0f);
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(pictureBox1.BackColor);
            foreach (MyPoint mp in fractal.l)
                mp.Draw(graphics, (-fractal.minX + 3), (-fractal.minY + 3), ScaleX, ScaleY);
            pictureBox1.Refresh();
        }
    }
}
