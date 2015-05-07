using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

using FreeImageAPI;
using RawMeat;
using System.Threading.Tasks;
using System.Threading;

namespace neIR
{

    public partial class MainWindow : Form
    {

        #region Members
        //private string m_filetypeFilter;        
        private string m_commonTypesFilter = "Common Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF";
        private string m_rawFilterExtensions = ".RAW .DNG .ARW .ORF .CR2 .NRW .RAF .NEF";
        private string m_rawFilter = "RAW Image Files"; //will be concatenated with extension filters
        //private string m_rawSplit_CacheFolder = "/4channels";
        private string m_rawSplit_Parameters = "";
        
        /*4channnels - splits RAW-file into four separate 16-bit grayscale TIFFs (per RAW channel).
        Command line switches:

        -s N selects N-th image from RAW with multiple images
        -g gamma correction (gamma 2.2)
        -A values autoscale by auto-calculated integer factor
        -B turn off black subtraction
        -N no RAW curve
        */

        private int m_rawSplit_ImageIndex = 0;
        private bool m_rawSplit_GammaCorrection = false;
        private bool m_rawSplit_AutoScaleValues = false;
        private bool m_rawSplit_NoBlackSubstraction = false;
        private bool m_rawSplit_NoRAWCurve = false;

        //private bool m_rawLoaded = false;
        //private bool m_rawSplit_processed = false;
        //stores the bitmaps for each channel (usually GRBG)

        private BayerImage m_rawBayer;

        private Bitmap m_bayerImage = null;
        private Bitmap[] m_splitChannelsImages = new Bitmap[4];
        private Bitmap m_rgbImage=null;

        private Bitmap m_dummyForLoading = new Bitmap(1, 1);
        
        private FileInfo m_curFileInfo;
        private bool m_curFile_locked = false;

        private enum DisplayMode { Debayered, Greyscale, Split};
        private DisplayMode m_displayMode = DisplayMode.Split;

        #endregion
        // private Accord.Extensions.Imaging.IplImage;
        // private Accord.Imaging.Converters.ImageToMatrix ddd;

        #region Constructors and Inits
        public MainWindow()
        {
            
            InitializeComponent();
            m_rawFilter = Helpers.CreateFileTypeFilter(m_rawFilter, m_rawFilterExtensions, " ");


            this.displayStrip_showAsGreyscale.Enabled = true;
            this.displayStrip_showDebayered.Enabled = true;
            this.displayStrip_showSplitChannels.Enabled = true;

            
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = false;
            
        }
        #endregion

        #region File Management
        /// <summary>
        /// Check if last file selected is a RAW image. For now, verifies using only file extension.
        /// Dev Note: Add FreeImage deep checking for better fool-proofing
        /// </summary>
        /// <returns>True if validated as RAW</returns>
        private bool FileIsRaw()
        {
            string[] exts = m_rawFilterExtensions.Split(new string[]{" "},StringSplitOptions.RemoveEmptyEntries);
            string fileExt = m_curFileInfo.Extension.ToLower();
            foreach (string ext in exts)
                if (fileExt.Equals(ext.ToLower())) return true;

            return false;
        }

        /// <summary>
        /// Check if last file selected is a supported common image type. For now, verifies using only file extension.
        /// </summary>
        /// <returns>True if validated as supported format</returns>
        private bool FileIsCommonType()
        {
            return
            m_curFileInfo.Extension.ToLower().Equals(".png")
            || m_curFileInfo.Extension.ToLower().Equals(".jpg")
            || m_curFileInfo.Extension.ToLower().Equals(".jpeg")
            || m_curFileInfo.Extension.ToLower().Equals(".bmp")
            || m_curFileInfo.Extension.ToLower().Equals(".tiff")
            || m_curFileInfo.Extension.ToLower().Equals(".tif")
            ; 
        }
        #endregion

        #region Image (Un)Loading

        /// <summary>
        /// Split channels of currently selected Bayer image 
        /// </summary>
        /// <param name="processingModes">Flags to enable/disable pre-processing features</param>
        internal async Task SplitRaw_Async(RawProcessingModes processingModes)
        {
            //if there's a RAW loaded in memory and it's not been split yet, do it
            if (m_rawBayer != null && !m_rawBayer.SplitBayerIsLoaded )
            {
                string loadMsg = "Splitting Bayer channels...";
                SetStatusMessage(loadMsg, Color.Black);

                if (!m_rawBayer.RunningTasks.HasFlag(ActivityFlags.LoadingSplitChannels))
                {
                    if (await m_rawBayer.LoadSplitRaw_Async(RawProcessingModes.RawProcess_AutoScale))
                    {
                        SetStatusMessage(loadMsg + " Done!", Color.Black);
                        m_splitChannelsImages = m_rawBayer.ChannelsToBitmaps();

                    }
                    else
                        SetStatusMessage(loadMsg + " Error!", Color.Red);

                }

            }
            else if (m_rawBayer == null) //warns in status bar if no RAW is loaded
            {
                SetStatusMessage("Can't split RAW file: none loaded", Color.Red);
            }

            Invalidate();
        }
        
