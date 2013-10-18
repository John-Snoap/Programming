// This program demonstrates the polyline function.
#include "DarkGDK.h"


//************************************************
// DarkGDK function                              *
//************************************************
void DarkGDK()
{
	const int BOX_SIZE = dbScreenWidth() / 10;

	DWORD red = dbRGB(255, 0, 0);
	DWORD white = dbRGB(255,255,255);

	int left = 0, top=0, right = left + BOX_SIZE, bottom = top + BOX_SIZE;
	bool odd = true;

	for (int i = 0; i < dbScreenWidth(); i += BOX_SIZE)
	{
		for (int j = 0; j < dbScreenWidth(); j += BOX_SIZE)
		{
			if (odd)
			{
				dbInk(red, white); // set drawing color to red (2nd parm is ignored)
				dbBox(left, top, right, bottom); // draw box at indicated position
			
				left += BOX_SIZE;
				right += BOX_SIZE;
		
				dbInk(white, red); // set drawing color to white (2nd parm is ignored)
				dbBox(left, top, right, bottom); // draw box at indicated position

				left += BOX_SIZE;
				right += BOX_SIZE;
			}
			else
			{
				dbInk(white, red); // set drawing color to white (2nd parm is ignored)
				dbBox(left, top, right, bottom); // draw box at indicated position

				left += BOX_SIZE;
				right += BOX_SIZE;

				dbInk(red, white); // set drawing color to red (2nd parm is ignored)
				dbBox(left, top, right, bottom); // draw box at indicated position
			
				left += BOX_SIZE;
				right += BOX_SIZE;
			}

		}

		if (odd)
		{
			odd = false;
		}
		else
		{
			odd = true;
		}

		left = 0, top += BOX_SIZE, right = left + BOX_SIZE, bottom = top + BOX_SIZE;
		

	}
	

    // Wait for the user to press a key.
    dbWaitKey();
}



