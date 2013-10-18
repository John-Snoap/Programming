#include <iostream>
#include <iomanip>
#include <cmath>
using namespace std;


int main()
{
	int x1, y1, x2, y2;
	float m;

	cout << fixed << showpoint << setprecision(2);
	cout << "This program will find the slope of two points.\n\n";
	cout << "Enter x1:  ";

	cin >> x1;

	cout << "\n\nEnter y1:  ";

	cin >> y1;

	cout << "\n\nEnter x2:  ";

	cin >> x2;

	cout << "\n\nEnter y2:  ";

	cin >> y2;


	

	if( x1 != x2)
	{
		m = static_cast<float>((y2-y1))/static_cast<float>((x2-x1));

		cout << "\n\nThe slope of the two coordinates is " << m << endl << endl;
	}
	else
	{
		cout << "\n\nThe slope of the two points is undefined\n\n";
	}



	return 0;
}