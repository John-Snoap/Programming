#include <iostream>

using namespace std;

// prototype of toRadians goes here
double toRadians (double degrees);

int main()
{
	cout << toRadians (180) << endl;

	return 0;
}

// definition of toRadians function goes here
double toRadians (double degrees)
{
	double radians;
	return radians = (degrees * 3.141592) / 180;
}