        /// <summary>
        /// [Obsolete?] Gets string containing command-line parameters for 4channels.exe
        /// </summary>
        /// <returns></returns>
        private string GetRawSplitterParams()
        {
            string rawparams = m_rawSplit_Parameters + "";
            if (m_rawSplit_GammaCorrection) rawparams += " -g";
            if (m_rawSplit_AutoScaleValues) rawparams += " -A";
            if (m_rawSplit_NoBlackSubstraction) rawparams += " -B";
            if (m_rawSplit_NoRAWCurve) rawparams += " -N";
            if (m_rawSplit_ImageIndex > 0) rawparams += " -s " + m_rawSplit_ImageIndex;

            return rawparams + " ";
        }        

        private void UnloadFreeImageBitmaps(){
            if (m_rawBayer != null)
            {
                m_rawBayer.UnloadFreeImageBitmaps();
                m_rawBayer = null;
            }                
        }

        private void UnloadWindowsBitmaps()
        {
            m_rgbImage=null;
            m_bayerImage=null;
            for (int b=0; b<m_splitChannelsImages.Length;b++)
                m_splitChannelsImages[b] = null;
        }
        #endregion

        #region Display

        internal void SetStatusMessage(string message, Color color)
        {
            this.StatusMessage.ForeColor = color;
            this.StatusMessage.Text = message;
        }

        internal void SetStatusMessage(string message)
        {
            SetStatusMessage(message, Color.Black);
        }


        /// <summary>
        /// Switches to the views corresponding to the requested DisplayMode
        /// </summary>
        /// <param name="displayMode">The requested display mode</param>
        private void SwitchView(DisplayMode displayMode)
        {
            m_displayMode = displayMode;
            //single view
            if (displayMode == DisplayMode.Debayered || displayMode == DisplayMode.Greyscale)
            {

                m_bigPictureBox.Enabled = true;
                m_bigPictureBox.Visible = true;
                quadView.Visible = false;
                quadView.Enabled = false;
                m_bigPictureBox.BringToFront();
                m_bigPictureBox.Focus();
            }
            //quad view
            else if (displayMode == DisplayMode.Split)
            {

                quadView.Enabled = true;
                quadView.Visible = true;                
                m_bigPictureBox.Enabled = false;
                m_bigPictureBox.Visible = false;
                quadView.BringToFront();
                quadView.Focus();
            }
            //request focus again (because it's lost after switching)
            this.Activate();        
            Invalidate();
        }

        
        private void ClearPictureBoxes(bool unloadResources)
        {
            if (unloadResources)
            {
                UnloadWindowsBitmaps();
                UnloadFreeImageBitmaps();                
            }

            m_bigPictureBox.Image = null;
            quadView.topLeftBox.Image = null;
            quadView.topRightBox.Image = null;
            quadView.bottomLeftBox.Image = null;
            quadView.bottomRightBox.Image = null;
            Invalidate();
        }


        private void DisplayCommon()
        {
            m_bigPictureBox.Image = m_rgbImage;
            SwitchView(DisplayMode.Debayered);
            Invalidate();
        }

        async private void LoadDebayered_Async()
        {
            string loadMsg = "Loading deBayered image...";

            if ( m_rgbImage == null && !m_rawBayer.RunningTasks.HasFlag(ActivityFlags.LoadingDebayered) )
            {
                m_rgbImage = m_dummyForLoading; //dummy to prevent multiple calls while it's loading
                
                SetStatusMessage(loadMsg+ "it might take a while.");

                m_rgbImage = await Task<Bitmap>.Run(() =>
                {
                    try
                    {                        
                        /*
                        FREE_IMAGE_FORMAT format = FREE_IMAGE_FORMAT.FIF_RAW;
                        while (m_curFileInfo.LastAccessTimeUtc.Millisecond > DateTime.UtcNow.Millisecond - 10) { Console.WriteLine("wait access"); };
                        return FreeImage.LoadBitmap(m_curFileInfo.FullName, FREE_IMAGE_LOAD_FLAGS.RAW_DISPLAY, ref format);*/
                        
                        return m_rawBayer.BayerToColorBitmap();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(loadMsg + " Exception!");
                        return null;
                    }

                });

                if (m_rgbImage != null)
                    SetStatusMessage(loadMsg + " Done!");
                else
                    SetStatusMessage(loadMsg + " Error!", Color.Red);

                if (m_displayMode == DisplayMode.Debayered)
                    m_bigPictureBox.Image = m_rgbImage;

            }
            else
            {
                Console.WriteLine("Bayer image already loaded or loading.");
            }

            Invalidate();

        }

        async private void DisplayDebayered_Async()
        {
            string loadMsg = "Loading deBayered image...";
            SwitchView(DisplayMode.Debayered);

            if (m_rawBayer != null){

                if (m_rgbImage == null )
                {
                    m_rgbImage = m_dummyForLoading;
                    m_rgbImage = await Task<Bitmap>.Run(() =>
                    {
                        return m_rawBayer.BayerToColorBitmap();
                    });
                }
                else if ( m_rawBayer.RunningTasks.HasFlag(ActivityFlags.LoadingDebayered) )
                {
                    SetStatusMessage(loadMsg + " still in progress.");
                }
            }

  
            m_bigPictureBox.Image = m_rgbImage;
            Invalidate();
            
        }


