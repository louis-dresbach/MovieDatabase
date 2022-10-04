namespace MoviesDatabase
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.contextMenuStripItemEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.copyTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openIMDBPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.watchedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMoviesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTvSeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.hideViewedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showNotViewedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnlyDownloadedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnlyNotDownloadedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.macrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForNewEpisodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadMissingSubtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMissingPlotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.createTextFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.listViewSearchResults = new System.Windows.Forms.ListView();
            this.timerCloseSearchResults = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialogTextFile = new System.Windows.Forms.SaveFileDialog();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.panelInfoMovie = new System.Windows.Forms.Panel();
            this.buttonInfoPlay = new System.Windows.Forms.Button();
            this.labelInfoPlot = new System.Windows.Forms.Label();
            this.labelInfoRuntime = new System.Windows.Forms.Label();
            this.labelInfoYear = new System.Windows.Forms.Label();
            this.labelInfoTitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelInfoTvSeries = new System.Windows.Forms.Panel();
            this.treeViewSeasons = new System.Windows.Forms.TreeView();
            this.labelTvInfoPlot = new System.Windows.Forms.Label();
            this.labelTvInfoTitle = new System.Windows.Forms.Label();
            this.listViewMovies = new MoviesDatabase.CustomListView();
            this.contextMenuStripItemEdit.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.panelInfoMovie.SuspendLayout();
            this.panelInfoTvSeries.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripItemEdit
            // 
            this.contextMenuStripItemEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.toolStripSeparator7,
            this.copyTitleToolStripMenuItem,
            this.openFolderToolStripMenuItem,
            this.openIMDBPageToolStripMenuItem,
            this.toolStripSeparator1,
            this.downloadedToolStripMenuItem,
            this.watchedToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolStripSeparator2,
            this.removeToolStripMenuItem});
            this.contextMenuStripItemEdit.Name = "contextMenuStrip1";
            this.contextMenuStripItemEdit.Size = new System.Drawing.Size(184, 220);
            this.contextMenuStripItemEdit.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStripItemEdit_Closed);
            this.contextMenuStripItemEdit.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStripItemEdit.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(180, 6);
            // 
            // copyTitleToolStripMenuItem
            // 
            this.copyTitleToolStripMenuItem.Name = "copyTitleToolStripMenuItem";
            this.copyTitleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyTitleToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyTitleToolStripMenuItem.Text = "Copy Title";
            this.copyTitleToolStripMenuItem.Click += new System.EventHandler(this.copyTitleToolStripMenuItem_Click);
            // 
            // openFolderToolStripMenuItem
            // 
            this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            this.openFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openFolderToolStripMenuItem.Text = "Open Folder";
            this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
            // 
            // openIMDBPageToolStripMenuItem
            // 
            this.openIMDBPageToolStripMenuItem.Name = "openIMDBPageToolStripMenuItem";
            this.openIMDBPageToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openIMDBPageToolStripMenuItem.Text = "Open IMDB Page";
            this.openIMDBPageToolStripMenuItem.Click += new System.EventHandler(this.openIMDBPageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // downloadedToolStripMenuItem
            // 
            this.downloadedToolStripMenuItem.Name = "downloadedToolStripMenuItem";
            this.downloadedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.downloadedToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.downloadedToolStripMenuItem.Text = "Downloaded";
            this.downloadedToolStripMenuItem.Click += new System.EventHandler(this.downloadedToolStripMenuItem_Click);
            // 
            // watchedToolStripMenuItem
            // 
            this.watchedToolStripMenuItem.Name = "watchedToolStripMenuItem";
            this.watchedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.watchedToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.watchedToolStripMenuItem.Text = "Watched";
            this.watchedToolStripMenuItem.Click += new System.EventHandler(this.watchedToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(180, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabel1.Text = "0 movies total.";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem1,
            this.viewToolStripMenuItem,
            this.macrosToolStripMenuItem,
            this.toolStripTextBox1,
            this.toolStripLabel1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 27);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator3,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.reloadToolStripMenuItem.Text = "&Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.refreshToolStripMenuItem.Text = "R&efresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator6,
            this.settingsToolStripMenuItem});
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(39, 23);
            this.editToolStripMenuItem1.Text = "&Edit";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(161, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.settingsToolStripMenuItem.Text = "Se&ttings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMoviesToolStripMenuItem,
            this.showTvSeriesToolStripMenuItem,
            this.toolStripSeparator5,
            this.hideViewedToolStripMenuItem,
            this.showNotViewedToolStripMenuItem,
            this.showOnlyDownloadedToolStripMenuItem,
            this.showOnlyNotDownloadedToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // showMoviesToolStripMenuItem
            // 
            this.showMoviesToolStripMenuItem.Name = "showMoviesToolStripMenuItem";
            this.showMoviesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showMoviesToolStripMenuItem.Text = "Show &Movies";
            this.showMoviesToolStripMenuItem.Click += new System.EventHandler(this.showMoviesToolStripMenuItem_Click);
            // 
            // showTvSeriesToolStripMenuItem
            // 
            this.showTvSeriesToolStripMenuItem.Name = "showTvSeriesToolStripMenuItem";
            this.showTvSeriesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showTvSeriesToolStripMenuItem.Text = "Show &Tv Series";
            this.showTvSeriesToolStripMenuItem.Click += new System.EventHandler(this.showTvSeriesToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(193, 6);
            // 
            // hideViewedToolStripMenuItem
            // 
            this.hideViewedToolStripMenuItem.Name = "hideViewedToolStripMenuItem";
            this.hideViewedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.hideViewedToolStripMenuItem.Text = "Show &Viewed";
            this.hideViewedToolStripMenuItem.Click += new System.EventHandler(this.hideViewedToolStripMenuItem_Click);
            // 
            // showNotViewedToolStripMenuItem
            // 
            this.showNotViewedToolStripMenuItem.Name = "showNotViewedToolStripMenuItem";
            this.showNotViewedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showNotViewedToolStripMenuItem.Text = "Show Not Viewed";
            this.showNotViewedToolStripMenuItem.Click += new System.EventHandler(this.showNotViewedToolStripMenuItem_Click);
            // 
            // showOnlyDownloadedToolStripMenuItem
            // 
            this.showOnlyDownloadedToolStripMenuItem.Name = "showOnlyDownloadedToolStripMenuItem";
            this.showOnlyDownloadedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showOnlyDownloadedToolStripMenuItem.Text = "Show &Downloaded";
            this.showOnlyDownloadedToolStripMenuItem.Click += new System.EventHandler(this.showOnlyDownloadedToolStripMenuItem_Click);
            // 
            // showOnlyNotDownloadedToolStripMenuItem
            // 
            this.showOnlyNotDownloadedToolStripMenuItem.Name = "showOnlyNotDownloadedToolStripMenuItem";
            this.showOnlyNotDownloadedToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showOnlyNotDownloadedToolStripMenuItem.Text = "Show Not Downloaded";
            this.showOnlyNotDownloadedToolStripMenuItem.Click += new System.EventHandler(this.showOnlyNotDownloadedToolStripMenuItem_Click);
            // 
            // macrosToolStripMenuItem
            // 
            this.macrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForNewEpisodesToolStripMenuItem,
            this.downloadMissingSubtitlesToolStripMenuItem,
            this.loadMissingPlotsToolStripMenuItem,
            this.toolStripSeparator4,
            this.createTextFileToolStripMenuItem});
            this.macrosToolStripMenuItem.Name = "macrosToolStripMenuItem";
            this.macrosToolStripMenuItem.Size = new System.Drawing.Size(58, 23);
            this.macrosToolStripMenuItem.Text = "&Macros";
            // 
            // checkForNewEpisodesToolStripMenuItem
            // 
            this.checkForNewEpisodesToolStripMenuItem.Name = "checkForNewEpisodesToolStripMenuItem";
            this.checkForNewEpisodesToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.checkForNewEpisodesToolStripMenuItem.Text = "Check for new Episodes";
            this.checkForNewEpisodesToolStripMenuItem.Click += new System.EventHandler(this.checkForNewEpisodesToolStripMenuItem_Click);
            // 
            // downloadMissingSubtitlesToolStripMenuItem
            // 
            this.downloadMissingSubtitlesToolStripMenuItem.Name = "downloadMissingSubtitlesToolStripMenuItem";
            this.downloadMissingSubtitlesToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.downloadMissingSubtitlesToolStripMenuItem.Text = "Download missing subtitles";
            this.downloadMissingSubtitlesToolStripMenuItem.Click += new System.EventHandler(this.downloadMissingSubtitlesToolStripMenuItem_Click);
            // 
            // loadMissingPlotsToolStripMenuItem
            // 
            this.loadMissingPlotsToolStripMenuItem.Name = "loadMissingPlotsToolStripMenuItem";
            this.loadMissingPlotsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.loadMissingPlotsToolStripMenuItem.Text = "Load missing &Information";
            this.loadMissingPlotsToolStripMenuItem.Click += new System.EventHandler(this.loadMissingInformationToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(216, 6);
            // 
            // createTextFileToolStripMenuItem
            // 
            this.createTextFileToolStripMenuItem.Name = "createTextFileToolStripMenuItem";
            this.createTextFileToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.createTextFileToolStripMenuItem.Text = "Create &text file";
            this.createTextFileToolStripMenuItem.Click += new System.EventHandler(this.createTextFileToolStripMenuItem_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.Margin = new System.Windows.Forms.Padding(5, 0, 35, 0);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(240, 23);
            this.toolStripTextBox1.Enter += new System.EventHandler(this.toolStripTextBox1_Enter);
            this.toolStripTextBox1.Leave += new System.EventHandler(this.toolStripTextBox1_Leave);
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 20);
            this.toolStripLabel1.Text = "Search: ";
            // 
            // listViewSearchResults
            // 
            this.listViewSearchResults.BackColor = System.Drawing.Color.LightGray;
            this.listViewSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewSearchResults.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSearchResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewSearchResults.HideSelection = false;
            this.listViewSearchResults.HoverSelection = true;
            this.listViewSearchResults.Location = new System.Drawing.Point(509, 27);
            this.listViewSearchResults.MultiSelect = false;
            this.listViewSearchResults.Name = "listViewSearchResults";
            this.listViewSearchResults.OwnerDraw = true;
            this.listViewSearchResults.Size = new System.Drawing.Size(240, 64);
            this.listViewSearchResults.TabIndex = 3;
            this.listViewSearchResults.TileSize = new System.Drawing.Size(220, 30);
            this.listViewSearchResults.UseCompatibleStateImageBehavior = false;
            this.listViewSearchResults.View = System.Windows.Forms.View.Tile;
            this.listViewSearchResults.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listViewSearchResults_DrawItem);
            this.listViewSearchResults.Enter += new System.EventHandler(this.listViewSearchResults_Enter);
            this.listViewSearchResults.Leave += new System.EventHandler(this.listViewSearchResults_Leave);
            this.listViewSearchResults.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSearchResults_MouseDoubleClick);
            // 
            // timerCloseSearchResults
            // 
            this.timerCloseSearchResults.Tick += new System.EventHandler(this.timerCloseSearchResults_Tick);
            // 
            // saveFileDialogTextFile
            // 
            this.saveFileDialogTextFile.DefaultExt = "txt";
            this.saveFileDialogTextFile.FileName = "MovieDatabase";
            this.saveFileDialogTextFile.Filter = "Text files|*.txt|All files|*.*";
            this.saveFileDialogTextFile.Title = "Save File....";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // panelInfoMovie
            // 
            this.panelInfoMovie.AutoSize = true;
            this.panelInfoMovie.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelInfoMovie.BackColor = System.Drawing.Color.White;
            this.panelInfoMovie.Controls.Add(this.buttonInfoPlay);
            this.panelInfoMovie.Controls.Add(this.labelInfoPlot);
            this.panelInfoMovie.Controls.Add(this.labelInfoRuntime);
            this.panelInfoMovie.Controls.Add(this.labelInfoYear);
            this.panelInfoMovie.Controls.Add(this.labelInfoTitle);
            this.panelInfoMovie.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelInfoMovie.Location = new System.Drawing.Point(236, 125);
            this.panelInfoMovie.MinimumSize = new System.Drawing.Size(160, 200);
            this.panelInfoMovie.Name = "panelInfoMovie";
            this.panelInfoMovie.Padding = new System.Windows.Forms.Padding(8);
            this.panelInfoMovie.Size = new System.Drawing.Size(160, 200);
            this.panelInfoMovie.TabIndex = 4;
            this.panelInfoMovie.Visible = false;
            this.panelInfoMovie.Paint += new System.Windows.Forms.PaintEventHandler(this.panelInfo_Paint);
            // 
            // buttonInfoPlay
            // 
            this.buttonInfoPlay.Location = new System.Drawing.Point(20, 166);
            this.buttonInfoPlay.Name = "buttonInfoPlay";
            this.buttonInfoPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonInfoPlay.TabIndex = 4;
            this.buttonInfoPlay.Text = "Play";
            this.buttonInfoPlay.UseVisualStyleBackColor = true;
            // 
            // labelInfoPlot
            // 
            this.labelInfoPlot.AutoSize = true;
            this.labelInfoPlot.Location = new System.Drawing.Point(20, 57);
            this.labelInfoPlot.MaximumSize = new System.Drawing.Size(150, 0);
            this.labelInfoPlot.MinimumSize = new System.Drawing.Size(0, 100);
            this.labelInfoPlot.Name = "labelInfoPlot";
            this.labelInfoPlot.Size = new System.Drawing.Size(84, 100);
            this.labelInfoPlot.TabIndex = 3;
            this.labelInfoPlot.Text = "Plot.... Blah blah";
            // 
            // labelInfoRuntime
            // 
            this.labelInfoRuntime.AutoSize = true;
            this.labelInfoRuntime.Location = new System.Drawing.Point(20, 40);
            this.labelInfoRuntime.Name = "labelInfoRuntime";
            this.labelInfoRuntime.Size = new System.Drawing.Size(40, 13);
            this.labelInfoRuntime.TabIndex = 2;
            this.labelInfoRuntime.Text = "90mins";
            // 
            // labelInfoYear
            // 
            this.labelInfoYear.AutoSize = true;
            this.labelInfoYear.Font = new System.Drawing.Font("Monotype Corsiva", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoYear.Location = new System.Drawing.Point(101, 16);
            this.labelInfoYear.Name = "labelInfoYear";
            this.labelInfoYear.Size = new System.Drawing.Size(33, 15);
            this.labelInfoYear.TabIndex = 1;
            this.labelInfoYear.Text = "(Year)";
            // 
            // labelInfoTitle
            // 
            this.labelInfoTitle.AutoSize = true;
            this.labelInfoTitle.BackColor = System.Drawing.Color.White;
            this.labelInfoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfoTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelInfoTitle.Location = new System.Drawing.Point(16, 16);
            this.labelInfoTitle.Name = "labelInfoTitle";
            this.labelInfoTitle.Size = new System.Drawing.Size(94, 20);
            this.labelInfoTitle.TabIndex = 0;
            this.labelInfoTitle.Text = "Movie Title";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelInfoTvSeries
            // 
            this.panelInfoTvSeries.AutoSize = true;
            this.panelInfoTvSeries.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelInfoTvSeries.BackColor = System.Drawing.Color.White;
            this.panelInfoTvSeries.Controls.Add(this.treeViewSeasons);
            this.panelInfoTvSeries.Controls.Add(this.labelTvInfoPlot);
            this.panelInfoTvSeries.Controls.Add(this.labelTvInfoTitle);
            this.panelInfoTvSeries.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelInfoTvSeries.Location = new System.Drawing.Point(402, 125);
            this.panelInfoTvSeries.MinimumSize = new System.Drawing.Size(160, 200);
            this.panelInfoTvSeries.Name = "panelInfoTvSeries";
            this.panelInfoTvSeries.Padding = new System.Windows.Forms.Padding(8);
            this.panelInfoTvSeries.Size = new System.Drawing.Size(169, 225);
            this.panelInfoTvSeries.TabIndex = 5;
            this.panelInfoTvSeries.Visible = false;
            // 
            // treeViewSeasons
            // 
            this.treeViewSeasons.Location = new System.Drawing.Point(11, 143);
            this.treeViewSeasons.Name = "treeViewSeasons";
            this.treeViewSeasons.Size = new System.Drawing.Size(147, 71);
            this.treeViewSeasons.TabIndex = 4;
            this.treeViewSeasons.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewSeasons_NodeMouseDoubleClick);
            // 
            // labelTvInfoPlot
            // 
            this.labelTvInfoPlot.AutoSize = true;
            this.labelTvInfoPlot.Location = new System.Drawing.Point(11, 40);
            this.labelTvInfoPlot.MaximumSize = new System.Drawing.Size(180, 0);
            this.labelTvInfoPlot.MinimumSize = new System.Drawing.Size(0, 100);
            this.labelTvInfoPlot.Name = "labelTvInfoPlot";
            this.labelTvInfoPlot.Size = new System.Drawing.Size(84, 100);
            this.labelTvInfoPlot.TabIndex = 3;
            this.labelTvInfoPlot.Text = "Plot.... Blah blah";
            // 
            // labelTvInfoTitle
            // 
            this.labelTvInfoTitle.AutoSize = true;
            this.labelTvInfoTitle.BackColor = System.Drawing.Color.White;
            this.labelTvInfoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTvInfoTitle.ForeColor = System.Drawing.Color.DodgerBlue;
            this.labelTvInfoTitle.Location = new System.Drawing.Point(16, 16);
            this.labelTvInfoTitle.Name = "labelTvInfoTitle";
            this.labelTvInfoTitle.Size = new System.Drawing.Size(94, 20);
            this.labelTvInfoTitle.TabIndex = 0;
            this.labelTvInfoTitle.Text = "Movie Title";
            // 
            // listViewMovies
            // 
            this.listViewMovies.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.listViewMovies.BackColor = System.Drawing.Color.AliceBlue;
            this.listViewMovies.ContextMenuStrip = this.contextMenuStripItemEdit;
            this.listViewMovies.Cursor = System.Windows.Forms.Cursors.Default;
            this.listViewMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMovies.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewMovies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewMovies.Location = new System.Drawing.Point(0, 27);
            this.listViewMovies.Name = "listViewMovies";
            this.listViewMovies.OwnerDraw = true;
            this.listViewMovies.Size = new System.Drawing.Size(784, 512);
            this.listViewMovies.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewMovies.TabIndex = 0;
            this.listViewMovies.TileSize = new System.Drawing.Size(143, 205);
            this.listViewMovies.UseCompatibleStateImageBehavior = false;
            this.listViewMovies.View = System.Windows.Forms.View.Tile;
            this.listViewMovies.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewMovies_KeyDown);
            this.listViewMovies.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewMovies_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panelInfoTvSeries);
            this.Controls.Add(this.panelInfoMovie);
            this.Controls.Add(this.listViewSearchResults);
            this.Controls.Add(this.listViewMovies);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movie Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStripItemEdit.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.panelInfoMovie.ResumeLayout(false);
            this.panelInfoMovie.PerformLayout();
            this.panelInfoTvSeries.ResumeLayout(false);
            this.panelInfoTvSeries.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripItemEdit;
        private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem downloadedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem watchedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem copyTitleToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMoviesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTvSeriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem hideViewedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnlyDownloadedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem macrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem openIMDBPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ListView listViewSearchResults;
        private System.Windows.Forms.Timer timerCloseSearchResults;
        private System.Windows.Forms.ToolStripMenuItem loadMissingPlotsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private CustomListView listViewMovies;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem createTextFileToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogTextFile;
        private System.Windows.Forms.ToolStripMenuItem showOnlyNotDownloadedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showNotViewedToolStripMenuItem;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Panel panelInfoMovie;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelInfoYear;
        private System.Windows.Forms.Label labelInfoTitle;
        private System.Windows.Forms.Label labelInfoRuntime;
        private System.Windows.Forms.Label labelInfoPlot;
        private System.Windows.Forms.Button buttonInfoPlay;
        private System.Windows.Forms.Panel panelInfoTvSeries;
        private System.Windows.Forms.Label labelTvInfoPlot;
        private System.Windows.Forms.Label labelTvInfoTitle;
        private System.Windows.Forms.TreeView treeViewSeasons;
        private System.Windows.Forms.ToolStripMenuItem checkForNewEpisodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadMissingSubtitlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;

    }
}

