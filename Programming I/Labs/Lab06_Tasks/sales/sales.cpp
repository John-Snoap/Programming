// =============================================================================
// John Snoap
// 10/12/2010
// Lab 3
// =============================================================================

#include <iostream>
#include <iomanip>
#include <cmath>
#include <fstream>
#include <cstring>
#include <cstdlib>
#include <ctime>
using namespace std;

int main()
{
	char name[50];
	int salesNumber;
	
	ifstream inputFile;

	inputFile.open("sales.txt");

	if (!inputFile)
	{
		cout << "\n\n\n\n\n\n\n\nFail\n\n";
	}

	else
	{
		while (inputFile >> name >> salesNumber)
		{
			cout << setw(10) << left << name << ":  ";
			for (int row = 1;  row <= (salesNumber / 100);  row++)
			{
				cout << "*";
			}

			cout << endl;

		}

		inputFile.close();


	}




	return 0;
}



