namespace neIR
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            m_dummyForLoading.Dispose();
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_bigPictureBox = new System.Windows.Forms.PictureBox();
            this.quadView = new neIR.QuadView();
            this.topBar = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenu_open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_displayModes = new System.Windows.Forms.ToolStrip();
            this.displayStrip_showDebayered = new System.Windows.Forms.ToolStripButton();
            this.displayStrip_showAsGreyscale = new System.Windows.Forms.ToolStripButton();
            this.displayStrip_showSplitChannels = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip_clearButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_bigPictureBox)).BeginInit();
            this.topBar.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStrip_displayModes.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusMessage});
            this.statusStrip.Location = new System.Drawing.Point(0, 548);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(792, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // StatusMessage
            // 
            this.StatusMessage.Name = "StatusMessage";
            this.StatusMessage.Size = new System.Drawing.Size(88, 17);
            this.StatusMessage.Text = "Status message";
            // 
            // m_bigPictureBox
            // 
            this.m_bigPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_bigPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_bigPictureBox.Location = new System.Drawing.Point(0, 58);
            this.m_bigPictureBox.Name = "m_bigPictureBox";
            this.m_bigPictureBox.Size = new System.Drawing.Size(792, 512);
            this.m_bigPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.m_bigPictureBox.TabIndex = 4;
            this.m_bigPictureBox.TabStop = false;
            // 
            // quadView
            // 
            this.quadView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.quadView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quadView.Location = new System.Drawing.Point(0, 58);
            this.quadView.Name = "quadView";
            this.quadView.Size = new System.Drawing.Size(792, 512);
            this.quadView.TabIndex = 3;
            // 
            // topBar
            // 
            this.topBar.AutoSize = true;
            this.topBar.Controls.Add(this.menuStrip);
            this.topBar.Controls.Add(this.toolStrip_displayModes);
            this.topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBar.Location = new System.Drawing.Point(0, 0);
            this.topBar.Name = "topBar";
            this.topBar.Size = new System.Drawing.Size(792, 58);
            this.topBar.TabIndex = 6;
            // 
            // menuStrip
            // 
            this.topBar.SetFlowBreak(this.menuStrip, true);
            this.menuStrip.GripMargin = new System.Windows.Forms.Padding(3, 3, 2, 2);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(2, 2);
            this.menuStrip.Margin = new System.Windows.Forms.Padding(2);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(2);
            this.menuStrip.Size = new System.Drawing.Size(43, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu_open});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // fileMenu_open
            // 
            this.fileMenu_open.Name = "fileMenu_open";
            this.fileMenu_open.Size = new System.Drawing.Size(152, 22);
            this.fileMenu_open.Text = "Open";
            this.fileMenu_open.Click += new System.EventHandler(this.fileMenu_open_Click);
            // 
            // toolStrip_displayModes
            // 
            this.toolStrip_displayModes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip_displayModes.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip_displayModes.Dock = System.Windows.Forms.DockStyle.None;
            this.topBar.SetFlowBreak(this.toolStrip_displayModes, true);
            this.toolStrip_displayModes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayStrip_showDebayered,
            this.displayStrip_showAsGreyscale,
            this.displayStrip_showSplitChannels,
            this.toolStripSeparator1,
            this.toolStrip_clearButton});
            this.toolStrip_displayModes.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip_displayModes.Location = new System.Drawing.Point(2, 31);
            this.toolStrip_displayModes.Margin = new System.Windows.Forms.Padding(2);
            this.toolStrip_displayModes.Name = "toolStrip_displayModes";
            this.toolStrip_displayModes.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip_displayModes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip_displayModes.Size = new System.Drawing.Size(458, 25);
            this.toolStrip_displayModes.Stretch = true;
            this.toolStrip_displayModes.TabIndex = 6;
            this.toolStrip_displayModes.Text = "toolStrip";
            // 
            // displayStrip_showDebayered
            // 
            this.displayStrip_showDebayered.Enabled = false;
            this.displayStrip_showDebayered.Image = ((System.Drawing.Image)(resources.GetObject("displayStrip_showDebayered.Image")));
            this.displayStrip_showDebayered.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.displayStrip_showDebayered.Name = "displayStrip_showDebayered";
            this.displayStrip_showDebayered.Size = new System.Drawing.Size(122, 22);
            this.displayStrip_showDebayered.Text = "Debayered as RGB";
            this.displayStrip_showDebayered.Click += new System.EventHandler(this.displayStrip_showDebayered_Click);
            // 
            // displayStrip_showAsGreyscale
            // 
            this.displayStrip_showAsGreyscale.Enabled = false;
            this.displayStrip_showAsGreyscale.Image = ((System.Drawing.Image)(resources.GetObject("displayStrip_showAsGreyscale.Image")));
            this.displayStrip_showAsGreyscale.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.displayStrip_showAsGreyscale.Name = "displayStrip_showAsGreyscale";
            this.displayStrip_showAsGreyscale.Size = new System.Drawing.Size(122, 22);
            this.displayStrip_showAsGreyscale.Text = "Bayer as greyscale";
            this.displayStrip_showAsGreyscale.ToolTipText = "Bayer as greyscale";
            this.displayStrip_showAsGreyscale.Click += new System.EventHandler(this.displayStrip_showAsGreyscale_Click);
            // 
            // displayStrip_showSplitChannels
            // 
            this.displayStrip_showSplitChannels.Enabled = false;
            this.displayStrip_showSplitChannels.Image = ((System.Drawing.Image)(resources.GetObject("displayStrip_showSplitChannels.Image")));
            this.displayStrip_showSplitChannels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.displayStrip_showSplitChannels.Name = "displayStrip_showSplitChannels";
            this.displayStrip_showSplitChannels.Size = new System.Drawing.Size(132, 22);
            this.displayStrip_showSplitChannels.Text = "Split Bayer channels";
            this.displayStrip_showSplitChannels.Click += new System.EventHandler(this.displayStrip_showSplitChannels_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip_clearButton
            // 
            this.toolStrip_clearButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStrip_clearButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStrip_clearButton.Image")));
            this.toolStrip_clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStrip_clearButton.Name = "toolStrip_clearButton";
            this.toolStrip_clearButton.Size = new System.Drawing.Size(65, 22);
            this.toolStrip_clearButton.Text = "Unload";
            this.toolStrip_clearButton.Click += new System.EventHandler(this.toolStrip_clearButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 570);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.m_bigPictureBox);
            this.Controls.Add(this.quadView);
            this.Controls.Add(this.topBar);
            this.DoubleBuffered = true;
            this.Name = "MainWindow";
            this.Text = "Bayer Splitter";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_bigPictureBox)).EndInit();
            this.topBar.ResumeLayout(false);
            this.topBar.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip_displayModes.ResumeLayout(false);
            this.toolStrip_displayModes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        internal System.Windows.Forms.ToolStripStatusLabel StatusMessage;
        private QuadView quadView;
        private System.Windows.Forms.PictureBox m_bigPictureBox;
        private System.Windows.Forms.FlowLayoutPanel topBar;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu_open;
        private System.Windows.Forms.ToolStrip toolStrip_displayModes;
        private System.Windows.Forms.ToolStripButton displayStrip_showDebayered;
        private System.Windows.Forms.ToolStripButton displayStrip_showAsGreyscale;
        private System.Windows.Forms.ToolStripButton displayStrip_showSplitChannels;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStrip_clearButton;
    }
}

