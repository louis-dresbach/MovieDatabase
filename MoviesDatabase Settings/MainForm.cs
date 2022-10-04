using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MoviesDatabase_Settings
{
    public partial class MainForm : Form
    {
        List<int> UsedFolders = new List<int>();
        string SettingsFileName = "/MovieDatabase.config";

        public MainForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SaveSettings()
        {
            // Save the paths (G:/Movies, G:/TV Series) and other settings to a file with encoded JSON.
            using (StreamWriter w = new StreamWriter(SettingsFileName))
            {
                Settings s = new Settings();
                foreach (int i in UsedFolders)
                {
                    string path = ((TextBox)this.Controls.Find("TextBoxFolder" + i, true)[0]).Text;
                    int type = (int)((ComboBox)this.Controls.Find("ComboBoxFolder" + i, true)[0]).SelectedValue;
                    s.Folders.Add(path, type);
                }
                w.Write(JsonConvert.SerializeObject(s));
                w.Flush();
            }
        }

        private void LoadSettings()
        {
            if (File.Exists(SettingsFileName))
            {
                using (StreamReader r = new StreamReader(SettingsFileName))
                {
                    try
                    {
                        Settings st = JsonConvert.DeserializeObject<Settings>(r.ReadToEnd());
                        foreach (KeyValuePair<string, int> folder in st.Folders)
                        {
                            AddFolder(folder.Key, folder.Value);
                        }
                    }
                    catch
                    {
                        AddFolder();
                    }
                }
            }
            else
            {
                AddFolder();
            }
        }

        private void AddFolder(string Path="", int Type=1)
        {
            int newNumber = NextUnusedNumber();
            int y = 12 + (newNumber - 1) * 24;

            // Add new row
            TextBox newBox = new TextBox();
            newBox.Name = "TextBoxFolder" + newNumber;
            newBox.Size = new Size(220, 22);
            newBox.Location = new Point(12, y);
            newBox.Text = Path;

            ComboBox newCombo = new ComboBox();
            newCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            newCombo.Name = "ComboBoxFolder" + newNumber;
            newCombo.Size = new Size(80, 22);
            newCombo.Location = new Point(237, y);
            newCombo.DisplayMember = "Text";
            newCombo.ValueMember = "ID";
            newCombo.DataSource = new ComboItem[] {
                new ComboItem{ID=1, Text="Movie"},
                new ComboItem{ID=2, Text="TV Series"}
            };
            newCombo.BindingContext = new BindingContext();
            newCombo.SelectedValue = Type;

            Button newRemoveButton = new Button();
            newRemoveButton.Size = new Size(22, 22);
            newRemoveButton.Location = new Point(322, y);
            newRemoveButton.Click += newRemoveButton_Click;
            newRemoveButton.Text = "-";
            newRemoveButton.Name = "ButtonRemove" + newNumber;

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
            newBox,
            newCombo,
            newRemoveButton});
            this.UsedFolders.Add(newNumber);

            // Move rows below
            MoveFolders();
        }

        void newRemoveButton_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(((Button)sender).Name.Substring(12));
            RemoveFolder(n);
        }

        private void RemoveFolder(int ID)
        {
            if (this.UsedFolders.Count > 1)
            {
                // Remove Controls
                this.Controls.RemoveByKey("TextBoxFolder" + ID);
                this.Controls.RemoveByKey("ComboBoxFolder" + ID);
                this.Controls.RemoveByKey("ButtonRemove" + ID);

                // Remove the ID
                this.UsedFolders.Remove(ID);

                MoveFolders();
            }
        }

        private void MoveFolders()
        {
            // Adjust Positions of other Controls
            int x = 1;
            foreach (int i in this.UsedFolders)
            {
                int y = 12 + (x - 1) * 24;
                ((TextBox)this.Controls.Find("TextBoxFolder" + i, true)[0]).Location = new Point(12, y);
                this.Controls.Find("ComboBoxFolder" + i, true)[0].Location = new Point(237, y);
                this.Controls.Find("ButtonRemove" + i, true)[0].Location = new Point(322, y);
                x++;
            }
            int y2 = 38 + (this.UsedFolders.Count - 1) * 24;
            buttonAddFolder.Location = new Point(12, y2);
            buttonSave.Location = new Point(184, y2);
            this.Height = y2 + 72;
        }

        private void buttonAddFolder_Click(object sender, EventArgs e)
        {
            AddFolder();
        }

        private int NextUnusedNumber()
        {
            int i = 1;
            int ret = 0;
            while (true)
            {
                if (!this.UsedFolders.Contains(i))
                {
                    ret = i;
                    break;
                }
                i++;
            }
            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        /*
         * THINGS TO DO
         * Create an icon
         * Make it save
         * Make the main app load and use the settings
         * Make sure the project builds itself
         * Make everything pretty! *.*
         * 
         */

    }

    public class Settings 
    {
        public Dictionary<string, int> Folders = new Dictionary<string,int>(); // Path | Type (Movie/TV Series)
    }

    public class ComboItem
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
