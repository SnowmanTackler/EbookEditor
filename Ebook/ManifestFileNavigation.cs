using SamSeifert.Utilities.FileParsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook
{
    public class ManifestFileNavigation : ManifestFile
    {
        public int _NavigationPointCloses = 0; // 1 = close 1 chapter, 2 = close chapter and higher (part)
        public enum NavigationType { None, Default, Custom }
        public NavigationType _NavigationType = NavigationType.None;
        public String _NavigationName = "null";

        /// <summary>
        /// Display Only
        /// </summary>
        public int _Indents = 0;

        public ManifestFileNavigation(TagFile node, string base_path) : base(node, base_path)
        {
        }

        public override void UpdateLinkedGuiElements()
        {
            base.UpdateLinkedGuiElements();

            var cc = this._Child as CellChapter;

            if (cc == null) return;

            cc.SetPrimaryColor(this.Checked ?
                Color.FromKnownColor(KnownColor.Control) :
                Color.FromKnownColor(KnownColor.InactiveCaption));

            cc.Padding = new System.Windows.Forms.Padding(this._Indents * 35, 0, 0, 0);

        }
    }
}
