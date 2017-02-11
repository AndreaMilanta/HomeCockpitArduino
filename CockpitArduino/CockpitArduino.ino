/*
 Name:		CockpitArduino.ino
 Created:	2/5/2017 10:37:01 PM
 Author:	Andrea
*/

#include "Arduino.h"
#include "Constants.h"
#include "ShiftRegister.h"
#include "StringFormatter.h"
#include "Sensor.h"

Sensor sensors[LAST];				//Array of sensors

void setup()
{
	//Serial Init
	Serial.begin(BAUDRATE);

	//Fill SensorArray
	sensors[LIGHTS] = ShiftRegister(LIGHTS, 1, 2 ,3);				//Lights

	//Send first value for all channels
	for(int i = 0; i < LAST; i++)
		Serial.println(sensors[i].getString());
}

void loop()
{
	//Sends channels that have changed
	for(int i = 0; i < LAST; i++)
	{
		if(sensors[i].hasChanged())
			Serial.println(sensors[i].getString());
		delay(SERIAL_WRITE_DELAY_ms);
	}
}

