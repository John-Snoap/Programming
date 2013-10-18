#include <iostream>
#include <iomanip>
using namespace std;

// prototype of calcPower function goes here


int main()
{
	int base, exp;

	// test with values entered by the user
	cout << "Enter the base: ";
	cin >> base;
	cout << "Enter the exponent: ";
	cin >> exp;
	calcPower(base, exp);


	// display powers of 2 up to 2^10
	cout << endl << "Powers of 2 from 2^0...2^10: " << endl;
	int i;
	i = 0;
	while (i <= 10)
	{
		calcPower(2, i);
		i++;
	}

	return 0;
}

// definition of calcPower function goes here
