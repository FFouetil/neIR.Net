// RawMeat.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "RawMeat.h"


using namespace std;
/*
// This is an example of an exported variable
RAWMEAT_API int nRawMeat=0;

// This is an example of an exported function.
RAWMEAT_API int fnRawMeat(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see RawMeat.h for the class definition
CRawMeat::CRawMeat()
{
	return;
}*/

//remembers if splitting was already done
static bool bayerIsSplit = false;
//pointer to channel array
static FIBITMAP* bayerChannels[4];

RAWMEAT_API FIBITMAP* RawMeat_LoadUnprocessedRaw(const char* fullpath){
	return FreeImage_Load(FIF_RAW, fullpath, RAW_UNPROCESSED);
}

RAWMEAT_API FIBITMAP* RawMeat_GetBayerChannel(FIBITMAP* bayerImage, int channelIndex){
	
	//if image bayer not split yet, split it 
	if (!bayerIsSplit)
		RawMeat_SplitBayerChannels(bayerImage);

	//make sure the requested channel index is valid
	const int c = channelIndex;
	bool validRange = (c >= 0 && c < 4);
	if (bayerChannels && validRange)
		return bayerChannels[c];
	
	if (!validRange){
		printf_s("Please use a channel value between 0 and 3\n");
		throw EXCEPTION_ARRAY_BOUNDS_EXCEEDED;
	}
	if (!bayerChannels){
		printf_s("Error: Bayer Channel array not initialized\n");
	}
	//default error -> return NULL
	return NULL;
}

RAWMEAT_API const char* RawMeat_BayerPatternToString(FIBITMAP* bayerImage, bool endsWithNewLine){
		
	FITAG* tag = NULL;
	FreeImage_GetMetadata(FIMD_COMMENTS, bayerImage, "Raw.BayerPattern", &tag);
	const char* bayerPattern=FreeImage_TagToString(FIMD_COMMENTS, tag);

	static string str = string(bayerPattern).substr(0,4);
	if (endsWithNewLine) str+="\n";

	return  str.c_str();		
}

//Processes Bayer bitmap to put each channel in memory, then returns the channel array
RAWMEAT_API FIBITMAP** RawMeat_SplitBayerChannels(FIBITMAP* bayerImage, bool forceProcessing, bool autoscale){

	//skips splitting if already done, unless forcing is enabled
	if (bayerIsSplit && !forceProcessing) return bayerChannels;

	if (forceProcessing && bayerIsSplit){
		//RawMeat_DeallocateChannels();
		bayerIsSplit = false;
	}

	//return NULL if bayerImage is NULL or has no pixel data to process
	if ( !bayerImage || !FreeImage_HasPixels(bayerImage)) return NULL;

	//dimensions of original RAW image
	const int width = (const int)FreeImage_GetWidth(bayerImage);
	const int height = (const int)FreeImage_GetHeight(bayerImage);

	//RAW pixel data
	BYTE* bits = FreeImage_GetBits(bayerImage);

	//value scaling
	unsigned valueScale = 1;
	//WIP! - tries to find best scaling ratio for current image.
	//note to self: try with float
	if (autoscale){

		unsigned max = 0;
		for (int p = 0; p<height*width; p++)
			if (max < ((WORD*)bits)[p])
				max = ((WORD*)bits)[p];

		unsigned dynamicRange = 16;
		
		if (max > 0 && max < 1 << 15)
		{
			//while (max <(unsigned)(1 << 15) && dynamicRange <= 15 ) dynamicRange++;
			valueScale = (1 << ( dynamicRange) ) / max;
			//printf("Valuescale %u - Range %d", valueScale, dynamicRange);		
		}

	}

	
	//allocates empty bitmaps to be copied to
	for (int i = 0; i < 4; i++)
	{
		
		bayerChannels[i] = FreeImage_AllocateT(FIT_UINT16, width / 2, height / 2);
	}

	//shortcuts to pixel data for each channel
	WORD *c0bits = (WORD*)FreeImage_GetBits(bayerChannels[0]);
	WORD *c1bits = (WORD*)FreeImage_GetBits(bayerChannels[1]);
	WORD *c2bits = (WORD*)FreeImage_GetBits(bayerChannels[2]);
	WORD *c3bits = (WORD*)FreeImage_GetBits(bayerChannels[3]);

	int pixIndex = 0;
	//each pass treats a Bayer quad
	for (int y = 0; y < height - 1; y += 2)
		for (int x = 0; x < width - 1; x += 2)		
		{
			int lineOffset = y*(width);
			int pixSrcIndex = lineOffset + x;

			c0bits[pixIndex] = ((WORD*)bits)[pixSrcIndex + width] * valueScale;
			c1bits[pixIndex] = ((WORD*)bits)[pixSrcIndex + width + 1] * valueScale;
			c2bits[pixIndex] = ((WORD*)bits)[pixSrcIndex] * valueScale;
			c3bits[pixIndex] = ((WORD*)bits)[pixSrcIndex + 1] * valueScale;

			pixIndex++;
		}

	bayerIsSplit = true;
	return bayerChannels;
}

RAWMEAT_API void RawMeat_testFunc_ExportChannelsToPNG
	(FIBITMAP* bayerImage, bool channel0, bool channel1 , bool channel2, bool channel3)
{
	printf(RawMeat_BayerPatternToString( bayerImage));

	if (channel0)
		FreeImage_Save(FIF_PNG, bayerChannels[0], "../../../vrac/OUT/outTest_c0.png"); printf("Channel#0 saved as PNG\n");
	if (channel1)
		FreeImage_Save(FIF_PNG, bayerChannels[1], "../../../vrac/OUT/outTest_c1.png"); printf("Channel#1 saved as PNG\n");
	if (channel2)
		FreeImage_Save(FIF_PNG, bayerChannels[2], "../../../vrac/OUT/outTest_c2.png"); printf("Channel#2 saved as PNG\n");
	if (channel3)
		FreeImage_Save(FIF_PNG, bayerChannels[3], "../../../vrac/OUT/outTest_c3.png"); printf("Channel#3 saved as PNG\n");

}

//WIP! - may cause issues when used
RAWMEAT_API bool RawMeat_DeallocateChannels(){
		
	for (int i = 0; i < 4; i++)
	{		
		FreeImage_Unload(bayerChannels[i]);
		delete bayerChannels[i];
	}
	
	return true;
}