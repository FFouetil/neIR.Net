using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeImageAPI;
using System.Threading;

namespace RawMeat
{
    [Flags]
    public enum PreloadModes
    {
        Nothing = 0, Unprocessed = 1, Greyscale = 2, Split = 4, Debayered = 8,
        All = Unprocessed | Greyscale | Split | Debayered
    }

    [Flags]
    public enum ActivityFlags {
        Idle=0, None=Idle,
        LoadingUnprocessed=1, LoadingGreyscale=2, LoadingSplitChannels=4, LoadingDebayered=8,              
        LoadingAny = LoadingUnprocessed | LoadingGreyscale | LoadingSplitChannels | LoadingDebayered,
        FreeingUnprocessed = 16, FreeingGreyscale = 32, FreeingSplitChannels = 64, FreeingDebayered = 128,
        FreeingAny = FreeingUnprocessed | FreeingGreyscale | FreeingSplitChannels | FreeingDebayered,
        LoadingOrFreeingAny = LoadingAny | FreeingAny,
        ConvertingChannelsToBitmaps = 512,
        ReadingFile = 1024

    }

    class BayerImage
    {
        #region Fields

        public FileInfo RAWFileInfo { get; protected set; }
        public RawProcessingModes LastChannelProcessingModes { get; protected set; }


        #endregion

        #region Members & Properties

        public ActivityFlags RunningTasks { get; protected set; }

        /// <summary>Bayer-pattern bitmap</summary>
        protected FIBITMAP m_unprocessedBayer;
        public bool UnprocessedBayerIsLoaded { get { return (m_unprocessedBayer != null && !m_unprocessedBayer.IsNull); } }

        /// <summary>Bayer-pattern as greyscale bitmap</summary>
        protected FIBITMAP m_greyscaleBayer;
        public bool GreyscaleBayerIsLoaded { get { return (m_greyscaleBayer != null && !m_greyscaleBayer.IsNull); } }

        /// <summary>Split Bayer channels as bitmap array</summary>        
        protected FIBITMAP[] m_splitBayer = new FIBITMAP[4];        
        public bool SplitBayerIsLoaded { get {
            if ( m_splitBayer == null ) return false;            
            foreach (FIBITMAP fbmp in m_splitBayer)
                if (fbmp == null || fbmp.IsNull) return false;
            return true; }
        }
        
        private FIBITMAP m_deBayered;
        public bool DeBayeredIsLoaded { get { return (m_deBayered != null && !m_deBayered.IsNull); } }

        private Bitmap m_deBayeredBmp;
        public bool DeBayeredBMPIsLoaded { get { return (m_deBayeredBmp != null); } }

        public bool IsAccessingFile { get; protected set; }

        #endregion

        #region Constructors, Init and Destructors


        private BayerImage(FileInfo fileInfo)
        {
            RAWFileInfo = fileInfo;
            IsAccessingFile = false;
        }

        ~BayerImage()
        {
            UnloadFreeImageBitmaps();
        }

        /// <summary>
        /// Loads only unprocessed Bayer data from RAW file. Doesn't preload bitmaps for greyscale and split channels (faster, less memory used)
        /// </summary>
        /// <param name="fullPath">Path to RAW file</param>
        /// <returns>A BayerImage with Unprocessed data loaded</returns>
        public static BayerImage LoadFromFile(string fullPath)
        {
            FileInfo tempFileInfo = new FileInfo(fullPath);
            BayerImage bayer = null;

            if (tempFileInfo.Exists)
            {
                bayer = new BayerImage(tempFileInfo);
                bayer.RunningTasks |= ActivityFlags.ReadingFile; 
                if (!bayer.LoadUnprocessedFromFile(tempFileInfo.FullName))
                {
                    bayer = null;
                }
                bayer.RunningTasks &= ~ActivityFlags.ReadingFile; 
            }

            return bayer;
        }

