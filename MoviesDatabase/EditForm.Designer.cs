namespace MoviesDatabase
{
    partial class EditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelYear = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.pictureBoxCover = new System.Windows.Forms.PictureBox();
            this.labelDownloaded = new System.Windows.Forms.Label();
            this.labelWatched = new System.Windows.Forms.Label();
            this.checkBoxWatched = new MoviesDatabase.CustomCheckBox();
            this.checkBoxDownloaded = new MoviesDatabase.CustomCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(68, 14);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(31, 14);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Title";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(260, 276);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(150, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(12, 276);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(108, 276);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(90, 23);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYear.Location = new System.Drawing.Point(12, 42);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(87, 14);
            this.labelYear.TabIndex = 5;
            this.labelYear.Text = "Published Year";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(9, 135);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(67, 14);
            this.labelDescription.TabIndex = 6;
            this.labelDescription.Text = "Description";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(12, 153);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(398, 117);
            this.textBoxDescription.TabIndex = 7;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(105, 12);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(220, 20);
            this.textBoxTitle.TabIndex = 8;
            // 
            // textBoxYear
            // 
            this.textBoxYear.Location = new System.Drawing.Point(104, 40);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(220, 20);
            this.textBoxYear.TabIndex = 9;
            // 
            // pictureBoxCover
            // 
            this.pictureBoxCover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxCover.Location = new System.Drawing.Point(330, 12);
            this.pictureBoxCover.Name = "pictureBoxCover";
            this.pictureBoxCover.Size = new System.Drawing.Size(80, 120);
            this.pictureBoxCover.TabIndex = 10;
            this.pictureBoxCover.TabStop = false;
            // 
            // labelDownloaded
            // 
            this.labelDownloaded.AutoSize = true;
            this.labelDownloaded.Location = new System.Drawing.Point(101, 78);
            this.labelDownloaded.Name = "labelDownloaded";
            this.labelDownloaded.Size = new System.Drawing.Size(67, 13);
            this.labelDownloaded.TabIndex = 11;
            this.labelDownloaded.Text = "Downloaded";
            // 
            // labelWatched
            // 
            this.labelWatched.AutoSize = true;
            this.labelWatched.Location = new System.Drawing.Point(212, 78);
            this.labelWatched.Name = "labelWatched";
            this.labelWatched.Size = new System.Drawing.Size(51, 13);
            this.labelWatched.TabIndex = 13;
            this.labelWatched.Text = "Watched";
            // 
            // checkBoxWatched
            // 
            this.checkBoxWatched.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxWatched.Location = new System.Drawing.Point(269, 69);
            this.checkBoxWatched.Name = "checkBoxWatched";
            this.checkBoxWatched.Size = new System.Drawing.Size(30, 30);
            this.checkBoxWatched.TabIndex = 14;
            this.checkBoxWatched.UseVisualStyleBackColor = true;
            // 
            // checkBoxDownloaded
            // 
            this.checkBoxDownloaded.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxDownloaded.Location = new System.Drawing.Point(175, 70);
            this.checkBoxDownloaded.Name = "checkBoxDownloaded";
            this.checkBoxDownloaded.Size = new System.Drawing.Size(30, 30);
            this.checkBoxDownloaded.TabIndex = 12;
            this.checkBoxDownloaded.UseVisualStyleBackColor = true;
            // 
            // EditForm
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(424, 311);
            this.Controls.Add(this.checkBoxWatched);
            this.Controls.Add(this.labelWatched);
            this.Controls.Add(this.checkBoxDownloaded);
            this.Controls.Add(this.labelDownloaded);
            this.Controls.Add(this.pictureBoxCover);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelYear);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.Load += new System.EventHandler(this.EditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCover)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.PictureBox pictureBoxCover;
        private System.Windows.Forms.Label labelDownloaded;
        private System.Windows.Forms.Label labelWatched;
        private CustomCheckBox checkBoxDownloaded;
        private CustomCheckBox checkBoxWatched;
    }
}