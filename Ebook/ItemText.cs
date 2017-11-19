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
    public partial class ItemText : UserControl
    {
        private Boolean _Enabled = true;
        new public Boolean Enabled
        {
            get { return this._Enabled; }
            set
            {
                this.checkBox1.Enabled = value;
                this.radioButton1.Enabled = value;
                this._Enabled = value;
            }
        }

        public int section;
        public ManifestFile _Parent = null;
        public ItemText()
        {
            InitializeComponent();
        }

        ~ItemText()
        {
            if (this._Parent != null)
            {
                if (this._Parent._CurrentCheckBox == this.checkBox1)
                {
                    this._Parent._CurrentCheckBox = null;
                }
            }
        }

        public void setup(ManifestFile mf)
        {
            this.checkBox1.Text = mf._StringPath;
            this.Enabled = mf._BoolFileExists;

            if (!this.Enabled) Console.WriteLine("ARG" + mf._StringPathFull);
            this.label1.Text = mf._IntRefrences + " / " + mf._IntRefrencesMax;
            mf._CurrentCheckBox = this.checkBox1;
            mf._CurrentLabelReferences = this.label1;
            mf.updateControls();
        }
    }
}