        public static async Task<BayerImage> LoadFromFile_Async(string fullPath)
        {
            FileInfo tempFileInfo = new FileInfo(fullPath);
            BayerImage bayer = null;

            if (tempFileInfo.Exists)
            {
                bayer = new BayerImage(tempFileInfo);
                bayer.RunningTasks |= ActivityFlags.ReadingFile; 
                if (! await bayer.LoadUnprocessedFromFile_Async(tempFileInfo.FullName))
                {
                    bayer = null;
                }
                bayer.RunningTasks &= ~ActivityFlags.ReadingFile; 
            }

            return bayer;
        }


        public async static Task<BayerImage> LoadFromFile_Async(string fullPath, PreloadModes preloadModes, RawProcessingModes splitProcessingModes = RawProcessingModes.RawProcess_AutoScale)
        {
            BayerImage bayer = new BayerImage(new FileInfo(fullPath));

            if (preloadModes.HasFlag(PreloadModes.Debayered))
            {
                Debug.WriteLine("Starting preload: " + Enum.GetName(typeof(PreloadModes), PreloadModes.Debayered));
                bayer.LoadDebayered_FromLoadedFile_Async();
            }


            if (preloadModes != PreloadModes.Nothing)
            {
                Debug.WriteLine("Starting preload: " +Enum.GetName(typeof(PreloadModes), PreloadModes.Unprocessed));
                await bayer.LoadUnprocessedFromFile_Async(fullPath);
            }

            if (bayer.UnprocessedBayerIsLoaded) {
                if (preloadModes.HasFlag(PreloadModes.Greyscale))
                {
                    Debug.WriteLine("Starting preload: " + Enum.GetName(typeof(PreloadModes), PreloadModes.Greyscale));
                    bayer.LoadGreyscale_Async();
                }

                if (preloadModes.HasFlag(PreloadModes.Split))
                {
                    Debug.WriteLine("Starting preload: " + System.Enum.GetName(typeof(PreloadModes), PreloadModes.Split));
                    bayer.LoadSplitRaw_Async(splitProcessingModes);
                }
            }

            return bayer;
        }


        internal void UnloadFreeImageBitmaps()
        {
            //wait for all activities to stop before freeing memory
            if (!RunningTasks.Equals(ActivityFlags.None) )
                while ( !RunningTasks.Equals(ActivityFlags.None) ) { };

            if (RunningTasks.Equals(ActivityFlags.None))
            {
                FreeImage.Unload(m_unprocessedBayer);
                m_unprocessedBayer.SetNull();
                FreeImage.Unload(m_greyscaleBayer);
                m_greyscaleBayer.SetNull();
                FreeImage.Unload(m_deBayered);
                m_deBayered.SetNull();
                
            
                if ( m_deBayeredBmp != null ) m_deBayeredBmp.Dispose();

                if (m_splitBayer != null)
                    foreach (FIBITMAP channel in m_splitBayer)
                    {
                        FreeImage.Unload(channel);
                        channel.SetNull();                        
                    }

                m_splitBayer = null;

                //RawMeatNETWrapper.DeallocateChannels();
            }
        }
        #endregion

        #region Data access
        /// <summary>
        /// Loads a Bayer image from specified RAW file
        /// </summary>
        /// <param name="fullPath">Path to RAW file</param>
        /// <returns>True if successfully load, false if error occured</returns>
        internal bool LoadUnprocessedFromFile(string fullPath)
        {
            if (!UnprocessedBayerIsLoaded && !RunningTasks.HasFlag(ActivityFlags.LoadingUnprocessed) )
            {
                RunningTasks |= ActivityFlags.LoadingUnprocessed;
                try { m_unprocessedBayer = RawMeatNETWrapper.LoadUnprocessedRaw(fullPath); }
                catch (FileLoadException ex)
                {
                    string errMsg = "Error reading file '" + fullPath + "'";
                    Console.WriteLine(errMsg);
                }
                finally
                {
                    RunningTasks &= ~ActivityFlags.LoadingUnprocessed;
                }
            }

            return UnprocessedBayerIsLoaded;
        }

        async internal Task<bool> LoadUnprocessedFromFile_Async(string fullPath)
        {
            return await Task<bool>.Run( () => {
                return LoadUnprocessedFromFile(fullPath);
            } );
        }

