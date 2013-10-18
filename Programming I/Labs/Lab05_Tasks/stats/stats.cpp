// 
// John Snoap
// 10/07/2010
// Lab 5
// 
// 

#include <iostream>
#include <iomanip>
#include <cmath>
#include <fstream>
#include <cstring>
using namespace std;


int main()
{

	int integer, totalValue = 0, numberOfIntegers = 0, minimumValue = 50000000, maximumValue = 0;
	float  averageValue;

	cout << "\tThis program adds all of the integers you enter\n";
	cout << "and displays the total, the average, the min, and the max\nof all the integers you enter at the end\n\n";
	cout << "\tEnter as many integers as you want\n";
	cout << "When you are done, enter -1 to stop\n\n";
	cout << "Enter an integer:  ";

	cin >> integer;


		while (integer != -1)
	{
		totalValue = totalValue + integer;
		numberOfIntegers++;


		if (integer < minimumValue)
		{
			minimumValue = integer;
		}

		if (integer > maximumValue)
		{
			maximumValue = integer;
		}


		cout << "\nEnter another integer or -1 if you are done:  ";
		cin >> integer;
	}

	averageValue = static_cast<float>(totalValue) / numberOfIntegers;

	cout << "\n\nThe total value is:  " << totalValue << "\n\n";
	cout << "The average value is:  " << averageValue << "\n\n";
	cout << "The minimum value is:  " << minimumValue << "\n\n";
	cout << "The maximum value is:  " << maximumValue << "\n\n";

	return 0;
}