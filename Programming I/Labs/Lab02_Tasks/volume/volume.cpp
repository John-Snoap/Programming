
// Enter and modify the book's code as needed to make the
// program work

#include <iostream>
#include <cmath>
using namespace std;

int main()
{
	double volume, radius, height;

	cout << "This program will tell you the volume of"		<< endl;
	cout << "a cylinder-shaped fuel tank."					<< endl << endl;
	cout << "How tall is the tank? ";

	cin >> height;

	cout << endl << "What is the radius of the tank? ";

	cin >> radius;



	volume = 3.14159 * pow(radius,2) * height;



	cout << endl << "The volume of your cilinder is: " << volume << endl << endl;

	return 0;
}