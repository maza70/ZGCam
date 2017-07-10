/*! @file GPMF_demo.c
*
*  @brief Demo to extract GPMF from an MP4
*
*  @version 1.0.0
*
*  (C) Copyright 2017 GoPro Inc (http://gopro.com/).
*
*  Licensed under the Apache License, Version 2.0 (the "License");
*  you may not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing, software
*  distributed under the License is distributed on an "AS IS" BASIS,
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*  See the License for the specific language governing permissions and
*  limitations under the License.
*  URL doku https://github.com/gopro/gpmf-parser
*/

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
uint32_t totalsamples= 0;

extern void PrintGPMF(GPMF_stream *ms);

int print_log(const char* format, ...)
{
	static char s_printf_buf[1024];
	va_list args;
	va_start(args, format);
	_vsnprintf(s_printf_buf, sizeof(s_printf_buf), format, args);
	va_end(args);
	OutputDebugStringA(s_printf_buf);
	return 0;
}
int print_Info(const char* format, ...) {
	return 0;
}


int printf_GPS(const char* format, ...) {
	static char s_printf_buf[1024];
	va_list args;
	va_start(args, format);
	_vsnprintf(s_printf_buf, sizeof(s_printf_buf), format, args);
	va_end(args);
	printf(s_printf_buf);
	return 0;
}

int printf_ACCL(const char* format, ...) {
	static char s_printf_buf[1024];
	va_list args;
	va_start(args, format);
	_vsnprintf(s_printf_buf, sizeof(s_printf_buf), format, args);
	va_end(args);
	printf("ACCL: %s", s_printf_buf);
	return 0;
}



double calc_g(double pMeterPerSecondQ) {
	
	double g;
	double mps;
	double multi = 1.0;

	if (pMeterPerSecondQ < 0.0) {
		multi = -1.0;
	}
	
	mps = sqrt(pMeterPerSecondQ * multi);
	g = (mps / 9.80665) * multi;

	return g;

}



uint32_t entrycounter = 0;



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
			print_log("FOUND GPSF =%d", GPSLOCK);
		}

		GPMF_CopyState(pMetastream, &find_stream);
		uint32_t GPSP = MAKEID('G', 'P', 'S', 'P');
		if (GPMF_OK == GPMF_FindPrev(&find_stream, GPSP, GPMF_CURRENT_LEVEL)) {
			uint16_t *data = (uint16_t *)GPMF_RawData(&find_stream);
			GPS_PDOP = (uint16_t)BYTESWAP16(*data);
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
		
			printf_GPS("<GPS n='%d' lk='%d' pdop='%03d' lat='%.8f' lon='%.8f' alt='%.1f' gs='%.1f' gs3d='%.1f'/>\n", 
							  	  totalsamples,
				                  GPSLOCK, 
				                  GPS_PDOP, 
				                  latitude, 
				                  longitude, 
				                  altitude , 
				                  (ground_speed *60.0*60.0) / 1000.0, 
				                  (ground_speed_3d*60.0*60.0) / 1000.0);
			totalsamples++;
		}
		free(tmpbuffer);
	}

}


