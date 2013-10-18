#include <iostream>
#include <iomanip>
#include <cmath>
#include <fstream>
using namespace std;

int main()
{

	int p1, p2, p3;		// three pressure readings

	ifstream inFile;	// input file

	inFile.open("data.txt");


	inFile >> p1 >> p2 >> p3;

	cout << p1 << ", " << p2 << ", " << p3 << ", " << endl;
	

	float average;

	average = ( static_cast<float>(p1) + static_cast<float>(p2) + static_cast<float>(p3) ) / ( 3.0 );

	if ( average <= 100 && average >= 71 )
		cout << "\n\nThe average pressure, " << average << ", is high\n\n";

	else if ( average >= 40 )
		cout << "\n\nThe average pressure, " << average << ", is normal\n\n";

	else
		cout << "\n\nThe average pressure, " << average << ", is low\n\n";




	return 0;
}