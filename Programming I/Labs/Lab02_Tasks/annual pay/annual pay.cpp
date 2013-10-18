#include <iostream>
using namespace std;

int main()
{
	
	float payAmount, annualPay;
	int   payPeriods;

	// replace these assignment statements with prompt/input statements
	// to ask the user for these values.
	
	cout << "Enter the amount paid:" << endl << endl;

	cin >> payAmount;

	cout << endl << "Enter the  pay periods in a year" << endl << endl;

	cin >> payPeriods;


	annualPay = payAmount * payPeriods;

	cout << "Your annual pay is: " << annualPay << endl;

	return 0;
}
