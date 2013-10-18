#include <iostream>
#include <iomanip>
using namespace std;

void calcStats(int a[], float* avg, int* max, int* min, int size);

int main()
{
	const int SIZE = 5;
	int values[SIZE] = { 10, 20, 30, 40, 50 };
	float avg;
	int   min, max;

	calcStats(values, &avg, &min, &max, SIZE);

	cout << "Statistics:" << endl << "----------" << endl;
	cout << setw(15) << left << "Average: " <<  avg << endl;
	cout << setw(15) << left << "Minimum value: " << min << endl;
	cout << setw(15) << left << "Maximum value: " << max << endl;

	return 0;
}

// Calculate the average, min, and max values in the array a

void calcStats(int a[], float* avg, int* min, int* max, int size)
{
	int total = 0;
	*min = *max = a[0];

	for(int i = 0; i < size; i++)
	{
		total += a[i];
		if ( a[i] < *min )
			*min = a[i];
		else if ( a[i] > *max )
			*max = a[i];
	}
	*avg = static_cast<float>(total) / size;
};

