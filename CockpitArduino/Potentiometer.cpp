/**
 * Class to manage Potentiometer
 */
#include "Potentiometer.h"

/// <summary>
/// Standard Constructor
/// </summary>
/// <param name="p">pin of the input, it MUST BE analog</param>
Potentiometer::Potentiometer(Channel _id, int p)
{
	id = _id;
	pin = p;
	min = MIN_P;
	max = MAX_P;
	pinMode(pin, INPUT);
	value = analogRead(pin);
}

/// <summary>
/// Constructor with custom limits
/// </summary>
/// <param name="p">pin of the input, it MUST BE analog</param>
/// <param name="mx">maximum value</param>
/// <param name="mn">minimum value</param>
Potentiometer::Potentiometer(Channel _id, int p, int mx, int mn)
{
	id = _id;
	pin = p;
	min = mn;
	max = mx;
	pinMode(pin, INPUT);
	value = getPercent();
}

/// <summary>
/// gets the percentage of the potentiometer position
/// </summary>
/// <returns></returns>
float Potentiometer::getPercent()
{
	value = analogRead(pin);
	return value * MAX_P / MAX_ADC_READ;
}

/// <summary>
/// gets the value corresponding to the current pot position
/// if no limits have been set, the percentage is returned
/// </summary>
/// <returns></returns>
float Potentiometer::getValue()
{
	value = analogRead(pin);
	return (value * (max - min) / MAX_ADC_READ) + min;
}

/// <summary>
/// checks if the value has changed. 
/// </summary>
/// <param name="v">is loaded with the current percentage.
///		if the return value is false its value is meaningless</param>
/// <returns>if the value has changed from the last read</returns>
bool Potentiometer::hasChanged()
{
	if(value != analogRead(pin))
		return true;
	return false;
}

/// <summary>
/// Returns the string to be written to the Serial
/// !!!It READS a new value!!!
/// The vaule is the percentage
/// </summary>
/// <returns></returns>
String Potentiometer::getString()
{
	return StringFormatter::getString(id, getPercent());
}

//Destructor
Potentiometer::~Potentiometer(void)
{
}
