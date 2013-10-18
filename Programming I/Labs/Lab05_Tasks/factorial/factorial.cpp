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
	int integer, factorialOfIntegerStepA, factorialOfIntegerStepB;

	cout << "This program calculates the factorial of an integer that you enter.\n\n";
	cout << "Enter an integer that you wish to be factorialized:  ";

	cin >> integer;

	for (factorialOfIntegerStepA = factorialOfIntegerStepB = integer; factorialOfIntegerStepA > 1; factorialOfIntegerStepA--)
	{
		factorialOfIntegerStepB = factorialOfIntegerStepB * (factorialOfIntegerStepA - 1);
	}



	cout << "\n\n" << integer << "! =  " << factorialOfIntegerStepB << "\n\n";


	return 0;
}