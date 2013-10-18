#include <iostream>
#include <iomanip>
using namespace std;

// Function Prototype
void multiplicationTable (int);


int main()
{
	int num;

	cout << "How big do you want the Multiplication Table to be?  ";

	cin >> num;

	multiplicationTable (num);


	return 0;
}

// function definition goes here
void multiplicationTable (int num)
{
	for (int row = 1; row <= num; row++)
	{
		for (int coloumn = 1; coloumn <= num; coloumn++)
		{
			cout << setw(5) << right << row * coloumn;
		}

		cout << endl;
	}
		
}