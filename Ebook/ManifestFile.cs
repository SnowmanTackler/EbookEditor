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

        public enum MediaType { Image, Text, Other };
        public String _StringPath;
        public String _StringPathFull;
        public MediaType _MediaType;
        public String _StringID;
        public bool _BoolFileExists { get; private set; }

        // Who is currently display this guys information
        public CellInterface _Child;
        public void ForgetChild(CellInterface c) { if (c == this._Child) this._Child = null; }

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

            this.UpdateAutoInclude(this._StringID);
            this.UpdateAutoInclude(this._StringPath);

            var media_type = params_["media-type"];
            if (media_type.Contains("image"))
            {
                this._MediaType = ManifestFile.MediaType.Image;
                this.Checked = false;
            }
            else if (media_type.Contains("xhtml+xml"))
            {
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

        public void UpdateAutoInclude(String checker)
        {
            if (this.Checked) return;
            if (checker == null) return;

            checker = checker.ToLower();

            if (
                ("titlepage" == checker) ||
                ("epl" == checker) ||
                ("prl" == checker) ||
                ("tp" == checker) ||

                checker.Contains("cover") ||
                checker.Contains("chapter") ||
                checker.Contains("prologue") ||
                checker.Contains("epilogue") ||
                checker.Contains("part") ||
                checker.Contains("title") ||

                checker.Contains("ars arcanum") || // Brandon Sanderson Mistborn

                false) this.Checked = true;
        }


        public void addReferenceMax()
        {
            this._IntRefrencesMax++;
        }

        public void addReference()
        {
            this._IntRefrences++;
            this.Checked = this._IntRefrences > 0;
            this.UpdateLinkedGuiElements();
        }

        public void subReference()
        {
            this._IntRefrences--;
            this.Checked = this._IntRefrences > 0;
            this.UpdateLinkedGuiElements();
        }

        public virtual void UpdateLinkedGuiElements()
        {
            if (this._Child != null)
            {
                this._Child.CheckboxEnabled = this._IntRefrences == 0;
                this._Child.CheckboxChecked = this._Checked;
                this._Child.DisplayNumber = (this._IntRefrencesMax == 0) ?
                    ((this._IntSpineCount >= 0) ? this._IntSpineCount.ToString() : "") :
                    ((this._IntRefrences == 0 ? " " : this._IntRefrences.ToString()) + " / " + this._IntRefrencesMax);
            }
        }

        public override string ToString()
        {
            return this._StringID;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="checkd"></param>
        /// <returns>Should Abandon Parent or Not</returns>
        public bool CheckedClicked(CellInterface sender, bool checkd)
        {
            if (sender != this._Child)
            {
                return false;
            }

            this.Checked = !checkd;

            return true;
        }

        private bool _Checked = false;
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

                    this.UpdateLinkedGuiElements();
                }
            }
        }

        /// <summary>
        /// My version of dealloc.  Clear all references so things can be GC'd
        /// </summary>
        internal void Clear()
        {
            this.Dependants.Clear();
            this._Child = null;
        }
    }
}
