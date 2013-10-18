//=========================================================================
// Project 6
// The Defense of Gondor
// John Snoap and Alex Orr
//		Alex Orr and I worked together for the majority of this project
//		which is why so much of it is exactly the same.  After a certain
//		point we both added our own quirks to the game.  This code has
//		my (John's) additional quirks:  infinite levels and a high score.
//
// 12/13/2010
//=========================================================================

#include "DarkGDK.h"
#include <ctime>


const int FRAME_RATE = 60;


// Identifiers required for each image we load.
// These identifiers are used to refer to the image after it's
// been loaded in memory.
const int BACKGROUND_IMAGE_ID = 1;
const int BOW_IMAGE_ID = 2;
const int ARROW_IMAGE_ID = 3;
const int ENEMY_IMAGE_ID = 4;

// Identifiers required for each file we load
// This identifier is used to refer to the high score after
// it's been loaded in memory.
const int HIGH_SCORE_FILE_ID = 1;


// Identifiers required for each sprite we load.
// These identifiers are used to manipulate
// images that are used as sprites
const int BOW_SPRITE_ID = 1;
const int ARROW_SPRITE_ID = 2;
const int ENEMY_SPRITE_ID = 3;


// Enemy information
const int NUMBER_OF_ENEMIES = 10;  // How many enemies there are
const int JUMP_DISTANCE = 45;  // How far the enemies move each jump

// Arrow information
const int ARROW_SPEED = 7;  // How quickly the arrow moves



///////// Structures /////////////////////////////////////////////////////


// This struct includes the enemy's location,
// and whether or not the enemies are alive and moving
struct enemy
{
	int x;
	int y;

	bool alive;
	bool moving;
};


// This struct includes the location of the cannon
struct cannon
{
	int x;
	int y;
};


// This struct includes the number of lives
// and the number of kills the player has.
// It also includes the player's score, what level
// he or she is on, and the all time high score
struct stats
{
	int lives;
	char livesChar[10];  // The char arrays are used to display the
						 // numbers on the screen

	int kills;
	char killsChar[10];

	int score;
	char scoreChar[10];

	int level;
	char levelChar[10];

	int highScore;
	char highScoreChar[10];
};


// This struct includes the arrow's location
// the angle of the arrow, and whether or not
// the arrow is being drawn
struct missile
{
	double x;
	double y;
	double angle;
	bool drawingArrow;
};




//////// Function prototypes /////////////////////////////////////////////

int loadHighScore ();
void createImages ();

void displayEnemies (enemy []);
void chooseWhoMoves (enemy [], int &, int &);
void aimBow (cannon, double &);
void shootArrow (missile &, cannon, double, double &, double &);
void collisionCheck (enemy [], stats &);
void enemyMovement (int &, enemy [], stats &, cannon);
void displayStats (stats &);

void displayWinScreen (stats &, DWORD, DWORD);
void youWin (stats &, DWORD, DWORD);
void saveScore (stats &player);
void gameOver (stats &, DWORD, DWORD);
void displayFinalStats (stats &, DWORD, DWORD);

double toRadians (double);

