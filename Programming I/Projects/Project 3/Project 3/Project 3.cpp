// =============================================================================
// John Snoap
// 10/15/2010
// Project 3
// =============================================================================

#include <iostream>
#include <iomanip>
#include <cstdlib>
#include <ctime>
using namespace std;

int main()
{
	unsigned int number1;				// The first random integer between 0 and 9
	unsigned int number2;				// The second random integer between 0 and 9
	unsigned int randomNumber;			// A random number to determine a response to give
	int answer;							// number1 * number2
	int numberOfAnswersCorrect = 0;		// The total number of questions answered correctly
	int numberOfAnswersIncorrect = 0;	// The total number of questions answered incorrectly
	int numberOfQuestions = 0;			// The total number of questions
	double percentCorrect;				// The percent of correctly answered questions
	

	cout << "\tWhy hello there eager student!\n";
	cout << "I am going to help you learn your multiplication tables!!!\n\n";
	cout << "\tI will ask you a question, let you answer,\n";
	cout << "and tell you if you get it right or wrong.\n";
	cout << "If you get it wrong, you can keep answering until you get it right!\n";
	cout << "That way you will always be able to find out the correct answer!\n";
	cout << "When you are done learning, enter -1 for your answer\n";
	cout << "and I will show you your results!\n\n";
	cout << "Good-luck!  Let's get started.\n\n";
	
	srand(time(0));
	number1 = rand() % 10;
	number2 = rand() % 10;

	cout << "What is " << number1 << " X " << number2 << " ?  ";

	cin >> answer;

	//////////////////// Loop of Multiplication Questions //////////////////////////////////

	while (answer != -1)
	{
		numberOfQuestions ++;

		/////////////////////////// If Answer is Correct ////////////////////////////////
		
		if (answer == (number1 * number2))
		{
			numberOfAnswersCorrect ++;
			randomNumber = rand() % 4;

			switch (randomNumber)
			{
				case 0:		cout << "\tYour mind is keen.\n\n";
							break;
				case 1:		cout << "\tCorrectomundo!\n\n";
							break;
				case 2:		cout << "\tWoW!  You're a genius!\n\n";
							break;
				default:	cout << "\tYou're brilliant!  Keep on going!\n\n";
			}

			number1 = rand() % 10;
			number2 = rand() % 10;
		}

		/////////////////////////// If Answer is Incorrect ////////////////////////////////

		else
		{
			numberOfAnswersIncorrect ++;
			randomNumber = rand() % 4;

			switch (randomNumber)
			{
				case 0:		cout << "\tNope.  Try again.\n\n";
							break;
				case 1:		cout << "\tSorry, that answer is not correct.\n\n";
							break;
				case 2:		cout << "\tUmm... give it another go.\n\n";
							break;
				default:	cout << "\tDing Dong!  You're Wrong.\n\n";
			}
		}

		cout << "What is " << number1 << " X " << number2 << " ?  ";

		cin >> answer;
	}

	/////////////////////////////// Display Results /////////////////////////////////////////

	if (numberOfQuestions == 0)  // Don't divide by zero
	{
		cout << "\n\nYour results are:  \n\n";
		cout << setw(28) << left << "Answers   Correct:  "				<< setw(8)	<< right << numberOfAnswersCorrect		<< endl;
		cout << setw(28) << left << "Answers Incorrect:  "				<< setw(8)	<< right << numberOfAnswersIncorrect	<< endl;
		cout << setw(28) << left << "Total Number of Questions:  "		<< setw(8)	<< right << numberOfQuestions			<< endl;
		cout << setw(28) << left << "Percent Correct:  "				<< setw(9)	<< right << "0.00%"				<< endl << endl;
	}

	else
	{
		percentCorrect = ((numberOfAnswersCorrect / static_cast<double>(numberOfQuestions)) * 100);
	
		cout << "\n\nYour results are:  \n\n";
		cout << setw(28) << left << "Answers   Correct:  "				<< setw(8)	<< right << numberOfAnswersCorrect		<< endl;
		cout << setw(28) << left << "Answers Incorrect:  "				<< setw(8)	<< right << numberOfAnswersIncorrect	<< endl;
		cout << setw(28) << left << "Total Number of Questions:  "		<< setw(8)	<< right << numberOfQuestions			<< endl;
		cout << fixed << showpoint << setprecision(2);
		cout << setw(28) << left << "Percent Correct:  "				<< setw(8)	<< right <<	percentCorrect << "%\n\n";
	}

	return 0;
}

