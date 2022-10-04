using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MoviesDatabase
{
    public partial class SettingsForm : Form
    {
        List<int> UsedFolders = new List<int>();
        string[] Langs = { "Dutch", "English", "French", "Italian", "Polski", "Portugese", "Romanian", "Spanish", "Swedish", "Turkish" };
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SaveSettings()
        {
            // Save the paths (G:/Movies, G:/TV Series) and other settings to a file with encoded JSON.
            using (StreamWriter w = new StreamWriter(Properties.Settings.Default.SettingFilename))
            {
                Settings s = new Settings();
                foreach (int i in UsedFolders)
                {
                    string path = ((TextBox)this.Controls.Find("TextBoxFolder" + i, true)[0]).Text;
                    int type = (int)((ComboBox)this.Controls.Find("ComboBoxFolder" + i, true)[0]).SelectedValue;
                    if (path != string.Empty)
                        s.Folders.Add(path, type);
                }
                w.Write(JsonConvert.SerializeObject(s));
                w.Flush();
            }
        }

        private void LoadSettings()
        {
            if (File.Exists(Properties.Settings.Default.SettingFilename))
            {
                using (StreamReader r = new StreamReader(Properties.Settings.Default.SettingFilename))
                {
                    try
                    {
                        Settings st = JsonConvert.DeserializeObject<Settings>(r.ReadToEnd());
                        foreach (KeyValuePair<string, int> folder in st.Folders)
                        {
                            AddFolder(folder.Key, folder.Value);
                        }
                        foreach (string s in st.Subtitles)
                        {
                            this.listBoxLanguages.Items.Add(s);
                        }
                        foreach (string s in Langs)
                        {
                            if (!listBoxLanguages.Items.Contains(s))
                                comboBoxNewLanguage.Items.Add(s);
                        }
                        comboBoxNewLanguage.SelectedIndex = 0;
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

        private void AddFolder(string Path = "", int Type = 1)
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

            panelFolders.Controls.AddRange(new System.Windows.Forms.Control[] {
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
                panelFolders.Controls.RemoveByKey("TextBoxFolder" + ID);
                panelFolders.Controls.RemoveByKey("ComboBoxFolder" + ID);
                panelFolders.Controls.RemoveByKey("ButtonRemove" + ID);

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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.listBoxLanguages.Items.Add(comboBoxNewLanguage.Text);
        }
    }

    public class Settings
    {
        public Dictionary<string, int> Folders = new Dictionary<string, int>(); // Path | Type (Movie/TV Series)
        public List<string> Subtitles = new List<string>();
    }

    public class ComboItem
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