/*
void print_GPS(GPMF_stream * pMetastream, uint32_t index) {

	uint32_t GPSLOCK = 0; // 0= no Lock, 2 = 2D Lock , 3= 3DLock
	uint16_t GPS_PDOP= 0; // < 300 Good GPS

	float startSec = 0.0, endSec = 0.0; //times
	
	uint32_t samples = GPMF_Repeat(pMetastream);
	totalsamples += samples;
	uint32_t elements = GPMF_ElementsInStruct(pMetastream);


	uint32_t buffersize = samples * elements * sizeof(float);
	GPMF_stream find_stream;
	float *ptr, *tmpbuffer = malloc(buffersize);
	char units[10][6] = { "" };
	uint32_t unit_samples = 1;

	if (GPMF_OK != GetGPMFPayloadTime(index, &startSec, &endSec)) {
		return;
	}

	



	if (tmpbuffer && samples)
	{
		uint32_t i, j;

		GPMF_CopyState(pMetastream, &find_stream);
		uint32_t GPSF = MAKEID('G', 'P', 'S', 'F');
		if (GPMF_OK == GPMF_FindPrev(&find_stream, GPSF, GPMF_CURRENT_LEVEL)) {
			uint32_t *data = (uint32_t *)GPMF_RawData(&find_stream);
			GPSLOCK = (int32_t)BYTESWAP32(*data);
			print_log("FOUND GPSF =%d", GPSLOCK);
		}

		GPMF_CopyState(pMetastream, &find_stream);
		uint32_t GPSP = MAKEID('G', 'P', 'S', 'P');
		if (GPMF_OK == GPMF_FindPrev(&find_stream, GPSP, GPMF_CURRENT_LEVEL)) {
			uint16_t *data = (uint16_t *)GPMF_RawData(&find_stream);
			GPS_PDOP = (uint16_t)BYTESWAP16(*data);
		}

		//Search for any units to display
		GPMF_CopyState(pMetastream, &find_stream);
		if (GPMF_OK == GPMF_FindPrev(&find_stream, GPMF_KEY_SI_UNITS, GPMF_CURRENT_LEVEL) ||
			GPMF_OK == GPMF_FindPrev(&find_stream, GPMF_KEY_UNITS, GPMF_CURRENT_LEVEL))
		{
			char *data = (char *)GPMF_RawData(&find_stream);
			int ssize = GPMF_StructSize(&find_stream);
			unit_samples = GPMF_Repeat(&find_stream);

			for (i = 0; i < unit_samples; i++)
			{
				memcpy(units[i], data, ssize);
				units[i][ssize] = 0;
				data += ssize;
			}
		}

		//GPMF_FormattedData(ms, tmpbuffer, buffersize, 0, samples); // Output data in LittleEnd, but no scale
		GPMF_ScaledData(pMetastream, tmpbuffer, buffersize, 0, samples, GPMF_TYPE_FLOAT);  //Output scaled data as floats
		
		
		float seconds = endSec - startSec;
		uint32_t sampels_cor = samples;
		if (samples > (20 * seconds)) {
			sampels_cor = (uint32_t)(20 * seconds);
		}

		

		int   avgsamples = sampels_cor / (seconds * 10);

		uint32_t repeat_round = avgsamples *(seconds * 10);

		
		ptr = tmpbuffer;
		int counter = 0;
		float latitude = 0.0f;  //deg
		float longitude = 0.0f;  //deg
		float altitude = 0.0f;    //Meter
		float ground_speed = 0.0f; //m/s
		float ground_speed_3d = 0.0f; //m/s
		int secondstart = startSec * 10;

		printf_GPS("<PAYLOAD startsec='%f' endsec='%f' samples='%d' samples_cor='%d' repeat='%d' elements='%d'/>\n", startSec, endSec, samples, sampels_cor, repeat_round, elements);
		for (i = 0; i < repeat_round; i++)
		{
			latitude = *ptr++;  //deg
			longitude = *ptr++;  //deg
			//Add for avg
			altitude += *ptr++;    //Meter
			ground_speed += *ptr++; //m/s
			ground_speed_3d += *ptr++; //m/s
		

			counter++;
			if ( (i % avgsamples) == 0) {
				//Print only ecery 1/10 second the values -> video ist 10 FPS
				
				printf_GPS("<GPSENTRY num='%d' time='%.2f' lock='%d' pdop='%03d'  lat='%.8f' lon='%.8f' alt='%.1f'  gs='%.3f'  gs3d='%.3f' />\n", entrycounter, secondstart / 10.0l, GPSLOCK, GPS_PDOP, latitude, longitude, altitude/counter, ((ground_speed / counter) *60.0*60.0) / 1000.0, ((ground_speed_3d/ counter)*60.0*60.0) / 1000.0);
				entrycounter++;
				counter = 0;
				altitude = 0;
				ground_speed = 0;
				ground_speed_3d = 0;
				secondstart++;
			}

		}
		free(tmpbuffer);
		
	}

}


void print_ACCL(GPMF_stream * pMetastream, uint32_t index)
{
	uint32_t repeat = GPMF_Repeat(pMetastream);
	uint32_t elements = GPMF_ElementsInStruct(pMetastream);
	uint32_t buffersize = repeat * elements * sizeof(float);
	float *tmpbuffer = malloc(buffersize);
	float startSec = 0.0, endSec = 0.0; //times
	if (GPMF_OK != GetGPMFPayloadTime(index, &startSec, &endSec)) {
	   return;
	}


	float seconds = endSec-startSec;
	int   avgsamples = repeat / (seconds * 10);

	uint32_t repeat_round = avgsamples *(seconds * 10);



	//print_log("%.3f to %.3f seconds repeats:%d avg:%d \n", startSec, endSec, repeat, avgsamples);

	if (GPMF_OK == GPMF_ScaledData(pMetastream, tmpbuffer, buffersize, 0, repeat, GPMF_TYPE_FLOAT))
	{ 
		float *abuffer = tmpbuffer;
		float Xms,XG = 0.0f, XGLast = 0.0f;
		float Yms,YG = 0.0f, YGLast = 0.0f;
		float Zms,ZG = 0.0f, ZGLast = 0.0f;
		int counter = 0;
		int secondstart = startSec * 10;
	

		while (repeat_round--)
		{
			XG += calc_g(abuffer[0]);
			YG += calc_g(abuffer[1]);
			ZG += calc_g(abuffer[2]);
			counter++;
			
			if ((repeat_round % avgsamples) == 0 ) {
	
		   	  XG = XG / counter;
			  YG = YG / counter;
			  ZG = ZG / counter;
		      printf_ACCL("%.2f: x=%.1f G,y=%.1f G,z=%.1f G\n", secondstart /10.0l, XG - XGLast, YG - YGLast, ZG - ZGLast );
			  XGLast = XG;
			  YGLast = YG;
			  YGLast = ZG;



			  secondstart ++;
			  counter = 0;
			  XG = 0.0f;
			  YG = 0.0f;
			  ZG = 0.0f;
			}
			abuffer = abuffer + 3;
		
		}

	}
	free(tmpbuffer);
}


void print_ACCL_2(GPMF_stream * pMetastream, uint32_t index)
{
	uint32_t repeat = GPMF_Repeat(pMetastream);
	uint32_t elements = GPMF_ElementsInStruct(pMetastream);
	uint32_t buffersize = repeat * elements * sizeof(int16_t);
	int16_t *tmpbuffer = malloc(buffersize);
	float startSec = 0.0, endSec = 0.0; //times
	if (GPMF_OK != GetGPMFPayloadTime(index, &startSec, &endSec)) {
		return;
	}

	GPMF_SampleType type = GPMF_Type(pMetastream);

	if (type != GPMF_TYPE_SIGNED_SHORT) {
		print_log("ERROR ACCL not  signed short \n");
	}

	GPMF_stream find_stream;
	GPMF_CopyState(pMetastream, &find_stream);
	int16_t *data = (int16_t *)GPMF_RawData(&find_stream);

	
	int16_t *dummy = data;
	while (repeat--)
	{
		double x = (int16_t)BYTESWAP16(*dummy);
		dummy++;
		double y = (int16_t)BYTESWAP16(*dummy);
		dummy++;
		double z = sqrt((int16_t)BYTESWAP16(*dummy));
		dummy++;


		print_log("x=%f ,y=%f, z=%f\n", x, y, z);
	}
	
	
}
*/


