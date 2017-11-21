namespace Ebook
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
            this.bOpen = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.bLook = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.bClear = new System.Windows.Forms.Button();
            this.bGo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAuthor = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxGenre = new System.Windows.Forms.TextBox();
            this.bExecuteAll = new System.Windows.Forms.Button();
            this.uiTableView3 = new Ebook.UITableView();
            this.uiTableView2 = new Ebook.UITableView();
            this.uiTableView1 = new Ebook.UITableView();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(12, 12);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(100, 23);
            this.bOpen.TabIndex = 0;
            this.bOpen.Text = "Open";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(118, 14);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(700, 20);
            this.textBoxPath.TabIndex = 1;
            this.textBoxPath.TextChanged += new System.EventHandler(this.textBoxPath_TextChanged);
            // 
            // bLook
            // 
            this.bLook.Enabled = false;
            this.bLook.Location = new System.Drawing.Point(12, 41);
            this.bLook.Name = "bLook";
            this.bLook.Size = new System.Drawing.Size(100, 23);
            this.bLook.TabIndex = 2;
            this.bLook.Text = "Take a Look";
            this.bLook.UseVisualStyleBackColor = true;
            this.bLook.Click += new System.EventHandler(this.bLook_Click);
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 806);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(16, 13);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "...";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::Ebook.Properties.Resources.backImgSmall;
            this.pictureBox1.Location = new System.Drawing.Point(824, 128);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(145, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(824, 434);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(145, 380);
            this.webBrowser1.TabIndex = 16;
            // 
            // bClear
            // 
            this.bClear.Enabled = false;
            this.bClear.Location = new System.Drawing.Point(12, 70);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(100, 23);
            this.bClear.TabIndex = 18;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bGo
            // 
            this.bGo.Enabled = false;
            this.bGo.Location = new System.Drawing.Point(12, 99);
            this.bGo.Name = "bGo";
            this.bGo.Size = new System.Drawing.Size(100, 23);
            this.bGo.TabIndex = 19;
            this.bGo.Text = "Convert";
            this.bGo.UseVisualStyleBackColor = true;
            this.bGo.Click += new System.EventHandler(this.bGo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(118, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Author:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(118, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Title:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(118, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Genre:";
            // 
            // textBoxAuthor
            // 
            this.textBoxAuthor.Location = new System.Drawing.Point(165, 43);
            this.textBoxAuthor.Name = "textBoxAuthor";
            this.textBoxAuthor.Size = new System.Drawing.Size(653, 20);
            this.textBoxAuthor.TabIndex = 23;
            this.textBoxAuthor.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(165, 72);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(653, 20);
            this.textBoxTitle.TabIndex = 24;
            this.textBoxTitle.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // textBoxGenre
            // 
            this.textBoxGenre.Location = new System.Drawing.Point(165, 101);
            this.textBoxGenre.Name = "textBoxGenre";
            this.textBoxGenre.Size = new System.Drawing.Size(653, 20);
            this.textBoxGenre.TabIndex = 25;
            this.textBoxGenre.TextChanged += new System.EventHandler(this.textBoxTextChanged);
            // 
            // bExecuteAll
            // 
            this.bExecuteAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bExecuteAll.Enabled = false;
            this.bExecuteAll.Location = new System.Drawing.Point(824, 14);
            this.bExecuteAll.Name = "bExecuteAll";
            this.bExecuteAll.Size = new System.Drawing.Size(145, 110);
            this.bExecuteAll.TabIndex = 26;
            this.bExecuteAll.Text = "Do All";
            this.bExecuteAll.UseVisualStyleBackColor = true;
            this.bExecuteAll.Click += new System.EventHandler(this.bExecuteAll_Click);
            // 
            // uiTableView3
            // 
            this.uiTableView3.BackColor = System.Drawing.Color.White;
            this.uiTableView3.Location = new System.Drawing.Point(468, 128);
            this.uiTableView3.Name = "uiTableView3";
            this.uiTableView3.Size = new System.Drawing.Size(350, 300);
            this.uiTableView3.Tab = 10;
            this.uiTableView3.TabIndex = 17;
            this.uiTableView3.VerticalSpacing = 1;
            // 
            // uiTableView2
            // 
            this.uiTableView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uiTableView2.BackColor = System.Drawing.Color.White;
            this.uiTableView2.Location = new System.Drawing.Point(468, 434);
            this.uiTableView2.Name = "uiTableView2";
            this.uiTableView2.Size = new System.Drawing.Size(350, 380);
            this.uiTableView2.Tab = 10;
            this.uiTableView2.TabIndex = 14;
            this.uiTableView2.VerticalSpacing = 1;
            // 
            // uiTableView1
            // 
            this.uiTableView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.uiTableView1.BackColor = System.Drawing.Color.White;
            this.uiTableView1.Location = new System.Drawing.Point(12, 128);
            this.uiTableView1.Name = "uiTableView1";
            this.uiTableView1.Size = new System.Drawing.Size(450, 686);
            this.uiTableView1.Tab = 10;
            this.uiTableView1.TabIndex = 13;
            this.uiTableView1.VerticalSpacing = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(981, 826);
            this.Controls.Add(this.bExecuteAll);
            this.Controls.Add(this.textBoxGenre);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.textBoxAuthor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bGo);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.uiTableView3);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.uiTableView2);
            this.Controls.Add(this.uiTableView1);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.bLook);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.bOpen);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "eBook Consolidator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button bLook;
        private System.Windows.Forms.Label labelName;
        private UITableView uiTableView1;
        private UITableView uiTableView2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private UITableView uiTableView3;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Button bGo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAuthor;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxGenre;
        private System.Windows.Forms.Button bExecuteAll;
    }
}

