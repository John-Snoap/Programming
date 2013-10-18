// Lab 1, Task 1
// John Snoap
// This program prompts the user for the radius
// of a circle (units are ignored). The program
// then calculates and displays the area.

#include <iostream>
using namespace std;

int main()
{
	double PI = 3.14159;
	double area, radius;

	cout << "please enter the radius of the circle: ";
	cin >> radius;

	area = PI * radius * radius;

	cout << "A circle with radius " << radius << " has " << "area: " << area << endl;

	return 0;
}



