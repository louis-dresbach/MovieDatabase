namespace MoviesDatabase
{
    partial class Loader
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loader));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelPercentage = new System.Windows.Forms.Label();
            this.timerStarter = new System.Windows.Forms.Timer(this.components);
            this.labelWhatsGoingOn = new System.Windows.Forms.Label();
            this.labelSubTitle = new MoviesDatabase.GradientLabel();
            this.labelTitle = new MoviesDatabase.GradientLabel();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.Color.MediumOrchid;
            this.progressBar.Location = new System.Drawing.Point(25, 220);
            this.progressBar.Maximum = 100000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(511, 12);
            this.progressBar.TabIndex = 0;
            // 
            // labelPercentage
            // 
            this.labelPercentage.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPercentage.Location = new System.Drawing.Point(25, 194);
            this.labelPercentage.Name = "labelPercentage";
            this.labelPercentage.Size = new System.Drawing.Size(511, 23);
            this.labelPercentage.TabIndex = 3;
            this.labelPercentage.Text = "0%";
            this.labelPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerStarter
            // 
            this.timerStarter.Interval = 300;
            this.timerStarter.Tick += new System.EventHandler(this.timerStarter_Tick);
            // 
            // labelWhatsGoingOn
            // 
            this.labelWhatsGoingOn.Location = new System.Drawing.Point(26, 235);
            this.labelWhatsGoingOn.Name = "labelWhatsGoingOn";
            this.labelWhatsGoingOn.Size = new System.Drawing.Size(510, 16);
            this.labelWhatsGoingOn.TabIndex = 4;
            this.labelWhatsGoingOn.Text = "What is currently going on?";
            // 
            // labelSubTitle
            // 
            this.labelSubTitle.AutoSize = true;
            this.labelSubTitle.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubTitle.ForeColor = System.Drawing.Color.LightCyan;
            this.labelSubTitle.Gradient1 = System.Drawing.Color.White;
            this.labelSubTitle.Gradient2 = System.Drawing.Color.LightCyan;
            this.labelSubTitle.Location = new System.Drawing.Point(298, 86);
            this.labelSubTitle.Name = "labelSubTitle";
            this.labelSubTitle.Size = new System.Drawing.Size(238, 35);
            this.labelSubTitle.TabIndex = 2;
            this.labelSubTitle.Text = "Loading Movies...";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Azure;
            this.labelTitle.Gradient1 = System.Drawing.Color.White;
            this.labelTitle.Gradient2 = System.Drawing.Color.Azure;
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(524, 77);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "MovieDatabase";
            // 
            // Loader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(561, 260);
            this.Controls.Add(this.labelWhatsGoingOn);
            this.Controls.Add(this.labelPercentage);
            this.Controls.Add(this.labelSubTitle);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Loader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Loader";
            this.Load += new System.EventHandler(this.Loader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelPercentage;
        private System.Windows.Forms.Timer timerStarter;
        private System.Windows.Forms.Label labelWhatsGoingOn;
        private GradientLabel labelTitle;
        private GradientLabel labelSubTitle;
    }
}