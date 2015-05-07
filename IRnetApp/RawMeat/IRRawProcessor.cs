using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using FreeImageAPI;

namespace RawMeat
{
    internal enum RawProcessingModes { RawProcess_Normal = 0, RawProcess_AutoScale = 1 };

    /*
    class IRRawProcessor
    {
        
        public static FIBITMAP RawChannelToFIBitmap
            (FIBITMAP unprocessedRaw, int subpixelIndex, RawProcessingModes processingMode=RawProcessingModes.RawProcess_Normal)
        {
            throw new NotImplementedException("RawChannelToBitmap function not implemented yet");
        }

        public static Bitmap RawChannelToBitmap
            (FIBITMAP unprocessedRaw, int subpixelIndex, RawProcessingModes processingMode=RawProcessingModes.RawProcess_Normal)
        {
            //call to RawChannelToFIBitmap and
            return FreeImage.GetBitmap(RawChannelToFIBitmap(unprocessedRaw, subpixelIndex, processingMode));
            throw new NotImplementedException("RawChannelToBitmap function not implemented yet");
            
        }
    }
    */
}
