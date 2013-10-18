//==============================================================================
//	John Snoap
//	09/20/2010
//	Programming Assignment #1
//	Description:  Interest Earned
//==============================================================================

#include <iostream>
#include <iomanip>
#include <cmath>
using namespace std;

int main()
{
	cout << fixed << showpoint << setprecision(2);
	
	float Principal, Rate, T, Interest, Amount;

	/*
		Principal	>> entered by the user			>> the balance in the savings account.
		Rate		>> entered by the user			>> the interest rate.
		T			>> entered by the user			>> the number of times the interest is compounded in one year
		Interest	>> calculated					>> the amount of money added to the balance
		Amount		>> calculated					>> the new balance
	*/



	//	Gathering Data		>> Start
	cout << "From your savings account, enter the principle BALANCE:  ";

	cin >> Principal;

	cout << endl << "From the principle balance, enter the INTEREST RATE:  ";

	cin >> Rate;

	cout << endl << "From the interest rate, enter the number of TIMES COMPOUNDED in one YEAR:  ";

	cin >> T;

	cout << endl;
	//	Gathering Data		>> Finish
	


	//	Calculations		>> Start
	Rate /= 100;
	Amount = Principal * pow((1 + (Rate/T)),T);
	Interest = Amount - Principal;
	Rate *= 100;
	//	Calculations		>> Finish



	//	Showing Results		>> Start
	cout << setw(20) << left << "Interest Rate:"				<< setw(15)	<< right	<< Rate			<< "%"	<< endl;
	cout << setw(20) << left << "Times Compounded:"				<< setw(15)	<< right	<< T					<< endl;
	cout << setw(20) << left << "Principal Balance:"	<< "$ " << setw(13)	<< right	<< Principal			<< endl;
	cout << setw(20) << left << "Interest:"				<< "$ " << setw(13)	<< right	<< Interest				<< endl;
	cout << setw(20) << left << "Amount in Savings:"	<< "$ " << setw(13)	<< right	<< Amount				<< endl		<< endl;
	//	Showing Results		>> Finish


	return 0;
}