using SamSeifert.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ebook
{
    public class OrganizerChapters : UITableView.DataSource
    {
        public void Reloading()
        {
            int level = 0;
            foreach (var mfn in this._Content)
            {
                if (mfn.Checked)
                {
                    mfn._Indents = level;
                    level += 1 - mfn._NavigationPointCloses;
                }
            }
        }

        private ManifestFileNavigation[] _Content = new ManifestFileNavigation[0];
        public WebBrowser _WebBrowser;

        public void SetContent(IEnumerable<ManifestFileNavigation> content)
        {
            if (content == null) this._Content = new ManifestFileNavigation[0];
            else this._Content = content.ToArray();
        }

        public IEnumerable<ManifestFileNavigation> EnumerateContent()
        {
            return this._Content;
        }


        public int numberOfSections(UITableView tv) { return this._Content.Length; }
    
        public int heightForHeaderInSection(UITableView tv, int section) { return 75; }
        public Control viewForHeaderInSection(UITableView tv, int section, Control c)
        {
            CellChapter cc = null;

            if (c == null) cc = new CellChapter();
            else if (c is CellChapter) cc = c as CellChapter;
            else cc = new CellChapter();

            var mf = this._Content[section];
            cc.setup(mf, section);
            if (section == this.lastRadioButtonSection)
            {
                cc.rbDisplayChapter.Checked = true;
                this.lastRadioButton = cc.rbDisplayChapter;
            }
//            cc.CheckBoxClick -= new EventHandler(mf._MF.CheckedClicked);
//            cc.CheckBoxClick += new EventHandler(mf._MF.CheckedClicked);
            cc.rbDisplayChapter.Click += new EventHandler(radioButton1_Click);

            return cc;
        }

        public int numberOfRowsInSection(UITableView tv, int section) { return 0; }
        public int heightForCellAtIndexPath(UITableView tv, UITableView.IndexPath ip) { return 0;  }
        public Control viewForCellAtIndexPath(UITableView tv, UITableView.IndexPath ip, Control c) { return null; }

        public int heightForFooterInSection(UITableView tv, int section) { return 0; }
        public Control viewForFooterInSection(UITableView tv, int section, Control c) { return null; }

        public void Clear()
        {
            foreach (var mf in this._Content) mf.Clear();
            this._Content = new ManifestFileNavigation[0];
            this.lastRadioButton = null;
            this.lastRadioButtonSection = -1;
        }

        internal void SelectAll()
        {
            foreach (var mf in this._Content)
            {
                mf.Checked = true;
            }
        }

        private RadioButton lastRadioButton = null;
        private int lastRadioButtonSection = -1;
        public void radioButton1_Click(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            var par = rb.Parent as CellChapter;
            if (!rb.Checked)
            {
                rb.Checked = true;
                if (this.lastRadioButton != null) this.lastRadioButton.Checked = false;
                this.lastRadioButton = rb;
                this.lastRadioButtonSection = par._Section;

                this._WebBrowser.Navigate(this._Content[par._Section]._StringPathFull);
            }
        }

    }
}
