#include <iostream>
using namespace std;



int main()
{
	int numberOfTestScores;
	float* ptrTestScores;
	float total = 0;
	float average;

	cout << "Enter how many test scores you have:  ";

	cin >> numberOfTestScores;
	cout << "\n\n";


	ptrTestScores = new float [numberOfTestScores];

	for (int i = 0; i < numberOfTestScores; i++)
	{
		cout << "Enter test score " << (i + 1) << ":  ";
		cin >> ptrTestScores[i];
		
		while (ptrTestScores[i] < 0)
		{
			cout << "Cannot be negative, Try again:  ";
			cin >> ptrTestScores[i];
		}
	}

	for (int i = 0; i < numberOfTestScores; i++)
	{
		total += ptrTestScores[i];
	}

	delete [] ptrTestScores;
	ptrTestScores = 0;

	average = (total / numberOfTestScores);

	cout << "\n\nThe average of the test scores is:  " << average << "\n\n";

	return 0;
}



