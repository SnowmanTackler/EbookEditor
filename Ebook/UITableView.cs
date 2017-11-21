using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SamSeifert.Utilities;

namespace Ebook
{
    public partial class UITableView : UserControl
    {
        public struct IndexPath           
        {
            public int section;
            public int row;

            public IndexPath(int section, int row)
            {
                this.row = row;
                this.section = section;
            }

            public static bool operator == (IndexPath x, IndexPath y) 
            {
                return (x.row == y.row) && (x.section == y.section);
            }

            public static bool operator != (IndexPath x, IndexPath y) 
            {
                return !(x == y);
            }

            public override bool Equals(object obj)
            {
                if (obj is IndexPath) return this == ((IndexPath)obj);
                else return false;
            }

            public override int GetHashCode() //from System.Double
            {
                UInt16 i1 = (UInt16)this.section;
                UInt16 i2 = (UInt16)this.row;
                return (i1 << 16) | i2;
            }
        }

        public interface DataSource
        {
            // Happens on Scroll, Really Anything
            void Reloading();

            int numberOfSections(UITableView tv);
            int numberOfRowsInSection(UITableView tv, int section);

            /// <summary>
            /// C can be null.  It's the control we've currently got there.
            /// </summary>
            /// <param name="tv"></param>
            /// <param name="ip"></param>
            /// <param name="c"></param>
            /// <returns></returns>
            Control viewForCellAtIndexPath(UITableView tv, IndexPath ip, Control c);
            int heightForCellAtIndexPath(UITableView tv, IndexPath ip);

            Control viewForHeaderInSection(UITableView tv, int section, Control c);
            int heightForHeaderInSection(UITableView tv, int section);

            Control viewForFooterInSection(UITableView tv, int section, Control c);
            int heightForFooterInSection(UITableView tv, int section);
        }

        public Panel _Panel;
        public DataSource _DataSource = null;
        public Delegate _Delegate = null;

        private int _TotalHeight = 0;
        private int _CurrentHeight = 0;

        private Dictionary<IndexPath, Control> _CurrentCells = new Dictionary<IndexPath, Control>();

        // Controls x offset for the rows! (vs headers and footers)
        public int Tab { get; set; } = 10;
        public int VerticalSpacing { get; set; } = 1;

        public UITableView()
        {
            InitializeComponent();
        }

        private void UITableView_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            this._Panel = new Panel();
            this._Panel.Dock = DockStyle.Fill;
            this.Controls.Add(this._Panel);

            this.ReloadData();

            this.MouseWheel += this._Panel_MouseMove;
        }

        public void ReloadData()
        {
            if (this._DataSource == null)
                return;

            if (this._Panel == null)
                return;

            this._DataSource.Reloading();

            using (new SamSeifert.Utilities.CustomControls.LayoutSuspender(this._Panel))
            {
                int w = this.Width - this._ScrollBar.Width - 2;

                int total_height = 1;


                var current_values = new HashSet<IndexPath>(this._CurrentCells.Keys);

                Action<IndexPath, Func<Control, Control>, int, int> addc =
                    (IndexPath ip, Func<Control, Control> refresh, int temp_height, int tab) =>
                {
                    Control c;
                    Boolean inRange =
                        (total_height + temp_height > this._CurrentHeight) &&
                        (this._CurrentHeight + this.Height > total_height);

                    int control_y = total_height - this._CurrentHeight;
                    total_height += temp_height + this.VerticalSpacing;

                    if (this._CurrentCells.TryGetValue(ip, out c))
                    {
                        current_values.Remove(ip);
                        if (inRange)
                        {
                            Control nc = refresh(c);
                            if (nc == c)
                            {
                                c.Top = control_y;
                                return;
                            }
                            else
                            {
                                c?.RemoveFromParent();
                                c?.Dispose();
                                c = nc;
                                // Add at end of action
                            }
                        }
                        else
                        {
                            this._CurrentCells.Remove(ip);
                            this._Panel.Controls.Remove(c);
                            return;
                        }
                    }
                    else if (inRange)
                    {
                        c = refresh(null);
                    }

                    if (c == null)
                    {
                        this._CurrentCells.Remove(ip);
                    }
                    else
                    {
                        c.Size = new Size(w - tab, temp_height);
                        c.Location = new Point(1 + tab, control_y);
                        this._CurrentCells[ip] = c;
                        this._Panel.Controls.Add(c);
                    }
                };


                for (int s = 0; s < this._DataSource.numberOfSections(this); s++)
                {
                    IndexPath ip = new IndexPath(s, -1);
                    addc(ip, // index path
                         (Control c) => this._DataSource.viewForHeaderInSection(this, s, c), // control refresh
                         this._DataSource.heightForHeaderInSection(this, s), // cell height
                         0); // cell x
                    
                    for (int r = 0; r < this._DataSource.numberOfRowsInSection(this, s); r++)
                    {
                        ip.row = r;
                        addc(ip, // index path
                             (Control c) => this._DataSource.viewForCellAtIndexPath(this, ip, c), // control refresh
                             this._DataSource.heightForCellAtIndexPath(this, ip), // cell height
                             this.Tab); // cell x
                    }
                    ip.row = -2;

                    addc(ip, // index path
                         (Control c) => this._DataSource.viewForFooterInSection(this, s, c), // control refresh
                         this._DataSource.heightForFooterInSection(this, s), // cell height
                         0); // cell x
                }

                foreach (var ip in current_values)
                {
                    Control c = this._CurrentCells[ip];
                    this._Panel.Controls.Remove(c);
                    this._CurrentCells.Remove(ip);
                }

                Boolean scroll = this.Height < total_height;
                if (scroll == this._ScrollBar.Enabled)
                {
                    if (scroll && (this._CurrentHeight + this.Height > total_height))
                    {
                        this._CurrentHeight = total_height - this.Height;
                        this.ReloadData();
                    }
                    else
                    {
                        this._TotalHeight = total_height;
                        this.SetScroll();
                    }
                }
                else
                {
                    this._ScrollBar.Enabled = scroll;
                    if (scroll)
                    {
                        this._TotalHeight = total_height;
                        this.SetScroll();
                    }
                    else
                    {
                        this._CurrentHeight = 0;
                        this.ReloadData();
                    }
                }
            }
        }

        private void _Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Delta != 0) && this._ScrollBar.Enabled)
            {
                this._CurrentHeight -= e.Delta / 10;
                this._CurrentHeight = Math.Max(0, this._CurrentHeight);
                this.ReloadData();
            }
        }


        private void ScrollEventHandler(object sender, ScrollEventArgs e)
        {
            Double f = this._ScrollBar.Value;
            f /= (this._ScrollBar.Maximum - this._ScrollBar.LargeChange + 1);
            this._CurrentHeight = (int)((this._TotalHeight - this.Height) * f);
            this.ReloadData();
        }

        public void SetScroll()
        {
            this._ScrollBar.Value = Math.Max(this._ScrollBar.Minimum, Math.Min(this._ScrollBar.Maximum,
                (this._CurrentHeight * (this._ScrollBar.Maximum - this._ScrollBar.LargeChange + 1)) / (this._TotalHeight - this.Height)));
        }

        private void UITableView_Resize(object sender, EventArgs e)
        {
            this.ReloadData();
        }
    }

    public static class UITableViewExtensions
    {
        /// <summary>
        /// Gets the nearest parent UITableView
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static UITableView getUITableView(this Control c)
        {
            if (c is UITableView) return c as UITableView;
            else return c.Parent?.getUITableView();

        }

    }


}
