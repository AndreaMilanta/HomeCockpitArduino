/**
 * Class to manage Potentiometer
 */
#pragma once

#include "Sensor.h"

#define MAX_P 100			//Maximum of percentage
#define MIN_P 0				//Minimum of percentage

class Potentiometer: public Sensor
{
	//Variables
private:
	float value;
	int pin;
	int max, min;	

	//Methods
public:
	Potentiometer(Channel _id, int p);
	Potentiometer(Channel _id, int p, int mx, int mn);
	~Potentiometer(void);
	float getPercent();
	float getValue();
	bool hasChanged();
	String getString();
};