// This program demonstrates the polyline function.
#include "DarkGDK.h"


//************************************************
// DarkGDK function                              *
//************************************************
void DarkGDK()
{
	int centerX = dbScreenWidth()/2;
	int centerY = dbScreenHeight()/2;

	
	for (int i = 20; i < 80; i = i + 3)
	{
		dbCircle(centerX, centerY, i);
	}
		

	// to pause briefly, use dbWait(milliseconds) to delay for the specified time
	

    // Wait for the user to press a key.
    dbWaitKey();
}



