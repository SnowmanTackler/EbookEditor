﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using SamSeifert.Utilities.FileParsing;

namespace Ebook
{
    public partial class Form1 : Form
    {
        public ItemTextHandler _ItemTextHandler = new ItemTextHandler();
        public ItemImageHandler _ItemImageHandler = new ItemImageHandler();
        public ItemOtherHandler _ItemOtherHandler = new ItemOtherHandler();

        public Form1()
        {
            InitializeComponent();

            if (this.DesignMode) return;

            this.textBoxPath.Text = Properties.Settings.Default.save_path;
            this.folderBrowserDialog1.SelectedPath = this.textBoxPath.Text;

            this.uiTableView1._DataSource = this._ItemTextHandler;
            this.uiTableView2._DataSource = this._ItemImageHandler;
            this.uiTableView3._DataSource = this._ItemOtherHandler;

            this._ItemImageHandler._PictureBox = this.pictureBox1;
            this._ItemTextHandler._WebBrowser = this.webBrowser1;
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            switch (this.folderBrowserDialog1.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    this.textBoxPath.Text = this.folderBrowserDialog1.SelectedPath;
                    break;
            }
        }

        private void textBoxPath_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(this.textBoxPath.Text, "content.opf")) &&
                File.Exists(Path.Combine(this.textBoxPath.Text, "toc.ncx")))
            {
                Properties.Settings.Default.save_path = this.textBoxPath.Text;
                Properties.Settings.Default.Save();

                this.textBoxPath.ForeColor = Color.Green;
                this.bLook.Enabled = true;
            }
            else
            {
                this.textBoxPath.ForeColor = Color.Red;
                this.bLook.Enabled = false;
            }
        }

        private void textBoxTextChanged(object sender, EventArgs e)
        {
            var tb = (sender as TextBox);
            String bases = tb.Text;
            String nbase = this.removeUnwanted(bases);
            if (!(bases.Equals(nbase))) tb.Text = nbase;
        }

        private String removeUnwanted(String bases)
        {
            var ca = bases.ToCharArray().ToList();
            int i = 0;
            while (i < ca.Count)
            {
                bool good = Char.IsLetterOrDigit(ca[i]);
                good |= (ca[i] == '-');
                good |= (ca[i] == ' ');
                if (good) i++;
                else ca.RemoveAt(i);
            }
            return new String(ca.ToArray());
        }

        private Dictionary<TagItem, ManifestFile> _DictItemsByNode = new Dictionary<TagItem, ManifestFile>();
        private Dictionary<String, ManifestFile> _DictItemsByPath = new Dictionary<String, ManifestFile>();
        private Dictionary<String, ManifestFile> _DictItemsByXMLID = new Dictionary<String, ManifestFile>();
        private List<ManifestFile> _ListItems = new List<ManifestFile>();
        private TagFile _TagFileBase;
        private TagFile _TagFileMetadata;
        private Boolean _DoingStuff = false;

        private void bLook_Click(object sender, EventArgs e)
        {
            if (this._DoingStuff) return;
            try
            {
                this._DoingStuff = true;
                this.bOpen.Enabled = false;
                this.bLook.Enabled = false;
                this.bClear.Enabled = true;
                this.bGo.Enabled = true;
                this.textBoxPath.Enabled = false;

                var p = this.textBoxPath.Text;

                var _ContentNonImageL = new List<ManifestFile>();
                var _ContentImageL = new List<ManifestFile>();

                var tf = TagFile.ParseText(File.ReadAllText(Path.Combine(p,"content.opf")));
                var package_matches = tf.getMatches("package");
                if (package_matches.Count != 1) throw new Exception("Invalid XML 1");
                if (!(package_matches[0] is TagFile)) throw new Exception("Invalid XML2");
                this._TagFileBase = package_matches[0] as TagFile;

                TagFile manifest = null;
                TagFile spine = null;
                foreach (var node1 in this._TagFileBase._Children) if (node1 is TagFile)
                    {
                        var node2 = node1 as TagFile;
                        switch (node2._Name)
                        {
                            case "manifest":
                                if (manifest == null) manifest = node2;
                                else Console.WriteLine("Error: Multiple Manifests");
                                break;
                            case "spine":
                                if (spine == null) spine = node2;
                                else Console.WriteLine("Error: Multiple Spines");
                                break;
                            case "metadata":
                                if (this._TagFileMetadata == null) this._TagFileMetadata = node2;
                                else Console.WriteLine("Error: Multiple Metadatas");
                                break;
                        }
                    }

                foreach (var node3 in manifest._Children) if (node3 is TagFile)
                    {
                        var node4 = node3 as TagFile;
                        var params_ = node4._Params;


                        var mt = new ManifestFile(node4, p);

                        bool include = true;

                        include &= !("toc.ncx".Equals(mt._StringPath));

                        if (include)
                        {
                            this._DictItemsByXMLID[mt._StringID] = mt;
                            this._DictItemsByPath[mt._StringPath] = mt;
                            this._DictItemsByNode[node4] = mt;
                            this._ListItems.Add(mt);

                            switch (mt._MediaType)
                            {
                                case ManifestFile.MediaType.Image:
                                    _ContentImageL.Add(mt);
                                    break;
                                case ManifestFile.MediaType.Text:
                                    _ContentNonImageL.Add(mt);
                                    break;
                                case ManifestFile.MediaType.Other:
                                    _ContentNonImageL.Add(mt);
                                    break;
                            }
                        }
                    }

                int spine_count = 0;
                foreach (var node3 in spine._Children) if (node3 is TagFile)
                    {
                        var node4 = node3 as TagFile;
                        var params_ = node4._Params;

                        ManifestFile relative = null;
                        String idref = params_["idref"];
                        if (this._DictItemsByXMLID.TryGetValue(idref, out relative))
                        {
                            this._DictItemsByNode[node4] = relative;
                            relative._IntSpineCount = spine_count++;
                        }
                        else
                        {
                            Console.WriteLine("Error, no corresponding manifest item for spine item: " + idref);
                        }

                    }

                bool found_author = false;
                bool found_title = false;
                bool found_genre = false;
                foreach (var node3 in this._TagFileMetadata._Children) if (node3 is TagFile)
                    {
                        var node4 = node3 as TagFile;
                        switch (node4._Name)
                        {
                            case "dc:creator":
                                {
                                    if (found_author)
                                    {
                                        Console.Write("Error: Second Author Found: ");
                                        node4.Display();
                                    }
                                    else if (node4._Children.Length == 1)
                                    {
                                        if (node4._Children.First() is TagText)
                                        {
                                            found_author = true;
                                            this.textBoxAuthor.Text = (node4._Children.First() as TagText).text;
                                        }
                                        else
                                        {
                                            Console.Write("Error: Author Wrong Type of Children: ");
                                            node4.Display();
                                        }
                                    }
                                    else
                                    {
                                        Console.Write("Error: Author Wrong Number of Children: ");
                                        node4.Display();
                                    }
                                }
                                break;
                            case "dc:title":
                                {
                                    if (found_title)
                                    {
                                        Console.Write("Error: Second Title Found: ");
                                        node4.Display();
                                    }
                                    else if (node4._Children.Length == 1)
                                    {
                                        if (node4._Children.First() is TagText)
                                        {
                                            found_title = true;
                                            this.textBoxTitle.Text = (node4._Children.First() as TagText).text;
                                        }
                                        else
                                        {
                                            Console.Write("Error: Title Wrong Type of Children: ");
                                            node4.Display();
                                        }
                                    }
                                    else
                                    {
                                        Console.Write("Error: Title Wrong Number of Children: ");
                                        node4.Display();
                                    }
                                }
                                break;
                            case "dc:genre":
                                {
                                    if (found_genre)
                                    {
                                        Console.Write("Error: Second Genre Found: ");
                                        node4.Display();
                                    }
                                    else if (node4._Children.Length == 1)
                                    {
                                        if (node4._Children.First() is TagText)
                                        {
                                            found_genre = true;
                                            this.textBoxGenre.Text = (node4._Children.First() as TagText).text;
                                        }
                                        else
                                        {
                                            Console.Write("Error: Genre Wrong Type of Children: ");
                                            node4.Display();
                                        }
                                    }
                                    else
                                    {
                                        Console.Write("Error: Genre Wrong Number of Children: ");
                                        node4.Display();
                                    }
                                }
                                break;
                        }
                    }

                var missing_files = new HashSet<String>();

                foreach (ManifestFile mf in _ContentNonImageL)
                {
                    if (mf._BoolFileExists && (mf._MediaType == ManifestFile.MediaType.Text))
                    {
                        int last_dex = mf._StringPath.LastIndexOf("/");
                        String dir = last_dex > 0 ? mf._StringPath.Substring(0, last_dex + 1) : "";

                        TagFile tf2 = TagFile.ParseText(File.ReadAllText(mf._StringPathFull));

                        foreach (var ls in tf2.getMatches("link"))
                            this.haveEmbeddedItem(ls._Params["href"], dir, mf, missing_files);
                        foreach (var ls in tf2.getMatches("img"))
                            this.haveEmbeddedItem(ls._Params["src"], dir, mf, missing_files);
                        foreach (var ls in tf2.getMatches("image"))
                            this.haveEmbeddedItem(ls._Params["xlink:href"], dir, mf, missing_files);

                        foreach (var ls in tf2.getMatches("style"))
                        {
                            foreach (var ls2 in ls._Children)
                            {
                                if (ls2 is TagText)
                                {
                                    String text = (ls2 as TagText).text;

                                    int dex = -1;
                                    int dlex = 1;

                                    int count = -1;
                                    while (dlex > 0)
                                    {
                                        count++;
                                        dex = dlex;
                                        dlex = text.IndexOf("@font-face", dex + 1);

                                        if (dlex > 0)
                                        {
                                            int first = text.IndexOf("{", dlex);
                                            int last = text.IndexOf("}", dlex);
                                            var path = FontFaceSerializer.getFontPath(
                                                text.Substring(first + 1, last - first - 1));
                                            if (path != null) this.haveEmbeddedItem(path, dir, mf, missing_files);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var comb in missing_files)
                {
                    Console.WriteLine("Error: Missing File Reference: " + comb);
                }

                var _ContentTextL = new List<ManifestFile>();
                var _ContentOtherL = new List<ManifestFile>();

                foreach (ManifestFile mf in _ContentNonImageL)
                {
                    if ((mf._MediaType == ManifestFile.MediaType.Text) && (mf._IntRefrencesMax == 0))
                        _ContentTextL.Add(mf);
                    else _ContentOtherL.Add(mf);
                }

                _ContentTextL.Sort();

                this._ItemImageHandler._Content = _ContentImageL.ToArray();
                this._ItemTextHandler._Content = _ContentTextL.ToArray();
                this._ItemOtherHandler._Content = _ContentOtherL.ToArray();

                this.uiTableView1.reloadData();
                this.uiTableView2.reloadData();
                this.uiTableView3.reloadData();
            }
            finally
            {
                this._DoingStuff = false;
            }
        }

        public string purgeIntermediatePaths(String comb)
        {
            while (comb.IndexOf("/../") > 0)
            {
                var last = -1;
                int delta = comb.IndexOf("/../");
                while (comb.IndexOf("/", last + 1) < delta) last = comb.IndexOf("/", last + 1);
                comb = ((last > 0) ? comb.Substring(0, last + 1) : "")  + comb.Substring(delta + 4);
            }
            comb = comb.Replace("%20", " ");
            return comb;
        }

        public void haveEmbeddedItem(String src, String dir, ManifestFile mf, HashSet<String> not_found)
        {
            String comb = this.purgeIntermediatePaths(dir + src);
            ManifestFile matching_file;
            if (this._DictItemsByPath.TryGetValue(comb, out matching_file))
            {
                if (mf.Dependants.Contains(matching_file)) ;
                else if (mf == matching_file) ;
                else
                {
                    mf.Dependants.Add(matching_file);
                    matching_file.addReferenceMax();
                    if (mf.Checked) matching_file.addReference();
                }
            }
            else
            {
                not_found.Add(comb);
            }                            
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            this.bOpen.Enabled = true;
            this.bLook.Enabled = true;
            this.bClear.Enabled = false;
            this.bGo.Enabled = false;
            this.textBoxPath.Enabled = true;

            this._ItemImageHandler.Clear();
            this._ItemOtherHandler.Clear();
            this._ItemTextHandler.Clear();

            this.uiTableView1.reloadData();
            this.uiTableView2.reloadData();
            this.uiTableView3.reloadData();

            this._DictItemsByNode.Clear();
            this._DictItemsByPath.Clear();
            this._DictItemsByXMLID.Clear();
            this._ListItems.Clear();

            if (this.pictureBox1.Image != null)
            {
                this.pictureBox1.Image.Dispose();
                this.pictureBox1.Image = null;
            }

            this.webBrowser1.Navigate("");

            this._TagFileBase = null;
            this._TagFileMetadata = null;

            Console.WriteLine("");
            Console.WriteLine("******************************");
            Console.WriteLine("*********** Cleared **********");
            Console.WriteLine("******************************");
            Console.WriteLine("");
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.A))
            {
                this._ItemTextHandler.SelectAll();
                this.uiTableView1.reloadData();
                return true;
            }
            else return base.ProcessCmdKey(ref msg, keyData);
        }




























        private void bGo_Click(object sender, EventArgs e)
        {
            Console.WriteLine("");
            Console.WriteLine("******************************");
            Console.WriteLine("*********** Saving ***********");
            Console.WriteLine("******************************");
            Console.WriteLine("");

            this.textBoxAuthor.Text = this.textBoxAuthor.Text.Trim();
            this.textBoxGenre.Text = this.textBoxGenre.Text.Trim();
            this.textBoxTitle.Text = this.textBoxTitle.Text.Trim();

            String base_path = Directory.GetParent(Directory.GetParent(this.textBoxPath.Text).FullName).FullName;

            String epub_title = this.textBoxGenre.Text + " " + this.textBoxTitle.Text + ".epub";

            epub_title = epub_title.ToLower();
            epub_title = epub_title.Replace(" ", "_");

            String new_home = Path.Combine(base_path, "new_books", epub_title);

            if (Directory.Exists(new_home)) Directory.Delete(new_home, true);

            System.Threading.Thread.Sleep(100);
            Directory.CreateDirectory(new_home);
            System.Threading.Thread.Sleep(50);

            foreach (var mf in this._ListItems)
            {
                if (mf.Checked)
                {
                    string new_path = Path.Combine(new_home, mf._StringPath);
                    if (File.Exists(new_path))
                    {
                        Console.WriteLine("Error: Trying to Copy A File Twice: " + mf._StringPath);

                    }
                    else
                    {
                        String dir = Directory.GetParent(new_path).FullName;
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                            System.Threading.Thread.Sleep(50);
                        }
                        File.Copy(mf._StringPathFull, new_path);
                    }
                }
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(new_home, "content.opf")))
            {
                sw.WriteLine("<?xml version='1.0' encoding='utf-8'?>");
                this.saveXML(this._TagFileBase, sw, 0);
            }

            var tf = TagFile.ParseText(File.ReadAllText(Path.Combine(this.textBoxPath.Text, "toc.ncx")));
            var ncx_matches = tf.getMatches("ncx");
            if (ncx_matches.Count != 1)
            {
                MessageBox.Show("Invalid XML 1: toc.ncx");
                return;
            }
            if (!(ncx_matches[0] is TagFile))
            {
                MessageBox.Show("Invalid XML 2: toc.ncx");
                return;
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(new_home, "toc.ncx")))
            {
                sw.WriteLine("<?xml version='1.0' encoding='utf-8'?>");
                this.saveXML(ncx_matches[0] as TagFile, sw, 0);
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(new_home, "mimetype")))
            {
                sw.Write("application/epub+zip");
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(new_home, "iTunesMetadata.plist")))
            {
                sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">\n<plist version=\"1.0\">\n<dict>\n\t<key>artistName</key>\n\t<string>"
                + this.textBoxAuthor.Text + "</string>\n\t<key>itemName</key>\n\t<string>"
                + this.textBoxTitle.Text + "</string>\n\t<key>genre</key>\n\t<string>"
                + this.textBoxGenre.Text + "</string>\n</dict>\n</plist>\n");
            }

            String meta_inf = Path.Combine(new_home, "META-INF");
            if (!Directory.Exists(meta_inf))
            {
                Directory.CreateDirectory(meta_inf);
                System.Threading.Thread.Sleep(50);
            }

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(meta_inf, "container.xml")))
            {
                sw.WriteLine("<?xml version=\"1.0\"?>");
                sw.WriteLine("<container version=\"1.0\" xmlns=\"urn:oasis:names:tc:opendocument:xmlns:container\">");
                sw.WriteLine("\t<rootfiles>");
                sw.WriteLine("\t\t<rootfile full-path=\"content.opf\" media-type=\"application/oebps-package+xml\"/>");
                sw.WriteLine("\t</rootfiles>");
                sw.WriteLine("</container>");
            }

            
            Console.WriteLine("");
            Console.WriteLine("Finished Save");
        }

        void saveXML(TagItem ti, System.IO.StreamWriter sw, int indent)
        {
            bool save = true;
            ManifestFile mf = null;
            if (this._DictItemsByNode.TryGetValue(ti, out mf)) save = mf.Checked;

            if (save)
            {
                if (ti is TagFile)
                {
                    var tf = ti as TagFile;
                    if (tf._Children.Length > 0)
                    {
                        String f, b;
                        tf.getStringXML(out f, out b);
                        if ((tf._Children.Length == 1) &&
                            ("text".Equals(tf._Name)) &&
                            (tf._Children[0] is TagText))
                        {
                            for (int i = 0; i < indent; i++) sw.Write('\t');
                            sw.Write(f);
                            sw.Write((tf._Children[0] as TagText).text);
                            sw.WriteLine(b);
                        }
                        else
                        {
                            for (int i = 0; i < indent; i++) sw.Write('\t'); sw.WriteLine(f);
                            this.saveNextLevel(tf, sw, indent + 1);
                            for (int i = 0; i < indent; i++) sw.Write('\t'); sw.WriteLine(b);
                        }
                    }
                    else
                    {
                        String mid;
                        tf.getStringXML(out mid);
                        for (int i = 0; i < indent; i++) sw.Write('\t'); sw.WriteLine(mid);
                    }

                }
                else if (ti is TagText)
                {
                    var tt = ti as TagText;
                    for (int i = 0; i < indent; i++) sw.Write('\t'); sw.WriteLine(tt.text);
                }
                else Console.WriteLine("Error: Saving not a file or text");
            }
        }

        public void saveNextLevel(TagFile tf, System.IO.StreamWriter sw, int indent)
        {
            if (tf == this._TagFileMetadata)
            {
                foreach (var ch in tf._Children)
                {
                    if (ch is TagFile)
                    {
                        var tft = ch as TagFile;
                        switch (tft._Name)
                        {
                            case "dc:identifier":
                                this.saveXML(ch, sw, indent);
                                break;
                        }
                    }
                    else this.saveXML(ch, sw, indent);
                }

                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("<dc:creator opf:role=\"aut\">");
                for (int i = 0; i <= indent; i++) sw.Write('\t');
                sw.WriteLine(this.textBoxAuthor.Text);
                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("</dc:creator>");

                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("<dc:title>");
                for (int i = 0; i <= indent; i++) sw.Write('\t');
                sw.WriteLine(this.textBoxTitle.Text);
                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("</dc:title>");

                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("<dc:genre>");
                for (int i = 0; i <= indent; i++) sw.Write('\t');
                sw.WriteLine(this.textBoxGenre.Text);
                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("</dc:genre>");

                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("<dc:language>");
                for (int i = 0; i <= indent; i++) sw.Write('\t');
                sw.WriteLine("en");
                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("</dc:language>");

                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("<meta name=\"cover\" content=\"cover\"/>");
            }
            else if ("navMap".Equals(tf._Name))
            {
                Console.WriteLine("Single Find: \"navMap\"");
                int play_order = 1;

                foreach (var ti in tf._Children)
                {
                    if (ti is TagFile)
                    {
                        var np = ti as TagFile;
                        if ("navPoint".Equals(np._Name))
                            this.saveNavPoint(np, ref play_order, sw, indent);

                        else Console.WriteLine("Error: navMap has something besides navPoint");
                    }
                    else Console.WriteLine("Error: navMap has TagText");
                }
            }
            else if ("docTitle".Equals(tf._Name))
            {
                Console.WriteLine("Single Find: \"docTitle\"");
                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("<text>");
                for (int i = 0; i <= indent; i++) sw.Write('\t');
                sw.WriteLine(this.textBoxTitle.Text);
                for (int i = 0; i < indent; i++) sw.Write('\t');
                sw.WriteLine("</text>");
            }
            else if ("guide".Equals(tf._Name))
            {
                Console.WriteLine("Single Find: \"guide\"");
                foreach (var ti in tf._Children)
                {
                    if (ti is TagFile)
                    {
                        var np = ti as TagFile;
                        String href = null;
                        if (np._Params.TryGetValue("href", out href))
                        {
                            if ("titlepage.xhtml".Equals(href))
                            {
                                this.saveXML(np, sw, indent);
                            }
                        }
                    }
                }
            }
            else foreach (var ch in tf._Children) this.saveXML(ch, sw, indent);
        }

        public void saveNavPoint(TagFile np, ref int play_order, StreamWriter sw, int indent)
        {
            if (np._Params.ContainsKey("playOrder"))
                np._Params["playOrder"] = play_order.ToString();

            bool save = false;

            foreach (var ti_c in np._Children)
            {
                if (ti_c is TagFile)
                {
                    var tf_c = ti_c as TagFile;
                    if ("content".Equals(tf_c._Name))
                    {
                        String src = null;
                        if (tf_c._Params.TryGetValue("src", out src))
                        {
                            ManifestFile mf = null;
                            if (this._DictItemsByPath.TryGetValue(src, out mf))
                            {
                                if (mf.Checked)
                                {
                                    save = true;
                                    play_order++;
                                    break;
                                }
                            }
                            else Console.WriteLine("Error: navPoint missing ManifestFile: " + src);
                        }
                        else Console.WriteLine("Error: navPoint no source on content");
                    }
                }
            }


            if (save)
            {
                String f, b;
                np.getStringXML(out f, out b);
                for (int i = 0; i < indent; i++) sw.Write('\t'); sw.WriteLine(f);
                
                foreach (var ti_c in np._Children)
                {
                    if (ti_c is TagFile)
                    {
                        var tf_c = ti_c as TagFile;
                        if ("navPoint".Equals(tf_c._Name)) this.saveNavPoint(tf_c, ref play_order, sw, indent + 1);
                        else this.saveXML(ti_c, sw, indent + 1);
                    }
                    else this.saveXML(ti_c, sw, indent + 1);
                }
                for (int i = 0; i < indent; i++) sw.Write('\t'); sw.WriteLine(b);
            }
        }










        private void bExecuteAll_Click(object sender, EventArgs e)
        {
            foreach (var dir in Directory.GetDirectories(Directory.GetParent(this.textBoxPath.Text).FullName))
            {
                this.textBoxPath.Text = dir;
                if (this.bLook.Enabled)
                {
                    this.bLook_Click(this.bLook, EventArgs.Empty);
                    foreach (var s in this._ListItems)
                    {
                        s.Checked = true;
                        s.updateControls();
                    }
                    this.bGo_Click(this.bGo, EventArgs.Empty);
                    this.bClear_Click(this.bClear, EventArgs.Empty);
                }
            }
        }
    }
}