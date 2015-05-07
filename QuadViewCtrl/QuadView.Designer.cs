using ZoomablePictureBoxCtrl;

namespace neIR
{
    partial class QuadView
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
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.quadViewPanel = new System.Windows.Forms.TableLayoutPanel();
            this.topLeftBox = new ZoomablePictureBox();
            this.topRightBox = new ZoomablePictureBox();
            this.bottomLeftBox = new ZoomablePictureBox();
            this.bottomRightBox = new ZoomablePictureBox();
            this.quadViewPanel.SuspendLayout();
            /*((System.ComponentModel.ISupportInitialize)(this.topLeftBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topRightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeftBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomRightBox)).BeginInit();*/
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
            this.quadViewPanel.Size = new System.Drawing.Size(640, 480);
            this.quadViewPanel.TabIndex = 1;
            this.quadViewPanel.MouseLeave += new System.EventHandler(this.quadViewPanel_MouseLeave);
            // 
            // topLeftBox
            // 
            this.topLeftBox.AccessibleName = "QuadView";
            this.topLeftBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topLeftBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topLeftBox.Location = new System.Drawing.Point(3, 3);
            this.topLeftBox.Name = "topLeftBox";
            this.topLeftBox.Size = new System.Drawing.Size(314, 234);
           // this.topLeftBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.topLeftBox.TabIndex = 0;
            this.topLeftBox.TabStop = false;
            this.topLeftBox.MouseEnter += new System.EventHandler(this.topLeftBox_MouseEnter);
            // 
            // topRightBox
            // 
            this.topRightBox.AccessibleName = "QuadView";
            this.topRightBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topRightBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topRightBox.Location = new System.Drawing.Point(323, 3);
            this.topRightBox.Name = "topRightBox";
            this.topRightBox.Size = new System.Drawing.Size(314, 234);
            //this.topRightBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.topRightBox.TabIndex = 1;
            this.topRightBox.TabStop = false;
            this.topRightBox.MouseEnter += new System.EventHandler(this.topRightBox_MouseEnter);
            // 
            // bottomLeftBox
            // 
            this.bottomLeftBox.AccessibleName = "QuadView";
            this.bottomLeftBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomLeftBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomLeftBox.Location = new System.Drawing.Point(3, 243);
            this.bottomLeftBox.Name = "bottomLeftBox";
            this.bottomLeftBox.Size = new System.Drawing.Size(314, 234);
           // this.bottomLeftBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bottomLeftBox.TabIndex = 2;
            this.bottomLeftBox.TabStop = false;
            this.bottomLeftBox.MouseEnter += new System.EventHandler(this.bottomLeftBox_MouseEnter);
            // 
            // bottomRightBox
            // 
            this.bottomRightBox.AccessibleName = "QuadView";
            this.bottomRightBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottomRightBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomRightBox.Location = new System.Drawing.Point(323, 243);
            this.bottomRightBox.Name = "bottomRightBox";
            this.bottomRightBox.Size = new System.Drawing.Size(314, 234);
            //this.bottomRightBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bottomRightBox.TabIndex = 3;
            this.bottomRightBox.TabStop = false;
            this.bottomRightBox.MouseEnter += new System.EventHandler(this.bottomRightBox_MouseEnter);
            // 
            // QuadView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.quadViewPanel);
            this.Name = "QuadView";
            this.Size = new System.Drawing.Size(640, 480);
            this.quadViewPanel.ResumeLayout(false);
            /*((System.ComponentModel.ISupportInitialize)(this.topLeftBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topRightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomLeftBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomRightBox)).EndInit();*/
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel quadViewPanel;
        public ZoomablePictureBoxCtrl.ZoomablePictureBox topLeftBox;
        public ZoomablePictureBoxCtrl.ZoomablePictureBox topRightBox;
        public ZoomablePictureBoxCtrl.ZoomablePictureBox bottomLeftBox;
        public ZoomablePictureBoxCtrl.ZoomablePictureBox bottomRightBox;
    }
}
