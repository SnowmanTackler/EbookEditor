using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ebook
{
    public class ItemOtherHandler : UITableView.DataSource
    {
        public ManifestFile[] _Content = new ManifestFile[0];


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
            cc.checkBox1.Click -= new EventHandler(mf.CheckedClicked);
            cc.checkBox1.Click += new EventHandler(mf.CheckedClicked);
            cc._Parent = mf;
        }

        public int heightForHeaderInSection(UITableView tv, int section)
        {
            return 20;
        }




    }
}