        /// <summary>
        /// Loads in memory the 4 Bayer channels
        /// </summary>
        /// <param name="processingModes">Flags to enable/disable pre-processing features</param>
        public bool LoadSplitRaw(RawProcessingModes processingModes)
        {
            if (RunningTasks.HasFlag(ActivityFlags.LoadingSplitChannels))
            {
                while ( RunningTasks.HasFlag(ActivityFlags.LoadingSplitChannels) );
                return SplitBayerIsLoaded;
            }

            //if there's a RAW loaded in memory and it's not been split yet, do it
            if (UnprocessedBayerIsLoaded && !RunningTasks.HasFlag(ActivityFlags.LoadingSplitChannels)
                && (m_splitBayer[0] == null || LastChannelProcessingModes != processingModes))
            {
                RunningTasks |= ActivityFlags.LoadingSplitChannels;
                //unload previously loaded images
                //UnloadFreeImageBitmaps();
                bool autoScale = (processingModes & RawProcessingModes.RawProcess_AutoScale) != 0;
                m_splitBayer = RawMeatNETWrapper.SplitBayerChannels(m_unprocessedBayer, true, autoScale);
    
                LastChannelProcessingModes=processingModes;
                RunningTasks &= ~ActivityFlags.LoadingSplitChannels;                
            }
            
            return SplitBayerIsLoaded;
        }

        async public Task<bool> LoadSplitRaw_Async(RawProcessingModes processingModes)
        {
            return await Task<bool>.Run(() =>
            {
                return LoadSplitRaw(processingModes);
            });
        }

        public bool LoadGreyscale()
        {
            //if there's a RAW loaded in memory and it's not been split yet, do it
            if (!GreyscaleBayerIsLoaded && UnprocessedBayerIsLoaded && !RunningTasks.HasFlag(ActivityFlags.LoadingGreyscale) )
            {
                RunningTasks |= ActivityFlags.LoadingGreyscale;
                m_greyscaleBayer = FreeImage.ConvertToType(m_unprocessedBayer, FREE_IMAGE_TYPE.FIT_BITMAP, true);
                RunningTasks &= ~ActivityFlags.LoadingGreyscale; 
            }
            return GreyscaleBayerIsLoaded;
            
        }

        async protected Task<bool> LoadGreyscale_Async()
        {
            return await Task<bool>.Run(() =>
            {
                return LoadGreyscale();
            });
        }

        /// <summary>
        /// Experimental! Don't use
        /// </summary>
        /// <returns></returns>
        [Obsolete]        
        protected bool LoadDebayered_FromUnprocessed()
        {

            if (!DeBayeredIsLoaded && UnprocessedBayerIsLoaded)
            {

                FIBITMAP clone = FreeImage.Clone(m_unprocessedBayer); //cloned to avoid access violation during testing. check if obsolete
                /*FIMEMORY cloneData = FreeImage.OpenMemory(FreeImage.GetBits(clone),FreeImage.GetDIBSize(clone));
                m_deBayered = FreeImage.LoadFromMemory(FREE_IMAGE_FORMAT.FIF_RAW, cloneData, FREE_IMAGE_LOAD_FLAGS.RAW_DISPLAY);
                FreeImage.CloseMemory(cloneData);*/
                m_deBayered = FreeImage.ConvertFromRawBits(
                    FreeImage.GetBits(clone),
                    (int)FreeImage.GetWidth(clone),
                    (int)FreeImage.GetHeight(clone),
                    (int)FreeImage.GetPitch(clone),
                    FreeImage.GetBPP(clone),
                    FreeImage.GetRedMask(clone),
                    FreeImage.GetGreenMask(clone),
                    FreeImage.GetBlueMask(clone),
                    true
                    );

                //m_deBayered = FreeImage.ConvertToType(clone, FREE_IMAGE_TYPE.FIT_BITMAP, true);
                m_deBayered = FreeImage.ConvertColorDepth(clone,FREE_IMAGE_COLOR_DEPTH.FICD_32_BPP);
                FreeImage.Unload(clone);                               
            }

            return DeBayeredIsLoaded;

        }

