using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MoviesDatabase
{
    public partial class MainForm : Form
    {
        [DllImport("user32")]
        private static extern bool SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam);

        private uint LVM_SETTEXTBKCOLOR = 0x1026;

        /// <summary>
        /// List of all movies and tv series.
        /// </summary>
        List<Movie> Movies;
        /// <summary>
        /// ImageList containing all covers.
        /// </summary>
        Dictionary<string, Image> MovieImages;
        /// <summary>
        /// ListViewGroup for all the Movies.
        /// </summary>
        ListViewGroup mvs = new ListViewGroup("Movies");
        /// <summary>
        /// BackgroundColor for all films that haven't been downloaded yet.
        /// </summary>
        Color NotDownloadedColor = Color.OrangeRed;
        /// <summary>
        /// BackgroundColor for all films that haven't been watched yet.
        /// </summary>
        Color NotWatchedColor = Color.SeaGreen;
        /// <summary>
        /// A semitransparent loading-screen.
        /// </summary>
        Plexiglass Plexi;
        /// <summary>
        /// BackgroundColor for all films that have been downloaded and watched.
        /// </summary>
        Color SeenColor = Color.WhiteSmoke;
        /// <summary>
        /// ListViewGroup for all the Tv shows.
        /// </summary>
        ListViewGroup tvs = new ListViewGroup("TV Shows");

        /// <summary>
        /// Initialize a new instance of this form.
        /// </summary>
        /// <param name="_Movies">List of all movies and Tv series.</param>
        /// <param name="_MovieImages">Dictionary containing the covers of all films.</param>
        public MainForm(List<Movie> _Movies, Dictionary<string, Image> _MovieImages, int movieCount, int tvCount)
        {
            Plexi = new Plexiglass(this);
            ShowLoadingScreen();
            InitializeComponent();
            this.Movies = _Movies;
            this.MovieImages = _MovieImages;
            toolStripStatusLabel1.Text = movieCount + " Movies | " + tvCount + " Tv Series.";
            showMoviesToolStripMenuItem.Checked = Properties.Settings.Default.ShowMovies;
            showTvSeriesToolStripMenuItem.Checked = Properties.Settings.Default.ShowTVSeries;
            showOnlyDownloadedToolStripMenuItem.Checked = Properties.Settings.Default.ShowDownloaded;
            showNotViewedToolStripMenuItem.Checked = Properties.Settings.Default.ShowNotViewed;
            hideViewedToolStripMenuItem.Checked = Properties.Settings.Default.ShowViewed;
            showOnlyNotDownloadedToolStripMenuItem.Checked = Properties.Settings.Default.ShowNotDownloaded;

            listViewSearchResults.Visible = false;
            listViewSearchResults.Height = 0;

            mvs.HeaderAlignment = HorizontalAlignment.Center;
            tvs.HeaderAlignment = HorizontalAlignment.Center;
            listViewMovies.Sorting = SortOrder.None;
            listViewMovies.ImageList = MovieImages;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SendMessage(listViewMovies.Handle, LVM_SETTEXTBKCOLOR, IntPtr.Zero, unchecked((IntPtr)(int)0xFFFFFF));
            listViewMovies.ImageList = this.MovieImages;
            listViewMovies.Groups.Add(mvs);
            listViewMovies.Groups.Add(tvs);
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 4;
            RefreshMovies();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (listViewMovies.SelectedItems.Count < 1)
            {
                downloadedToolStripMenuItem.Enabled = false;
                watchedToolStripMenuItem.Enabled = false;
                downloadedToolStripMenuItem.Checked = false;
                watchedToolStripMenuItem.Checked = false;
                contextMenuStripItemEdit.Close();
            }
            else if (listViewMovies.SelectedItems.Count > 1)
            {
                downloadedToolStripMenuItem.Enabled = false;
                watchedToolStripMenuItem.Enabled = false;
                downloadedToolStripMenuItem.Checked = false;
                watchedToolStripMenuItem.Checked = false;
            }
            else
            {
                Movie m = (Movie)listViewMovies.SelectedItems[0].Tag;
                if (m != null)
                {
                    downloadedToolStripMenuItem.Checked = m.Downloaded;
                    watchedToolStripMenuItem.Checked = m.Watched;
                }
                downloadedToolStripMenuItem.Enabled = true;
                watchedToolStripMenuItem.Enabled = true;
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            if (listViewMovies.SelectedItems.Count < 1)
                contextMenuStripItemEdit.Close();
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listViewMovies.SelectedItems)
            {
                Movie m = (Movie)i.Tag;
                if (m != null)
                    Process.Start(m.FullPath);
            }
        }

        private void downloadedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            downloadedToolStripMenuItem.Checked = !downloadedToolStripMenuItem.Checked;
            if (listViewMovies.SelectedItems.Count == 1)
            {
                Movie m = (Movie)listViewMovies.SelectedItems[0].Tag;
                if (m != null)
                {
                    m.Downloaded = downloadedToolStripMenuItem.Checked;
                    m.Watched = watchedToolStripMenuItem.Checked;
                    listViewMovies.SelectedItems[0].BackColor = Color.Transparent;
                    listViewMovies.SelectedItems[0].ForeColor = Color.Black;
                    if (!m.Downloaded)
                        listViewMovies.SelectedItems[0].BackColor = NotDownloadedColor;
                    else if (!m.Watched)
                        listViewMovies.SelectedItems[0].BackColor = NotWatchedColor;
                    else
                        listViewMovies.SelectedItems[0].BackColor = SeenColor;
                }
            }
        }

        private void watchedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            watchedToolStripMenuItem.Checked = !watchedToolStripMenuItem.Checked;
            if (watchedToolStripMenuItem.Checked)
                downloadedToolStripMenuItem.Checked = true;
            if (listViewMovies.SelectedItems.Count == 1)
            {
                Movie m = (Movie)listViewMovies.SelectedItems[0].Tag;
                if (m != null)
                {
                    m.Downloaded = downloadedToolStripMenuItem.Checked;
                    m.Watched = watchedToolStripMenuItem.Checked;
                    listViewMovies.SelectedItems[0].BackColor = Color.Transparent;
                    listViewMovies.SelectedItems[0].ForeColor = Color.Black;
                    if (!m.Downloaded)
                        listViewMovies.SelectedItems[0].BackColor = NotDownloadedColor;
                    else if (!m.Watched)
                        listViewMovies.SelectedItems[0].BackColor = NotWatchedColor;
                    else
                        listViewMovies.SelectedItems[0].BackColor = SeenColor;
                }
            }
        }

        private void contextMenuStripItemEdit_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (listViewMovies.SelectedItems.Count == 1)
            {
                Movie m = (Movie)listViewMovies.SelectedItems[0].Tag;
                if (m != null)
                {
                    m.Downloaded = downloadedToolStripMenuItem.Checked;
                    m.Watched = watchedToolStripMenuItem.Checked;
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMovies.SelectedItems.Count == 1)
            {
                ListViewItem it = listViewMovies.SelectedItems[0];
                Movie m = (Movie)it.Tag;
                if (m != null)
                {
                    EditForm ef = new EditForm(m);
                    if (ef.ShowDialog() == DialogResult.OK)
                    {
                        m.Name = ef.NewTitle;
                        m.PublishedYear = ef.NewYear;
                        m.Watched = ef.NewWatched;
                        m.Downloaded = ef.NewDownloaded;
                        m.Description = ef.NewDescription;

                        it.Text = m.Name;
                        if(m.Type == 1)
                            it.SubItems.Add(m.YearToString());
                        if (!m.Downloaded)
                            it.BackColor = NotDownloadedColor;
                        else if (!m.Watched)
                            it.BackColor = NotWatchedColor;
                        else
                            it.BackColor = SeenColor;
                    }
                }
            }
        }

        private void copyTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listViewMovies.SelectedItems[0].Text);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            ReloadMovies();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadMovies();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Copy title
            if (listViewMovies.SelectedItems.Count == 1)
            {
                Clipboard.SetText(listViewMovies.SelectedItems[0].Text);
            }
        }

        private void showMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showMoviesToolStripMenuItem.Checked = !showMoviesToolStripMenuItem.Checked;
            if (!showMoviesToolStripMenuItem.Checked && !showTvSeriesToolStripMenuItem.Checked)
                showTvSeriesToolStripMenuItem.Checked = true;
            Properties.Settings.Default.ShowMovies = showMoviesToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowTVSeries = showTvSeriesToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            RefreshMovies();
        }

        private void showTvSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showTvSeriesToolStripMenuItem.Checked = !showTvSeriesToolStripMenuItem.Checked;
            if (!showMoviesToolStripMenuItem.Checked && !showTvSeriesToolStripMenuItem.Checked)
                showMoviesToolStripMenuItem.Checked = true;
            Properties.Settings.Default.ShowMovies = showMoviesToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowTVSeries = showTvSeriesToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            RefreshMovies();
        }

        private void hideViewedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideViewedToolStripMenuItem.Checked = !hideViewedToolStripMenuItem.Checked;
            if (!hideViewedToolStripMenuItem.Checked && !showNotViewedToolStripMenuItem.Checked)
                showNotViewedToolStripMenuItem.Checked = true;
            Properties.Settings.Default.ShowNotViewed = showNotViewedToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowViewed = hideViewedToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            RefreshMovies();
        }

        private void showNotViewedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showNotViewedToolStripMenuItem.Checked = !showNotViewedToolStripMenuItem.Checked;
            if (!hideViewedToolStripMenuItem.Checked && !showNotViewedToolStripMenuItem.Checked)
                hideViewedToolStripMenuItem.Checked = true;
            Properties.Settings.Default.ShowNotViewed = showNotViewedToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowViewed = hideViewedToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            RefreshMovies();
        }

        private void showOnlyDownloadedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showOnlyDownloadedToolStripMenuItem.Checked = !showOnlyDownloadedToolStripMenuItem.Checked;
            if (!showOnlyDownloadedToolStripMenuItem.Checked && !showOnlyNotDownloadedToolStripMenuItem.Checked)
                showOnlyNotDownloadedToolStripMenuItem.Checked = true;
            Properties.Settings.Default.ShowDownloaded = showOnlyDownloadedToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowNotDownloaded = showOnlyNotDownloadedToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            RefreshMovies();
        }

        private void showOnlyNotDownloadedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showOnlyNotDownloadedToolStripMenuItem.Checked = !showOnlyNotDownloadedToolStripMenuItem.Checked;
            if (!showOnlyDownloadedToolStripMenuItem.Checked && !showOnlyNotDownloadedToolStripMenuItem.Checked)
                showOnlyDownloadedToolStripMenuItem.Checked = true;
            Properties.Settings.Default.ShowDownloaded = showOnlyDownloadedToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowNotDownloaded = showOnlyNotDownloadedToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
            RefreshMovies();
        }

        private void listViewMovies_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenMovie();
        }

        private void openIMDBPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMovies.SelectedItems.Count == 1)
            {
                Movie m = (Movie)listViewMovies.SelectedItems[0].Tag;
                if (m != null)
                {
                    WebRequest req = WebRequest.Create("http://www.omdbapi.com/?t=" + m.Name);
                    req.Method = "GET";
                    req.Proxy = null;
                    using (StreamReader r = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        string response = r.ReadToEnd();
                        JObject obj = JObject.Parse(response);
                        Process.Start("http://www.imdb.com/title/" + (string)obj["imdbID"]);
                        r.Dispose();
                    }
                }
            }
        }

        private void downloadPostersFromIMDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Movie m in this.Movies.Where(s => s.NoCover == true))
            {
                WebRequest req = WebRequest.Create("http://www.omdbapi.com/?t=" + m.Name.Replace("-", "").Replace("And", " ").Replace("and", " ").Replace("The", " ").Replace("the", " "));
                req.Method = "GET";
                using (StreamReader r = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    string response = r.ReadToEnd();
                    JObject obj = JObject.Parse(response); 
                    using(WebClient webClient = new WebClient())
                        if(obj["Poster"] != null && obj["Poster"].ToString() != "N/A")
                            webClient.DownloadFile((string)obj["Poster"], m.PathToImage);
                    r.Dispose();
                }
            }
            ReloadMovies();
        }

        private void toggleWatchedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMovies.SelectedItems.Count >= 1)
            {
                foreach (ListViewItem it in listViewMovies.SelectedItems)
                {
                    Movie m = (Movie)it.Tag;
                    if (m != null)
                    {
                        m.Watched = !m.Watched;
                        it.BackColor = Color.Transparent;
                        it.ForeColor = Color.Black;
                        if (!m.Downloaded)
                            it.BackColor = NotDownloadedColor;
                        else if (!m.Watched)
                            it.BackColor = NotWatchedColor;
                        else
                            it.BackColor = SeenColor;
                    }
                }
            }
        }

        private void toggleDownlaodedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMovies.SelectedItems.Count >= 1)
            {
                foreach (ListViewItem it in listViewMovies.SelectedItems)
                {
                    Movie m = (Movie)it.Tag;
                    if (m != null)
                    {
                        m.Downloaded = !m.Downloaded;
                        it.BackColor = Color.Transparent;
                        it.ForeColor = Color.Black;
                        if (!m.Downloaded)
                            it.BackColor = NotDownloadedColor;
                        else if (!m.Watched)
                            it.BackColor = NotWatchedColor;
                        else
                            it.BackColor = SeenColor;
                    }
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewMovies.Items)
            {
                item.Selected = true;
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            string search = toolStripTextBox1.Text.ToLower();
            if (string.IsNullOrWhiteSpace(search))
                listViewSearchResults.Visible = false;
            else {
                listViewSearchResults.Visible = true;
                listViewSearchResults.Items.Clear();
                listViewSearchResults.Height = 0;
                int i = 0;
                foreach (Movie m in Movies.Where(s => s.Name.ToLower().Contains(search)))
                {
                    ListViewItem res = new ListViewItem();
                    res.Tag = m;
                    res.Text = m.Name;
                    
                    if (m.NoCover)
                        res.ImageKey = "NoCover";
                    if (!m.Downloaded)
                        res.BackColor = NotDownloadedColor;
                    else if (!m.Watched)
                        res.BackColor = NotWatchedColor;
                    else
                        res.BackColor = Color.WhiteSmoke;

                    res.ImageKey = m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2");
                    if (i <= 4)
                    {
                        listViewSearchResults.Height += 32;
                        i++;
                    }
                    listViewSearchResults.Items.Add(res);
                }
                if (listViewSearchResults.Items.Count >= 1)
                {
                    listViewSearchResults.Items[0].Selected = true;
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            listViewSearchResults.Left = listViewMovies.Width - listViewSearchResults.Width - 35;
        }

        private void toolStripTextBox1_Leave(object sender, EventArgs e)
        {
            timerCloseSearchResults.Start();
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            listViewSearchResults.Visible = true;
            timerCloseSearchResults.Stop();
        }

        private void timerCloseSearchResults_Tick(object sender, EventArgs e)
        {
            timerCloseSearchResults.Stop();
            listViewSearchResults.Visible = false;
        }

        private void listViewSearchResults_Enter(object sender, EventArgs e)
        {
            timerCloseSearchResults.Stop();
        }

        private void listViewSearchResults_Leave(object sender, EventArgs e)
        {
            timerCloseSearchResults.Start();
        }

        private void listViewSearchResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewSearchResults.SelectedItems.Count == 1)
            {
                Movie m = (Movie)listViewSearchResults.SelectedItems[0].Tag;
                if(m!=null)
                {
                    ListViewItem it = listViewMovies.FindItemWithText(m.Name);
                    if (it != null)
                    {
                        it.EnsureVisible();
                        it.Selected = true;
                        listViewMovies.Focus();
                        toolStripTextBox1.Text = "";
                    }
                }
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (listViewSearchResults.SelectedItems.Count == 1)
                {
                    Movie m = (Movie)listViewSearchResults.SelectedItems[0].Tag;
                    if (m != null)
                    {
                        ListViewItem it = listViewMovies.FindItemWithText(m.Name);
                        it.EnsureVisible();
                        it.Selected = true;
                        listViewMovies.Focus();
                        toolStripTextBox1.Text = "";
                    }
                }
            }
        }

        private void loadMissingInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLoadingScreen();
            for (int i = 0; i < Movies.Count; i++)
            {
                Movie m = Movies[i];
                if (m.Description == string.Empty || m.PublishedYear == -1 || m.NoCover || m.RunTime == -1)
                {
                    WebRequest req = WebRequest.Create("http://www.omdbapi.com/?t=" + m.Name.Replace("-", "").Replace("And", " ").Replace("and", " ").Replace("The", " ").Replace("the", " ") + "&plot=full&r=json");
                    req.Method = "GET";
                    req.Proxy = null;
                    using (StreamReader r = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        string response = r.ReadToEnd();
                        JObject obj = JObject.Parse(response);
                        if (obj["Response"].ToString() == "True")
                        {
                            if (m.NoCover && obj["Poster"].ToString() != "N/A")
                            {
                                using (WebClient webClient = new WebClient())
                                    webClient.DownloadFile((string)obj["Poster"], m.PathToImage);

                                // Create a smaller image for the program to use.
                                using (FileStream fs = new FileStream(m.PathToImage, FileMode.Open, FileAccess.Read))
                                {
                                    Image bigCover = Image.FromStream(fs);
                                    using (Bitmap smallCover = new Bitmap(100, 150, bigCover.PixelFormat))
                                    {
                                        smallCover.SetResolution(120, 120);
                                        using (Graphics g = Graphics.FromImage(smallCover))
                                        {
                                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                            ImageFormat format = ImageFormat.Jpeg;
                                            g.DrawImage(bigCover, 0, 0, smallCover.Width, smallCover.Height);
                                            smallCover.Save(m.PathToSmallImage, format);
                                            MovieImages.Add(m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2"), smallCover);
                                            g.Dispose();
                                        }
                                        smallCover.Dispose();
                                    }
                                    bigCover.Dispose();
                                    var items = listViewMovies.Items.Cast<ListViewItem>().Where(s => s.Tag == m);
                                    if(items.Count()==1)
                                        items.ElementAt(0).ImageKey = m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2");
                                }
                                m.NoCover = false;
                            }
                            if (m.Description == String.Empty && obj["Plot"].ToString() != "N/A")
                                m.Description = (string)obj["Plot"];
                            if (m.RunTime == -1 && obj["Runtime"].ToString() != "N/A")
                            {
                                string runtime = obj["Runtime"].ToString();
                                int rt;
                                if (int.TryParse(runtime.Substring(0, runtime.Length - 4), out rt))
                                    m.RunTime = rt;
                            }
                            if (m.PublishedYear == -1 && obj["Year"].ToString() != "N/A")
                            {
                                try
                                {
                                    m.SetYear((int)obj["Year"]);
                                }
                                catch
                                {
                                    m.SetYear(-1);
                                }
                            }
                        }
                        r.Dispose();
                    }
                }
                // Check for subtitles:
            }
            RefreshMovies();
            HideLoadingScreen();
        }

        private void listViewMovies_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenMovie();
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMovie();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Focus();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.ShowDialog();
            sf.Dispose();
        }

        private void listViewSearchResults_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            using (Brush b = new LinearGradientBrush(new Point(e.Bounds.X, e.Bounds.Y), new Point(e.Bounds.X, e.Bounds.Y + e.Bounds.Height), Color.FromArgb(100, e.Item.BackColor), Color.FromArgb(150, 60, 60, 60)))
            {
                e.Graphics.FillRectangle(b, e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 20, e.Bounds.Height);
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;

                    if(MovieImages.ContainsKey(e.Item.ImageKey))
                    {
                        Image img = MovieImages[e.Item.ImageKey];
                        if (img != null)
                            e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y, 18, 28);
                        else
                            e.Graphics.DrawImage(MovieImages["NoCover"], e.Bounds.X, e.Bounds.Y, 18, 28);
                    }
                    else
                        e.Graphics.DrawImage(MovieImages["NoCover"], e.Bounds.X, e.Bounds.Y, 18, 28);

                    Rectangle r = new Rectangle(e.Bounds.X + 20, e.Bounds.Y, e.Bounds.Width - 20, e.Bounds.Height);
                    Font f = new Font(listViewSearchResults.Font, FontStyle.Regular);
                    while (true) {
                        if (e.Graphics.MeasureString(e.Item.Text, f).Width >= r.Width)
                            f = new Font(f.FontFamily, f.Size - 1, FontStyle.Regular);
                        else
                            break;
                    }

                    if (e.Item.Text.IndexOf(toolStripTextBox1.Text, StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        float Y = r.Y + r.Height/2 - f.Height / 2;
                        RectangleF searchMatch = new RectangleF(r.X + e.Graphics.MeasureString(e.Item.Text.Substring(0, e.Item.Text.IndexOf(toolStripTextBox1.Text, StringComparison.CurrentCultureIgnoreCase)), f).Width, Y, e.Graphics.MeasureString(toolStripTextBox1.Text, f).Width, e.Graphics.MeasureString(toolStripTextBox1.Text, f).Height);
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Yellow)), searchMatch);
                    }
                    
                    e.Graphics.DrawString(e.Item.Text, f, Brushes.Black, r, sf);
                }
            }
        }

        #region Custom functions
        /// <summary>
        /// Hides the semi-transparent loading screen.
        /// </summary>
        private void HideLoadingScreen()
        {
            Plexi.Hide();
        }

        /// <summary>
        /// Starts the currently selected movie.
        /// </summary>
        private void OpenMovie()
        {
            // Try to open the movie...
            if (listViewMovies.SelectedItems.Count == 1)
            {
                Movie m = (Movie)listViewMovies.SelectedItems[0].Tag;
                if (m != null)
                {
                    OpenMovie(m);
                }
            }
        }

        private void OpenMovie(Movie movie)
        {
            if (movie.Type == 1)
            {
                if (!movie.Downloaded)
                {
                    Process.Start("http://xmovies8.co/?s=" + movie.Name.Replace(" - ", " ").Replace(' ', '+'));
                    return;
                }

                string Path;
                // Look for the file..
                string[] AVIs = Directory.GetFiles(movie.FullPath + "/", "*.avi");
                if (AVIs.Count() > 1)
                    Path = movie.FullPath;
                else if (AVIs.Count() == 1)
                    Path = AVIs.First();
                else
                {
                    var films = Directory.EnumerateFiles(movie.FullPath, "*.*").Where(s => s.EndsWith(".mp4") || s.EndsWith(".avi") || s.EndsWith(".mkv") || s.EndsWith(".flv"));
                    if (films.Count() == 1)
                        Path = films.First();
                    else
                        Path = movie.FullPath;
                }
                if (!string.IsNullOrEmpty(Path))
                {
                    Process pc = new Process { StartInfo = new ProcessStartInfo { FileName = Path } };
                    pc.EnableRaisingEvents = true;
                    Stopwatch watchElapsed = new Stopwatch();
                    watchElapsed.Start();
                    if (movie.RunTime > -1)
                        pc.Exited += (object se, EventArgs eva) =>
                        {
                            watchElapsed.Stop();
                            if (watchElapsed.ElapsedMilliseconds / 1000 >= movie.RunTime - 25)
                            {
                                movie.Watched = true;
                                int ind = listViewMovies.FindItemWithText(movie.Name).Index;
                                listViewMovies.RedrawItems(ind, ind, true);
                            }
                        };
                    pc.Start();
                }
            }
            else
            {
                for (int i = 0; i < movie.WatchedEpisodes.Count; i++)
                {
                    if (movie.WatchedEpisodes.ElementAt(i).Value == false)
                    {
                        string s, e;
                        int si, ei;
                        string[] info = movie.WatchedEpisodes.ElementAt(i).Key.Split('_');
                        if (int.TryParse(info[0], out si))
                        {
                            s = "Season " + Convert.ToInt16(info[0]).ToString();
                        }
                        else
                        {
                            s = "Season " + info[0];
                        }
                        if(int.TryParse(info[1], out ei)) 
                        {
                            e = "Episode " + Convert.ToInt16(info[1]).ToString();
                        }
                        else 
                        {
                            e = "Episode " + info[1];
                        }
                        string[] files = Directory.GetFiles(movie.FullPath + "\\" + s + "\\", "[" + e + "]*");
                        if (files.Count() == 1)
                        {
                            Process pc = new Process { StartInfo = new ProcessStartInfo { FileName = files[0] } };
                            pc.EnableRaisingEvents = true;
                            Stopwatch watchElapsed = new Stopwatch();
                            watchElapsed.Start();
                            pc.Start();
                            pc.Exited += (object se, EventArgs eva) =>
                            {
                                watchElapsed.Stop();
                                if (watchElapsed.ElapsedMilliseconds / 1000 >= 180)
                                    movie.WatchedEpisodes[movie.WatchedEpisodes.ElementAt(i).Key] = true;
                            };
                            pc.WaitForExit();
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Refreshes the list of films without reloading information.
        /// </summary>
        private void RefreshMovies()
        {
            ShowLoadingScreen();
            listViewMovies.BeginUpdate();
            this.listViewMovies.Clear();
            for (int i = 0; i < Movies.Count; i++)
            {
                Movie m = Movies[i];
                if ((m.Type == 1 && Properties.Settings.Default.ShowMovies) || (m.Type == 2 && Properties.Settings.Default.ShowTVSeries))
                {
                    if (((m.Downloaded && Properties.Settings.Default.ShowDownloaded) || (!m.Downloaded && Properties.Settings.Default.ShowNotDownloaded)) && ((m.Watched && Properties.Settings.Default.ShowViewed) || (!m.Watched && Properties.Settings.Default.ShowNotViewed)))
                    {
                        ListViewItem it = new ListViewItem();
                        it.Tag = m;
                        it.Text = m.Name;
                        if (m.Type == 1)
                        {
                            it.Checked = true;
                            it.SubItems.Add(m.YearToString());
                        }
                        else
                        {
                            it.Checked = false;
                        }
                        it.ToolTipText = m.Description;
                        it.ImageKey = m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2");
                        if (m.NoCover)
                            it.ImageKey = "NoCover";
                        it.BackColor = Color.Transparent;
                        it.ForeColor = Color.Black;

                        if (m.Type == 1)
                            it.Group = mvs;
                        else
                            it.Group = tvs;

                        if (!m.Downloaded)
                            it.BackColor = NotDownloadedColor;
                        else if (!m.Watched)
                            it.BackColor = NotWatchedColor;
                        else
                            it.BackColor = SeenColor;
                        listViewMovies.Items.Add(it);
                    }
                }
            }
            listViewMovies.EndUpdate();
            listViewMovies.ListViewItemSorter = new AlphanumComparator();
            HideLoadingScreen();
        }

        /// <summary>
        /// Reloads all information and refreshes the list.
        /// </summary>
        private void ReloadMovies()
        {
            ShowLoadingScreen();
            listViewMovies.BeginUpdate();
            // Load settings..
            List<string> moviesStrings = new List<string>();
            List<string> tvStrings = new List<string>();
            while (true)
            {
                if (File.Exists(Properties.Settings.Default.SettingFilename))
                {
                    using (StreamReader r = new StreamReader(Properties.Settings.Default.SettingFilename))
                    {
                        try
                        {
                            Settings st = JsonConvert.DeserializeObject<Settings>(r.ReadToEnd());
                            bool ValidFolderFound = false;
                            foreach (KeyValuePair<string, int> folder in st.Folders)
                            {
                                if (Directory.Exists(folder.Key))
                                {
                                    ValidFolderFound = true;
                                    if (folder.Value == 1)
                                        moviesStrings.AddRange(Directory.GetDirectories(folder.Key));
                                    else if (folder.Value == 2)
                                        tvStrings.AddRange(Directory.GetDirectories(folder.Key));
                                }
                            }
                            if (ValidFolderFound) // There was atleast one folder
                                break;
                            else
                            { // Settings contain no folders.
                                // Open settings
                                SettingsForm sf = new SettingsForm();
                                DialogResult dr = sf.ShowDialog();
                                if (dr != DialogResult.OK && dr != DialogResult.Cancel)
                                    this.Close();
                                sf.Dispose();
                            }
                        }
                        catch
                        {
                            // Open settings
                            SettingsForm sf = new SettingsForm();
                            DialogResult dr = sf.ShowDialog();
                            if (dr != DialogResult.OK && dr != DialogResult.Cancel)
                                this.Close();
                            sf.Dispose();
                        }
                    }
                }
                else
                {
                    // Open settings
                    SettingsForm sf = new SettingsForm();
                    DialogResult dr = sf.ShowDialog();
                    if (dr != DialogResult.OK && dr != DialogResult.Cancel)
                        this.Close();
                    sf.Dispose();
                }
            }
            // Count Movies to display progress.
            int count = moviesStrings.Count() + tvStrings.Count();

            // Gather Information
            this.Movies.Clear();
            this.listViewMovies.Clear();
            this.MovieImages.Clear();
            this.MovieImages.Add("NoCover", Properties.Resources.NoCover);
            for (int i = 0; i < moviesStrings.Count; i++)
            {
                Movie m = new Movie(moviesStrings[i], 1);
                if (!File.Exists(m.PathToSmallImage) && File.Exists(m.PathToImage))
                {
                    // Create a smaller image for the program to use.
                    using (FileStream fs = new FileStream(m.PathToImage, FileMode.Open, FileAccess.Read))
                    {
                        Image bigCover = Image.FromStream(fs);
                        using (Bitmap smallCover = new Bitmap(100, 150, bigCover.PixelFormat))
                        {
                            smallCover.SetResolution(120, 120);
                            using (var g = Graphics.FromImage(smallCover))
                            {
                                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                ImageFormat format = ImageFormat.Jpeg;
                                g.DrawImage(bigCover, 0, 0, smallCover.Width, smallCover.Height);
                                smallCover.Save(m.PathToSmallImage, format);
                                g.Flush();
                                g.Dispose();
                            }
                            smallCover.Dispose();
                        }
                        bigCover.Dispose();
                    }
                }

                if (File.Exists(m.PathToSmallImage))
                {
                    using (FileStream fs = new FileStream(m.PathToSmallImage, FileMode.Open, FileAccess.Read))
                    {
                        this.MovieImages.Add(m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2"), Image.FromStream(fs));
                        m.NoCover = false;
                    }
                }
                this.Movies.Add(m);

                ListViewItem it = new ListViewItem();
                it.Tag = m;
                it.Text = m.Name;
                it.SubItems.Add(m.YearToString());
                it.ToolTipText = m.Description;
                it.ImageKey = m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2");
                if (m.NoCover)
                    it.ImageKey = "NoCover";
                it.BackColor = Color.Transparent;
                it.ForeColor = Color.Black;
                if (!m.Downloaded)
                    it.BackColor = NotDownloadedColor;
                else if (!m.Watched)
                    it.BackColor = NotWatchedColor;
                else
                    it.BackColor = SeenColor;

                it.Group = mvs;

                if (Properties.Settings.Default.ShowMovies)
                    if (((m.Downloaded && Properties.Settings.Default.ShowDownloaded) || (!m.Downloaded && Properties.Settings.Default.ShowNotDownloaded)) && ((m.Watched && Properties.Settings.Default.ShowViewed) || (!m.Watched && Properties.Settings.Default.ShowNotViewed)))
                        listViewMovies.Items.Add(it);
            }
            for (int i = 0; i < tvStrings.Count; i++)
            {
                Movie m = new Movie(tvStrings[i], 2);
                if (!File.Exists(m.PathToSmallImage) && File.Exists(m.PathToImage))
                {
                    // Create a smaller image for the program to use.
                    using (FileStream fs = new FileStream(m.PathToImage, FileMode.Open, FileAccess.Read))
                    {
                        Image bigCover = Image.FromStream(fs);
                        using (Bitmap smallCover = new Bitmap(100, 150, bigCover.PixelFormat))
                        {
                            smallCover.SetResolution(120, 120);
                            using (var g = Graphics.FromImage(smallCover))
                            {
                                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                ImageFormat format = ImageFormat.Jpeg;
                                g.DrawImage(bigCover, 0, 0, smallCover.Width, smallCover.Height);
                                smallCover.Save(m.PathToSmallImage, format);
                                g.Flush();
                                g.Dispose();
                            }
                            smallCover.Dispose();
                        }
                        bigCover.Dispose();
                    }
                }

                if (File.Exists(m.PathToSmallImage))
                {
                    using (FileStream fs = new FileStream(m.PathToSmallImage, FileMode.Open, FileAccess.Read))
                    {
                        this.MovieImages.Add(m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2"), Image.FromStream(fs));
                        m.NoCover = false;
                    }
                }
                this.Movies.Add(m);

                ListViewItem it = new ListViewItem();
                it.Tag = m;
                it.Text = m.Name;

                it.ImageKey = m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2");
                if (m.NoCover)
                    it.ImageKey = "NoCover";
                it.BackColor = Color.Transparent;
                it.ForeColor = Color.Black;
                if (!m.Downloaded)
                    it.BackColor = NotDownloadedColor;
                else if (!m.Watched)
                    it.BackColor = NotWatchedColor;
                else
                    it.BackColor = SeenColor;

                it.Group = tvs;

                if (Properties.Settings.Default.ShowTVSeries)
                    if (((m.Downloaded && Properties.Settings.Default.ShowDownloaded) || (!m.Downloaded && Properties.Settings.Default.ShowNotDownloaded)) && ((m.Watched && Properties.Settings.Default.ShowViewed) || (!m.Watched && Properties.Settings.Default.ShowNotViewed)))
                        listViewMovies.Items.Add(it);
            }
            listViewMovies.Refresh();
            listViewMovies.EndUpdate();

            toolStripStatusLabel1.Text = moviesStrings.Count() + " Movies | " + tvStrings.Count() + " Tv Series";
            HideLoadingScreen();
        }

        /// <summary>
        /// Saves all the information on the films in the database.
        /// </summary>
        private void Save()
        {
            ShowLoadingScreen();

            for (int i = 0; i < Movies.Count; i++)
            {
                Movie m = Movies[i];
                try
                {
                    if (!string.Equals(m.FullPath, m.FullPathOriginal) || !Directory.Exists(m.FullPath))
                    {
                        Directory.Move(m.FullPathOriginal, m.FullPath);
                    }
                    using (StreamWriter w = new StreamWriter(m.FullPath + @"\ManagerInfo.txt"))
                    {
                        w.Write(JsonConvert.SerializeObject(new SaveData(m)));
                        w.Flush();
                        w.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
            HideLoadingScreen();
        }

        /// <summary>
        /// Shows the semi-transparent loading screen.
        /// </summary>
        private void ShowLoadingScreen()
        {
            Plexi.Show();
        }
        #endregion

        private void createTextFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogTextFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialogTextFile.FileName))
                {
                    sw.WriteLine(DateTime.Now.ToLongDateString());
                    sw.WriteLine(DateTime.Now.ToLongTimeString());
                    sw.WriteLine();
                    sw.WriteLine("File automatically created by MoviesDatabase V1.0");
                    sw.WriteLine("\t(c) Louis Dresbach");
                    sw.WriteLine();
                    sw.WriteLine(Movies.Where(s => s.Type == 1).Count().ToString() + " movies total.");
                    sw.WriteLine(Movies.Where(s => s.Type == 2).Count().ToString() + " Tv series total.");
                    sw.WriteLine();
                    sw.WriteLine("Movies marked with [D] have not yet been downloaded.");
                    sw.WriteLine("Those marked with [W] haven't been watched yet.");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("\tMovies:");
                    foreach (Movie m in Movies.Where(s => s.Type == 1))
                    {
                        sw.Write("\t\t" + m.Name.PadRight(60) + "\t" + ("[" + m.YearToString() + "]").PadRight(12));
                        if (!m.Downloaded)
                            sw.Write("[D]");
                        if (!m.Watched)
                            sw.Write("[W]");
                        sw.WriteLine();
                    }
                    sw.WriteLine("");
                    sw.WriteLine("\tTv series:");
                    foreach (Movie m in Movies.Where(s => s.Type == 2))
                    {
                        sw.WriteLine("\t\t" + m.Name);
                        foreach (KeyValuePair<int, int> kv in m.Seasons)
                        {
                            sw.WriteLine("\t\t\tSeason " + kv.Key.ToString() + ": " + kv.Value.ToString() + " Episodes");
                        }
                    }
                }
            }
        }

        private void panelInfo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(listViewMovies.BackColor), e.ClipRectangle);
            int Radius = 25;
            Rectangle r = new Rectangle(e.ClipRectangle.X + 2, e.ClipRectangle.Y + 2, e.ClipRectangle.Width - 4, e.ClipRectangle.Height - 4);
            GraphicsPath gfp = new GraphicsPath();
            gfp.AddArc(r.X, r.Y, Radius, Radius, 180, 90);
            gfp.AddArc(r.X + r.Width - Radius, r.Y, Radius, Radius, 270, 90);
            gfp.AddArc(r.X + r.Width - Radius, r.Y + r.Height - Radius, Radius, Radius, 0, 90);
            gfp.AddArc(r.X, r.Y + r.Height - Radius, Radius, Radius, 90, 90);
            gfp.CloseAllFigures();

            e.Graphics.FillPath(new SolidBrush(panelInfoMovie.BackColor), gfp);
            e.Graphics.DrawPath(new Pen(Color.Black, 1), gfp);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point pos = panelInfoMovie.PointToClient(Cursor.Position);
            if (panelInfoMovie.ClientRectangle.Contains(pos))
                return;
            pos = panelInfoTvSeries.PointToClient(Cursor.Position);
            if (panelInfoTvSeries.ClientRectangle.Contains(pos))
                return;
            Point localPoint = listViewMovies.PointToClient(Cursor.Position);
            ListViewItem it = listViewMovies.GetItemAt(localPoint.X, localPoint.Y);
            if (it == null)
            {
                panelInfoMovie.Visible = false;
                panelInfoMovie.Tag = null;
                panelInfoTvSeries.Visible = false;
                panelInfoTvSeries.Tag = null;
                return;
            }
            Movie m = (Movie)it.Tag;

            Rectangle r = new Rectangle(it.Bounds.X + 15, it.Bounds.Y + 5, it.Bounds.Width - 30, MovieImages[it.ImageKey].Height + 10);
            // Don't hide the box if cursor is between box and image (the blank space)
            if (!r.Contains(localPoint)) // This ensures that the infos are only shown when cursor is on the image part
            {
                panelInfoMovie.Visible = false;
                panelInfoMovie.Tag = null;
                panelInfoTvSeries.Visible = false;
                panelInfoTvSeries.Tag = null;
                return;
            }

            if (m.Type == 1)
            {
                if (panelInfoMovie.Tag == m)
                    return;
                panelInfoMovie.Tag = m;
                labelInfoPlot.MaximumSize = new Size(10, 60);
                labelInfoTitle.Text = m.Name;
                labelInfoYear.Text = "(" + m.YearToString() + ")";
                labelInfoYear.Location = new Point(labelInfoTitle.Location.X + labelInfoTitle.Width, labelInfoTitle.Location.Y + labelInfoTitle.Height - labelInfoYear.Height);
                labelInfoRuntime.Text = m.RunTime.ToString() + " mins";
                labelInfoPlot.MaximumSize = new Size(panelInfoMovie.Width - 24, 100);
                labelInfoPlot.Text = m.Description;

                buttonInfoPlay.Enabled = m.Downloaded;
                FieldInfo f1 = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);
                object obj = f1.GetValue(buttonInfoPlay);
                PropertyInfo pi = buttonInfoPlay.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                EventHandlerList list = (EventHandlerList)pi.GetValue(buttonInfoPlay, null);
                list.RemoveHandler(obj, list[obj]);
                buttonInfoPlay.Click += delegate(object s, EventArgs eargs) { OpenMovie(m); };

                panelInfoMovie.Visible = true;
                panelInfoMovie.Invalidate();
            }
            else
            {
                if (panelInfoTvSeries.Tag == m)
                    return;
                panelInfoTvSeries.Tag = m;
                treeViewSeasons.Width = 10;
                labelTvInfoPlot.MaximumSize = new Size(10, 60);
                labelTvInfoTitle.Text = m.Name;
                labelTvInfoPlot.MaximumSize = new Size(panelInfoMovie.Width - 24, 100);
                labelTvInfoPlot.Text = m.Description;

                treeViewSeasons.Width = panelInfoTvSeries.Width - 22;
                treeViewSeasons.Nodes.Clear();
                for (int i = 0; i < m.WatchedEpisodes.Count; i++)
                {
                    KeyValuePair<string, bool> ep = m.WatchedEpisodes.ElementAt(i);
                    string[] info = ep.Key.Split('_');
                    TreeNode[] tn = treeViewSeasons.Nodes.Find("Season " + Convert.ToInt16(info[0]).ToString(), true);
                    TreeNode epi = new TreeNode("Episode " + info[1]);
                    epi.Name = ep.Key;
                    epi.Tag = m.WatchedEpisodes.ElementAt(i);
                    if (!ep.Value)
                        epi.BackColor = Color.Green;
                    if (tn.Count() <= 0)
                    {
                        TreeNode season = new TreeNode("Season " + Convert.ToInt16(info[0]).ToString(), new TreeNode[] { epi });
                        season.Name = "Season " + Convert.ToInt16(info[0]).ToString();
                        treeViewSeasons.Nodes.Add(season);
                    }
                    else
                    {
                        if (treeViewSeasons.Nodes.Find(ep.Key, true).Count() <= 0)
                            tn[0].Nodes.Add(epi);
                    }
                }
                panelInfoTvSeries.Visible = true;
                panelInfoTvSeries.Invalidate();
            }

            int newX = it.Bounds.X - panelInfoMovie.Width;
            int newY = it.Bounds.Y + 32;
            if (newX < 0)
                newX = it.Bounds.X + it.Bounds.Width;
            if (newY < 0)
            {
                newX = it.Bounds.X;
                newY = it.Bounds.Y + it.Bounds.Height;
            }
            panelInfoMovie.Location = new Point(newX, newY);
            newX = it.Bounds.X - panelInfoTvSeries.Width;
            newY = it.Bounds.Y + 32;
            if (newX < 0)
                newX = it.Bounds.X + it.Bounds.Width;
            if (newY < 0)
            {
                newX = it.Bounds.X;
                newY = it.Bounds.Y + it.Bounds.Height;
            }
            panelInfoTvSeries.Location = new Point(newX, newY);
        }

        private void treeViewSeasons_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Movie m = (Movie)panelInfoTvSeries.Tag;
            if (e.Node.Text.StartsWith("Season"))
            {
                Process.Start(m.FullPath + @"\" + e.Node.Text);
            }
            else if (e.Node.Text.StartsWith("Episode"))
            {
                string prevFileName = string.Empty;
                string[] files = Directory.GetFiles(m.FullPath + "\\" + e.Node.Parent.Text + "\\", "[" + e.Node.Text + "]*");
                bool cont = true;
                string curKey = e.Node.Name;
                while (cont)
                {
                    if (files.Count() == 1)
                    {
                        Process pc = new Process { StartInfo = new ProcessStartInfo { FileName = files[0] } };
                        pc.EnableRaisingEvents = true;
                        Stopwatch watchElapsed = new Stopwatch();
                        watchElapsed.Start();
                        pc.Exited += (object se, EventArgs eva) =>
                        {
                            watchElapsed.Stop();

                            if (string.Equals(prevFileName, files[0]))
                                cont = false;
                            // Say that the episode has been/is being watched if process runs for more than 3mins(=180secs)
                            else if (watchElapsed.ElapsedMilliseconds / 1000 >= 180)
                            {
                                m.WatchedEpisodes[curKey] = true;
                                IEnumerable<KeyValuePair<string, bool>> enu = m.WatchedEpisodes.SkipWhile(k => k.Key != curKey);
                                if (enu.Count() > 1)
                                {
                                    if (MessageBox.Show("Do you want to watch the next episode?", "MoviesDatabase", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        TreeNode[] t = treeViewSeasons.Nodes.Find(enu.ElementAt(1).Key, true);
                                        if (t.Count() == 1)
                                        {
                                            curKey = t[0].Name;
                                            files = Directory.GetFiles(m.FullPath + "\\" + t[0].Parent.Text + "\\", "[" + t[0].Text + "]*");
                                            if (files.Count() != 1)
                                            {
                                                MessageBox.Show("Couldn't find film.", "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                                                cont = false;
                                            }
                                        }
                                        else
                                            cont = false;
                                    }
                                    else
                                        cont = false;
                                }
                                else
                                    cont = false;
                            }
                            else
                                cont = false;
                        };
                        pc.Start();
                        pc.WaitForExit();
                    }
                }
            }
        }

        private void checkForNewEpisodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int total = 0;
            ShowLoadingScreen();
            foreach (Movie m in Movies.Where(s=>s.Type==2))
            {
                total += m.CheckForNewSeries();
            }
            HideLoadingScreen();
            MessageBox.Show("Found " + total + " new episodes.");
        }

        private void downloadMissingSubtitlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int total = 0;
            string[] l = { "English" };
            ShowLoadingScreen();
            foreach (Movie m in Movies.Where(s => s.Type == 1))
            {
                total += m.DownloadSubtitles(l);
            }
            HideLoadingScreen();
            MessageBox.Show("Downloaded " + total + " subtitles.");
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewMovies.SelectedItems.Count == 1)
            {
                ListViewItem it = listViewMovies.SelectedItems[0];
                Movie m = (Movie)it.Tag;
                if (m != null)
                {
                    if (MessageBox.Show("Are you sure you want to delete the film '"+m.Name+"'?", "MoviesDatabase", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        MovieImages.Remove(it.ImageKey);
                        listViewMovies.Items.Remove(it);
                        Movies.Remove(m);
                        if(Directory.Exists(m.FullPath))
                            Directory.Delete(m.FullPath, true);
                        else if (Directory.Exists(m.FullPathOriginal))
                            Directory.Delete(m.FullPathOriginal, true);
                    }
                }
            }
        }

        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }
    }
}
