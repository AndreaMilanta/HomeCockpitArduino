#include "ShiftRegister.h"

/// <summary>
/// Standard Constructor for SR with selfLoading
/// mode is LSBFIRST by default
/// </summary>
/// <param name="dP">data Pin</param>
/// <param name="cP">clock Pin</param>
ShiftRegister::ShiftRegister(Channel _id, int dP, int cP)
{
	id = _id;
	dataPin = dP;
	clockPin = cP;
	selfLoad = true;
	mode = LSBFIRST;
	setPins();
	value = read();
}

/// <summary>
/// Constructor with mode selection for SR with selfLoading
/// </summary>
/// <param name="dP">data Pin</param>
/// <param name="cP">clock Pin</param>
/// <param name="msbFirst">mode MSBFIRST?</param>
ShiftRegister::ShiftRegister(Channel _id, int dP, int cP, bool msbFirst)
{
	id = _id;
	dataPin = dP;
	clockPin = cP;
	selfLoad = true;
	if(msbFirst)
		mode = MSBFIRST;
	else
		mode = LSBFIRST;
	setPins();
	value = read();
}

/// <summary>
/// Standard Constructor for SR
/// mode is LSBFIRST by default
/// </summary>
/// <param name="dP">data Pin</param>
/// <param name="cP">clock Pin</param>
/// <param name="lP">latch Pin</param>
ShiftRegister::ShiftRegister(Channel _id, int dP, int cP, int lP)
{
	id = _id;
	dataPin = dP;
	clockPin = cP;
	latchPin = lP;
	selfLoad = false;
	mode = LSBFIRST;
	setPins();
	value = read();
}

/// <summary>
/// Constructor with mode selection for default SR
/// </summary>
/// <param name="dP">data Pin</param>
/// <param name="cP">clock Pin</param>
/// <param name="lP">latch Pin</param>
/// <param name="msbFirst">mode MSBFIRST?</param>
ShiftRegister::ShiftRegister(Channel _id, int dP, int cP, int lP, bool msbFirst)
{
	id = _id;
	dataPin = dP;
	clockPin = cP;
	latchPin = lP;
	selfLoad = false;
	if(msbFirst)
		mode = MSBFIRST;
	else
		mode = LSBFIRST;
	setPins();
	value = read();
}

void ShiftRegister::setPins()
{
	pinMode(latchPin, OUTPUT);
	pinMode(clockPin, OUTPUT); 
	pinMode(dataPin, INPUT);
}

/// <summary>
/// Reads the shift register values
/// </summary>
/// <returns>Read byte</returns>
byte ShiftRegister::read()
{
	if(!selfLoad)
	{
		//Loads shift Register
		digitalWrite(latchPin, HIGH);
		delayMicroseconds(READ_DELAY_us);							
		digitalWrite(latchPin, LOW);
	}
	//Reads shift register
	value = shiftIn(dataPin, clockPin, mode);
	return value;
}

/// <summary>
/// Checks if the register values have changed
/// </summary>
/// <returns></returns>
bool ShiftRegister::hasChanged()
{
	if(value != read())
		return true;
	return false;
}

/// <summary>
/// Returns the formatted string with the id and channel
/// !!!It does NOT read a new value!!!	It should always be called after hasChanged()
/// </summary>
/// <returns></returns>
String ShiftRegister::getString()
{
	return StringFormatter::getString(id, value);
}

ShiftRegister::~ShiftRegister(void)
{
}
