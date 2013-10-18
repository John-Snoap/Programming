// =============================================================================
// John Snoap
// 11/05/2010
// Project Star Search
// =============================================================================

#include <iostream>
#include <iomanip>
using namespace std;


// Function Prototypes
void getJudgeData (float &);
float calculateScore (float [], int);
float findHighestScore (float [], int);
float findLowestScore (float [], int);


int main()
{
	const int ARRAY_SIZE = 5;			// Constant for easy change later
	float	fiveScores[ARRAY_SIZE],		// Five scores all in one array
			finalScore;					// The final score of the contestant

	// Fills the array fiveScores with five valid judge scores
	for (int count = 0; count < ARRAY_SIZE; count++)
	{
		getJudgeData (fiveScores[count]);
	}
	

	// Calculates the contestant's final score
	finalScore = calculateScore (fiveScores, ARRAY_SIZE);


	cout << fixed << showpoint << setprecision(2);
	cout << "\nThe final score of the contestant is " << finalScore << "\n\n";


	return 0;
}



//****************************************************************
// The getJudgeData function accepts a float reference           *
// variable.  It asks the user for the judge's score and         *
// checks the score to make sure it is valid.  After five        *
// complete loops in main, the array fiveScores will be filled.  *
//****************************************************************
void getJudgeData (float &score)
{
	// The number of the actual judge is static
	// so it will retain its value for each
	// increment of the loop in main
	static int numberOfJudge = 1;

	// Asks the user for the judge's score
	cout << "Enter the score from Judge "
		 << numberOfJudge << ":  ";

	cin >> score;

	// Checks to make sure the score is valid
	while (score < 0 || score > 10)
	{
		cout << "Invalid score, Re-enter the score from Judge "
			 << numberOfJudge << ":  ";

		cin >> score;
	}

	numberOfJudge++;
}



//**********************************************************
// The calculateScore function accepts a float array       *
// and an int for the array's size as arguments.           *
// The score is averaged by summing the elements of the    *
// array, subtracting the highest and lowest values,       *
// and dividing that value by 2 less than the array size.  *
// This average is returned as a float.                    *
//**********************************************************
float calculateScore (float fiveScores[], int ARRAY_SIZE)
{
	float average;  // The final score of the contestant is an average
	
	// Finds the highest and lowest scores so they can be tossed out
	float highestScore = findHighestScore (fiveScores, ARRAY_SIZE);
	float lowestScore = findLowestScore (fiveScores, ARRAY_SIZE);
	
	float score = 0;  // The contestant's score is set to zero to start with

	// Adds each element of fiveScores to the contestant's score
	for (int count = 0; count < ARRAY_SIZE; count++)
	{
		score += fiveScores[count];
	}
	
	// The highest and lowest scores are tossed out
	score -= highestScore;
	score -= lowestScore;

	// The final score is calculated by an average
	average = score / (ARRAY_SIZE - 2);

	return average;
}



//*********************************************************************
// The findHighestScore function accepts a float array                *
// and an int for the array's size as arguments.                      *
// The maximum value in the array's elements is returned as a float.  *
//*********************************************************************
float findHighestScore (float fiveScores[], int ARRAY_SIZE)
{
	float maximum = fiveScores[0];  // The first element of fiveScores
									// is stored in maximum to begin with

	// The maximum of fiveScores is found
	for (int count = 1; count < ARRAY_SIZE; count++)
	{
		if (fiveScores[count] > maximum)
			maximum = fiveScores[count];
	}

	return maximum;
}



//*********************************************************************
// The findLowestScore function accepts a float array                 *
// and an int for the array's size as arguments.                      *
// The minimum value in the array's elements is returned as a float.  *
//*********************************************************************
float findLowestScore (float fiveScores[], int ARRAY_SIZE)
{
	float minimum = fiveScores[0];  // The first element of fiveScores
									// is stored in minimum to begin with

	// The minimum of fiveScores is found
	for (int count = 1; count < ARRAY_SIZE; count++)
	{
		if (fiveScores[count] < minimum)
			minimum = fiveScores[count];
	}

	return minimum;
}

