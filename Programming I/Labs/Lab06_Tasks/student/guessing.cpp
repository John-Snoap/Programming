// =============================================================================
// John Snoap
// 10/12/2010
// Lab 3
// =============================================================================

#include <iostream>
#include <iomanip>
#include <cmath>
#include <fstream>
#include <cstring>
#include <cstdlib>
#include <ctime>
using namespace std;

int main()
{
	int computerNumber, humanNumber;
	
	srand(time(0));
	computerNumber = (rand() % 100) + 1;

	cout << "\tI am thinking of an integer from [0-100]\n";
	cout << "Guess my number:  ";

	cin >> humanNumber;

	while (humanNumber != computerNumber)
	{
		if (humanNumber > computerNumber)
		{
			cout << "\n\n" << humanNumber << "\nis too high!\n\n";
		}

		else
		{
			cout << "\n\n" << humanNumber << "\n\nis too low!\n\n";
		}

		cout << "Guess again:  ";
		
		cin >> humanNumber;

	}

	cout << "\n\nCongratulations!\nYou guessed my number!!!\n\n";

	return 0;
}