//************************************************
// DarkGDK function                              *
//************************************************
void DarkGDK()
{
	srand (time(0)); // This forces the enemies to deploy in different
					 // orders every time the game is played

	DWORD red = dbRGB (255, 0, 0);			// These set colors
	DWORD green = dbRGB (0, 255, 0);		// to be easily used
	DWORD blue = dbRGB (0, 0, 255);
	DWORD yellow = dbRGB (255, 255, 0);
	DWORD black = dbRGB (0, 0, 0);

	dbInk (black, black);



	createImages();  // This loads the images

	dbSyncOn();
	dbSyncRate(FRAME_RATE);

	int oneSecondCounter = 0;  // These counters are to control
	int twoSecondCounter = 0;  // when the enemies move

	
	// Enemy information
	enemy oliphants[NUMBER_OF_ENEMIES + 1];
	for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
	{
		oliphants[i].x = dbScreenWidth() - 60 - (45*(i-1));
		oliphants[i].y = 10;
		oliphants[i].alive = true;
		oliphants[i].moving = false;
	}


	// The bow will be in the bottom left corner of the screen
	cannon bow;
	bow.x = 45;
	bow.y = (dbScreenHeight() - dbScreenHeight()/8);


	// Player stats
	stats player;

	player.lives = 3;
	player.kills = 0;
	player.score = 0;
	player.level = 1;
	player.highScore = loadHighScore();




	// Arrow information
	missile arrow;

	arrow.x = bow.x;
	arrow.y = bow.y;
	arrow.angle = 0;
	arrow.drawingArrow = false;

	
	// Angle for the bow
	double currentRotation = 0;

	// Conversion from degrees to radians so arrow can move properly
	double radians = 0;

	double time = 0;
	int timeInterval = player.level; // This is used to speed up the enemies every level

	int randCount = 0;

	bool flag = false;  // Used to determine if all the enemies are gone
						// to go to the next level
	bool winBool = true;  // Lets the user know that they won at after level 2

	
	// Main game loop. Won't end until user tells us to
	while ( LoopGDK() )
	{
		//////// Draws background ////////////////////////////////////////////
		dbPasteImage(BACKGROUND_IMAGE_ID, 0, 0);


		//////// Draws enemies //////////////////////////////////////////////
		displayEnemies (oliphants);

		/////// Randomly picks an enemy to deploy
		if (twoSecondCounter == (125 - 5*timeInterval) && randCount < NUMBER_OF_ENEMIES)
		{
			chooseWhoMoves (oliphants, twoSecondCounter, randCount);
		}


		///////// Draws bow  ////////////////////////////////////////////////
		dbSprite(BOW_SPRITE_ID, bow.x, bow.y, BOW_IMAGE_ID);
		// Sets the center as the insertion point for rotation
		dbOffsetSprite(BOW_SPRITE_ID, dbSpriteWidth(BOW_SPRITE_ID) / 2, dbSpriteHeight(BOW_SPRITE_ID) / 2);

		///////// Aims bow
		aimBow (bow, currentRotation);


		//////// Draws and moves the arrow when shot ////////////////////////
		shootArrow (arrow, bow, currentRotation, time, radians);


		/////// collision check /////////////////////////////////////////////
		collisionCheck (oliphants, player);
		
		
		// Moves enemies if alive
		if (oneSecondCounter == (65 - 5*timeInterval))  // Movement is more often when level goes up
		{
			enemyMovement (oneSecondCounter, oliphants, player, bow);
		}

		// Display stats
		displayStats (player);



		/////////// Display Win after level 2 ///////////////////////////////////////


		// When you get past level two, this displays a message
		if (player.level > 2 && player.lives > 0 && winBool)
		{
			flag = true;
			oneSecondCounter = 0;  // Reseting these counters keeps the game
			twoSecondCounter = 0;  // from counting too high and failing


			////////// Hides all the sprites ///////////////////////////

			for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
			{
				dbHideSprite (ENEMY_SPRITE_ID + i);
				oliphants[i].alive = false;
				oliphants[i].moving = false;
			}

			dbHideSprite (ARROW_SPRITE_ID);
			dbHideSprite (BOW_SPRITE_ID);

			displayWinScreen (player, green, blue);  // This is the message
													 // that is displayed
			

			///////// It goes back to the game when you press enter ////////

			if ( dbReturnKey() )
			{
				for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
				{
					dbShowSprite (ENEMY_SPRITE_ID + i);
					oliphants[i].alive = true;
					oliphants[i].moving = false;
				}

				dbShowSprite (ARROW_SPRITE_ID);
				dbShowSprite (BOW_SPRITE_ID);

				dbInk (black, black);

				winBool = false;
				flag = false;
			}
		}




		/////// Check player lives ///////////////////////////////////////////


		if (player.lives <= 0)  // If player is dead, game is over
		{
			player.lives = 0;
			oneSecondCounter = 0;  // Reseting these counters keeps the game
			twoSecondCounter = 0;  // from counting too high and failing


			//////////// Hides all the sprites ////////////////////////////

			for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
			{
				dbHideSprite (ENEMY_SPRITE_ID + i);
				oliphants[i].alive = false;
				oliphants[i].moving = false;
			}


			dbDeleteSprite(BOW_SPRITE_ID);
			dbDeleteSprite(ARROW_SPRITE_ID);


			/////////// Checks for a high score ///////////////////////////

			if (player.level > 7 && player.score > player.highScore)
			{
				player.highScore = player.score;  // saves the high score
			}


			////////// Displays a success or failure message ///////////////////

			if (player.level > 7)
				youWin (player, yellow, blue);
			else
				gameOver (player, red, black);
		}



		///////// If the player is still alive /////////////////////////

		////////////// Check to go to the next level //////////////////////

		for (int i = 1; i < (NUMBER_OF_ENEMIES + 1) && flag == false; i++)
		{
			if (oliphants[i].alive) // If any enemy is alive the loop quits
				flag = true;		// and the next if fails
			else
				flag = false;
		}


		////////////// Puts all the enemies back at the top ///////////////
		////////////// and increments the level by one ////////////////////

		if ( flag == false && player.lives > 0)
		{
			for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
			{
				oliphants[i].x = dbScreenWidth() - 60 - (45*(i-1));
				oliphants[i].y = 10;
				oliphants[i].alive = true;
				oliphants[i].moving = false;
				dbShowSprite (ENEMY_SPRITE_ID + i);
			}

			player.level++;
			randCount = 0;
			oneSecondCounter = 0;
			twoSecondCounter = 0;
		}


		/////// Makes the enemies move and deploy faster each level until //
		/////// the level gets past level 11, then the speed is the same ///

		if (player.level <= 11)
			timeInterval = player.level;



		oneSecondCounter++; // Update so the enemies can move
		twoSecondCounter++; // Update so the enemies can deploy

		flag = false; // resests the flag to check to see if the enemies are
					  // still alive to go to the next level

		dbSync(); // Update the screen

	}
}



