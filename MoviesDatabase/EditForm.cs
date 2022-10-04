using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MoviesDatabase
{
    public partial class EditForm : Form
    {
        Movie editing;
        public string NewTitle = "";
        public int NewYear = -1;
        public bool NewDownloaded = false;
        public bool NewWatched = false;
        public string NewDescription = "";

        /// <summary>
        /// A new instance of this form for adding a new movie.
        /// </summary>
        public EditForm()
        {
            InitializeComponent();
            this.Text = "Add New film - MovieDatabase";
            this.editing = new Movie();
        }

        /// <summary>
        /// A new instance of this form for editing an exisiting movie.
        /// </summary>
        /// <param name="_MovieToEdit">The movie to edit.</param>
        public EditForm(Movie _MovieToEdit)
        {
            this.Text = "Editing: " + _MovieToEdit.Name + " - MovieDatabase";
            this.editing = _MovieToEdit;
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }

        private void LoadInfo()
        {
            textBoxTitle.Text = editing.Name;
            textBoxYear.Text = editing.PublishedYear.ToString();
            if (editing.PublishedYear == -1)
                textBoxYear.Text = "";
            checkBoxDownloaded.Checked = editing.Downloaded;
            checkBoxWatched.Checked = editing.Watched;
            textBoxDescription.Text = editing.Description;

            if (File.Exists(editing.PathToImage))
                using (FileStream stream = new FileStream(editing.PathToImage, FileMode.Open, FileAccess.Read))
                    pictureBoxCover.BackgroundImage = Image.FromStream(stream);
            else
                pictureBoxCover.BackgroundImage = Properties.Resources.NoCover;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            LoadInfo();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            int newYear = -1;
            string newName = textBoxTitle.Text;
            char[] forbiddenChars = { '<', '>', ':', '"', '/', '\\', '|', '?', '*' };
            // Check if name is valid
            if (newName.IndexOfAny(forbiddenChars) != -1)
            {
                MessageBox.Show("Error: The title includes some forbidden characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if year is valid
            if (textBoxYear.Text != "" && !int.TryParse(textBoxYear.Text, out newYear))
            {
                MessageBox.Show("Error: The year you entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.NewTitle = textBoxTitle.Text;
            this.NewYear = newYear;
            this.NewDownloaded = checkBoxDownloaded.Checked;
            this.NewWatched = checkBoxWatched.Checked;
            this.NewDescription = textBoxDescription.Text;

            this.DialogResult = DialogResult.OK;
            editing = null;
            this.Close();
        }
    }
}
