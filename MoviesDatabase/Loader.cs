using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MoviesDatabase
{
    public partial class Loader : Form
    {
        public Loader()
        {
            InitializeComponent();
        }

        private void Loader_Load(object sender, EventArgs e)
        {
            labelWhatsGoingOn.Text = "";
            timerStarter.Start();
        }

        private void timerStarter_Tick(object sender, EventArgs e)
        {
            timerStarter.Stop();

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
                                sf.ShowDialog();
                                sf.Dispose();
                            }
                        }
                        catch
                        {
                            // Open settings
                            SettingsForm sf = new SettingsForm();
                            sf.ShowDialog();
                            sf.Dispose();
                        }
                    }
                }
                else
                {
                    // Open settings
                    SettingsForm sf = new SettingsForm();
                    sf.ShowDialog();
                    sf.Dispose();
                }
            }

            // Count Movies to display progress.
            /* Old way of doing things..
            string[] moviesStrings = Directory.GetDirectories(@"G:\Movies");
            string[] tvStrings = Directory.GetDirectories(@"G:\TV Series");*/
            int count = moviesStrings.Count() + tvStrings.Count();

            // Gather Information
            float progress = 0;
            float pc = (float)100 / count;
            progressBar.Maximum = count;
            List<Movie> movies = new List<Movie>();
            Dictionary<string, Image> im = new Dictionary<string, Image>();
            im.Add("NoCover", Properties.Resources.NoCover);

            // Checking for new movies and creating small covers....
            labelWhatsGoingOn.Text = "Checking for new movies and creating downsized covers.. ";
            labelWhatsGoingOn.Refresh();
            foreach (string s in moviesStrings)
            {
                progress += pc;
                labelPercentage.Text = Math.Round(progress, 1).ToString() + "%";
                labelPercentage.Refresh();
                progressBar.Value += 1;
                progressBar.Refresh();

                Movie m = new Movie(s, 1);
                if (!File.Exists(m.PathToSmallImage) && File.Exists(m.PathToImage))
                {
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
                        im.Add(m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2"), Image.FromStream(fs));
                    }
                    m.NoCover = false;
                }
                movies.Add(m);
            }
            labelWhatsGoingOn.Text = "Checking for new TV Series and creating downsized covers.. ";
            labelWhatsGoingOn.Refresh();
            foreach (string s in tvStrings)
            {
                progress += pc;
                labelPercentage.Text = Math.Round(progress, 1).ToString() + "%";
                labelPercentage.Refresh();
                progressBar.Value += 1;
                progressBar.Refresh();

                Movie m = new Movie(s, 2);
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
                        im.Add(m.Name + "-" + m.YearToString() + "-" + m.Type.ToString("D2"), Image.FromStream(fs));
                    }
                    m.NoCover = false;
                }
                movies.Add(m);
            }

            // Move to main form with gathered information
            MainForm f = new MainForm(movies, im, moviesStrings.Count, tvStrings.Count);
            f.Show();
            this.Close();
        }
    }
}