//***********************************************************
// The function loadHighScore returns an integer that is    *
// either loaded from the high score file, or created with  *
//  0 if it does not already exist.                         *
//***********************************************************
int loadHighScore ()
{
	int highScore = 0; // initializes highScore with
					   // the minimum score


	char highScoreChar[10] = "0";
	itoa (highScore, highScoreChar, 10); // converts the highScore
										 // to a char

	char* ptrHighScoreChar[10] = {highScoreChar}; // uses a pointer because
												  // the functions below require
												  // a pointer to a char for them
												  // them to work

	if( dbFileExist ("highScore.txt") )  // if the file exists
	{
		dbOpenToRead (HIGH_SCORE_FILE_ID, "highScore.txt");
		*ptrHighScoreChar = dbReadString(HIGH_SCORE_FILE_ID); // loads the high score
															  // as a string array
		highScore = atoi (*ptrHighScoreChar); // converts the string array to an int
	}
	else
	{	
		dbOpenToWrite (HIGH_SCORE_FILE_ID, "highScore.txt");  // Creates the file
		dbWriteString (HIGH_SCORE_FILE_ID, highScoreChar);  // Saves 0 to the file
															// as a string
		dbCloseFile (HIGH_SCORE_FILE_ID);

		dbOpenToRead (HIGH_SCORE_FILE_ID, "highScore.txt");
		*ptrHighScoreChar = dbReadString(HIGH_SCORE_FILE_ID); // loads the high score
															  // as a string array
		highScore = atoi (*ptrHighScoreChar); // converts the string array to an int
	}

	dbCloseFile (HIGH_SCORE_FILE_ID);

	return highScore;
}



//*****************************************************
// The function createImages loads the images in the  *
// game folder so that they may be used as sprites    *
// to make the game look cool and fun.                *
//*****************************************************
void createImages ()
{
	dbLoadImage("minasTirith.jpg", BACKGROUND_IMAGE_ID);
	dbLoadImage("bow.png", BOW_IMAGE_ID);
	dbLoadImage("arrow.png", ARROW_IMAGE_ID);
	dbLoadImage("oliphant.png", ENEMY_IMAGE_ID);
}



//************************************************************
// The function displayEnemies takes a struct array of type  *
// enemy which includes the x and y positions of the enemy   *
// oliphants.  It then displays them on the screen.          *
//************************************************************
void displayEnemies (enemy oliphants[])
{
	// make 9 more enemies
	for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
	{
		dbCloneSprite(ENEMY_SPRITE_ID, ENEMY_SPRITE_ID + i);
	}
	// draw all 10 enemies
	for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
	{
		dbSprite(ENEMY_SPRITE_ID + i, oliphants[i].x, oliphants[i].y, ENEMY_IMAGE_ID);
	}
}