        async private void DisplayAsGreyscale_Async()
        {
            SwitchView(DisplayMode.Greyscale);
            if (m_bayerImage == null && m_rawBayer != null)
            {
                m_bayerImage = m_dummyForLoading; //dummy to prevent multiple calls while it's loading
                m_bayerImage = await Task<Bitmap>.Run(() =>
                {
                    while (m_rawBayer.RunningTasks.HasFlag(ActivityFlags.LoadingGreyscale) );
                    return  m_rawBayer.BayerToGreyscaleBitmap();
                });
                
            }
            m_bigPictureBox.Image = m_bayerImage;
            Invalidate();
        }


        async private void DisplaySplitChannels_Async()
        {
            SwitchView(DisplayMode.Split);
            if (m_rawBayer != null)
            {
                if (!m_rawBayer.SplitBayerIsLoaded && !m_rawBayer.RunningTasks.HasFlag(ActivityFlags.LoadingSplitChannels))
                {
                    m_splitChannelsImages[0] = new Bitmap(8, 8); //dummy to prevent multiple calls while it's loading
                    await SplitRaw_Async(RawProcessingModes.RawProcess_Normal);

                    //SetStatusMessage("Can't display RAW file channels: not processed", Color.Red);
                }
                                
                //wait for and of loading if called multiple times
                while ( m_rawBayer.RunningTasks.HasFlag(ActivityFlags.LoadingSplitChannels) );

                if (m_rawBayer.SplitBayerIsLoaded)
                {
                    if (m_splitChannelsImages[m_splitChannelsImages.Length-1] == null)
                        m_splitChannelsImages = m_rawBayer.ChannelsToBitmaps();

                    this.quadView.topLeftBox.Image = m_splitChannelsImages[0];
                    this.quadView.topRightBox.Image = m_splitChannelsImages[1];
                    this.quadView.bottomLeftBox.Image = m_splitChannelsImages[2];
                    this.quadView.bottomRightBox.Image = m_splitChannelsImages[3];
                }
                Console.WriteLine("SplitBayerIsLoaded: " + m_rawBayer.SplitBayerIsLoaded);
                
            }

            Invalidate();
        }

        private void SetDisplayMode(DisplayMode displayMode)
        {
            m_displayMode = displayMode;
            switch (displayMode)
            {
                case DisplayMode.Debayered: DisplayDebayered_Async(); break;
                case DisplayMode.Greyscale: DisplayAsGreyscale_Async(); break;
                case DisplayMode.Split: DisplaySplitChannels_Async(); break;
                default: DisplaySplitChannels_Async(); break;
            }
            
        }
        #endregion

        #region GUI Events


        /// <summary>
        /// Opens a file dialog to pick an image to load, then displays it.
        /// </summary>
        private async void fileMenu_open_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Filter = m_rawFilter + "|" + m_commonTypesFilter;

            if (fileOpen.ShowDialog(this) == DialogResult.OK)
            {
                m_curFileInfo = new FileInfo(fileOpen.FileName);
                m_curFileInfo.IsReadOnly = true;
                
                SetStatusMessage("Loading " + m_curFileInfo.FullName);

                ClearPictureBoxes(true);

                try
                {
                    if (FileIsCommonType())
                    {
                        DisplayCommon();
                    }
                    else if (FileIsRaw())
                    {
                        m_curFile_locked = true;
                        m_rawBayer = await BayerImage.LoadFromFile_Async(m_curFileInfo.FullName,PreloadModes.All);
                        Invalidate();
                        //LoadDebayered_Async();
                        m_curFile_locked = false;

                        this.displayStrip_showAsGreyscale.Enabled = true;
                        this.displayStrip_showDebayered.Enabled = true;
                        this.displayStrip_showSplitChannels.Enabled = true;
                    }
                    else
                    {
                        throw new FileLoadException("File format not supported");
                    }

                }
                catch (FileLoadException )
                {
                    SetStatusMessage("Failed opening file " + fileOpen.FileName, Color.Red);
                    Console.WriteLine("Failed opening file " + fileOpen.FileName);

                }

                SetDisplayMode(m_displayMode);
                this.Activate();

                SetStatusMessage("Loaded " + m_curFileInfo.FullName);
                Invalidate();
            }
        }


        private void displayStrip_showDebayered_Click(object sender, EventArgs e)
        {            
            DisplayDebayered_Async();
        }

        private void displayStrip_showAsGreyscale_Click(object sender, EventArgs e)
        {
           
            DisplayAsGreyscale_Async();
        }        

        private void displayStrip_showSplitChannels_Click(object sender, EventArgs e)
        {            
            DisplaySplitChannels_Async();
        }

        private void toolStrip_clearButton_Click(object sender, EventArgs e)
        {
            UnloadFreeImageBitmaps();
            UnloadWindowsBitmaps();
            ClearPictureBoxes(false);            
        }
        #endregion


    }
}
