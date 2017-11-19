using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

using SamSeifert.Utilities.FileParsing;

namespace Ebook
{
    public class ManifestFile : IComparable<ManifestFile>
    {
        public List<ManifestFile> Dependants = new List<ManifestFile>();

        public int _IntRefrences { get; private set; }
        public int _IntRefrencesMax { get; private set; }
        public int _IntSpineCount = -1;

        public CheckBox _CurrentCheckBox = null;
        public Label _CurrentLabelReferences = null;

        public enum MediaType { Image, Text, Other };
        public String _StringPath;
        public String _StringPathFull;
        public MediaType _MediaType;
        public String _StringID;
        public bool _BoolFileExists { get; private set; }
        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                if (value != this._Checked)
                {
                    this._Checked = value;

                    foreach (var mf in this.Dependants)
                    {
                        if (this._Checked) mf.addReference();
                        else mf.subReference();
                    }

                    this.updateControls();
                }
            }
        }
        private bool _Checked = false;

        public int CompareTo(ManifestFile other)
        {
            if (other == null) return 1;
            return this._IntSpineCount.CompareTo(other._IntSpineCount);
        }

        public ManifestFile(TagFile node, String base_path)
        {
            this._IntRefrences = 0;
            this._IntRefrencesMax = 0;

            var params_ = node._Params;
            this._StringID = params_["id"];
            this._StringPath = params_["href"];
            this._StringPathFull = Path.Combine(base_path, this._StringPath);
            this._BoolFileExists = File.Exists(this._StringPathFull);
            this.Checked = false;

            var media_type = params_["media-type"];
            if (media_type.Contains("image"))
            {
                this._MediaType = ManifestFile.MediaType.Image;
                this.Checked = false;
            }
            else if (media_type.Contains("xhtml+xml"))
            {
                if ("titlepage" == this._StringID) this.Checked = true;
                if ("epl" == this._StringID) this.Checked = true;
                if ("prl" == this._StringID) this.Checked = true;
                if ("tp" == this._StringID) this.Checked = true;

                if (this._StringID.Length > 1)
                {
                    var num = this._StringID.Substring(1);
                    int xyz;
                    if (int.TryParse(num, out xyz))
                    {
                        switch (this._StringID[0])
                        {
                            case 'c': this.Checked = true; break;
                            case 'p': this.Checked = true; break;
                        }
                    }
                }

                this._MediaType = ManifestFile.MediaType.Text;
            }
            else
            {
                this._MediaType = ManifestFile.MediaType.Other;
            }

        }

        public void addReferenceMax()
        {
            this._IntRefrencesMax++;
        }

        public void addReference()
        {
            this._IntRefrences++;
            this.Checked = this._IntRefrences > 0;
            this.updateControls();
        }

        public void subReference()
        {
            this._IntRefrences--;
            this.Checked = this._IntRefrences > 0;
            this.updateControls();
        }

        public void updateControls()
        {
            if (this._CurrentCheckBox != null)
            {
                this._CurrentCheckBox.Checked = this._Checked;
                this._CurrentCheckBox.ForeColor = ((!this.Checked) && (this._IntRefrences > 0)) ? Color.Red : Color.Black;
            }
            if (this._CurrentLabelReferences != null)
                this._CurrentLabelReferences.Text = (this._IntRefrencesMax == 0) ?
                    ((this._IntSpineCount >= 0) ? this._IntSpineCount.ToString() : "") : 
                    ((this._IntRefrences == 0 ? " " : this._IntRefrences.ToString()) + " / " + this._IntRefrencesMax);
        }

        public override string ToString()
        {
            return this._StringID;
        }

        public void CheckedClicked(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;

            if (cb != null)
            {
                if (cb != this._CurrentCheckBox)
                {
                    cb.Click -= new EventHandler(this.CheckedClicked);
                }
                else
                {
                    if (this.Checked == cb.Checked)
                    {
                        this.Checked = !this.Checked;
                    }
                }
            }
        }

        internal void Clear()
        {
            this.Dependants.Clear();
            this._CurrentCheckBox = null;
            this._CurrentLabelReferences = null;
        }
    }
}
