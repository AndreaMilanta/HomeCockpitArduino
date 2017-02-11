/*
 * Generic input sensor 
 * It is in fact an interface
 */

#pragma once

#include "Arduino.h"
#include "Constants.h"
#include "StringFormatter.h"

class Sensor

{
protected:
	Channel id;
	//Method
public:
	Sensor(void);
	virtual String getString();	//Sets the class as interface
	virtual bool hasChanged();
};

