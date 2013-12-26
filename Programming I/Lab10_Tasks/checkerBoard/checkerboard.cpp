// This program demonstrates color handling and drawing boxes
#include "DarkGDK.h"


//************************************************
// DarkGDK function                              *
//************************************************
void DarkGDK()
{
	const int BOX_SIZE = 50;

	DWORD red = dbRGB(255, 0, 0);
	DWORD white = dbRGB(255,255,255);

	int left = 0, top=0, right = left + BOX_SIZE, bottom = top + BOX_SIZE;

	dbInk(red, white); // set drawing color to red (2nd parm is ignored)
	
	dbBox(left, top, right, bottom); // draw box at indicated position
			
	left += BOX_SIZE;
	right += BOX_SIZE;
		
	dbInk(white, red); // set drawing color to white (2nd parm is ignored)
	dbBox(left, top, right, bottom); // draw box at indicated position
		
    // Wait for the user to press a key.
    dbWaitKey();
}



