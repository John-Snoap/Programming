#include <iostream>

using namespace std;

int main()
{
	char NAME[100], CITY[100], AGE[100], COLLEGE[100], PROFESSION[100], ANIMAL[100], PETNAME[100];


	cout << "What is your name?" << endl;

	cin >> NAME;

	cout << endl << "How old are you?" << endl;

	cin >> AGE;

	cout << endl << "What city do you live in?" << endl;

	cin >> CITY;

	cout << endl << "Where are you at college?" << endl;

	cin >> COLLEGE;

	cout << endl << "What profession are you studying?" << endl;

	cin >> PROFESSION;

	cout << endl << "What is your favorite animal?" << endl;

	cin >> ANIMAL;

	cout << endl << "What would you name that animal if you owned it for a pet?" << endl;

	cin >> PETNAME;




	cout << endl << endl << endl << endl << "There once was a person named " << NAME << " who lived in " << CITY << "." << endl << endl;
	cout << "At the age of " << AGE << ", " << NAME << " went to college at " << COLLEGE << "." << endl << endl;
	cout << NAME << " graduated and went to work as a " << PROFESSION << ".  " << endl << endl;
	cout << "Then, " << NAME << " adopted a(n) " << ANIMAL << " named " << PETNAME << "." << endl << endl;
	cout << "Then he got married..." << endl;





	return 0;
}