using SamSeifert.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ebook
{
    public class OrganizerOthers : UITableView.DataSource
    {
        public ManifestFile[] _Content = new ManifestFile[0];

        public void Reloading() { }

        public int numberOfSections(UITableView tv) { return this._Content.Length; }

        public int heightForHeaderInSection(UITableView tv, int section) { return Constants._CellHeight; }
        public Control viewForHeaderInSection(UITableView tv, int section, Control c)
        {
            CellText cc = null;

            if (c == null) cc = new CellText();
            else if (c is CellText) cc = c as CellText;
            else cc = new CellText();

            var mf = this._Content[section];
            cc.setup(mf, section);

            return cc;
        }

        public int numberOfRowsInSection(UITableView tv, int section) { return 0; }
        public int heightForCellAtIndexPath(UITableView tv, UITableView.IndexPath ip) { return 0; }
        public Control viewForCellAtIndexPath(UITableView tv, UITableView.IndexPath ip, Control c) { return null; }

        public int heightForFooterInSection(UITableView tv, int section) { return 0; }
        public Control viewForFooterInSection(UITableView tv, int section, Control c) { return null; }

        public void Clear()
        {
            foreach (var mf in this._Content) mf.Clear();
            this._Content = new ManifestFile[0];
        }
    }
}
