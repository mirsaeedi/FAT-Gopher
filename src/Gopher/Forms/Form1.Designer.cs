namespace Gopher
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnGo = new System.Windows.Forms.Button();
            this.cmbPath = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trvDirectory = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMBRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSEExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lsvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.listImages = new System.Windows.Forms.ImageList(this.components);
            this.btnBack = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(846, 41);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(59, 28);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cmbPath
            // 
            this.cmbPath.FormattingEnabled = true;
            this.cmbPath.Location = new System.Drawing.Point(290, 46);
            this.cmbPath.Name = "cmbPath";
            this.cmbPath.Size = new System.Drawing.Size(550, 21);
            this.cmbPath.TabIndex = 3;
            this.cmbPath.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPath_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Path :";
            // 
            // trvDirectory
            // 
            this.trvDirectory.Location = new System.Drawing.Point(0, 3);
            this.trvDirectory.Name = "trvDirectory";
            this.trvDirectory.Size = new System.Drawing.Size(223, 482);
            this.trvDirectory.TabIndex = 1;
            this.trvDirectory.DoubleClick += new System.EventHandler(this.trvDirectory_DoubleClick);
            this.trvDirectory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvDirectory_AfterSelect);
            this.trvDirectory.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.trvDirectory_KeyPress);
            this.trvDirectory.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvDirectory_AfterExpand);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(915, 24);
            this.menuStrip1.TabIndex = 11;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMBRToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // showMBRToolStripMenuItem
            // 
            this.showMBRToolStripMenuItem.Name = "showMBRToolStripMenuItem";
            this.showMBRToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.showMBRToolStripMenuItem.Text = "Show MBR";
            this.showMBRToolStripMenuItem.Click += new System.EventHandler(this.showMBRToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutUSToolStripMenuItem,
            this.aboutSEExplorerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutUSToolStripMenuItem
            // 
            this.aboutUSToolStripMenuItem.Name = "aboutUSToolStripMenuItem";
            this.aboutUSToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.aboutUSToolStripMenuItem.Text = "About US";
            this.aboutUSToolStripMenuItem.Click += new System.EventHandler(this.aboutUSToolStripMenuItem_Click);
            // 
            // aboutSEExplorerToolStripMenuItem
            // 
            this.aboutSEExplorerToolStripMenuItem.Name = "aboutSEExplorerToolStripMenuItem";
            this.aboutSEExplorerToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.aboutSEExplorerToolStripMenuItem.Text = "About SE Explorer";
            this.aboutSEExplorerToolStripMenuItem.Click += new System.EventHandler(this.aboutSEExplorerToolStripMenuItem_Click);
            // 
            // lsvFiles
            // 
            this.lsvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lsvFiles.FullRowSelect = true;
            this.lsvFiles.GridLines = true;
            this.lsvFiles.Location = new System.Drawing.Point(243, 78);
            this.lsvFiles.Name = "lsvFiles";
            this.lsvFiles.RightToLeftLayout = true;
            this.lsvFiles.Size = new System.Drawing.Size(662, 405);
            this.lsvFiles.TabIndex = 2;
            this.lsvFiles.TileSize = new System.Drawing.Size(250, 90);
            this.lsvFiles.UseCompatibleStateImageBehavior = false;
            this.lsvFiles.View = System.Windows.Forms.View.Tile;
            this.lsvFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvFiles_MouseDoubleClick);
            this.lsvFiles.SelectedIndexChanged += new System.EventHandler(this.lsvFiles_SelectedIndexChanged);
            this.lsvFiles.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lsvFiles_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Attribute";
            this.columnHeader4.Width = 71;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Creation Date";
            this.columnHeader5.Width = 94;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "LastAccess";
            this.columnHeader6.Width = 85;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Write Date";
            this.columnHeader7.Width = 98;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.lblSize);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Location = new System.Drawing.Point(243, 487);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 79);
            this.panel1.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(580, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 72);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(2, 57);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(0, 13);
            this.lblDate.TabIndex = 2;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(2, 33);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(0, 13);
            this.lblSize.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(1, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 0;
            // 
            // images
            // 
            this.images.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.images.ImageSize = new System.Drawing.Size(24, 24);
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listImages
            // 
            this.listImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.listImages.ImageSize = new System.Drawing.Size(72, 72);
            this.listImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // btnBack
            // 
            this.btnBack.BackgroundImage = global::FatGopher.Properties.Resources.backD;
            this.btnBack.Enabled = false;
            this.btnBack.Location = new System.Drawing.Point(11, 27);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(50, 50);
            this.btnBack.TabIndex = 9;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.BackgroundImage = global::FatGopher.Properties.Resources.forwardD;
            this.btnForward.Enabled = false;
            this.btnForward.Location = new System.Drawing.Point(67, 27);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(50, 50);
            this.btnForward.TabIndex = 8;
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackgroundImage = global::FatGopher.Properties.Resources.upD;
            this.btnUp.Enabled = false;
            this.btnUp.Location = new System.Drawing.Point(187, 27);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(50, 50);
            this.btnUp.TabIndex = 7;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.trvDirectory);
            this.panel2.Location = new System.Drawing.Point(11, 78);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(226, 488);
            this.panel2.TabIndex = 13;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 575);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lsvFiles);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPath);
            this.Controls.Add(this.btnGo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SE Explorer";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ComboBox cmbPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView trvDirectory;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMBRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSEExplorerToolStripMenuItem;
        private System.Windows.Forms.ListView lsvFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ImageList images;
        private System.Windows.Forms.ImageList listImages;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
    }
}

