using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ebook
{
    public partial class CellText : UserControl, CellInterface
    {
        public int _Section { get; private set; }
        private ManifestFile _Parent = null;

        public CellText()
        {
            InitializeComponent();
        }

        ~CellText()
        {
            this._Parent?.ForgetChild(this);
        }

        public void setup(ManifestFile mf, int section)
        {
            this._Parent?.ForgetChild(this);

            this._Parent = mf;
            this._Section = section;

            // Set all things that won't change while this has same parent
            this.lName.Text = mf._StringPath;
            this.radioButton1.Enabled = this._Parent._BoolFileExists; // not given to parent

            // Update things that will change while this has same parent
            this._Parent._Child = this;
            this._Parent.UpdateLinkedGuiElements();
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
                this.lNumber.Text = value;
            }
        }


        private void CheckClick(object sender, EventArgs e)
        {
            if (this._Parent == null) return;

            if (!this._Parent.CheckedClicked(this, this.checkBox1.Checked))
                this._Parent = null; // Abandon!
        }
    }
}
