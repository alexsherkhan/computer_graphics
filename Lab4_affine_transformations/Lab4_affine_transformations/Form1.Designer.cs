using System.Windows.Forms;

namespace Lab4_affine_transformations
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.setPoint = new System.Windows.Forms.RadioButton();
            this.selectedPr = new System.Windows.Forms.RadioButton();
            this.rbPolygon = new System.Windows.Forms.RadioButton();
            this.rbEdge = new System.Windows.Forms.RadioButton();
            this.rbPoint = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.newPolygon = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.setPoint);
            this.groupBox1.Controls.Add(this.selectedPr);
            this.groupBox1.Controls.Add(this.rbPolygon);
            this.groupBox1.Controls.Add(this.rbEdge);
            this.groupBox1.Controls.Add(this.rbPoint);
            this.groupBox1.Location = new System.Drawing.Point(14, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(207, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите примитив";
            // 
            // setPoint
            // 
            this.setPoint.AutoSize = true;
            this.setPoint.Location = new System.Drawing.Point(8, 102);
            this.setPoint.Margin = new System.Windows.Forms.Padding(4);
            this.setPoint.Name = "setPoint";
            this.setPoint.Size = new System.Drawing.Size(117, 21);
            this.setPoint.TabIndex = 18;
            this.setPoint.Text = "Задать точку";
            this.setPoint.UseVisualStyleBackColor = true;
            // 
            // selectedPr
            // 
            this.selectedPr.AutoSize = true;
            this.selectedPr.Location = new System.Drawing.Point(8, 81);
            this.selectedPr.Margin = new System.Windows.Forms.Padding(4);
            this.selectedPr.Name = "selectedPr";
            this.selectedPr.Size = new System.Drawing.Size(161, 21);
            this.selectedPr.TabIndex = 17;
            this.selectedPr.Text = "Выделить приметив";
            this.selectedPr.UseVisualStyleBackColor = true;
            // 
            // rbPolygon
            // 
            this.rbPolygon.AutoSize = true;
            this.rbPolygon.Location = new System.Drawing.Point(8, 62);
            this.rbPolygon.Margin = new System.Windows.Forms.Padding(4);
            this.rbPolygon.Name = "rbPolygon";
            this.rbPolygon.Size = new System.Drawing.Size(127, 21);
            this.rbPolygon.TabIndex = 3;
            this.rbPolygon.Text = "Многоугольник";
            this.rbPolygon.UseVisualStyleBackColor = true;
            // 
            // rbEdge
            // 
            this.rbEdge.AutoSize = true;
            this.rbEdge.Location = new System.Drawing.Point(8, 43);
            this.rbEdge.Margin = new System.Windows.Forms.Padding(4);
            this.rbEdge.Name = "rbEdge";
            this.rbEdge.Size = new System.Drawing.Size(85, 21);
            this.rbEdge.TabIndex = 2;
            this.rbEdge.TabStop = true;
            this.rbEdge.Text = "Отрезок";
            this.rbEdge.UseVisualStyleBackColor = true;
            // 
            // rbPoint
            // 
            this.rbPoint.AutoSize = true;
            this.rbPoint.Checked = true;
            this.rbPoint.Location = new System.Drawing.Point(8, 23);
            this.rbPoint.Margin = new System.Windows.Forms.Padding(4);
            this.rbPoint.Name = "rbPoint";
            this.rbPoint.Size = new System.Drawing.Size(69, 21);
            this.rbPoint.TabIndex = 1;
            this.rbPoint.TabStop = true;
            this.rbPoint.Text = "Точка";
            this.rbPoint.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(22, 544);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 32);
            this.button2.TabIndex = 6;
            this.button2.Text = "Найти";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 524);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Точка пересечения двух ребер:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 580);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Принадлежность точки полигону:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(24, 601);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(184, 28);
            this.button3.TabIndex = 9;
            this.button3.Text = "Метод углов";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(24, 637);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(184, 28);
            this.button4.TabIndex = 10;
            this.button4.Text = "Метод лучей";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(22, 483);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(113, 28);
            this.button5.TabIndex = 11;
            this.button5.Text = "Повернуть на";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(139, 486);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(61, 22);
            this.textBox1.TabIndex = 12;
            this.textBox1.Text = "0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(845, 750);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.newPolygon);
            this.splitContainer1.Panel2.Controls.Add(this.button9);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.button6);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button5);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1057, 750);
            this.splitContainer1.SplitterDistance = 845;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // newPolygon
            // 
            this.newPolygon.Location = new System.Drawing.Point(15, 141);
            this.newPolygon.Margin = new System.Windows.Forms.Padding(4);
            this.newPolygon.Name = "newPolygon";
            this.newPolygon.Size = new System.Drawing.Size(184, 28);
            this.newPolygon.TabIndex = 17;
            this.newPolygon.Text = "Новый многоугольник";
            this.newPolygon.UseVisualStyleBackColor = true;
            this.newPolygon.Click += new System.EventHandler(this.newPolygon_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(24, 713);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(184, 28);
            this.button9.TabIndex = 16;
            this.button9.Text = "Классифицировать";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 675);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.MaximumSize = new System.Drawing.Size(250, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(216, 34);
            this.label5.TabIndex = 15;
            this.label5.Text = "Классифицировать положение точки относительно ребра";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Location = new System.Drawing.Point(14, 203);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(220, 275);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Аффинные преобразования";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.comboBox3.Location = new System.Drawing.Point(9, 244);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 24);
            this.comboBox3.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Масштаб";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "относительно произвольной точки",
            "относительно своего центра"});
            this.comboBox2.Location = new System.Drawing.Point(10, 197);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(201, 24);
            this.comboBox2.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 162);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 28);
            this.button1.TabIndex = 10;
            this.button1.Text = "Масштабирование";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(114, 58);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(59, 22);
            this.textBox3.TabIndex = 9;
            this.textBox3.Text = "10";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(25, 58);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(59, 22);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "x";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "вокруг произвольной точки",
            "вокруг своего центра"});
            this.comboBox1.Location = new System.Drawing.Point(10, 131);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(201, 24);
            this.comboBox1.TabIndex = 5;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(9, 96);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(184, 28);
            this.button8.TabIndex = 4;
            this.button8.Text = "Поворот ";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(8, 23);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(184, 28);
            this.button7.TabIndex = 3;
            this.button7.Text = "Сместить на ";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(15, 172);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(184, 28);
            this.button6.TabIndex = 13;
            this.button6.Text = "Очистить сцену";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 750);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Lab4_affine_transformations";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton rbPolygon;
        private RadioButton rbEdge;
        private RadioButton rbPoint;
        private Button button2;
        private Label label2;
        private Label label3;
        private Button button3;
        private Button button4;
        private Button button5;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private SplitContainer splitContainer1;
        private GroupBox groupBox2;
        private TextBox textBox3;
        private TextBox textBox2;
        private Label label4;
        private Label label1;
        private ComboBox comboBox1;
        private Button button8;
        private Button button7;
        private Button button6;
        private ComboBox comboBox2;
        private Button button1;
        private Button button9;
        private Label label5;
        private ComboBox comboBox3;
        private Label label6;
        private RadioButton selectedPr;
        private Button newPolygon;
        private RadioButton setPoint;
    }
}


