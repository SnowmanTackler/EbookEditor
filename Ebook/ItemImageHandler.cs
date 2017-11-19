using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ebook
{
    public class ItemImageHandler : UITableView.DataSource
    {
        public ManifestFile[] _Content = new ManifestFile[0];
        public PictureBox _PictureBox;

        public int numberOfSections(UITableView tv)
        {
            return this._Content.Length;
        }

        public int numberOfRowsInSection(UITableView tv, int section) { return 0; }

        public Control viewForFooterInSection(UITableView tv, int section) { return null; }
        public Control viewForFooterInSection(UITableView tv, int section, Control c) { return null; }
        public int heightForFooterInSection(UITableView tv, int section) { return 0; }

        public Control viewForCellAtIndexPath(UITableView tv, UITableView.IndexPath ip) { return null; }
        public Control viewForCellAtIndexPath(UITableView tv, UITableView.IndexPath ip, Control c) { return null; }
        public int heightForCellAtIndexPath(UITableView tv, UITableView.IndexPath ip) { return 0; }

        public void Clear()
        {
            foreach (var mf in this._Content) mf.Clear();
            this._Content = new ManifestFile[0];
            this.lastRadioButton = null;
            this.lastRadioButtonSection = -1;
        }


        public Control viewForHeaderInSection(UITableView tv, int section)
        {
            var c = new ItemText();
            this.setupHeaderForSection(c, section);
            return c;
        }

        public Control viewForHeaderInSection(UITableView tv, int section, Control c)
        {
            if (c is ItemText)
            {
                this.setupHeaderForSection(c as ItemText, section);
                return null;
            }
            else return this.viewForHeaderInSection(tv, section);
        }

        public void setupHeaderForSection(ItemText c, int section)
        {
            var cc = c as ItemText;
            var mf = this._Content[section];
            cc.setup(mf);
            cc.section = section;
            if (section == this.lastRadioButtonSection)
            {
                cc.radioButton1.Checked = true;
                this.lastRadioButton = cc.radioButton1;
            }
            cc.radioButton1.Click += new EventHandler(radioButton1_Click);
            // cc.checkBox1.Enabled = mf.enabled; 
        }

        public int heightForHeaderInSection(UITableView tv, int section)
        {
            return 20;
        }

        private RadioButton lastRadioButton = null;
        private int lastRadioButtonSection = -1;
        public void radioButton1_Click(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            var par = rb.Parent as ItemText;
            if (!rb.Checked)
            {
                rb.Checked = true;
                if (this.lastRadioButton != null) this.lastRadioButton.Checked = false;
                this.lastRadioButton = rb;
                this.lastRadioButtonSection = par.section;

                var im = Image.FromFile(this._Content[par.section]._StringPathFull);                
                if (this._PictureBox != null)
                {
                    if (this._PictureBox.Image != null) this._PictureBox.Image.Dispose();
                    this._PictureBox.Image = im;
                }
            }
        }
    }
}