//**************************************************************
// The function chooseWhoMoves takes a struct array of type    *
// enemy, and two reference integers.  It then randomly picks  *
// from the enemies who are alive and not moving which enemy   *
// to deploy.                                                  *
//**************************************************************
void chooseWhoMoves (enemy oliphants[], int &twoSecondCounter, int &randCount)
{
	int chooseFrom[NUMBER_OF_ENEMIES]; // This array is filled up with the enemies
									   // who are alive and not moving
	int chosenOne; // This is an int from 0 to NUMBER_OF_ENEMIES left 
	int enemyToMove; // This will be the actual enemy that is chosen
	int count = 0; // This is a counter that allows the chooseFrom array to be filled

	for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
	{
		if (oliphants[i].alive && !oliphants[i].moving)
		{
			chooseFrom[count] = i; // fills the array only with enemies that
								   // are alive and not moving
			count++; // updates to the next space in the array
		}
	}

	chosenOne = rand () % (NUMBER_OF_ENEMIES - randCount); // randCount keeps track
														// of how many enemies have
														// been deployed, and subtracts
														// that number from the total
														// number of enemies to make sure
														// that the chosenOne will be a
														// location in chooseFrom
														// with a valid number

	enemyToMove = chooseFrom[chosenOne]; // the enemyToMove is one of the enemy oliphants
										 // that were stored in chooseFrom to begin with

	oliphants[enemyToMove].moving = true; // sets the enemy.moving that was chosen to true
										  // so that is starts moving


	randCount++;  // updates randCount because now one less enemy is available to choose from
   	twoSecondCounter = 0; // the counter to check how often to perform this function is reset
}



//******************************************************
// The function aimBow takes a struct type cannon and  *
// the double currentRotation.  It then aims the bow   *
// based on how the player tells it to move            *
// with the arrow keys.                                *
//******************************************************
void aimBow (cannon bow, double &currentRotation)
{
   const int ROTATE_SPEED = 3;

   if ( dbUpKey() || dbLeftKey() )  // player can use up or left arrow key
   {
	   if (currentRotation > -45)  // will not let the player aim backwards
	   {
		   currentRotation -= ROTATE_SPEED;  // changes the angle
	   }

	   dbRotateSprite(BOW_SPRITE_ID, currentRotation);  // rotates the bow based on
														// the angle

   }
   else if ( dbDownKey() || dbRightKey() )  // player can use down or right arrow key
   {
	   if (currentRotation < 45)  // will not let the player aim below parallel
	   {						  // to the ground
		   currentRotation += ROTATE_SPEED;  // changes the angle
	   }

	   dbRotateSprite(BOW_SPRITE_ID, currentRotation);  // rotates the bow based on
														// the angle

   }

}



//**************************************************************************
// The function shootArrow takes a reference struct type missile,          *
// a struct type cannon, the double currentRotation, a double              *
// reference for the time, and a double reference for degrees in radians.  *
// It draws the arrow when you press the spacebar and deletes it after     *
// the arrow goes off the screen.                                          *
//**************************************************************************
void shootArrow (missile &arrow, cannon bow, double currentRotation,
						double &time, double &radians)
{
	if (dbSpaceKey() && !arrow.drawingArrow)  // The player can only shoot one arrow at a time
		{
			arrow.drawingArrow = true;  // turns drawingArrow on
			arrow.x = bow.x;  // initializes the arrow's x and y positions with
			arrow.y = bow.y;  // the bow's x and y positions
			time = 0;  // initializes time for the arrow with zero

			dbSprite(ARROW_SPRITE_ID, arrow.x, arrow.y, ARROW_IMAGE_ID); // initially draws and angles
			arrow.angle = currentRotation;								 // the arrow on top of the bow
			dbRotateSprite(ARROW_SPRITE_ID, currentRotation);

			radians = toRadians (currentRotation);  // This sets the angle of the arrow and
			radians -= asin ( sqrt(2.0)/2.0 );		// therefore it's trajectory when fired
		}

		if (arrow.drawingArrow)  // if the arrow is shot, it will be drawn
		{
			// Draws the Arrow
			dbSprite(ARROW_SPRITE_ID, arrow.x, arrow.y, ARROW_IMAGE_ID);
			// Sets the center as the insertion point for rotation
			dbOffsetSprite(ARROW_SPRITE_ID, dbSpriteWidth(ARROW_SPRITE_ID) / 2, dbSpriteHeight(ARROW_SPRITE_ID) / 2);

			// Updates the X position of the arrow
			arrow.x += cos (radians) * ARROW_SPEED;

			
			time += (static_cast<double>(ARROW_SPEED)/1000);
			

			// Updates the Y position of the arrow with gravity
			arrow.y += sin (radians) * ARROW_SPEED;
			arrow.y = (  4.9 * pow(time, 2) - sin (radians) * ARROW_SPEED * time + arrow.y  );

			// Rotates the arrow so it will look realistic
			arrow.angle += ( (9.8 * time - sin (radians) * ARROW_SPEED) ) / 16;
			dbRotateSprite (ARROW_SPRITE_ID, arrow.angle);


			

			// if the arrow goes off the screen, it is deleted.
			if ( arrow.y <= 0 || arrow.y >= bow.y + 15 || arrow.x >= dbScreenWidth() )
			{
				dbDeleteSprite(ARROW_SPRITE_ID);
				arrow.drawingArrow = false;  // this enables the player to shoot another arrow
											 // because he or she cannot shoot more than one
											 // arrow at a time
			}
		}

}



