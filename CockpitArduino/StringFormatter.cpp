#include "StringFormatter.h"

/// <returns>"INT_SYM valueLength & value"</returns>
String StringFormatter::toString(int v)
{
	String s =  String(v, DEC);
	return INT_SYM + DIV_SYM + s;
}

/// <returns>"f NUMDEC & valueLength & value"</returns>
String StringFormatter::toString(float v)
{
	String s = String(v, NUM_DEC);
	return FLT_SYM + (char)(NUM_DEC) + DIV_SYM + s;
}

String StringFormatter::toString(bool v)
{
	String s = String(v, BIN);
	return BOL_SYM + (DIV_SYM + s);
}

String StringFormatter::toString(byte v)
{
	String s = String(v, BIN);
	return BTE_SIM + (DIV_SYM + s);
}

StringFormatter::StringFormatter(void)
{
}
StringFormatter::~StringFormatter(void)
{
}
