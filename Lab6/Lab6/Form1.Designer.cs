﻿namespace Lab6
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_cube = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_translation = new System.Windows.Forms.Label();
            this.label_rotation = new System.Windows.Forms.Label();
            this.trans_x = new System.Windows.Forms.TextBox();
            this.trans_y = new System.Windows.Forms.TextBox();
            this.trans_z = new System.Windows.Forms.TextBox();
            this.label_scaling = new System.Windows.Forms.Label();
            this.scaling_x = new System.Windows.Forms.TextBox();
            this.scaling_y = new System.Windows.Forms.TextBox();
            this.scaling_z = new System.Windows.Forms.TextBox();
            this.button_exec = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.clear_button = new System.Windows.Forms.Button();
            this.rot_line_x1 = new System.Windows.Forms.TextBox();
            this.rot_line_y1 = new System.Windows.Forms.TextBox();
            this.rot_line_z1 = new System.Windows.Forms.TextBox();
            this.rot_line_z2 = new System.Windows.Forms.TextBox();
            this.rot_line_y2 = new System.Windows.Forms.TextBox();
            this.rot_line_x2 = new System.Windows.Forms.TextBox();
            this.button_tetraeder = new System.Windows.Forms.Button();
            this.button_octaeder = new System.Windows.Forms.Button();
            this.button_ikosaeder = new System.Windows.Forms.Button();
            this.button_dodecaeder = new System.Windows.Forms.Button();
            this.button_refl_x = new System.Windows.Forms.Button();
            this.button_refl_y = new System.Windows.Forms.Button();
            this.button_refl_z = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.angle_x = new System.Windows.Forms.TextBox();
            this.angle_y = new System.Windows.Forms.TextBox();
            this.angle_z = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button33 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button_exec_camera = new System.Windows.Forms.Button();
            this.trans_z_camera = new System.Windows.Forms.TextBox();
            this.trans_y_camera = new System.Windows.Forms.TextBox();
            this.trans_x_camera = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.camera_translation = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.camera_x_rotate = new System.Windows.Forms.TextBox();
            this.camera_y_rotate = new System.Windows.Forms.TextBox();
            this.camera_z_rotate = new System.Windows.Forms.TextBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.light_x = new System.Windows.Forms.TextBox();
            this.light_y = new System.Windows.Forms.TextBox();
            this.light_z = new System.Windows.Forms.TextBox();
            this.label_color = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(381, 34);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(799, 604);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_cube
            // 
            this.button_cube.Location = new System.Drawing.Point(19, 505);
            this.button_cube.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_cube.Name = "button_cube";
            this.button_cube.Size = new System.Drawing.Size(101, 60);
            this.button_cube.TabIndex = 1;
            this.button_cube.Text = "Гексаэдр";
            this.button_cube.UseVisualStyleBackColor = true;
            this.button_cube.Click += new System.EventHandler(this.Button_cube_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "перспективная",
            "изометрическая",
            "ортографическая (ось X)",
            "ортографическая (ось Y)",
            "ортографическая (ось Z)"});
            this.comboBox1.Location = new System.Drawing.Point(16, 34);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(180, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите вид проекции";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Афинные преобразования";
            // 
            // label_translation
            // 
            this.label_translation.AutoSize = true;
            this.label_translation.Location = new System.Drawing.Point(16, 105);
            this.label_translation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_translation.Name = "label_translation";
            this.label_translation.Size = new System.Drawing.Size(127, 17);
            this.label_translation.TabIndex = 5;
            this.label_translation.Text = "Смещение (x, y, z)";
            // 
            // label_rotation
            // 
            this.label_rotation.AutoSize = true;
            this.label_rotation.Location = new System.Drawing.Point(16, 145);
            this.label_rotation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_rotation.Name = "label_rotation";
            this.label_rotation.Size = new System.Drawing.Size(113, 17);
            this.label_rotation.TabIndex = 6;
            this.label_rotation.Text = "Поворот (angle)";
            // 
            // trans_x
            // 
            this.trans_x.Location = new System.Drawing.Point(153, 101);
            this.trans_x.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trans_x.Name = "trans_x";
            this.trans_x.Size = new System.Drawing.Size(40, 22);
            this.trans_x.TabIndex = 8;
            this.trans_x.Text = "0";
            // 
            // trans_y
            // 
            this.trans_y.Location = new System.Drawing.Point(203, 101);
            this.trans_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trans_y.Name = "trans_y";
            this.trans_y.Size = new System.Drawing.Size(40, 22);
            this.trans_y.TabIndex = 9;
            this.trans_y.Text = "0";
            // 
            // trans_z
            // 
            this.trans_z.Location = new System.Drawing.Point(252, 101);
            this.trans_z.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trans_z.Name = "trans_z";
            this.trans_z.Size = new System.Drawing.Size(40, 22);
            this.trans_z.TabIndex = 10;
            this.trans_z.Text = "0";
            // 
            // label_scaling
            // 
            this.label_scaling.AutoSize = true;
            this.label_scaling.Location = new System.Drawing.Point(16, 190);
            this.label_scaling.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_scaling.Name = "label_scaling";
            this.label_scaling.Size = new System.Drawing.Size(118, 17);
            this.label_scaling.TabIndex = 12;
            this.label_scaling.Text = "Масштаб (x, y, z)";
            // 
            // scaling_x
            // 
            this.scaling_x.Location = new System.Drawing.Point(153, 186);
            this.scaling_x.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scaling_x.Name = "scaling_x";
            this.scaling_x.Size = new System.Drawing.Size(40, 22);
            this.scaling_x.TabIndex = 25;
            this.scaling_x.Text = "1";
            // 
            // scaling_y
            // 
            this.scaling_y.Location = new System.Drawing.Point(203, 186);
            this.scaling_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scaling_y.Name = "scaling_y";
            this.scaling_y.Size = new System.Drawing.Size(40, 22);
            this.scaling_y.TabIndex = 28;
            this.scaling_y.Text = "1";
            // 
            // scaling_z
            // 
            this.scaling_z.Location = new System.Drawing.Point(252, 186);
            this.scaling_z.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scaling_z.Name = "scaling_z";
            this.scaling_z.Size = new System.Drawing.Size(40, 22);
            this.scaling_z.TabIndex = 29;
            this.scaling_z.Text = "1";
            // 
            // button_exec
            // 
            this.button_exec.Location = new System.Drawing.Point(16, 369);
            this.button_exec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_exec.Name = "button_exec";
            this.button_exec.Size = new System.Drawing.Size(100, 28);
            this.button_exec.TabIndex = 37;
            this.button_exec.Text = "Выполнить";
            this.button_exec.UseVisualStyleBackColor = true;
            this.button_exec.Click += new System.EventHandler(this.Button_exec_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 241);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 17);
            this.label3.TabIndex = 39;
            this.label3.Text = "Выбрать прямую для поворота";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Прямая, параллельная оси X",
            "Прямая, параллельная оси Y",
            "Прямая, параллельная оси Z",
            "Задать свою прямую"});
            this.comboBox2.Location = new System.Drawing.Point(16, 261);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(276, 24);
            this.comboBox2.TabIndex = 40;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(124, 369);
            this.clear_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(100, 28);
            this.clear_button.TabIndex = 41;
            this.clear_button.Text = "Очистить";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // rot_line_x1
            // 
            this.rot_line_x1.Enabled = false;
            this.rot_line_x1.Location = new System.Drawing.Point(16, 293);
            this.rot_line_x1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_x1.Name = "rot_line_x1";
            this.rot_line_x1.Size = new System.Drawing.Size(68, 22);
            this.rot_line_x1.TabIndex = 42;
            this.rot_line_x1.Text = "0";
            // 
            // rot_line_y1
            // 
            this.rot_line_y1.Enabled = false;
            this.rot_line_y1.Location = new System.Drawing.Point(116, 293);
            this.rot_line_y1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_y1.Name = "rot_line_y1";
            this.rot_line_y1.Size = new System.Drawing.Size(68, 22);
            this.rot_line_y1.TabIndex = 43;
            this.rot_line_y1.Text = "0";
            // 
            // rot_line_z1
            // 
            this.rot_line_z1.Enabled = false;
            this.rot_line_z1.Location = new System.Drawing.Point(223, 293);
            this.rot_line_z1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_z1.Name = "rot_line_z1";
            this.rot_line_z1.Size = new System.Drawing.Size(69, 22);
            this.rot_line_z1.TabIndex = 44;
            this.rot_line_z1.Text = "0";
            // 
            // rot_line_z2
            // 
            this.rot_line_z2.Enabled = false;
            this.rot_line_z2.Location = new System.Drawing.Point(223, 325);
            this.rot_line_z2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_z2.Name = "rot_line_z2";
            this.rot_line_z2.Size = new System.Drawing.Size(69, 22);
            this.rot_line_z2.TabIndex = 47;
            this.rot_line_z2.Text = "1";
            // 
            // rot_line_y2
            // 
            this.rot_line_y2.Enabled = false;
            this.rot_line_y2.Location = new System.Drawing.Point(116, 325);
            this.rot_line_y2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_y2.Name = "rot_line_y2";
            this.rot_line_y2.Size = new System.Drawing.Size(68, 22);
            this.rot_line_y2.TabIndex = 46;
            this.rot_line_y2.Text = "1";
            // 
            // rot_line_x2
            // 
            this.rot_line_x2.Enabled = false;
            this.rot_line_x2.Location = new System.Drawing.Point(16, 325);
            this.rot_line_x2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rot_line_x2.Name = "rot_line_x2";
            this.rot_line_x2.Size = new System.Drawing.Size(68, 22);
            this.rot_line_x2.TabIndex = 45;
            this.rot_line_x2.Text = "1";
            // 
            // button_tetraeder
            // 
            this.button_tetraeder.Location = new System.Drawing.Point(128, 505);
            this.button_tetraeder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_tetraeder.Name = "button_tetraeder";
            this.button_tetraeder.Size = new System.Drawing.Size(101, 60);
            this.button_tetraeder.TabIndex = 48;
            this.button_tetraeder.Text = "Тетраэдр";
            this.button_tetraeder.UseVisualStyleBackColor = true;
            this.button_tetraeder.Click += new System.EventHandler(this.Button_tetraeder_Click);
            // 
            // button_octaeder
            // 
            this.button_octaeder.Location = new System.Drawing.Point(237, 505);
            this.button_octaeder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_octaeder.Name = "button_octaeder";
            this.button_octaeder.Size = new System.Drawing.Size(101, 60);
            this.button_octaeder.TabIndex = 49;
            this.button_octaeder.Text = "Октаэдр";
            this.button_octaeder.UseVisualStyleBackColor = true;
            this.button_octaeder.Click += new System.EventHandler(this.Button_octaeder_Click);
            // 
            // button_ikosaeder
            // 
            this.button_ikosaeder.Location = new System.Drawing.Point(128, 572);
            this.button_ikosaeder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_ikosaeder.Name = "button_ikosaeder";
            this.button_ikosaeder.Size = new System.Drawing.Size(101, 60);
            this.button_ikosaeder.TabIndex = 50;
            this.button_ikosaeder.Text = "Икосаэдр";
            this.button_ikosaeder.UseVisualStyleBackColor = true;
            this.button_ikosaeder.Click += new System.EventHandler(this.Button_ikosaeder_Click);
            // 
            // button_dodecaeder
            // 
            this.button_dodecaeder.Location = new System.Drawing.Point(19, 572);
            this.button_dodecaeder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_dodecaeder.Name = "button_dodecaeder";
            this.button_dodecaeder.Size = new System.Drawing.Size(100, 60);
            this.button_dodecaeder.TabIndex = 51;
            this.button_dodecaeder.Text = "Додекаэдр";
            this.button_dodecaeder.UseVisualStyleBackColor = true;
            this.button_dodecaeder.Click += new System.EventHandler(this.Button_dodecaeder_Click);
            // 
            // button_refl_x
            // 
            this.button_refl_x.Location = new System.Drawing.Point(16, 452);
            this.button_refl_x.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_refl_x.Name = "button_refl_x";
            this.button_refl_x.Size = new System.Drawing.Size(69, 28);
            this.button_refl_x.TabIndex = 52;
            this.button_refl_x.Text = "X";
            this.button_refl_x.UseVisualStyleBackColor = true;
            this.button_refl_x.Click += new System.EventHandler(this.Button_refl_x_Click);
            // 
            // button_refl_y
            // 
            this.button_refl_y.Location = new System.Drawing.Point(93, 452);
            this.button_refl_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_refl_y.Name = "button_refl_y";
            this.button_refl_y.Size = new System.Drawing.Size(69, 28);
            this.button_refl_y.TabIndex = 53;
            this.button_refl_y.Text = "Y";
            this.button_refl_y.UseVisualStyleBackColor = true;
            this.button_refl_y.Click += new System.EventHandler(this.Button_refl_y_Click);
            // 
            // button_refl_z
            // 
            this.button_refl_z.Location = new System.Drawing.Point(171, 452);
            this.button_refl_z.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_refl_z.Name = "button_refl_z";
            this.button_refl_z.Size = new System.Drawing.Size(69, 28);
            this.button_refl_z.TabIndex = 54;
            this.button_refl_z.Text = "Z";
            this.button_refl_z.UseVisualStyleBackColor = true;
            this.button_refl_z.Click += new System.EventHandler(this.Button_refl_z_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 432);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 55;
            this.label4.Text = "Отразить по:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(235, 380);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 17);
            this.label9.TabIndex = 77;
            this.label9.Text = "Центр фигуры";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(235, 410);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 17);
            this.label10.TabIndex = 78;
            this.label10.Text = "label10";
            // 
            // angle_x
            // 
            this.angle_x.Location = new System.Drawing.Point(153, 145);
            this.angle_x.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.angle_x.Name = "angle_x";
            this.angle_x.Size = new System.Drawing.Size(40, 22);
            this.angle_x.TabIndex = 79;
            this.angle_x.Text = "0";
            // 
            // angle_y
            // 
            this.angle_y.Location = new System.Drawing.Point(203, 145);
            this.angle_y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.angle_y.Name = "angle_y";
            this.angle_y.Size = new System.Drawing.Size(40, 22);
            this.angle_y.TabIndex = 80;
            this.angle_y.Text = "0";
            // 
            // angle_z
            // 
            this.angle_z.Location = new System.Drawing.Point(251, 145);
            this.angle_z.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.angle_z.Name = "angle_z";
            this.angle_z.Size = new System.Drawing.Size(40, 22);
            this.angle_z.TabIndex = 81;
            this.angle_z.Text = "0";
            this.angle_z.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 658);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 46);
            this.button1.TabIndex = 82;
            this.button1.Text = "Загрузить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(171, 658);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 46);
            this.button2.TabIndex = 83;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button33
            // 
            this.button33.Location = new System.Drawing.Point(238, 573);
            this.button33.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(101, 59);
            this.button33.TabIndex = 84;
            this.button33.Text = "Фигура вращения";
            this.button33.UseVisualStyleBackColor = true;
            this.button33.Click += new System.EventHandler(this.button33_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(381, 658);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 23);
            this.button3.TabIndex = 84;
            this.button3.Text = "график функции";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.Location = new System.Drawing.Point(1198, 34);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(799, 604);
            this.pictureBox3.TabIndex = 85;
            this.pictureBox3.TabStop = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(1584, 729);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 21);
            this.radioButton2.TabIndex = 100;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Z-буфер";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_Checked);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(1584, 701);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(102, 21);
            this.radioButton1.TabIndex = 99;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Отсечения";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // button_exec_camera
            // 
            this.button_exec_camera.Location = new System.Drawing.Point(1586, 811);
            this.button_exec_camera.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_exec_camera.Name = "button_exec_camera";
            this.button_exec_camera.Size = new System.Drawing.Size(100, 28);
            this.button_exec_camera.TabIndex = 98;
            this.button_exec_camera.Text = "Выполнить";
            this.button_exec_camera.UseVisualStyleBackColor = true;
            this.button_exec_camera.Click += new System.EventHandler(this.button_exec_camera_Click);
            // 
            // trans_z_camera
            // 
            this.trans_z_camera.Location = new System.Drawing.Point(1331, 740);
            this.trans_z_camera.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trans_z_camera.Name = "trans_z_camera";
            this.trans_z_camera.Size = new System.Drawing.Size(40, 22);
            this.trans_z_camera.TabIndex = 93;
            this.trans_z_camera.Text = "0";
            // 
            // trans_y_camera
            // 
            this.trans_y_camera.Location = new System.Drawing.Point(1283, 740);
            this.trans_y_camera.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trans_y_camera.Name = "trans_y_camera";
            this.trans_y_camera.Size = new System.Drawing.Size(40, 22);
            this.trans_y_camera.TabIndex = 92;
            this.trans_y_camera.Text = "0";
            // 
            // trans_x_camera
            // 
            this.trans_x_camera.Location = new System.Drawing.Point(1235, 740);
            this.trans_x_camera.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.trans_x_camera.Name = "trans_x_camera";
            this.trans_x_camera.Size = new System.Drawing.Size(40, 22);
            this.trans_x_camera.TabIndex = 91;
            this.trans_x_camera.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1232, 658);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 86;
            this.label5.Text = "Camera";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1232, 711);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 17);
            this.label6.TabIndex = 90;
            this.label6.Text = "Смещение (x, y, z)";
            // 
            // camera_translation
            // 
            this.camera_translation.Location = new System.Drawing.Point(1116, 740);
            this.camera_translation.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.camera_translation.Name = "camera_translation";
            this.camera_translation.Size = new System.Drawing.Size(89, 22);
            this.camera_translation.TabIndex = 101;
            this.camera_translation.Text = "Выполнить";
            this.camera_translation.UseVisualStyleBackColor = true;
            this.camera_translation.Click += new System.EventHandler(this.Camera_translation_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1116, 800);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(89, 22);
            this.button4.TabIndex = 102;
            this.button4.Text = "Выполнить";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1232, 776);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(267, 17);
            this.label7.TabIndex = 103;
            this.label7.Text = "Поворот камеры относительно фигуры";
            // 
            // camera_x_rotate
            // 
            this.camera_x_rotate.Location = new System.Drawing.Point(1235, 800);
            this.camera_x_rotate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.camera_x_rotate.Name = "camera_x_rotate";
            this.camera_x_rotate.Size = new System.Drawing.Size(36, 22);
            this.camera_x_rotate.TabIndex = 104;
            this.camera_x_rotate.Text = "0";
            // 
            // camera_y_rotate
            // 
            this.camera_y_rotate.Location = new System.Drawing.Point(1279, 800);
            this.camera_y_rotate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.camera_y_rotate.Name = "camera_y_rotate";
            this.camera_y_rotate.Size = new System.Drawing.Size(36, 22);
            this.camera_y_rotate.TabIndex = 105;
            this.camera_y_rotate.Text = "0";
            // 
            // camera_z_rotate
            // 
            this.camera_z_rotate.Location = new System.Drawing.Point(1323, 800);
            this.camera_z_rotate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.camera_z_rotate.Name = "camera_z_rotate";
            this.camera_z_rotate.Size = new System.Drawing.Size(36, 22);
            this.camera_z_rotate.TabIndex = 106;
            this.camera_z_rotate.Text = "0";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(1584, 758);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(136, 21);
            this.radioButton3.TabIndex = 107;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Затенение Гуро";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // light_x
            // 
            this.light_x.Location = new System.Drawing.Point(859, 741);
            this.light_x.Name = "light_x";
            this.light_x.Size = new System.Drawing.Size(57, 22);
            this.light_x.TabIndex = 108;
            this.light_x.Text = "0";
            // 
            // light_y
            // 
            this.light_y.Location = new System.Drawing.Point(929, 742);
            this.light_y.Name = "light_y";
            this.light_y.Size = new System.Drawing.Size(57, 22);
            this.light_y.TabIndex = 109;
            this.light_y.Text = "0";
            // 
            // light_z
            // 
            this.light_z.Location = new System.Drawing.Point(1004, 740);
            this.light_z.Name = "light_z";
            this.light_z.Size = new System.Drawing.Size(57, 22);
            this.light_z.TabIndex = 110;
            this.light_z.Text = "300";
            // 
            // label_color
            // 
            this.label_color.AutoSize = true;
            this.label_color.Location = new System.Drawing.Point(1020, 785);
            this.label_color.Name = "label_color";
            this.label_color.Size = new System.Drawing.Size(41, 17);
            this.label_color.TabIndex = 111;
            this.label_color.Text = "Color";
            this.label_color.Click += new System.EventHandler(this.label_color_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(863, 711);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 17);
            this.label8.TabIndex = 112;
            this.label8.Text = "Источник света";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(1584, 783);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(145, 21);
            this.radioButton4.TabIndex = 113;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Текстурирование";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1710, 834);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label_color);
            this.Controls.Add(this.light_z);
            this.Controls.Add(this.light_y);
            this.Controls.Add(this.light_x);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.camera_z_rotate);
            this.Controls.Add(this.camera_y_rotate);
            this.Controls.Add(this.camera_x_rotate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.camera_translation);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button_exec_camera);
            this.Controls.Add(this.trans_z_camera);
            this.Controls.Add(this.trans_y_camera);
            this.Controls.Add(this.trans_x_camera);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.button33);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.angle_z);
            this.Controls.Add(this.angle_y);
            this.Controls.Add(this.angle_x);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_refl_z);
            this.Controls.Add(this.button_refl_y);
            this.Controls.Add(this.button_refl_x);
            this.Controls.Add(this.button_dodecaeder);
            this.Controls.Add(this.button_ikosaeder);
            this.Controls.Add(this.button_octaeder);
            this.Controls.Add(this.button_tetraeder);
            this.Controls.Add(this.rot_line_z2);
            this.Controls.Add(this.rot_line_y2);
            this.Controls.Add(this.rot_line_x2);
            this.Controls.Add(this.rot_line_z1);
            this.Controls.Add(this.rot_line_y1);
            this.Controls.Add(this.rot_line_x1);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_exec);
            this.Controls.Add(this.scaling_z);
            this.Controls.Add(this.scaling_y);
            this.Controls.Add(this.scaling_x);
            this.Controls.Add(this.label_scaling);
            this.Controls.Add(this.trans_z);
            this.Controls.Add(this.trans_y);
            this.Controls.Add(this.trans_x);
            this.Controls.Add(this.label_rotation);
            this.Controls.Add(this.label_translation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button_cube);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_cube;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_translation;
        private System.Windows.Forms.Label label_rotation;
        private System.Windows.Forms.TextBox trans_x;
        private System.Windows.Forms.TextBox trans_y;
        private System.Windows.Forms.TextBox trans_z;
        private System.Windows.Forms.Label label_scaling;
        private System.Windows.Forms.TextBox scaling_x;
        private System.Windows.Forms.TextBox scaling_y;
        private System.Windows.Forms.TextBox scaling_z;
        private System.Windows.Forms.Button button_exec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.TextBox rot_line_x1;
        private System.Windows.Forms.TextBox rot_line_y1;
        private System.Windows.Forms.TextBox rot_line_z1;
        private System.Windows.Forms.TextBox rot_line_z2;
        private System.Windows.Forms.TextBox rot_line_y2;
        private System.Windows.Forms.TextBox rot_line_x2;
        private System.Windows.Forms.Button button_tetraeder;
        private System.Windows.Forms.Button button_octaeder;
        private System.Windows.Forms.Button button_ikosaeder;
        private System.Windows.Forms.Button button_dodecaeder;
        private System.Windows.Forms.Button button_refl_x;
        private System.Windows.Forms.Button button_refl_y;
        private System.Windows.Forms.Button button_refl_z;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox angle_x;
        private System.Windows.Forms.TextBox angle_y;
        private System.Windows.Forms.TextBox angle_z;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button33;

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button_exec_camera;
        private System.Windows.Forms.TextBox trans_z_camera;
        private System.Windows.Forms.TextBox trans_y_camera;
        private System.Windows.Forms.TextBox trans_x_camera;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button camera_translation;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox camera_x_rotate;
        private System.Windows.Forms.TextBox camera_y_rotate;
        private System.Windows.Forms.TextBox camera_z_rotate;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.TextBox light_x;
        private System.Windows.Forms.TextBox light_y;
        private System.Windows.Forms.TextBox light_z;
        private System.Windows.Forms.Label label_color;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButton4;
    }
}

