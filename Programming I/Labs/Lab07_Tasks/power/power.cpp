#include <iostream>
#include <iomanip>
using namespace std;

// prototype of calcPower function
double calcPower(int base, int exponent);

int main()
{
	int base, exp;
	double answer;

	// test with values entered by the user
	cout << "Enter the base: ";
	cin >> base;
	cout << "Enter the exponent: ";
	cin >> exp;
	answer = calcPower(base, exp);
	cout << base << " raised to the power " << exp << " is " << answer << "\n\n";


	// display powers of 2 up to 2^10
	cout << endl << "Powers of 2 from 2^0...2^10: " << endl;
	int i;
	i = 0;
	while (i <= 10)
	{
		answer = calcPower(2, i);
		cout << 2 << " raised to the power " << i << " is " << answer << "\n";
		i++;
	}

	return 0;
}

// definition of calcPower function goes here
double calcPower(int base, int exponent)
{
	double answer = 1;
	for (int count = 1; count <= exponent; count++)
	{
		answer = answer * base;
	}
	return answer;
}

