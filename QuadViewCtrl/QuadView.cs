using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZoomablePictureBoxCtrl;


namespace neIR
{
    public partial class QuadView : UserControl
    {
        private ZoomablePictureBox m_enteredPicBox;

        public QuadView()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.ResizeRedraw = false;
        }


        public void topLeftBox_MouseEnter(object sender, EventArgs e)
        {
            m_enteredPicBox = topLeftBox;
        }

        public void topRightBox_MouseEnter(object sender, EventArgs e)
        {
            m_enteredPicBox = topRightBox;
        }

        public void bottomLeftBox_MouseEnter(object sender, EventArgs e)
        {
            m_enteredPicBox = bottomLeftBox;
        }

        public void bottomRightBox_MouseEnter(object sender, EventArgs e)
        {
            m_enteredPicBox = bottomLeftBox;
        }

        public void quadViewPanel_MouseLeave(object sender, EventArgs e)
        {
            m_enteredPicBox = null;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (m_enteredPicBox != null )
                ZoomPicture(m_enteredPicBox, e);

            Invalidate();
        }

        public void ZoomPicture(ZoomablePictureBox pic, MouseEventArgs e)
        {
            
            {
                Console.WriteLine("Scroll delta: " + e.Delta);
                if (e.Delta > 0)
                {
                    //pic.Image.
                    
                    pic.Width *= 2;
                    pic.Height *= 2;
                }               
                else if (e.Delta < 0)
                {
                    pic.Width /= 2;
                    pic.Height /= 2;
                }
            }
            //throw new NotImplementedException();
        }




    }
}
