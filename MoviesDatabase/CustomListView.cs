using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MoviesDatabase
{
    /// <summary>
    /// Double-Buffered verion of the Windows ListView.
    /// </summary>
    public class CustomListView : System.Windows.Forms.ListView
    {
        /// <summary>
        /// BackgroundColor for all currently selected films.
        /// </summary>
        Color SelectedColor = Color.DodgerBlue;
        /// <summary>
        /// Corner radius for background of title.
        /// </summary>
        int CornerRadius = 10;
        /// <summary>
        /// Size the images will be drawn with.
        /// </summary>
        public Size ImageSize = new Size(100, 150);
        /// <summary>
        /// Way better than using an ImageList
        /// </summary>
        public Dictionary<string, Image> ImageList = new Dictionary<string, Image>();

        /// <summary>
        /// Turn fancyvisuals on/off.
        /// </summary>
        bool FancyVisuals = true;
        public CustomListView()
            : base()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            this.OwnerDraw = true;
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (e.ItemIndex == -1)
            {
                e.DrawDefault = true;
                e.DrawBackground();
                e.DrawText();
                return;
            }
            if (this.View == View.Tile || this.View == View.LargeIcon)
            {
                try
                {
                    e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
                    using (StringFormat sf = new StringFormat())
                    {
                        Color Background;
                        Brush b, TitleFore, YearFore;
                        Image img = this.ImageList[e.Item.ImageKey];
                        if (img == null)
                        {
                            e.DrawDefault = true;
                            e.DrawBackground();
                            e.DrawText();
                            return;
                        }

                        Rectangle r = new Rectangle(e.Bounds.X + 15, e.Bounds.Y + 5, e.Bounds.Width - 30, img.Height + 10);
                        int x = e.Bounds.X + (e.Bounds.Width - img.Width) / 2;
                        int y = e.Bounds.Y + 10;

                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        Rectangle tr = new Rectangle(e.Bounds.X + 5, e.Bounds.Y + img.Height + 20, e.Bounds.Width - 10, this.Font.Height);
                        Rectangle tr2 = new Rectangle(e.Bounds.X + 5, e.Bounds.Y + img.Height + 20 + this.Font.Height, e.Bounds.Width - 10, this.Font.Height);

                        if ((e.State & ListViewItemStates.Selected) == ListViewItemStates.Selected)
                        {
                            Background = this.SelectedColor;
                            TitleFore = Brushes.Black;
                            YearFore = Brushes.Black;
                        }
                        else
                        {
                            Background = e.Item.BackColor;
                            TitleFore = Brushes.DarkRed;
                            YearFore = Brushes.Gray;
                        }

                        GraphicsPath gfp = new GraphicsPath();
                        gfp.AddArc(tr.X, tr.Y, this.CornerRadius, this.CornerRadius, 180, 90);
                        gfp.AddArc(tr.X + tr.Width - this.CornerRadius, tr.Y, this.CornerRadius, this.CornerRadius, 270, 90);
                        gfp.AddArc(tr.X + tr.Width - this.CornerRadius, tr.Y + tr.Height - this.CornerRadius, this.CornerRadius, this.CornerRadius, 0, 90);
                        gfp.AddArc(tr.X, tr.Y + tr.Height - this.CornerRadius, this.CornerRadius, this.CornerRadius, 90, 90);
                        gfp.CloseAllFigures();


                        if (FancyVisuals)
                        {
                            b = new LinearGradientBrush(new Point(e.Bounds.X, e.Bounds.Y), new Point(e.Bounds.X + 200, e.Bounds.Y + 20), Background, Color.Black);
                            e.Graphics.FillRectangle(b, r);
                            if (e.Bounds.Contains(this.PointToClient(Cursor.Position)))
                            {
                                using (Brush br = new SolidBrush(Color.FromArgb(200, Background)))
                                    e.Graphics.FillPath(br, gfp);
                                TitleFore = Brushes.Black;
                            }
                            else
                                using (Brush br = new SolidBrush(Color.FromArgb(80, Background)))
                                    e.Graphics.FillPath(br, gfp);
                        }
                        else
                        {
                            b = new SolidBrush(Background);
                            e.Graphics.FillRectangle(b, r);
                            e.Graphics.FillPath(new SolidBrush(Background), gfp);
                        }
                        e.Graphics.DrawImage(img, new Rectangle(x, y, this.ImageSize.Width, this.ImageSize.Height));

                        Font f = new Font(this.Font, FontStyle.Regular);
                        while (true)
                        {
                            if (e.Graphics.MeasureString(e.Item.Text, f).Width >= r.Width)
                                f = new Font(f.FontFamily, f.Size - 1, FontStyle.Regular);
                            else
                                break;
                        }
                        e.Graphics.DrawString(e.Item.Text, f, TitleFore, tr, sf);
                        if (e.Item.Group.Header == "Movies") // Draw Year if it is a movie
                            e.Graphics.DrawString(e.Item.SubItems[1].Text, this.Font, YearFore, tr2, sf);
                        else
                        { // Show seasons/episodes
                            if (e.Item.SubItems.Count >= 2)
                            {
                                string text = "";
                                for (int i = 1; i < e.Item.SubItems.Count; i++)
                                    text += e.Item.SubItems[i].Text + " ";
                                e.Graphics.DrawString(text, this.Font, YearFore, tr2, sf);
                            }
                        }
                        e.Graphics.Flush();
                        b.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
        }
    }

    public partial class CustomCheckBox : CheckBox
    {
        private int CornerRadius = 15;
        private Pen BorderPen { get; set; }
        private Pen CheckPen { get; set; }
        private int padding = 2;
        private Brush backgroundbrush = new SolidBrush(Color.Transparent);

        public CustomCheckBox()
            : base()
        {
            base.FlatStyle = FlatStyle.Flat;
            BorderPen = new Pen(Color.DodgerBlue, 3);
            CheckPen = new Pen(Color.Green, 3);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF R = new RectangleF(e.Graphics.ClipBounds.X, e.Graphics.ClipBounds.Y, e.Graphics.ClipBounds.Width, e.Graphics.ClipBounds.Height);
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), R);

            GraphicsPath gfp = new GraphicsPath();
            gfp.StartFigure();
            gfp.AddArc(R.X + BorderPen.Width + padding, R.Y + BorderPen.Width + padding, this.CornerRadius, this.CornerRadius, 180, 90);
            gfp.AddArc(R.X + R.Width - this.CornerRadius - BorderPen.Width - padding, R.Y + BorderPen.Width + padding, this.CornerRadius, this.CornerRadius, 270, 90);
            gfp.AddArc(R.X + R.Width - this.CornerRadius - BorderPen.Width - padding, R.Y + R.Height - this.CornerRadius - BorderPen.Width - padding, this.CornerRadius, this.CornerRadius, 0, 90);
            gfp.AddArc(R.X + BorderPen.Width + padding, R.Y + R.Height - this.CornerRadius - BorderPen.Width - padding, this.CornerRadius, this.CornerRadius, 90, 90);
            gfp.CloseAllFigures();
            e.Graphics.DrawPath(BorderPen, gfp);

            if (this.Checked)
            {
                e.Graphics.DrawLines(CheckPen, new PointF[] { new PointF(e.ClipRectangle.X + e.ClipRectangle.Width * (float)0.2, e.ClipRectangle.Y + e.ClipRectangle.Height * (float)0.4), new PointF(e.ClipRectangle.X + e.Graphics.ClipBounds.Width * (float)0.4, e.ClipRectangle.Y + e.Graphics.ClipBounds.Height * (float)0.7), new PointF(e.ClipRectangle.X + e.Graphics.ClipBounds.Width * (float)0.8, e.ClipRectangle.Y + e.Graphics.ClipBounds.Height * (float)0.3) });
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            this.Checked = !this.Checked;
        }
    }

    public partial class GradientLabel : Label
    {
        [Description("Color of Gradient 1"), DefaultValue(typeof(Color), "LightGray")]
        public Color Gradient1 { get; set; }
        [Description("Color of Gradient 2"), DefaultValue(typeof(Color), "DarkBlue"), EditorBrowsable(EditorBrowsableState.Always)]
        public Color Gradient2 { get; set; }

        public GradientLabel()
            : base()
        {
            this.Gradient1 = Color.LightGray;
            this.Gradient2 = Color.DarkBlue;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, Width, Height + 5), Gradient1, Gradient2, LinearGradientMode.Vertical);
            e.Graphics.DrawString(Text, this.Font, brush, 0, 0);
        }
    }
}
