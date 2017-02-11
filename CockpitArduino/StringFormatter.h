/**
 * Static class to provide a common string formatter 
 * for Serial communication
 */

#pragma once

#include "Arduino.h"
#include "Constants.h"

//MUST MODIFY THE EQUIVALENT CONSTANTS ON THE COMPUTER SIDE
#define DIV_SYM '&'
#define INT_SYM 'i'
#define FLT_SYM 'f'
#define BOL_SYM 'b'
#define BTE_SIM 't'
#define NUM_DEC 3

class StringFormatter
{
public:
	//Constructers
	StringFormatter(void);
	~StringFormatter(void);

	//Templates
	template <typename NUM>

	/// <summary>
	/// Formats the id and value into a string which follows the 
	/// standard format 
	/// Standard Format is:  &CHAN_ID&VALUE_LENGTH&VALUE&
	/// </summary>
	/// <param name="id">id of channel</param>
	/// <param name="value">value of channel</param>
	/// <returns></returns>
	static String getString(Channel id, NUM value)
	{
		return DIV_SYM + (char)id + DIV_SYM + toString(value) + DIV_SYM;
	}

	//Number formatters
	static String toString(int v);
	static String toString(float v);
	static String toString(bool v);
	static String toString(byte v);

};

