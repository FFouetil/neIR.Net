namespace IRnetApp
{
    partial class ImgQuadView
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
            this.quadViewPanel = new System.Windows.Forms.TableLayoutPanel();
            this.topLeftBox = new System.Windows.Forms.PictureBox();
            this.topRightBox = new System.Windows.Forms.PictureBox();
            this.bottomLeftBox = new System.Windows.Forms.PictureBox();
            this.bottomRightBox = new System.Windows.Forms.PictureBox();
            this.quadViewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topLeftBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topRightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeftBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomRightBox)).BeginInit();
            this.SuspendLayout();
            // 
            // quadViewPanel
            // 
            this.quadViewPanel.AutoSize = true;
            this.quadViewPanel.ColumnCount = 2;
            this.quadViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.quadViewPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.quadViewPanel.Controls.Add(this.topLeftBox, 0, 0);
            this.quadViewPanel.Controls.Add(this.topRightBox, 1, 0);
            this.quadViewPanel.Controls.Add(this.bottomLeftBox, 0, 1);
            this.quadViewPanel.Controls.Add(this.bottomRightBox, 1, 1);
            this.quadViewPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quadViewPanel.Location = new System.Drawing.Point(0, 0);
            this.quadViewPanel.Name = "quadViewPanel";
            this.quadViewPanel.RowCount = 2;
            this.quadViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.quadViewPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.quadViewPanel.Size = new System.Drawing.Size(768, 524);
            this.quadViewPanel.TabIndex = 0;
            // 
            // topLeftBox
            // 
            this.topLeftBox.AccessibleName = "QuadView";
            this.topLeftBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topLeftBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topLeftBox.Location = new System.Drawing.Point(3, 3);
            this.topLeftBox.Name = "topLeftBox";
            this.topLeftBox.Size = new System.Drawing.Size(378, 256);
            this.topLeftBox.TabIndex = 0;
            this.topLeftBox.TabStop = false;
            this.topLeftBox.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.topLeftBox_LoadCompleted);
            this.topLeftBox.Click += new System.EventHandler(this.topLeftBox_Click);
            // 
            // topRightBox
            // 
            this.topRightBox.AccessibleName = "QuadView";
            this.topRightBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topRightBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topRightBox.Location = new System.Drawing.Point(387, 3);
            this.topRightBox.Name = "topRightBox";
            this.topRightBox.Size = new System.Drawing.Size(378, 256);
            this.topRightBox.TabIndex = 1;
            this.topRightBox.TabStop = false;
            // 
            // bottomLeftBox
            // 
            this.bottomLeftBox.AccessibleName = "QuadView";
            this.bottomLeftBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomLeftBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomLeftBox.Location = new System.Drawing.Point(3, 265);
            this.bottomLeftBox.Name = "bottomLeftBox";
            this.bottomLeftBox.Size = new System.Drawing.Size(378, 256);
            this.bottomLeftBox.TabIndex = 2;
            this.bottomLeftBox.TabStop = false;
            // 
            // bottomRightBox
            // 
            this.bottomRightBox.AccessibleName = "QuadView";
            this.bottomRightBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomRightBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomRightBox.Location = new System.Drawing.Point(387, 265);
            this.bottomRightBox.Name = "bottomRightBox";
            this.bottomRightBox.Size = new System.Drawing.Size(378, 256);
            this.bottomRightBox.TabIndex = 3;
            this.bottomRightBox.TabStop = false;
            // 
            // ImgQuadView
            // 
            this.ClientSize = new System.Drawing.Size(768, 524);
            this.Controls.Add(this.quadViewPanel);
            this.Name = "ImgQuadView";
            this.Text = "QuadView";
            this.quadViewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topLeftBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topRightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeftBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomRightBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel quadViewPanel;
        private System.Windows.Forms.PictureBox topLeftBox;
        private System.Windows.Forms.PictureBox topRightBox;
        private System.Windows.Forms.PictureBox bottomLeftBox;
        private System.Windows.Forms.PictureBox bottomRightBox;

    }
}