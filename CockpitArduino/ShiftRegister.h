/**
 * Class to manage shift register
 * It includes the loading of the shift register
 */

#pragma once

#include "Sensor.h"

#define READ_DELAY_us 20

class ShiftRegister: public Sensor
{
private:
	byte value;
	int dataPin, clockPin, latchPin;
	int mode, selfLoad;

public:
	ShiftRegister(Channel _id, int dP, int cP);
	ShiftRegister(Channel _id, int dP, int cP, bool msbFirst);
	ShiftRegister(Channel _id, int dP, int cP, int lP);
	ShiftRegister(Channel _id, int dP, int cP, int lP, bool msbFirst);
	~ShiftRegister(void);
	String getString();
	bool hasChanged();

private:
	void setPins();
	byte read();
};

