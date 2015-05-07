using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using FreeImageAPI;


namespace RawMeat
{

    class SafeNativeMethods
    {

        [DllImport("RawMeat", CharSet = CharSet.Ansi, EntryPoint = "RawMeat_LoadUnprocessedRaw", CallingConvention = CallingConvention.Cdecl)]
        internal static extern FIBITMAP LoadUnprocessedRaw(string fullpath);

        [DllImport("RawMeat", EntryPoint = "RawMeat_SplitBayerChannels", CallingConvention = CallingConvention.Cdecl)]
        //[return: MarshalAs(UnmanagedType.ByValArray,SizeConst=4,ArraySubType=UnmanagedType.LPStruct)]
        internal static extern IntPtr SplitBayerChannelsPtr(FIBITMAP bayerImage, bool forceProcessing = false, bool autoscale = true);
        /// <summary>
        /// Returns a 4-slots array containing a 16bpp greyscale image for each Bayer channel
        /// </summary>
        /// <param name="bayerImage">Source RAW Bayer-pattern image</param>
        /// <param name="forceProcessing">Force processing the image even if one is already in memory. (can fix access violation issues)</param>
        /// <param name="autoscale"></param>
        /// <returns>The 4 channels in an array</returns>
        internal static FIBITMAP[] SplitBayerChannels(FIBITMAP bayerImage, bool forceProcessing = false, bool autoscale = true)
        {
            FIBITMAP clone = FreeImage.Clone(bayerImage);
            IntPtr ptrs = SplitBayerChannelsPtr(clone, forceProcessing, autoscale);
            int ptrSize = Marshal.SizeOf(typeof(FIBITMAP));

            FIBITMAP[] chans = new FIBITMAP[4];

            for (int i=0; i<4; i++){
                chans[i] = (FIBITMAP)Marshal.PtrToStructure(ptrs, typeof(FIBITMAP));
                ptrs=IntPtr.Add(ptrs, ptrSize);
            }

            FreeImage.Unload(clone);
            
            return chans;
        }

        internal async static Task<FIBITMAP[]> SplitBayerChannels_Async(FIBITMAP bayerImage, bool forceProcessing = false, bool autoscale = true)
        {
            return await Task<FIBITMAP[]>.Run(  () =>
            {
                return SplitBayerChannels( bayerImage, forceProcessing,  autoscale);
            });
        }

        [DllImport("RawMeat", EntryPoint = "RawMeat_GetBayerChannel", CallingConvention = CallingConvention.Cdecl)]
        internal static extern FIBITMAP GetBayerChannel(FIBITMAP bayerImage, int channelIndex);

        [DllImport("RawMeat", EntryPoint = "RawMeat_DeallocateChannels", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DeallocateChannels();
    }
}