//********************************************************************
// The function collisionCheck takes a struct array type enemy, and  *
// a struct reference type stats.  It then checks to see if there    *
// is a collision.  If there is a collision, it updates the score    *
// and hides the enemy so it is dead.                                *
//********************************************************************
void collisionCheck (enemy oliphants[], stats &player)
{
	for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)
		{
			// does the check for all the enemies
			if ( dbSpriteCollision(ARROW_SPRITE_ID, ENEMY_SPRITE_ID + i) && oliphants[i].alive && oliphants[i].moving)
			{
				dbHideSprite(ENEMY_SPRITE_ID + i);
				oliphants[i].alive = false;  // alive and moving are set to false so the enemy is
				oliphants[i].moving = false; // actually dead
				player.kills++;  // increases the player's kills
				player.score += pow ( (.68), ((-1*player.kills)/10) ); // updates the player's score
			}
		}
}



//*****************************************************************
// The function enemyMovement takes an int reference to update    *
// a counter, a struct array type enemy, a struct reference type  *
// stats, and a struct type cannon.  It then moves the enemies.   *
// If the enemies hit the ground it takes away one life and some  *
// points.  The further into the game the player is, the more     *
// the points will decrease.                                      *
//*****************************************************************
void enemyMovement (int &oneSecondCounter, enemy oliphants[], stats &player, cannon bow)
{
	for (int i = 1; i < (NUMBER_OF_ENEMIES + 1); i++)  // it does this for all the enemies
	{
		if ( oliphants[i].alive && oliphants[i].moving)  // they must be alive and moving
		{												 // in order to move
			oliphants[i].y += 20;

			// if one gets to the bottom, the enemy goes away and the player dies once
			if (oliphants[i].y >= bow.y + 15 && oliphants[i].alive)
			{
				dbHideSprite(ENEMY_SPRITE_ID + i);
				oliphants[i].alive = false;
				oliphants[i].moving = false;
				player.lives--;
				player.score -= pow ( .68, ((-1*player.kills)/10) ) * 5;  // the player can loose
																		  // a lot of points because
																		  // I am evil

				if (player.score < 0)
					player.score = 0;  // However, the player's score cannot go below 0
			}
		}
	}

	oneSecondCounter = 0;  // Reseting this allows the enemies to move again
}



//******************************************************************
// The function displayStats takes a struct reference type stats.  *
// It then displays the player's stats in the top left corner      *
// of the screen.                                                  *
//******************************************************************
void displayStats (stats &player)
{
	itoa (player.score, player.scoreChar, 10);  // This converts the int stats
	itoa (player.kills, player.killsChar, 10);  // to char array stats so
	itoa (player.lives, player.livesChar, 10);  // they can be displayed as
	itoa (player.level, player.levelChar, 10);  // sensical data to the player

	dbText (20, 10, "Score:");
	dbText (80, 10, player.scoreChar);
	dbText (20, 30, "Kills:");
	dbText (80, 30, player.killsChar);
	dbText (20, 50, "Lives:");
	dbText (80, 50, player.livesChar);
	dbText (20, 70, "Level:");
	dbText (80, 70, player.levelChar);
}



//*******************************************************************
// The function displayWinScreen takes a struct reference type      *
// stats, and two DWORDs to set the text color.  It then draws the  *
// background and the message with the two colors given.            *
//*******************************************************************
void displayWinScreen (stats &player, DWORD color1, DWORD color2)
{
	dbInk (color2, color1); // The background is color2
	dbBox ( 0, 0, dbScreenWidth(), dbScreenHeight() );

	dbInk (color1, color2); // The text is color1
	dbText ( dbScreenWidth()/2 - 170, dbScreenHeight()/2 - 70, "You must get past level 7 to save Minas Tirith." );
	dbText ( dbScreenWidth()/2 - 170, dbScreenHeight()/2 - 50, "You can press Enter to keep playing," );
	dbText ( dbScreenWidth()/2 - 170, dbScreenHeight()/2 - 30, "or if you exit now, you win!!!" );

	displayFinalStats (player, color1, color2);  // This displays player stats at the bottom so the
												 // player knows how well he or she is doing
}



