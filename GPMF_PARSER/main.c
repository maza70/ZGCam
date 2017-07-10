#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <stdint.h>
#include <windows.h>
#include <math.h>


#include "GPMF_parser.h"
#include "GPMF_mp4reader.h"


uint32_t KEY_ID_ACCL = MAKEID('A', 'C', 'C', 'L');
uint32_t KEY_ID_GPSF = MAKEID('G', 'P', 'S', 'F');
uint32_t totalsamples = 0;

int zprintf(const char* format, ...) {
	static char s_printf_buf[1024];
	va_list args;
	va_start(args, format);
	_vsnprintf_s(s_printf_buf, sizeof(s_printf_buf), 1024, format, args);
	va_end(args);
	printf(s_printf_buf);
	printf("\n");
	return 0;
}
int zprintError(const char* format, ...) {

	printf("<ERROR>");
	static char s_printf_buf[1024];
	va_list args;
	va_start(args, format);
	_vsnprintf_s(s_printf_buf, sizeof(s_printf_buf), 1024, format, args);
	va_end(args);
	printf(s_printf_buf);
	printf("</ERROR>\n");
	return 0;
}


void print_GPS_18hz(GPMF_stream * pMetastream, uint32_t index) {

	uint32_t GPSLOCK = 0; // 0= no Lock, 2 = 2D Lock , 3= 3DLock
	uint16_t GPS_PDOP = 0; // < 300 Good GPS
	uint32_t samples = GPMF_Repeat(pMetastream);
	uint32_t elements = GPMF_ElementsInStruct(pMetastream);
	uint32_t buffersize = samples * elements * sizeof(float);
	GPMF_stream find_stream;
	float *ptr, *tmpbuffer = malloc(buffersize);

	if (tmpbuffer && samples)
	{
		uint32_t i;

		GPMF_CopyState(pMetastream, &find_stream);
		uint32_t GPSF = MAKEID('G', 'P', 'S', 'F');
		if (GPMF_OK == GPMF_FindPrev(&find_stream, GPSF, GPMF_CURRENT_LEVEL)) {
			uint32_t *data = (uint32_t *)GPMF_RawData(&find_stream);
			GPSLOCK = (int32_t)BYTESWAP32(*data);

		}
		else {
			zprintError("No GPSF found");
		}



		GPMF_CopyState(pMetastream, &find_stream);
		uint32_t GPSP = MAKEID('G', 'P', 'S', 'P');
		if (GPMF_OK == GPMF_FindPrev(&find_stream, GPSP, GPMF_CURRENT_LEVEL)) {
			uint16_t *data = (uint16_t *)GPMF_RawData(&find_stream);
			GPS_PDOP = (uint16_t)BYTESWAP16(*data);
		}
		else 
		{
			zprintError("No GPSP found");
		}

		//GPMF_FormattedData(ms, tmpbuffer, buffersize, 0, samples); // Output data in LittleEnd, but no scale
		GPMF_ScaledData(pMetastream, tmpbuffer, buffersize, 0, samples, GPMF_TYPE_FLOAT);  //Output scaled data as floats

		ptr = tmpbuffer;
		int counter = 0;
		float latitude = 0.0f;  //deg
		float longitude = 0.0f;  //deg
		float altitude = 0.0f;    //Meter
		float ground_speed = 0.0f; //m/s
		float ground_speed_3d = 0.0f; //m/s



		for (i = 0; i < samples; i++)
		{
			latitude = *ptr++;  //deg
			longitude = *ptr++;  //deg
								 //Add for avg
			altitude = *ptr++;    //Meter
			ground_speed = *ptr++; //m/s
			ground_speed_3d = *ptr++; //m/s

			zprintf("<GPS n='%d' lk='%d' pdop='%03d' lat='%.8f' lon='%.8f' alt='%.1f' gs='%.1f' gs3d='%.1f'/>",
				totalsamples,
				GPSLOCK,
				GPS_PDOP,
				latitude,
				longitude,
				altitude,
				(ground_speed *60.0*60.0) / 1000.0,
				(ground_speed_3d*60.0*60.0) / 1000.0);
			totalsamples++;
		}
		free(tmpbuffer);
	}

}



int main(int argc, char *argv[])
{

	int32_t ret = GPMF_OK;
	GPMF_stream metadata_stream, *ms = &metadata_stream;
	float metadatalength;
	uint32_t *payload = NULL; //buffer to store GPMF samples from the MP4.
			
	if (argc != 2)
	{
		printf("usage: %s <file_with_GPMF>\n", argv[0]);
		return -1;
	}
	zprintf("<?xml version='1.0' encoding='UTF-8'?>");
	zprintf("<GOPRO5>");
	metadatalength = OpenGPMFSource(argv[1]);
	if (metadatalength > 0.0)
	{
		uint32_t index, payloads = GetNumberGPMFPayloads();
		for (index = 0; index < payloads; index++)
		{
			uint32_t payloadsize = GetGPMFPayloadSize(index);
			float in = 0.0, out = 0.0; //times
			payload = GetGPMFPayload(payload, index);
			if (payload == NULL)
				goto cleanup;
			ret = GetGPMFPayloadTime(index, &in, &out);
			if (ret != GPMF_OK)
				goto cleanup;

			ret = GPMF_Init(ms, payload, payloadsize);
			if (ret != GPMF_OK)
				goto cleanup;

			GPMF_ResetState(ms);
			// Find GPS values and return scaled floats. 
			if (GPMF_OK == GPMF_FindNext(ms, STR2FOURCC("GPS5"), GPMF_RECURSE_LEVELS) || //GoPro Hero5 GPS
				GPMF_OK == GPMF_FindNext(ms, STR2FOURCC("GPRI"), GPMF_RECURSE_LEVELS))   //GoPro Karma GPS
			{
				print_GPS_18hz(ms, index);
			}
			GPMF_ResetState(ms);
		}

	cleanup:
		if (payload) FreeGPMFPayload(payload); 
		  payload = NULL;
		CloseGPMFSource();
		zprintf("<SUMMARY totalsamples='%d' frequence_hz='18'/>", totalsamples);
	}
	else {
		zprintError("no data");
	}
	zprintf("</GOPRO5>");
	return ret;
}
