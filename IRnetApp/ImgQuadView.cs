using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace IRnetApp
{
    public partial class ImgQuadView : Form
    {
        private FileInfo m_imageFile;
        private Bitmap m_channelTL;
        private Bitmap m_channelTR;
        private Bitmap m_channelBL;
        private Bitmap m_channelBR;

        private ImgQuadView()
        {
            InitializeComponent();
            
        }

        public ImgQuadView(Control parent):this()
        {
            Parent = parent;
        }

        public ImgQuadView(Control parent, FileInfo rawFile)
            : this(parent)
        {
            m_imageFile = rawFile;
        }

        private void topLeftBox_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {            
            LoadTopLeftBitmap();
            LoadTopRightBitmap();
            LoadBottomLeftBitmap();
            LoadBottomRightBitmap();

            Invalidate();
        }

        private void LoadBottomRightBitmap()
        {
            throw new NotImplementedException();
        }

        private void LoadBottomLeftBitmap()
        {
            throw new NotImplementedException();
        }

        private void LoadTopRightBitmap()
        {
            throw new NotImplementedException();
        }

        private void LoadTopLeftBitmap()
        {
            throw new NotImplementedException();
        }

        public void SetTopLeftBitmap(Bitmap topleft)
        {
            m_channelTL = topleft;
        }

        public void SetTopRightBitmap(Bitmap topright)
        {
            m_channelTR = topright;
        }
        public void SetBottomLeftBitmap(Bitmap bottomleft)
        {
            m_channelBL = bottomleft;
        }
        public void SetBottomRightBitmap(Bitmap bottomright)
        {
            m_channelBR = bottomright;
        }

        private void topLeftBox_Click(object sender, EventArgs e)
        {

        }

    }
}
