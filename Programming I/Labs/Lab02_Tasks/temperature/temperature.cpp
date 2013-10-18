
#include <iostream>
#include <cmath>
using namespace std;

int main()
{
	float Fahrenheit, Celsius;

	cout << "This program will convert Celsius into Fahrentheit" << endl << endl;
	cout << "Enter the degrees in Celsius." << endl;

	cin >> Celsius;


	Fahrenheit = (9.0/5.0) * Celsius + 32.0;

	
	cout << "The degrees in Fahrenheit are:  " << Fahrenheit << endl << endl;

	return 0;
}
