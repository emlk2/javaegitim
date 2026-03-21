namespace LAB_14._03._24
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            btnKaydet = new Button();
            txt = new TextBox();
            cb = new CheckBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            progressBar1 = new ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(-1, 9);
            label1.Name = "label1";
            label1.Size = new Size(69, 24);
            label1.TabIndex = 0;
            label1.Text = "label";
            label1.Click += label1_Click;
            // 
            // btnKaydet
            // 
            btnKaydet.Font = new Font("Verdana", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            btnKaydet.Location = new Point(346, 212);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(183, 78);
            btnKaydet.TabIndex = 1;
            btnKaydet.Text = "button1";
            btnKaydet.UseVisualStyleBackColor = true;
            btnKaydet.Click += btnKaydet_Click;
            // 
            // txt
            // 
            txt.Location = new Point(316, 148);
            txt.Name = "txt";
            txt.Size = new Size(238, 33);
            txt.TabIndex = 2;
            txt.TextChanged += txt_TextChanged;
            // 
            // cb
            // 
            cb.AutoSize = true;
            cb.Location = new Point(46, 198);
            cb.Name = "cb";
            cb.Size = new Size(114, 33);
            cb.TabIndex = 3;
            cb.Text = "checkBox1";
            cb.UseVisualStyleBackColor = true;
            cb.CheckedChanged += cb_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(46, 237);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(77, 33);
            radioButton1.TabIndex = 4;
            radioButton1.TabStop = true;
            radioButton1.Text = "kadın";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(46, 276);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(75, 33);
            radioButton2.TabIndex = 5;
            radioButton2.TabStop = true;
            radioButton2.Text = "erkek";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(227, 335);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(455, 29);
            progressBar1.TabIndex = 6;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 29F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Pink;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(807, 473);
            Controls.Add(progressBar1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(cb);
            Controls.Add(txt);
            Controls.Add(btnKaydet);
            Controls.Add(label1);
            Cursor = Cursors.Cross;
            Font = new Font("Sitka Banner", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "eml";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnKaydet;
        private TextBox txt;
        private CheckBox cb;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
    }
}
