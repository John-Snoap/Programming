#include <iostream>
using namespace std;

// Function Prototype
int letterToInt (char);

int main()
{
	char romanNumeral;
	int arabicNumeral;

	cout << "Enter a Roman Numeral and I shalt giveth thee an Arabic Numeral ";

	cin >> romanNumeral;

	arabicNumeral = letterToInt (romanNumeral);

	if (arabicNumeral != -1)
	{
		cout << "\nThe Arabic Numeral of " << romanNumeral << " is " << arabicNumeral << "\n\n";
	}
	else
	{
		cout << "\nInvalid Roman Numeral\n\n";
	}

	return 0;
}



//****************************************************
// The letterToInt function accepts a character.     *
// It converts Roman Numerals into normal numbers.   *
// It will return a -1 if the letter is not a valid  *
// Roman Numeral.  It returns an int.                *   
//****************************************************
int letterToInt (char romanNumeral)
{
	int arabicNumeral;

	switch (romanNumeral)
	{
		case ('_'):
			arabicNumeral = 0;
			break;
		case ('i'):
		case ('I'):
			arabicNumeral = 1;
			break;
		case ('v'):
		case ('V'):
			arabicNumeral = 5;
			break;
		case ('x'):
		case ('X'):
			arabicNumeral = 10;
			break;
		case ('l'):
		case ('L'):
			arabicNumeral = 50;
			break;
		case ('c'):
		case ('C'):
			arabicNumeral = 100;
			break;
		case ('m'):
		case ('M'):
			arabicNumeral = 1000;
			break;
		default:
			arabicNumeral = -1;
			break;
	}

	return arabicNumeral;
}
