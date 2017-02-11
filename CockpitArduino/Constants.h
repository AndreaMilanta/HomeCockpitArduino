/**
 * File with all the shared constants of the project
 */
#pragma once

//Timing Constants
#define SERIAL_WRITE_DELAY_ms 1

//Serial Constants
#define BAUDRATE 115200		//Serial Communication BaudRate

//Hardware Constants
#define MAX_ADC_READ 1023	//Maximum value possibly read by the ADC. Depends on the hardware.

//For formatting issues there must be less than 128 channels
enum Channel
{
	LIGHTS,
	LAST		//This must always be kept as last value as it is used for reference
};
