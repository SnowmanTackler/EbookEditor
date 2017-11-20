namespace Ebook
{
    partial class CellText
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.lNumber = new System.Windows.Forms.Label();
            this.lName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoCheck = false;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Padding = new System.Windows.Forms.Padding(4);
            this.checkBox1.Size = new System.Drawing.Size(23, 124);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.CheckClick);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoCheck = false;
            this.radioButton1.AutoSize = true;
            this.radioButton1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radioButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.radioButton1.Location = new System.Drawing.Point(383, 0);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Padding = new System.Windows.Forms.Padding(4);
            this.radioButton1.Size = new System.Drawing.Size(22, 124);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // lNumber
            // 
            this.lNumber.Dock = System.Windows.Forms.DockStyle.Right;
            this.lNumber.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lNumber.Location = new System.Drawing.Point(316, 0);
            this.lNumber.Name = "lNumber";
            this.lNumber.Size = new System.Drawing.Size(67, 124);
            this.lNumber.TabIndex = 3;
            this.lNumber.Text = "0 / 1";
            this.lNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lName
            // 
            this.lName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lName.Location = new System.Drawing.Point(23, 0);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(293, 124);
            this.lName.TabIndex = 4;
            this.lName.Text = "label2";
            this.lName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CellText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lName);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.lNumber);
            this.Controls.Add(this.radioButton1);
            this.Name = "CellText";
            this.Size = new System.Drawing.Size(405, 124);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label lNumber;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lName;
    }
}
