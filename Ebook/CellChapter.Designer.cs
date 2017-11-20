namespace Ebook
{
    partial class CellChapter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lPath = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.rbDisplayChapter = new System.Windows.Forms.RadioButton();
            this.rbNavCustom = new System.Windows.Forms.RadioButton();
            this.rbNavDefault = new System.Windows.Forms.RadioButton();
            this.rbNavNone = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbNavTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.nudSectionCloses = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSectionCloses)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lPath
            // 
            this.lPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lPath.Enabled = false;
            this.lPath.Location = new System.Drawing.Point(75, 0);
            this.lPath.Name = "lPath";
            this.lPath.Size = new System.Drawing.Size(273, 20);
            this.lPath.TabIndex = 8;
            this.lPath.Text = "path path";
            this.lPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoCheck = false;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Padding = new System.Windows.Forms.Padding(4);
            this.checkBox1.Size = new System.Drawing.Size(23, 70);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.CheckClick);
            // 
            // rbDisplayChapter
            // 
            this.rbDisplayChapter.AutoCheck = false;
            this.rbDisplayChapter.AutoSize = true;
            this.rbDisplayChapter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbDisplayChapter.Dock = System.Windows.Forms.DockStyle.Right;
            this.rbDisplayChapter.Location = new System.Drawing.Point(464, 0);
            this.rbDisplayChapter.Name = "rbDisplayChapter";
            this.rbDisplayChapter.Padding = new System.Windows.Forms.Padding(4);
            this.rbDisplayChapter.Size = new System.Drawing.Size(22, 70);
            this.rbDisplayChapter.TabIndex = 6;
            this.rbDisplayChapter.TabStop = true;
            this.rbDisplayChapter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbDisplayChapter.UseVisualStyleBackColor = true;
            // 
            // rbNavCustom
            // 
            this.rbNavCustom.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbNavCustom.Location = new System.Drawing.Point(10, 50);
            this.rbNavCustom.Name = "rbNavCustom";
            this.rbNavCustom.Size = new System.Drawing.Size(83, 20);
            this.rbNavCustom.TabIndex = 3;
            this.rbNavCustom.Text = "Custom";
            this.rbNavCustom.UseVisualStyleBackColor = true;
            this.rbNavCustom.CheckedChanged += new System.EventHandler(this.navRadioButtonCheckChanged);
            // 
            // rbNavDefault
            // 
            this.rbNavDefault.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbNavDefault.Location = new System.Drawing.Point(10, 20);
            this.rbNavDefault.Name = "rbNavDefault";
            this.rbNavDefault.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.rbNavDefault.Size = new System.Drawing.Size(83, 30);
            this.rbNavDefault.TabIndex = 2;
            this.rbNavDefault.Text = "Default";
            this.rbNavDefault.UseVisualStyleBackColor = true;
            this.rbNavDefault.CheckedChanged += new System.EventHandler(this.navRadioButtonCheckChanged);
            // 
            // rbNavNone
            // 
            this.rbNavNone.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbNavNone.Location = new System.Drawing.Point(10, 0);
            this.rbNavNone.Name = "rbNavNone";
            this.rbNavNone.Size = new System.Drawing.Size(83, 20);
            this.rbNavNone.TabIndex = 0;
            this.rbNavNone.Text = "None";
            this.rbNavNone.UseVisualStyleBackColor = true;
            this.rbNavNone.CheckedChanged += new System.EventHandler(this.navRadioButtonCheckChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbNavCustom);
            this.panel1.Controls.Add(this.rbNavDefault);
            this.panel1.Controls.Add(this.rbNavNone);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(371, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(93, 70);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbNavTitle);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(23, 20);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panel2.Size = new System.Drawing.Size(348, 30);
            this.panel2.TabIndex = 11;
            // 
            // tbNavTitle
            // 
            this.tbNavTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNavTitle.Location = new System.Drawing.Point(75, 5);
            this.tbNavTitle.Name = "tbNavTitle";
            this.tbNavTitle.Size = new System.Drawing.Size(273, 20);
            this.tbNavTitle.TabIndex = 1;
            this.tbNavTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbNavTitle.TextChanged += new System.EventHandler(this.tbNavTitle_TextChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nav Title:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.nudSectionCloses);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(23, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(348, 20);
            this.panel3.TabIndex = 12;
            // 
            // nudSectionCloses
            // 
            this.nudSectionCloses.Dock = System.Windows.Forms.DockStyle.Left;
            this.nudSectionCloses.Location = new System.Drawing.Point(75, 0);
            this.nudSectionCloses.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudSectionCloses.Name = "nudSectionCloses";
            this.nudSectionCloses.Size = new System.Drawing.Size(50, 20);
            this.nudSectionCloses.TabIndex = 1;
            this.nudSectionCloses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudSectionCloses.ValueChanged += new System.EventHandler(this.nudSectionCloses_ValueChanged);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nav Closes:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lPath);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(23, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(348, 20);
            this.panel4.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Path:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CellChapter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.rbDisplayChapter);
            this.Name = "CellChapter";
            this.Size = new System.Drawing.Size(486, 70);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSectionCloses)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lPath;
        private System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.RadioButton rbDisplayChapter;
        private System.Windows.Forms.RadioButton rbNavCustom;
        private System.Windows.Forms.RadioButton rbNavDefault;
        private System.Windows.Forms.RadioButton rbNavNone;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbNavTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudSectionCloses;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
    }
}
