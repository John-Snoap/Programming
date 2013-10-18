

//	John Snoap

#include <iostream>
#include <iomanip>
using namespace std;

int main()
{
	int amount_paid, quarters, dimes, nickels, pennies, change;
	double cents, total_cost;
	const double tax = .075;

	//cout << fixed << showpoint << setprecision(1);
	cout << "Enter the cost of the item (in cents):  ";

	cin >> cents;

	cout << endl << "Enter the amount paid for the item (in cents):  ";

	cin >> amount_paid;

	
	
	
	
	total_cost = cents * tax;
	total_cost += cents;
	total_cost = static_cast<int>(total_cost + .5);			//	round total_cost

	cout << endl << "The total cost of the item is:  " << total_cost << " cents" << endl << endl;
	
	change = amount_paid - total_cost;

	cout << setw(22) << left << "the total change is:  "	<< setw(10) << right << change		<< " cents"	   << endl;
	




	quarters = change / 25;
	change %= 25;

	dimes = change / 10;
	change %= 10;

	nickels = change / 5;
	change %= 5;

	pennies = change;





	cout << setw(22) << left << "Number of quarters:  "		<< setw(10) << right << quarters	<< " quarters"  << endl;
	cout << setw(22) << left << "Number of dimes:  "		<< setw(10) << right << dimes		<< " dimes"		<< endl;
	cout << setw(22) << left << "Number of nickels:  "		<< setw(10) << right << nickels		<< " nickels"	<< endl;
	cout << setw(22) << left << "Number of pennies:  "		<< setw(10)	<< right << pennies		<< " pennies"	<< endl << endl;





	return 0;
}
