#include <iostream>
#include <iomanip>
#include <cmath>
using namespace std;


int main()
{

	int score;		// numeric test score

	cout << "Enter your score: ";
	cin >> score;

	if ( score >= 0 && score <= 100)
	{
		
		if ( score >= 90 )
			cout << "\n\nYou earned an A\n\n" << endl;

		else if ( score >=80 )
			cout << "\n\nYou earned a B\n\n" << endl;

		else if ( score >= 70 )
			cout << "\n\nYou earned a C\n\n" << endl;

		else if ( score >= 60 )
			cout << "\n\nYou earned a D\n\n" << endl;

		else
			cout << "\n\nYou earned an F\n\n" << endl;
	}
	else
	{
		cout << "\n\nInvalid score\n\n";
	}


	return 0;
}