        protected bool LoadDebayered_FromLoadedFile()
        {
            //if already loading, wait for the task to finish and return the result
            if ( RunningTasks.HasFlag(ActivityFlags.LoadingDebayered) ){
                while (RunningTasks.HasFlag(ActivityFlags.LoadingDebayered) );
                return DeBayeredBMPIsLoaded;
            }
                
            if (!DeBayeredBMPIsLoaded )
            {
                RunningTasks |= ActivityFlags.LoadingDebayered;
                try {
                    FREE_IMAGE_FORMAT format = FREE_IMAGE_FORMAT.FIF_RAW;
                    m_deBayeredBmp = FreeImage.LoadBitmap(RAWFileInfo.FullName, FREE_IMAGE_LOAD_FLAGS.RAW_DISPLAY, ref format);
                }
                catch (Exception ex) {
                    Console.WriteLine("Error loading deBayered file:\n" + ex.Message);
                    RunningTasks &= ~ActivityFlags.LoadingDebayered;
                    return false;
                }
                RunningTasks &= ~ActivityFlags.LoadingDebayered;
            }
            return DeBayeredBMPIsLoaded;
            
        }

        async protected Task<bool> LoadDebayered_FromLoadedFile_Async()
        {
            return await Task<bool>.Run(() =>
            {
                return LoadDebayered_FromLoadedFile();
            });
        }
        
        #endregion

        /// <summary>
        /// Converts the 4 FIBitmap channels to Windows Bitmap format for PictureBoxes
        /// </summary>
        /// <returns>A Windows Bitmap array containing the 4 split channels</returns>
        public Bitmap[] ChannelsToBitmaps()
        {

            RunningTasks |= ActivityFlags.ConvertingChannelsToBitmaps; 
            Bitmap[] chans = new Bitmap[4];

            for (int c = 0; c < 4; c++)
            {
                
                FIBITMAP clone = FreeImage.Clone(m_splitBayer[c]); //cloned to avoid access violation during testing. check if obsolete
                FIBITMAP converted = FreeImage.ConvertToType(clone, FREE_IMAGE_TYPE.FIT_BITMAP, true);

                chans[c] = FreeImage.GetBitmap(converted);

                FreeImage.Unload(clone);
                clone.SetNull();
                FreeImage.Unload(converted);
                converted.SetNull();
            }
            RunningTasks &= ~ActivityFlags.ConvertingChannelsToBitmaps; 

            return chans;
        }
        
        public Bitmap BayerToGreyscaleBitmap()
        {
            if (m_greyscaleBayer == null) LoadGreyscale();
            
            if (m_greyscaleBayer != null && !m_greyscaleBayer.IsNull)
            {
                return FreeImage.GetBitmap(m_greyscaleBayer);
            }

            return null;
        }

        /// <summary>
        /// Returns a typical DCRAW RAW->BitMap image
        /// </summary>
        /// <param name="fromUnprocessed">If true, use experimental alternate loading method (from memory instead of dcraw reprocessing). For testing purpose only.</param>
        /// <returns>An RGB Bitmap</returns>
        public Bitmap BayerToColorBitmap(bool fromUnprocessed=false)
        {
            //if debayering already in progress, wait for result
            if ( RunningTasks.HasFlag(ActivityFlags.LoadingDebayered) ){
                Console.WriteLine("Debayer from file -> Already running");
                while (RunningTasks.HasFlag(ActivityFlags.LoadingDebayered));                
            }

            if (DeBayeredIsLoaded) return m_deBayeredBmp;

            Bitmap result = null;            

            if ( fromUnprocessed ){
                if ( DeBayeredIsLoaded || LoadDebayered_FromUnprocessed() ){
                    result = FreeImage.GetBitmap(m_deBayered);                        
                }
                else throw new Exception("Debayer from unprocessed RAW failed");
            }
            else
            {

                if ( DeBayeredIsLoaded || LoadDebayered_FromLoadedFile() )
                {
                    result = m_deBayeredBmp;   
                }
                else if ( !RunningTasks.HasFlag(ActivityFlags.LoadingDebayered) )
                    throw new Exception("Debayer from file failed");
            }                    

            return result;   
        }
        
    }
}
