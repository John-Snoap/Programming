
// John Snoap

#include <iostream>
#include <iomanip>
using namespace std;

int main()
{
	char name[100];
	const double adult_ticket = 6.00;
	const double child_ticket = 3.00;
	double adult_tickets_sold, child_tickets_sold, gross_box_office_profit, net_box_office_profit, amount_paid_to_distributor;


	cout << "Enter the name of the movie:  ";
	
	cin.getline(name, 100);

	cout << endl << "Enter the number of Adult Tickets sold:  ";

	cin >> adult_tickets_sold;

	cout << endl << "Enter the number of Child Tickets sold:  ";

	cin >> child_tickets_sold;


	cout << fixed << showpoint << setprecision(0);

	cout << endl	<< setw(29) << left << "Movie Name:  "							<< setw(20) << right << name						<< endl;
	cout			<< setw(29) << left << "Adult Tickets Sold:  "					<< setw(18) << right << adult_tickets_sold			<< endl;
	cout			<< setw(29) << left << "Child Tickets Sold:  "					<< setw(18) << right << child_tickets_sold			<< endl;




	cout << fixed << showpoint << setprecision(2);

	gross_box_office_profit = ((adult_ticket * adult_tickets_sold) + (child_ticket * child_tickets_sold));
	net_box_office_profit = gross_box_office_profit * .20;
	amount_paid_to_distributor = gross_box_office_profit - net_box_office_profit;



	cout			<< setw(29) << left << "Gross Box Office Profit:  "		<< "$ "	<< setw(18) << right << gross_box_office_profit		<< endl;
	cout			<< setw(29) << left << "Net Box Office Profit:  "		<< "$ " << setw(18) << right << net_box_office_profit		<< endl;
	cout			<< setw(29) << left << "Amount Paid to Distributor:  "	<< "$ " << setw(18) << right << amount_paid_to_distributor	<< endl << endl;







	return 0;
}
