// This program demonstrates basic darkgdk graphics.
#include "DarkGDK.h"


//************************************************
// DarkGDK function                              *
//************************************************
void DarkGDK()
{
	int centerX = dbScreenWidth()/2; 
	int centerY = dbScreenHeight()/2;

	// draw a circle centered at x, y with radius 20
	dbCircle(centerX, centerY, 20);
		

	// to pause briefly, use dbWait(milliseconds) to delay for the specified time
	

    // Wait for the user to press a key.
    dbWaitKey();
}



