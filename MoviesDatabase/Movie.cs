using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace MoviesDatabase
{
    /// <summary>
    /// Class used to store information on a movie or TV series.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// The film's description/plot.
        /// </summary>
        public string Description = "";
        /// <summary>
        /// Whether the film has been downloaded or not.
        /// </summary>
        public bool Downloaded = false;
        /// <summary>
        /// Whether the TV series has ended already. Only applies to TV Series.
        /// </summary>
        public bool Ended = false;
        /// <summary>
        /// The foldername of the film.
        /// </summary>
        public string FolderName = "";
        /// <summary>
        /// The complete path to the film.
        /// </summary>
        public string FullPath = "";
        public string FullPathOriginal = "";
        /// <summary>
        /// The film's Title.
        /// </summary>
        private string _Name = "";
        public string Name { set { _Name = value; UpdateFullPath(); } get { return _Name; } }
        /// <summary>
        /// Whether the film has a cover.
        /// </summary>
        public bool NoCover = true;
        /// <summary>
        /// Complete path to the cover.
        /// </summary>
        public string PathToImage = "";
        /// <summary>
        /// Complete path to the smaller version of the cover.
        /// </summary>
        public string PathToSmallImage = "";
        /// <summary>
        /// The year in which the film was published. Will be -1 if unknown.
        /// </summary>
        private int _PublishedYear = -1;
        public int PublishedYear { set { _PublishedYear = value; UpdateFullPath(); } get { return _PublishedYear; } }
        /// <summary>
        /// The length of the movie in minutes. Will be -1 if unknown; only applies to movies.
        /// </summary>
        public int RunTime = -1;
        /// <summary>
        /// Number of episodes in each season.
        /// </summary>
        public Dictionary<int, int> Seasons = new Dictionary<int, int>();
        /// <summary>
        /// Contains a list of the subtitles for this film.
        /// </summary>
        public List<string> Subtitles = new List<string>();
        /// <summary>
        /// The type of the film. 1=Movie | 2=Tv series
        /// </summary>
        public int Type = 1;
        /// <summary>
        /// Whether this film has been watched or not.
        /// </summary>
        public bool Watched = false;
        /// <summary>
        /// Dictionary containing all seasons and a bool whether they have been watched or not.
        /// String in format: "season_episode".
        /// Example for season 2 episode 23: "2_23".
        /// </summary>
        public Dictionary<string, bool> WatchedEpisodes = new Dictionary<string, bool>();

        /// <summary>
        /// Initializes a new blank instance of the Movie class.
        /// </summary>
        public Movie()
        {

        }

        /// <summary>
        /// Initializes a new instance of the Movie class based on a given Folder.
        /// </summary>
        /// <param name="_FullPath">Full path to the Folder.</param>
        public Movie(string _FullPath, int _Type)
        {
            this.FullPath = _FullPath;
            this.FullPathOriginal = _FullPath;
            this.FolderName = Path.GetFileName(_FullPath);
            this.PathToImage = _FullPath + "/cover.jpg";
            this.PathToSmallImage = _FullPath + "/cover_small.jpg";
            this.Type = _Type;

            if (this.FolderName.IndexOf('[') != -1 && this.FolderName.IndexOf(']') != -1)
            {
                int index = this.FolderName.LastIndexOf("[") + 1;
                this._Name = this.FolderName.Substring(0, index - 1).Trim();
                string year = this.FolderName.Substring(index, this.FolderName.LastIndexOf("]") - index);
                int y;
                if (int.TryParse(year, out y))
                    SetYear(y);
            }
            else
                this._Name = this.FolderName;

            // Get all seasons and episodes.
            if (this.Type == 2)
            {
                string[] seasonFolders = Directory.GetDirectories(_FullPath, "Season *");
                Array.Sort(seasonFolders, new AlphanumComparator());
                foreach (string s in seasonFolders)
                {
                    int c = 0, se;
                    string[] episodes = Directory.GetFiles(s, "[Episode *]*");
                    Array.Sort(episodes, new AlphanumComparator());
                    string season = Path.GetFileName(s).Substring(7);
                    if (int.TryParse(season, out se))
                    {
                        for (int i = 0; i < episodes.Count();i++ )
                        {
                            string e = episodes[i];
                            string ext = Path.GetExtension(e);
                            if (ext == ".avi" || ext == ".mkv" || ext == ".mp4" || ext == ".flv")
                            {
                                string episode = Path.GetFileNameWithoutExtension(e).Substring(9);
                                episode = episode.Substring(0, episode.IndexOf(']'));
                                this.WatchedEpisodes.Add(se.ToString("D2") + "_" + episode, false);
                                c++;
                            }
                        }
                        this.Seasons[se] = c;
                    }
                }
            }

            if (File.Exists(_FullPath + "/ManagerInfo.txt"))
            {
                SaveData svd;
                try
                {
                    using (StreamReader r = new StreamReader(_FullPath + "/ManagerInfo.txt"))
                    {
                        svd = JsonConvert.DeserializeObject<SaveData>(r.ReadToEnd());
                    }
                }
                catch 
                {
                    using (StreamWriter w = new StreamWriter(_FullPath + "/ManagerInfo.txt"))
                    {
                        svd = new SaveData();
                        w.Write(JsonConvert.SerializeObject(svd));
                    }
                }

                if (svd == null)
                {
                    svd = new SaveData();
                }

                if (!svd.Downloaded)
                {
                    var films = Directory.EnumerateFiles(this.FullPath, "*.*").Where(s => s.EndsWith(".mp4") || s.EndsWith(".avi") || s.EndsWith(".mkv") || s.EndsWith(".flv"));
                    if (films.Count() == 1)
                        svd.Downloaded = true;
                }

                this.Downloaded = svd.Downloaded;
                this.Watched = svd.Watched;
                this.Description = svd.Description;
                this.RunTime = svd.RunTime;

                foreach(KeyValuePair<string, bool> kv in svd.WatchedEpisodes) {
                    this.WatchedEpisodes[kv.Key] = kv.Value;
                }
                this.WatchedEpisodes.OrderBy(k => k.Key, new AlphanumComparatorString());
            }
            else
            {
                using (StreamWriter w = new StreamWriter(_FullPath + "/ManagerInfo.txt"))
                {
                    SaveData sobj = new SaveData();

                    WebRequest req = WebRequest.Create("http://www.omdbapi.com/?t=" + this.Name.Replace("-", "").Replace("And", " ").Replace("and", " ").Replace("The", " ").Replace("the", " ") + "&plot=full&r=json");
                    req.Method = "GET";
                    using (StreamReader r = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        string response = r.ReadToEnd();
                        JObject obj = JObject.Parse(response);
                        if (obj["Response"].ToString() == "True")
                        {
                            if (obj["Plot"].ToString() != "N/A")
                                this.Description = (string)obj["Plot"];
                            if (!File.Exists(this.PathToImage) && obj["Poster"].ToString() != "N/A")
                            {
                                using (WebClient webClient = new WebClient())
                                    webClient.DownloadFile((string)obj["Poster"], this.PathToImage);
                            }
                            if (this._PublishedYear == -1 && obj["Year"].ToString() != "N/A")
                            {
                                int year;
                                if(int.TryParse(obj["Year"].ToString(), out year))
                                    this.SetYear(year);
                            }
                            if (this.RunTime == -1 && obj["Runtime"].ToString() != "N/A")
                            {
                                string runtime = obj["Runtime"].ToString();
                                int rt;
                                if (int.TryParse(runtime.Substring(0, runtime.Length - 4), out rt))
                                    this.RunTime = rt;
                            }
                        }
                        r.Dispose();
                    }
                    w.Write(JsonConvert.SerializeObject(sobj));
                }
            }
        }

        /// <summary>
        /// Updates the full path
        /// </summary>
        private void UpdateFullPath()
        {
            this.FolderName = this.Name;
            if (this._PublishedYear != -1)
                this.FolderName += " [" + this._PublishedYear + "]";
            this.FullPath = Directory.GetParent(this.FullPath).FullName + @"\" + this.FolderName;
            this.PathToImage = this.FullPath + "/cover.jpg";
            this.PathToSmallImage = this.FullPath + "/cover_small.jpg";
        }

        /// <summary>
        /// Downloads missing subtitles. Currently only downloads english ones.
        /// </summary>
        /// <param name="languages"></param>
        /// <returns>Count of downloaded subtitles.</returns>
        public int DownloadSubtitles(string[] languages)
        {
            int ret = 0;
            if (this.Downloaded)
            {
                var films = Directory.EnumerateFiles(this.FullPath, "*.*").Where(s => s.EndsWith(".mp4") || s.EndsWith(".avi") || s.EndsWith(".mkv") || s.EndsWith(".flv"));
                if (films.Count() == 0)
                    this.Downloaded = false;
                else
                {
                    string[] subs = Directory.GetFiles(this.FullPath, "Subtitles_*.srt", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < subs.Count(); i++)
                    {
                        string language = Path.GetFileNameWithoutExtension(subs[i]).Substring(10);
                        this.Subtitles.Add(language);
                    }
                    string[] NeededLangs = { "English|en" };
                    for (int i = 0; i < NeededLangs.Count(); i++)
                    {
                        string[] Lang = NeededLangs[i].Split('|');
                        if (Subtitles.FindIndex(s => s.Contains(Lang[0])) == -1)
                        {
                            string hash = ComputeMovieHash(films.First());
                            string url = "http://api.thesubdb.com/?action=download&language=" + Lang[1] + "&hash=" + hash;
                            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                            req.UserAgent = "SubDB/1.0 (MoviesDatabase/0.1; http://dascriptz.com/)";
                            req.Method = "GET";
                            req.Proxy = null;

                            try
                            {
                                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                                {
                                    using (Stream input = resp.GetResponseStream())
                                    {
                                        byte[] buffer = new byte[1024];
                                        using (FileStream fs = File.OpenWrite(this.FullPath + @"\Subtitles_" + Lang[0] + ".srt"))
                                        {
                                            int size = input.Read(buffer, 0, buffer.Length);
                                            while (size > 0)
                                            {
                                                fs.Write(buffer, 0, size);
                                                size = input.Read(buffer, 0, buffer.Length);
                                            }
                                        }
                                        ret++;
                                    }
                                }
                            }
                            catch (WebException ex)
                            {
                                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                                {
                                    var resp = (HttpWebResponse)ex.Response;
                                    if (resp.StatusCode != HttpStatusCode.NotFound && resp.StatusCode != HttpStatusCode.BadRequest)
                                    {
                                        ret = -1;
                                        MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                                    }
                                }
                                else
                                {
                                    ret = -1;
                                    MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                                }
                            }
                        }
                    }
                }
            } 
            return ret;
        }

        /// <summary>
        /// Checks for new series.
        /// </summary>
        /// <returns>Count of new series.</returns>
        public int CheckForNewSeries()
        {
            // Get whether series has ended already or not; check for missing episodes; check whether latest one has been downloaded already or not.
            int ret = 0;
            if (this.Type == 2 && (!this.Ended || !this.Downloaded))
            {
                try
                {
                    using (XmlReader x = XmlReader.Create("http://services.tvrage.com/feeds/search.php?show=" + this.Name))
                    {
                        x.MoveToContent();
                        x.Read();
                        string id = "";
                        while (!x.EOF && x.ReadState == ReadState.Interactive)
                        {
                            if (x.Name == "status")
                            {
                                string status = x.ReadElementContentAsString();
                                if (status == "Canceled" || status == "Ended")
                                    this.Ended = true;
                            }
                            else if (x.NodeType == XmlNodeType.Element && x.Name == "showid")
                            {
                                id = x.ReadElementContentAsString();
                                break;
                            }
                            else
                                x.Read();
                        }

                        using (XmlReader reader = XmlReader.Create("http://services.tvrage.com/feeds/episode_list.php?sid=" + id))
                        {
                            reader.MoveToContent();
                            reader.Read();
                            int ts = -1;
                            int currentSeason = -1;
                            while (!reader.EOF && reader.ReadState == ReadState.Interactive)
                            {
                                if (reader.NodeType == XmlNodeType.Element)
                                {
                                    if (reader.Name == "totalseasons")
                                    {
                                        Int32.TryParse(reader.ReadElementContentAsString(), out ts);
                                    }
                                    else if (reader.Name == "Season")
                                    {
                                        if (!int.TryParse(reader.GetAttribute("no"), out currentSeason))
                                            break;
                                        reader.Read();
                                    }
                                    else if (reader.Name == "episode")
                                    {
                                        reader.ReadToFollowing("seasonnum");
                                        if (reader.NodeType == XmlNodeType.Element)
                                        {
                                            if (this.WatchedEpisodes.ContainsKey(currentSeason.ToString("D2") + "_" + reader.ReadElementContentAsString()))
                                            {
                                                this.Downloaded = true;
                                            }
                                            else
                                            {
                                                this.Downloaded = false;
                                                ret++;
                                            }
                                        }
                                        reader.Read();
                                    }
                                    else
                                        reader.Read();
                                }
                                else
                                    reader.Read();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ret = -1;
                    MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
            return ret;
        }

        /// <summary>
        /// Sets the year of current film.
        /// </summary>
        /// <param name="_PublishedYear">New year</param>
        public void SetYear(int _PublishedYear)
        {
            if (_PublishedYear > 1700 && _PublishedYear < 3000)
                this._PublishedYear = _PublishedYear;
            else
                this._PublishedYear = -1;   
        }

        override public string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Displays the year as a string.
        /// </summary>
        /// <returns>Year as string, "Unknown" if year not set.</returns>
        public string YearToString()
        {
            if (this._PublishedYear == -1)
                return "Unknown";
            return this._PublishedYear.ToString();
        }

        private static string ComputeMovieHash(string filename)
        {
            string result = "";
            int readsize = 64 * 1024;
            byte[] buffer = new byte[128 * 1024];

            using (Stream input = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read, readsize))
            {
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    input.Read(buffer, 0, readsize);
                    if (input.Length > readsize)
                        input.Seek(-readsize, SeekOrigin.End);
                    else
                        input.Seek(0, SeekOrigin.Begin);
                    for (int i = readsize; i < 2 * readsize; i++)
                    {
                        int b = input.ReadByte();
                        if (b != -1)
                            buffer[i] = Convert.ToByte(b);
                    }

                    buffer = md5.ComputeHash(buffer);

                    foreach (byte b in buffer)
                    {
                        result += b.ToString("x2");
                    }
                }
            }
            return result;
        }
    }

    /// <summary>
    /// Class used to load/save Information of a movie.
    /// </summary>
    public class SaveData
    {
        /// <summary>
        /// Description/Plot of the film.
        /// </summary>
        public string Description = "";
        /// <summary>
        /// Whether the film has been downloaded.
        /// </summary>
        public bool Downloaded = false;
        /// <summary>
        /// Whether the TV series has ended already. Only applies to TV Series.
        /// </summary>
        public bool Ended = false;
        /// <summary>
        /// The length of the movie in minutes. Will be -1 if unknown; only applies to movies.
        /// </summary>
        public int RunTime = -1;
        /// <summary>
        /// Whether the movie has been watched. Only applies to movies.
        /// </summary>
        public bool Watched = false;
        /// <summary>
        /// Dictionary containing all seasons and a bool whether they have been watched or not.
        /// String in format: "season_episode".
        /// Example for season 2 episode 23: "2_23".
        /// </summary>
        public Dictionary<string, bool> WatchedEpisodes = new Dictionary<string, bool>();

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public SaveData() { }
        /// <summary>
        /// Creates a new instance and populates it with information of a movie.
        /// </summary>
        /// <param name="_m">Movie to take the information from.</param>
        public SaveData(Movie _m) {
            this.Description = _m.Description;
            this.Downloaded = _m.Downloaded;
            this.Ended = _m.Ended;
            this.RunTime = _m.RunTime;
            this.Watched = _m.Watched;
            this.WatchedEpisodes = _m.WatchedEpisodes;
        }
    }

    public class AlphanumComparator : IComparer
    {
        public int Compare(object x, object y)
        {
            string s1 = x.ToString();
            if (s1 == null)
            {
                return 0;
            }
            string s2 = y.ToString();
            if (s2 == null)
            {
                return 0;
            }

            int len1 = s1.Length;
            int len2 = s2.Length;
            int marker1 = 0;
            int marker2 = 0;

            // Walk through two the strings with two markers.
            while (marker1 < len1 && marker2 < len2)
            {
                char ch1 = s1[marker1];
                char ch2 = s2[marker2];

                // Some buffers we can build up characters in for each chunk.
                char[] space1 = new char[len1];
                int loc1 = 0;
                char[] space2 = new char[len2];
                int loc2 = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    space1[loc1++] = ch1;
                    marker1++;

                    if (marker1 < len1)
                    {
                        ch1 = s1[marker1];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                do
                {
                    space2[loc2++] = ch2;
                    marker2++;

                    if (marker2 < len2)
                    {
                        ch2 = s2[marker2];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                string str1 = new string(space1);
                string str2 = new string(space2);

                int result;

                if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                {
                    int thisNumericChunk = int.Parse(str1);
                    int thatNumericChunk = int.Parse(str2);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                {
                    result = str1.CompareTo(str2);
                }

                if (result != 0)
                {
                    return result;
                }
            }
            return len1 - len2;
        }
    }


    public class AlphanumComparatorString : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string s1 = x;
            if (s1 == null)
            {
                return 0;
            }
            string s2 = y;
            if (s2 == null)
            {
                return 0;
            }

            int len1 = s1.Length;
            int len2 = s2.Length;
            int marker1 = 0;
            int marker2 = 0;

            // Walk through two the strings with two markers.
            while (marker1 < len1 && marker2 < len2)
            {
                char ch1 = s1[marker1];
                char ch2 = s2[marker2];

                // Some buffers we can build up characters in for each chunk.
                char[] space1 = new char[len1];
                int loc1 = 0;
                char[] space2 = new char[len2];
                int loc2 = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    space1[loc1++] = ch1;
                    marker1++;

                    if (marker1 < len1)
                    {
                        ch1 = s1[marker1];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                do
                {
                    space2[loc2++] = ch2;
                    marker2++;

                    if (marker2 < len2)
                    {
                        ch2 = s2[marker2];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                string str1 = new string(space1);
                string str2 = new string(space2);

                int result;

                if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                {
                    int thisNumericChunk = int.Parse(str1);
                    int thatNumericChunk = int.Parse(str2);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                {
                    result = str1.CompareTo(str2);
                }

                if (result != 0)
                {
                    return result;
                }
            }
            return len1 - len2;
        }
    }
}
