#include <iostream>
using namespace std;

// Function Prototype


int main()
{
	const int SALSA_TYPES = 5;
	char salsaName[SALSA_TYPES][8] = { "Mild", "Medium", "Sweet", "Hot", "Zesty" };
	int numberOfJarsSold[SALSA_TYPES],
		salsaTotal = 0,
		maximum,
		maxName,
		minimum,
		minName,
		index;

	for (index = 0; index < SALSA_TYPES; index++)
	{
		cout << "Enter the number of " << salsaName[index] << " Jars sold:  ";

		cin >> numberOfJarsSold[index];

		while (numberOfJarsSold[index] < 0)
		{
			cout << "Invalid number of " << salsaName[index] << " Jars.  Re-enter:  ";

			cin >> numberOfJarsSold[index];
		}

	}

	

	for (index = 0; index < SALSA_TYPES; index++)
	{
		salsaTotal += numberOfJarsSold[index];
	}




	maximum = numberOfJarsSold[0];
	maxName = 0;

	for (index = 0; index < SALSA_TYPES; index++)
	{
		if (numberOfJarsSold[index] > maximum)
		{
			maximum = numberOfJarsSold[index];
			maxName = index;
		}

	}




	minimum = numberOfJarsSold[0];
	minName = 0;

	for (index = 0; index < SALSA_TYPES; index++)
	{
		if (numberOfJarsSold[index] < minimum)
		{
			minimum = numberOfJarsSold[index];
			minName = index;
		}

	}




	for (index = 0; index < SALSA_TYPES; index++)
	{
		cout << "\nThe number of " << salsaName[index] << " Jars sold was:  " << numberOfJarsSold[index];
	}

	cout << "\n\nThe total amount of Salsa sold was:  " << salsaTotal;

	cout << "\n\nThe highest selling product was " << salsaName[maxName];
	cout << "\nThe lowest selling product was " << salsaName[minName];
	cout << "\n\n";






	return 0;
}