//*****************************************************************
// The function youWin is the legitimate win screen.  It takes    *
// the same variables as the displayWinScreen function.  The      *
// only thing is does differently is dislay a different message.  *
//*****************************************************************
void youWin (stats &player, DWORD color1, DWORD color2)
{
	dbInk (color2, color1); // The background is color2
	dbBox ( 0, 0, dbScreenWidth(), dbScreenHeight() );

	dbInk (color1, color2); // The text is color1
	dbText ( dbScreenWidth()/2 - 110, dbScreenHeight()/2 - 50, "Minas Tirith is saved" );
	dbText ( dbScreenWidth()/2 - 110, dbScreenHeight()/2 - 30, "through your death!" );

	saveScore (player);

	displayFinalStats (player, color1, color2);  // This displays player stats at the bottom so the
												 // player knows how well he or she did
}



//**************************************************************
// The function saveScore takes a struct reference type stats  *
// and saves a new high score.                                 *
//**************************************************************
void saveScore (stats &player)
{
	// When you OpenToWrite to the file, it will not work if the the file
	// already exists.  So I solved this by simply deleting the file so
	// it can be recreated when I open it.  There may be a better way to do
	// this, but I don't exactly know what is going on with these functions.
	// For this game and for right now deleting the file and recreating
	// it will work just fine.
	dbDeleteFile ("highScore.txt");
	dbOpenToWrite (HIGH_SCORE_FILE_ID, "highScore.txt");  // creates and opens the file

	dbWriteString (HIGH_SCORE_FILE_ID, player.highScoreChar); // saves the new high score
														   // as a string so when loaded
														   // again can be converted
														   // from a string to an int
	
	dbCloseFile (HIGH_SCORE_FILE_ID); // closes the file
}



//*************************************************************
// The function gameOver takes a struct reference type stats  *
// and two DWORDs to set the text color.  It then draws the   *
// background and the message with the two colors given.      *
//*************************************************************

void gameOver (stats &player, DWORD color1, DWORD color2)
{
	dbInk (color2, color1); // The background is color2
	dbBox ( 0, 0, dbScreenWidth(), dbScreenHeight() );

	dbInk (color1, color2); // The text is color1
	dbText ( dbScreenWidth()/2 - 70, dbScreenHeight()/2 - 50, "Minas Tirith is overrun." );
	dbText ( dbScreenWidth()/2 - 70, dbScreenHeight()/2 - 30, "You have failed." );

	displayFinalStats (player, color1, color2);  // This displays player stats at the bottom so the
												 // player knows how well he or she did
}



//****************************************************************
// The function displayFinalStats takes a struct reference type  *
// stats and two DWORDs to se the text color.  It then displays  *
// the player stats at the bottom of the screen so the player    *
// can see how well he or she did.                               *
//****************************************************************
void displayFinalStats (stats &player, DWORD color1, DWORD color2)
{
	dbInk (color1, color2); // The text is color1


	itoa (player.score, player.scoreChar, 10);			// This converts the int stats
	itoa (player.highScore, player.highScoreChar, 10);	// to char array stats so
	itoa (player.kills, player.killsChar, 10);			// they can be displayed as
	itoa (player.level, player.levelChar, 10);			// sensical data to the player
	


	dbText ( dbScreenWidth()/4 - 70, dbScreenHeight()*3/4 - 20, "Score:" );
	dbText ( dbScreenWidth()/4 - 10, dbScreenHeight()*3/4 - 20, player.scoreChar );

	dbText ( dbScreenWidth()/4 - 110, dbScreenHeight()*3/4, "High Score:" );
	dbText ( dbScreenWidth()/4 - 10, dbScreenHeight()*3/4, player.highScoreChar );

	dbText ( dbScreenWidth()/2 - 70, dbScreenHeight()*3/4 - 20, "Kills:" );
	dbText ( dbScreenWidth()/2 - 10, dbScreenHeight()*3/4 - 20, player.killsChar );

	dbText ( dbScreenWidth()*3/4 - 70, dbScreenHeight()*3/4 - 20, "Level:" );
	dbText ( dbScreenWidth()*3/4 - 10, dbScreenHeight()*3/4 - 20, player.levelChar );
}



//***********************************************************
// The function toRadians takes a double, converts it from  *
// degrees to radians, and returns the radian double.       *
//***********************************************************
double toRadians (double degrees)
{
	double radians;

	radians = (degrees * 3.141592) / 180;

	return radians;
}