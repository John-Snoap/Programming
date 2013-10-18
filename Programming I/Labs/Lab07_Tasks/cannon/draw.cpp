#include ".\form1.h"


using namespace System::Drawing::Drawing2D;


// The variables angle and speed will have the corresponding values 
// entered by the user in the GUI. Expected values are:
//  angle: from ~5..90
//  speed: from ~0..100
// Ignore the strange function syntax  below; you don't need
// to understand it right now.
void cannon::Form1::animate(Graphics *g, int speed, int angle)
{
	currentg = g;
	// create pen to use for drawing ground

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

	grndPen = new Pen(Color::Green, static_cast<float>(GROUND_THICK));
	
	// create brushes for drawing building and cannon ball
	bldgBrush = new HatchBrush(HatchStyle::HorizontalBrick, Color::Firebrick);
	ballBrush = new SolidBrush(Color::Black);
	ballErase = new SolidBrush(panel1->BackColor);

	// create pen for drawing cannon
	cannonPen = new Pen(Color::Black, static_cast<float>(CANNON_THICK));

	
	// create pen to use for drawing ground
	grndPen = new Pen(Color::Green, static_cast<float>(GROUND_THICK));
	
	// create brushes for drawing building and cannon ball
	bldgBrush = new HatchBrush(HatchStyle::HorizontalBrick, Color::Firebrick);
	ballBrush = new SolidBrush(Color::Black);
	ballErase = new SolidBrush(panel1->BackColor);

	// create pen for drawing cannon
	cannonPen = new Pen(Color::Black, static_cast<float>(CANNON_THICK));

	// draw ground
	g->DrawLine(grndPen, GROUND_X, GROUND_Y, 
		        GROUND_X + GROUND_SIZE, GROUND_Y);  
	
	// draw building
	g->FillRectangle(bldgBrush,BLDG_LEFT, BLDG_TOP, BLDG_WIDTH, BLDG_HEIGHT);

	shoot(speed, angle);

}
