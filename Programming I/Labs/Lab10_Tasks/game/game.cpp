// This program demonstrates the polyline function.
#include "DarkGDK.h"

const int FRAME_RATE = 60;
// Identifiers required for each image we load.
// These identifiers are used to refer to the image after it's
// been loaded in memory.
const int BACKGROUND_IMAGE_ID = 1;
const int BOW_IMAGE_ID = 2;
const int ARROW_IMAGE_ID = 3;
const int ALIEN_IMAGE_ID = 4;

// Identifiers required for each sprite. These identifiers are used
// to manipulate images that are used as sprites
const int BOW_SPRITE_ID = 1;
const int ARROW_SPRITE_ID = 2;
const int ALIEN_SPRITE_ID = 3;


struct alien
{
	int x;
	int y;

	bool alive;
	bool moving;
};




void createImages ();
void aimBow (int &, int &, double &);
void shootArrow (bool &, double &, double &, int, int, double, const double, double &, double &, double &);
double toRadians (double);

//************************************************
// DarkGDK function                              *
//************************************************
void DarkGDK()
{
	createImages();
	dbSyncOn();
	dbSyncRate(FRAME_RATE);

	// the position of the ship will be the bottom left hand corner of the screen
	int bowX = 30;
	int bowY = (dbScreenHeight() - dbScreenHeight()/8) - 20;

	const int NUMBER_OF_ALIENS = 10;
	const int JUMP_DISTANCE = 45;

	alien a[10];
	for (int i = 0; i < NUMBER_OF_ALIENS; i++)
	{
		a[i].x = dbScreenWidth() - 60 - (45*i);
		a[i].y = 100;
		a[i].alive = true;
		a[i].moving = false;
	}





	int lives = 3;
	char livesChar[10];
	int kills = 0;
	char killsChar[10];
	int score = 0;
	char scoreChar[10];

	double arrowX = bowX;
	double arrowY = bowY;
	double currentRotation = 0;
	const double ARROW_SPEED = 7;
	double time = 0;
	double arrowAngle = 0;
	double radians = 0;
	bool drawingArrow = false;

	
	// main game loop. Won't end until user tells us to
	while ( LoopGDK() )
	{
		// draw background
		dbPasteImage(BACKGROUND_IMAGE_ID, 0, 0);



		// draw ship 
		dbSprite(BOW_SPRITE_ID, bowX, bowY, BOW_IMAGE_ID);
		// move ship
		aimBow(bowX, bowY, currentRotation);


		// draw and move missile
		shootArrow (drawingArrow, arrowX, arrowY, bowX, bowY, currentRotation, ARROW_SPEED, time, arrowAngle, radians);

		
		// make 9 more enemies
		for (int i = 0; i < NUMBER_OF_ALIENS; i++)
		{
			dbCloneSprite(ALIEN_SPRITE_ID, ALIEN_SPRITE_ID + i);
		}
		// draw all 10 enemies
		for (int i = 0; i < NUMBER_OF_ALIENS; i++)
		{
			dbSprite(ALIEN_SPRITE_ID + i, a[i].x, a[i].y, ALIEN_IMAGE_ID);
		}


		// collision check
		for (int i = 0; i < 10; i++)
		{

			if ( dbSpriteCollision(ARROW_SPRITE_ID, ALIEN_SPRITE_ID + i) && a[i].alive )
			{
				dbHideSprite(ALIEN_SPRITE_ID + i);
				a[i].alive = false;
				kills++;
				score = (pow (1.05, kills) - 1) * 100;
			}
		}
		


		// display score, kills, lives
		itoa (score, scoreChar, 10);
		itoa (kills, killsChar, 10);
		itoa (lives, livesChar, 10);

		dbText (20, 20, "Score:");
		dbText (80, 20, scoreChar);
		dbText (20, 40, "Kills:");
		dbText (80, 40, killsChar);
		dbText (20, 60, "Lives:");
		dbText (80, 60, livesChar);





		dbSync(); // update the screen

	}
}

void createImages()
{
	dbLoadImage("minasTirith.jpg", BACKGROUND_IMAGE_ID);
	dbLoadImage("bow.png", BOW_IMAGE_ID);
	dbLoadImage("arrow.png", ARROW_IMAGE_ID);
	dbLoadImage("oliphant.png", ALIEN_IMAGE_ID);
}

void aimBow (int &x, int &y, double &currentRotation)
{
   const int ROTATE_SPEED = 3;

   if ( dbUpKey() || dbLeftKey() )
   {
	   if (currentRotation > -45)
	   {
		   currentRotation -= ROTATE_SPEED;
	   }

	   dbRotateSprite(BOW_SPRITE_ID, currentRotation);

   }
   else if ( dbDownKey() || dbRightKey() )
   {
	   if (currentRotation < 45)
	   {
		   currentRotation += ROTATE_SPEED;
	   }

	   dbRotateSprite(BOW_SPRITE_ID, currentRotation);

   }

}



void shootArrow (bool &drawingArrow, double &arrowX, double &arrowY, int bowX, int bowY, double currentRotation,
						const double ARROW_SPEED, double &time, double &arrowAngle, double &radians)
{
	if (dbSpaceKey() && !drawingArrow)
		{
			drawingArrow = true;
			arrowX = bowX;
			arrowY = bowY;
			time = 0;

			dbSprite(ARROW_SPRITE_ID, arrowX, arrowY, ARROW_IMAGE_ID);
			arrowAngle = currentRotation;
			dbRotateSprite(ARROW_SPRITE_ID, currentRotation);

			radians = toRadians (currentRotation);  // This sets how the arrow will look
			radians -= asin ( sqrt(2.0)/2.0 );		// and where it will go when fired
		}

		if (drawingArrow)
		{
			dbSprite(ARROW_SPRITE_ID, arrowX, arrowY, ARROW_IMAGE_ID);

			arrowX += cos (radians) * ARROW_SPEED;

			
			time += (ARROW_SPEED/1000);
			

			// This updates the yPosition with gravity
			arrowY += sin (radians) * ARROW_SPEED;
			arrowY = (  4.9 * pow(time, 2) - sin (radians) * ARROW_SPEED * time + arrowY  );

			arrowAngle += ( (9.8 * time - sin (radians) * ARROW_SPEED) ) / 16;
			dbRotateSprite (ARROW_SPRITE_ID, arrowAngle);


			


			if ( arrowY <= 0 || arrowY >= bowY + 15 || arrowX >= dbScreenWidth() )
			{
				dbDeleteSprite(ARROW_SPRITE_ID);
				drawingArrow = false;
			}
		}

}


double toRadians (double degrees)
{
	double radians;
	return radians = (degrees * 3.141592) / 180;
}