int main(int argc, char *argv[])
{
	
	int32_t ret = GPMF_OK;
	GPMF_stream metadata_stream, *ms = &metadata_stream;
	float metadatalength;
	uint32_t *payload = NULL; //buffer to store GPMF samples from the MP4.


	// get file return data
	if (argc != 2)
	{
		printf("usage: %s <file_with_GPMF>\n", argv[0]);
		return -1;
	}
	printf_GPS("<?xml version='1.0' encoding='UTF-8'?>\n");
	
	printf_GPS("<GOPRO5>\n");
	metadatalength = OpenGPMFSource(argv[1]);
	if (metadatalength > 0.0)
	{
		uint32_t index, payloads = GetNumberGPMFPayloads();
		print_Info("found %.2fs of metadata, from %d payloads, within %s\n", metadatalength, payloads, argv[1]);

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
			    GPMF_OK == GPMF_FindNext(ms, STR2FOURCC("GPRI"), GPMF_RECURSE_LEVELS) )   //GoPro Karma GPS
			{
				print_GPS_18hz(ms, index);
			}
			GPMF_ResetState(ms);
			print_Info("\n");
		}

	cleanup:
		if (payload) FreeGPMFPayload(payload); payload = NULL;
		  CloseGPMFSource();

		  
	  printf_GPS("<SUMMARY totalsamples='%d' frequence_hz='18'/>\n", totalsamples);
	  printf_GPS("</GOPRO5>\n");
	} else {
	     print_Info("no data\n");
        }

	return ret;
}
