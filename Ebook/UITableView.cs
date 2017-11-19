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
            int numberOfSections(UITableView tv);
            int numberOfRowsInSection(UITableView tv, int section);

            Control viewForCellAtIndexPath(UITableView tv, IndexPath ip);
            Control viewForCellAtIndexPath(UITableView tv, IndexPath ip, Control c);
            int heightForCellAtIndexPath(UITableView tv, IndexPath ip);

            Control viewForHeaderInSection(UITableView tv, int section);
            Control viewForHeaderInSection(UITableView tv, int section, Control c);
            int heightForHeaderInSection(UITableView tv, int section);

            Control viewForFooterInSection(UITableView tv, int section);
            Control viewForFooterInSection(UITableView tv, int section, Control c);
            int heightForFooterInSection(UITableView tv, int section);
        }

        public class MyDisplay : Panel
        {
            public MyDisplay()
            {
                this.DoubleBuffered = true;
                // or
/*                SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                UpdateStyles();*/
            }
        }

        public Panel _Panel;
        public DataSource _DataSource = null;
        public Delegate _Delegate = null;

        private int _TotalHeight = 0;
        private int _CurrentHeight = 0;

        private Dictionary<IndexPath, Control> _CurrentCells = new Dictionary<IndexPath, Control>();

        public UITableView()
        {
            InitializeComponent();

            if (this.DesignMode) return;

            this._Panel = new MyDisplay();
            this._Panel.Location = new Point(0, 0);
            this._Panel.Size = new Size(this.Width - this._ScrollBar.Width, this.Height);
            this._Panel.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(this._Panel);
        }

        public void reloadData()
        {
            int w = this.Width - this._ScrollBar.Width - 2;

            int total_height = 1;

            if (this._DataSource == null)
            {
                Console.WriteLine("UITableView: (null) data source");
                return;
            }

            int temp_height;

            var current_values = this._CurrentCells.Keys.ToList();

            for (int s = 0; s < this._DataSource.numberOfSections(this); s++)
            {
                IndexPath ip = new IndexPath(s, -1);
                temp_height = this._DataSource.heightForHeaderInSection(this, s);
                if (temp_height > 0)
                {
                    Boolean inRange = 
                        (total_height + temp_height > this._CurrentHeight) &&
                        (this._CurrentHeight + this.Height > total_height);
                    Control c;
                    if (this._CurrentCells.TryGetValue(ip, out c))
                    {
                        if (inRange)
                        {
                            Control nc = this._DataSource.viewForHeaderInSection(this, s, c);
                            if (nc == null) c.Top = total_height - _CurrentHeight;
                            else 
                            {
                                this._Panel.Controls.Remove(c);
                                nc.Size = new Size(w, temp_height);
                                nc.Location = new Point(1, total_height - _CurrentHeight);
                                this._CurrentCells[ip] = nc;
                                this._Panel.Controls.Add(nc);
                            }
                        }
                        else
                        {
                            this._CurrentCells.Remove(ip);
                            this._Panel.Controls.Remove(c);
                        }
                        current_values.Remove(ip);
                    }
                    else if (inRange)
                    {
                        c = this._DataSource.viewForHeaderInSection(this, s);
                        c.Size = new Size(w, temp_height);
                        c.Location = new Point(1, total_height - _CurrentHeight);
                        this._CurrentCells[ip] = c;
                        this._Panel.Controls.Add(c);
                    }
                    total_height += temp_height + 1;
                }
                for (int r = 0; r < this._DataSource.numberOfRowsInSection(this, s); r++)
                {
                    ip.row = r;
                    temp_height = this._DataSource.heightForCellAtIndexPath(this, new IndexPath(s, r));
                    if (temp_height > 0)
                    {
                        int tab = 30;
                        Boolean inRange = (total_height + temp_height > _CurrentHeight) && (_CurrentHeight + this.Height > total_height);
                        Control c;
                        if (this._CurrentCells.TryGetValue(ip, out c))
                        {
                            current_values.Remove(ip);
                            if (inRange)
                            {
                                c.Top = total_height - _CurrentHeight;
                                Control nc = this._DataSource.viewForCellAtIndexPath(this, ip, c);
                                if (nc == null) c.Top = total_height - _CurrentHeight;
                                else
                                {
                                    this._Panel.Controls.Remove(c);
                                    nc.Size = new Size(w - tab, temp_height);
                                    nc.Location = new Point(1 + tab, total_height - _CurrentHeight);
                                    this._CurrentCells[ip] = nc;
                                    this._Panel.Controls.Add(nc);
                                }
                            }
                            else
                            {
                                this._CurrentCells.Remove(ip);
                                this._Panel.Controls.Remove(c);
                            }
                        }
                        else if (inRange)
                        {
                            c = this._DataSource.viewForCellAtIndexPath(this, ip);
                            c.Size = new Size(w - tab, temp_height);
                            c.Location = new Point(1 + tab, total_height - _CurrentHeight);
                            this._CurrentCells[ip] = c;
                            this._Panel.Controls.Add(c);
                        }
                        total_height += temp_height + 1;
                    }
                }
                ip.row = -2;
                temp_height = this._DataSource.heightForFooterInSection(this, s);
                if (temp_height > 0)
                {
                    Boolean inRange = (total_height + temp_height > _CurrentHeight) && (_CurrentHeight + this.Height > total_height);
                    Control c;
                    if (this._CurrentCells.TryGetValue(ip, out c))
                    {
                        current_values.Remove(ip);
                        if (inRange)
                        {
                            Control nc = this._DataSource.viewForFooterInSection(this, s, c);
                            if (nc == null) c.Top = total_height - _CurrentHeight;
                            else
                            {
                                this._Panel.Controls.Remove(c);
                                nc.Size = new Size(w, temp_height);
                                nc.Location = new Point(1, total_height - _CurrentHeight);
                                this._CurrentCells[ip] = nc;
                                this._Panel.Controls.Add(nc);
                            }
                        }
                        else
                        {
                            this._CurrentCells.Remove(ip);
                            this._Panel.Controls.Remove(c);
                        }
                    }
                    else if (inRange)
                    {
                        c = this._DataSource.viewForFooterInSection(this, s);
                        c.Size = new Size(w, temp_height);
                        c.Location = new Point(1, total_height - _CurrentHeight);
                        this._CurrentCells[ip] = c;
                        this._Panel.Controls.Add(c);
                    }
                    total_height += temp_height + 1;
                }
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
                    this.reloadData();
                }
                else
                {
                    this._TotalHeight = total_height;
                    this.setScroll();
                }
            }
            else
            {
                this._ScrollBar.Enabled = scroll;
                if (scroll)
                {
                    this._TotalHeight = total_height;
                    this.setScroll();
                }
                else
                {
                    this._CurrentHeight = 0;
                    this.reloadData();
                }
            }
        }

        private void _ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Double f = this._ScrollBar.Value;
            f /= (this._ScrollBar.Maximum - this._ScrollBar.LargeChange + 1);
            this._CurrentHeight = (int)((this._TotalHeight - this.Height) * f);
            this.reloadData();
        }

        public void setScroll()
        {
            this._ScrollBar.Value =
                (this._CurrentHeight * (this._ScrollBar.Maximum - this._ScrollBar.LargeChange + 1)) / (this._TotalHeight - this.Height);
        }

        private void UITableView_Scroll(object sender, ScrollEventArgs e)
        {
            Console.WriteLine("Scroll");
        }

        private void UITableView_Resize(object sender, EventArgs e)
        {
            this.reloadData();
        }
    }
}
