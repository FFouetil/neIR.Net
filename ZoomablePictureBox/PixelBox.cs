using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZoomablePictureBoxCtrl
{
    /// <summary>A generic PictureBox that supports changing interpolation modes</summary>
    public class PixelBox : PictureBox
    {
        /// <summary>Gets or sets interpolation mode</summary>
        public InterpolationMode InterpolationMode { get; set; }

        public PixelBox():base()
        {
            InterpolationMode = InterpolationMode.Default;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(pe);
        }
    }
}
