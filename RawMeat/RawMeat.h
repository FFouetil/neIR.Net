// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the RAWMEAT_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// RAWMEAT_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef RAWMEAT_EXPORTS
#define RAWMEAT_API __declspec(dllexport)
#else
#define RAWMEAT_API __declspec(dllimport)
#endif
#include <stdio.h>
#include <string>

#include "FreeImage.h"

/* VS samples
// This class is exported from the RawMeat.dll
class RAWMEAT_API CRawMeat {
public:
	CRawMeat(void);
	// TODO: add your methods here.
};

extern RAWMEAT_API int nRawMeat;

extern RAWMEAT_API int fnRawMeat(void);
*/

extern "C"{
	//Shortcut for loading an un-deBayered RAW file. Alias for FreeImage 16bpp unprocessed RAW loading
	RAWMEAT_API FIBITMAP* RawMeat_LoadUnprocessedRaw(const char* fullpath);
	//Returns a 4-slots array of FIBITMAPs, one for each channel. 1st call is slower (processing).
	//ForceProcessing makes it ignore that an image is already loaded: you're responsible for deallocating the previous one.
	RAWMEAT_API FIBITMAP** RawMeat_SplitBayerChannels(FIBITMAP* bayerImage, bool forceProcessing = false, bool autoscale = true);
	//! Untested ! Returns a FIBITMAP corresponding to the specified channel. Automatically calls the split function if not yet processed
	RAWMEAT_API FIBITMAP* RawMeat_GetBayerChannel(FIBITMAP* bayerImage, int channelIndex);
	//Returns a 4-character string describing the detected Bayer pattern
	RAWMEAT_API const char* RawMeat_BayerPatternToString(FIBITMAP* bayerImage, bool endsWithNewLine = true);
	//Deallocates the memory used by the 4 cached channels - WIP: can cause memory errors
	RAWMEAT_API bool RawMeat_DeallocateChannels();


	//--------- test calls
	//Exports selected channels to 16bit grayscale PNG
	extern RAWMEAT_API void RawMeat_testFunc_ExportChannelsToPNG(FIBITMAP* bayerImage, bool channel0 = true, bool channel1 = true, bool channel2 = true, bool channel3 = true);

}
