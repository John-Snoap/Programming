
#include ".\form1.h"
#include <cmath>

// ignore the weird :: syntax for now
double toRadians (double degrees);
void cannon::Form1::shoot(int speed, int angle)
{
	//====================================================
	// DO NOT CHANGE ANY OF THE CONSTANTS IN THIS CODE.
	// ALL OF YOUR CHANGES WILL BE TOWARD THE END OF THIS
	// FUNCTION. SEE COMMENTS LATER.
	//====================================================

	// position and length of line repesenting ground
	const int GROUND_Y = 450;
	const int GROUND_X = 10;
	const int GROUND_SIZE = 900;
	const int GROUND_THICK = 10;

	// position and size of "building"
	const int BLDG_HEIGHT = 100;
	const int BLDG_WIDTH  = 50;
	const int BLDG_OFFSET = 700;
	const int BLDG_LEFT = GROUND_X + BLDG_OFFSET;
	const int BLDG_TOP = GROUND_Y - GROUND_THICK/2 - BLDG_HEIGHT;
	const int BLDG_RIGHT = BLDG_LEFT + BLDG_WIDTH;

	// misc constants affecting animation
	const float GRAVITY = 9.8f;		// force of gravity
	const int   BALL_RADIUS = 4;	// radius of cannon ball
	const float DELTA_TIME = .10f;	// time increment
	const int   TIME_LIMIT = 20;    // animation loop limit

	// starting position of line representing cannon barrel
	// This is the location of the "base" of the barrel
	const int CANNON_BASE_X = GROUND_X + 50;
	const int CANNON_BASE_Y = GROUND_Y - GROUND_THICK/2;

	// cannon barrel length and thickness
	const int CANNON_LEN = 25;
	const int CANNON_THICK = 2 * BALL_RADIUS;

	// Variables representing position/speed of ball, and angle
	// of cannon barrel
	int   ballx, bally;		// x,y center of cannon ball
	double vx, vy;			// components of velocity vector for ball
	float time = 0;			// time counter for loop (note float!)

	// needed for calculating coordinates of cannon barrel tip
	const double PI = 3.14159;

	int canTipX;			// x coord of tip of cannon barrel
	int canTipY;			// y coord of tip of cannon barrel

	
	//=============================================================
	// DO NOT CHANGE ANY OF THE CODE ABOVE. YOUR CODE CHANGES BEGIN
	// BELOW.
	//
	// Locations of end of cannon barrel are hard-coded below so you can see
	// how cannon is drawn. You need to replace these values
	// with values calculated based on value in the parameter 'angle'

	// Replace the following two lines of code to calculate coordinates for tip of 
	// cannon barrel.

	double radians;
	radians = toRadians (angle);

	canTipX = CANNON_BASE_X + CANNON_LEN*cos(radians);	// tip is 20 pixels right of base
	canTipY = CANNON_BASE_Y - CANNON_LEN*sin(radians);	// tip is 20 pixels above base 

	// draws cannon:
	// specify starting x,y and ending x,y of the line to be drawn

	drawCannon(CANNON_BASE_X, CANNON_BASE_Y, canTipX, canTipY);

	// PHASE 2 code goes here
	bool animate = true;
	vx = speed * cos(radians);	
	vy = speed * sin(radians);
	
	for (time = 0; time < TIME_LIMIT && animate; time += DELTA_TIME)
	{
		ballx = vx * time + canTipX;
		bally = canTipY  - (vy * time) + (0.5 * GRAVITY * (time * time));
		
		
		drawBall(ballx, bally);
		if (bally >= GROUND_Y)
		{
			animate = false;
		}

		if (ballx >= BLDG_LEFT && ballx <= BLDG_RIGHT && bally >= BLDG_TOP)
		{
			animate = false;
		}

		delay(25);

		if (animate)
		{
			eraseBall(ballx, bally);
		}

	}


}

double toRadians (double degrees)
{
	double radians;
	return radians = (degrees * 3.141592) / 180;
}
