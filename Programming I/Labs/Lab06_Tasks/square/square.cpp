#include <iostream>
using namespace std;

int main()
{
	unsigned int max, row, column;
	
	cout << "How many rows do you want?  ";

	cin >> max;

	for (row = 1;  row <= max;  row++)
	{
		for (column = 1;  column <= max;  column++)
		{
			cout << "X ";
		}

		cout << endl;

	}

	return 0;
}