using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SamSeifert.Utilities;
using SamSeifert.Utilities.Json;

namespace Ebook
{
    public partial class CellChapter : UserControl, CellInterface
    {
        public int _Section { get; private set; }


        private ManifestFileNavigation _Parent = null;

        public CellChapter()
        {
            InitializeComponent();
        }

        ~CellChapter()
        {
            this._Parent?.ForgetChild(this);
            this._Parent = null;
        }

        public void setup(ManifestFileNavigation mf, int section)
        {
            this._Parent?.ForgetChild(this);
            this._Parent = null;

            this._Section = section;

            this.lPath.Text = mf._StringPath;
            this.rbDisplayChapter.Enabled = mf._BoolFileExists;
            this.nudSectionCloses.SetValueMinMaxSafe(mf._NavigationPointCloses);

            RadioButton rb = null;

            switch (mf._NavigationType)
            {
                case ManifestFileNavigation.NavigationType.None:
                    rb = this.rbNavNone;
                    break;
                case ManifestFileNavigation.NavigationType.Default:
                    rb = this.rbNavDefault;
                    break;
                case ManifestFileNavigation.NavigationType.Custom:
                    rb = this.rbNavCustom;
                    break;
                default:
                    Logger.WriteError(this, "nav type");
                    return;
            }

            rb.Checked = true;

            mf._Child = this;
            mf.UpdateLinkedGuiElements();
            this._Parent = mf; // Do this last to prevent event handlers from firing

            this.navRadioButtonCheckChanged(rb, EventArgs.Empty);
        }

        private static int OffsetForNavitem(ManifestFileNavigation.NavigationType t) { return t == ManifestFileNavigation.NavigationType.None ? 0 : 1; }
        private void navRadioButtonCheckChanged(object sender, EventArgs e)
        {
            if (this._Parent == null) return;
            if (sender is RadioButton)
            {
                var rb = sender as RadioButton;
                if (rb.Checked)
                {
                    this.tbNavTitle.Enabled = rb == this.rbNavCustom;

                    int start_offset = OffsetForNavitem(this._Parent._NavigationType);

                    if (rb == this.rbNavNone)
                    {
                        this.tbNavTitle.Text = null;
                        this._Parent._NavigationType = ManifestFileNavigation.NavigationType.None;
                    }
                    else if (rb == this.rbNavDefault)
                    {
                        this.tbNavTitle.Text = this._Parent._NavigationNameDefault;
                        this._Parent._NavigationType = ManifestFileNavigation.NavigationType.Default;
                    }
                    else if (rb == this.rbNavCustom)
                    {
                        this.tbNavTitle.Text = this._Parent._NavigationName;
                        this._Parent._NavigationType = ManifestFileNavigation.NavigationType.Custom;
                    }
                    else
                    {
                        Logger.WriteError(this, "Radio Button Check Changed");
                    }

                    int end_offset = OffsetForNavitem(this._Parent._NavigationType);
                    int dif_offset = end_offset - start_offset;
                    if (dif_offset != 0)
                    {
                        this.nudSectionCloses.AddValueMinMaxSafe(dif_offset);
                        this.nudSectionCloses_ValueChanged(null, EventArgs.Empty);
                    }
                }
            }
        }

        private void tbNavTitle_TextChanged(object sender, EventArgs e)
        {
            if (this._Parent == null) return;
            this._Parent._NavigationName = this.tbNavTitle.Text;
        }

        private void nudSectionCloses_ValueChanged(object sender, EventArgs e)
        {
            if (this._Parent == null) return;
            this._Parent._NavigationPointCloses = (int)Math.Round(this.nudSectionCloses.Value);
            this.getUITableView().ReloadData(); // Refresh Indents
        }





        public bool CheckboxEnabled
        {
            set
            {
                this.checkBox1.Enabled = value;
            }
        }
        public bool CheckboxChecked
        {
            set
            {
                this.checkBox1.Checked = value;
            }
        }

        public string DisplayNumber
        {
            set
            {
                // Ignore Value
            }
        }

        private void CheckClick(object sender, EventArgs e)
        {
            if (this._Parent == null) return;

            if (!this._Parent.CheckedClicked(this, this.checkBox1.Checked))
                this._Parent = null; // Abandon!
        }

        public void SetPrimaryColor(Color c)
        {
            this.pColor.BackColor = c;
        }
    }
}
