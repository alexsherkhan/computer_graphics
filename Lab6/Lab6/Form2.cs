using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form2 : Form
    {
        //       internal delegate float Function(float x, float y);
        internal Function f;

        public float X0 { get; set; }
        public float X1 { get; set; }
        public float Y0 { get; set; }
        public float Y1 { get; set; }
        public int Cnt_of_breaks { get; set; }

        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void textBox_KeyPress_double(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '-') && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void Button_ok_Click_1(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    f = (x, y) => x + y;
                    break;
                case 1:
                    f = (x, y) => x * x + y * y;
                    break;
                case 2:
                    f = (x, y) => (float)Math.Cos(x * x + y * y + 1) / (x * x + y * y + 1);
                    break;
                case 3:
                    f = (x, y) => (float)(Math.Sin(x) * Math.Cos(y));
                    break;
                case 4:
                    f = (x, y) => (float)(Math.Sin(x) + Math.Cos(y));
                    break;
                default:
                    f = (x, y) => 0;
                    break;
            }

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                X0 = float.Parse(textBox1.Text);
                X1 = float.Parse(textBox2.Text);
                Y0 = float.Parse(textBox3.Text);
                Y1 = float.Parse(textBox4.Text);

                Cnt_of_breaks = (int)numericUpDown1.Value;

                Close();
            }
        }
    }
}
