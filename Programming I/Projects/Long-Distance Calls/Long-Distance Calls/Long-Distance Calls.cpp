// ============================================================================= 
// John Snoap
// 10/03/2010
// Project 2
// Long-Distance Calls
// ============================================================================= 

#include <iostream>
#include <iomanip>
using namespace std;

int main()
{

	const double				RATE_PER_MINUTE_1 = 0.12,	RATE_PER_MINUTE_2 = 0.55,	RATE_PER_MINUTE_3 = 0.35; 
	// Start rate at time:		00:00 - 06:59				07:00 - 19:00				19:01 - 23:59

	double startTimeOfCall, minutesOfCall, costOfLongDistanceCall;
	char again[5];

	// startTimeOfCall:				The start time of the call					Input by the user
	// minutesOfCall:				How long the call was						Input by the user
	// costOfLongDistanceCall:		The cost of the call						Calculated
	// again:						If the user wants to do it again			Input by the user


/////////////// Entering the Call Time Data ///////////////////////////////////////////////////////////////////////////////

	cout << fixed << showpoint << setprecision(2);
	cout << "\tThis program calculates the cost of a long distance telephone call.\n";
	cout << "Enter the time the call was dialed with a period instead of a colon;\n";
	cout << "for instance:  '7:00' SHOULD BE ENTERED AS '7.00'.";
	
	do
	{	
		cout << "\n\nStart time:  ";

		cin >> startTimeOfCall;

		while ((static_cast<int>(startTimeOfCall) > 23) || ((startTimeOfCall - static_cast<int>(startTimeOfCall)) > .59))
		{
			cout << "\n\tAn invalid start time was entered.\n";
			cout << "The HOURS of the start time CANNOT BE GREATER THAN 23.\n\n";
			cout << "The MINUTES of the start time CANNOT BE GREATER THAN 59.\n\n";
			cout << "Start time:  ";
			cin >> startTimeOfCall;
			cout << endl;
		}

		cout << "\nEnter the total time of the call in MINUTES ONLY:  ";

		cin >> minutesOfCall;

////////// Calculating the Cost of the Call /////////////////////////////////////////////////////////////////////////

		if (startTimeOfCall <= 06.59)
		{
			costOfLongDistanceCall = minutesOfCall * RATE_PER_MINUTE_1;
		}
	
		else if (startTimeOfCall <= 19.00)
		{
			costOfLongDistanceCall = minutesOfCall * RATE_PER_MINUTE_2;
		}

		else
		{
			costOfLongDistanceCall = minutesOfCall * RATE_PER_MINUTE_3;
		}

////////// Displaying the Cost of the Call /////////////////////////////////////////////////////////////////////////////

		cout << "\n\nThe cost of that long distance call is:  $ " << costOfLongDistanceCall << "\n\n\n";
		cout << "Would you like to calculate the cost of another long distance call?  (Y/N):  ";

		cin >> again;

	}  while (again[0] == 'Y' || again[0] == 'y');

	cout << endl << endl;

	return 0;
}