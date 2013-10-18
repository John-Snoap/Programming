// =============================================================================
// John Snoap
// 11/21/2010
// Assignment 5
// =============================================================================

#include <iostream>
#include <fstream>
#include <cstring>
using namespace std;

// Global Constant for easy change if not big enough
const int SIZE = 128;

// Function Prototypes
void loadList				(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int, int &);

void addPerson				(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int &);
void deletePerson			(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int &);
void findAndDisplayPerson	(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int &);
void listAllPeople			(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int &);
void saveList				(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int);
bool exitProgram			();
void invalidInput			();

int findContact				(char[][SIZE], int);
void displayContact			(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int);
void deleteContact			(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int &, int);
void emailContact			(char[]);
void deleteOrEmail			(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int &, int);

void alphabetizeContacts	(char[][SIZE], char[][SIZE], char[][SIZE], char[][SIZE], int);



int main()
{
	bool persevere = true;	// Used to exit when done
	char choice[SIZE];	// Allows the user to select options from the menu
	const int MAXIMUM_CONTACTS = 100;		// Determines how many contacts can be stored

	char firstName[MAXIMUM_CONTACTS][SIZE],	// These arrays hold what they are named
		lastName[MAXIMUM_CONTACTS][SIZE],
		phone[MAXIMUM_CONTACTS][SIZE],
		email[MAXIMUM_CONTACTS][SIZE];

	int count = 0;	// Keeps the arrays in parallel


	// Loads a previously saved contact list for manipulation
	loadList (firstName, lastName, phone, email, MAXIMUM_CONTACTS, count);



	while (persevere)
	{
		//////// Display the Menu /////////////////////////////////////////////

		system ("cls");  // Clears the screen

		cout << "\tMain Menu\n\n";
		cout << "A - Add Person\n";
		cout << "D - Delete Person\n";
		cout << "F - Find and Display Person\n";
		cout << "L - List All People\n";
		cout << "S - Save List\n";
		cout << "E - Exit\n\n";
		cout << "Enter Choice:  ";

		cin >> choice;

		switch (choice[0])
		{
			case 'A':	// Adds a person to the contact list
			case 'a':	addPerson (firstName, lastName, phone, email, count);
						break;
			case 'D':	// Deletes a person from the contact list
			case 'd':	deletePerson (firstName, lastName, phone, email, count);
						break;
			case 'F':	// Displays a person picked out from the contact list
			case 'f':	findAndDisplayPerson (firstName, lastName, phone, email, count);
						break;
			case 'L':	// Displays a list of all contacts
			case 'l':	listAllPeople (firstName, lastName, phone, email, count);
						break;
			case 'S':	// Saves the list of all contacts to a file named "book.txt"
			case 's':	saveList (firstName, lastName, phone, email, count);
						break;
			case 'E':	// Exits the program
			case 'e':	persevere = exitProgram ();
						break;
			default:	// Displays an invalid input message
						invalidInput ();
						break;
		}
	}

	return 0;
}



//**********************************************************
// The function loadList takes all of the contacts' array  *
// locations, the maximum possible contacts, and the       *
// counter as parameters.  It then loads them from a text  *
// file named "book.txt" to be manipulated if so desired.  *
//**********************************************************
void loadList (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
				char email[][SIZE], int MAXIMUM_CONTACTS, int &count)
{
	ifstream contactList;

	contactList.open ("book.txt");  // Opens the file the information will be loaded from

	// Fills the arrays with each contacts' info from each line, and updates the
	// counter to keep track of the total number of contacts.
	for (int person = 0; person < MAXIMUM_CONTACTS
		&& contactList >> firstName[person] >> lastName[person] >> phone[person] >> email[person];
		person++)
	{
		count++;
	}

	contactList.close();

	// Alphabetizes the list in case it is not in order to begin with
	alphabetizeContacts (firstName, lastName, phone, email, count);

}



//***********************************************************
// The function addPerson takes what it is updating and     *
// where it stores the information as parameters.  It adds  *
// one contact to the list and then updates the location    *
// so that the newly entered contact is not overridden.     *
//***********************************************************
void addPerson (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
				char email[][SIZE], int &totalContacts)
{
	char isInfoCorrect[4];  // Allows the user to make sure the information he or she
							// added is correct before he or she moves on

	do
	{
		///////// Goes through the process of adding a new contact /////////////

		system ("cls");  // Clears the screen
		
		cout << "Enter the contact's first name and last name\n";
		cout << "separated by a space.  Ex:  \"John Smith\"\n\n";
		cout << "Fistname Lastname:  ";

		cin >> firstName[totalContacts] >> lastName[totalContacts];  // The user inputs the contact's name

		cout << "\nEnter the contact's phone number with no spaces.\n";
		cout << "For example:  1(234)567-8901\n\n";
		cout << "Enter number:  ";

		cin >> phone[totalContacts];		// The user inputs the contact's phone number

		cout << "\nEnter the contact's email address:  ";

		cin >> email[totalContacts];		// The user inputs the contact's email address


		displayContact (firstName, lastName, phone, email, totalContacts);

		cout << "\n\nIs this information correct?  (Y/N):  ";

		cin >> isInfoCorrect;		// The user determines if the information is correct

	} while (isInfoCorrect[0] == 'N' || isInfoCorrect[0] == 'n');  // Gives the option to re-enter the info

	totalContacts++;

	alphabetizeContacts (firstName, lastName, phone, email, totalContacts);  // Alphabetizes the contacts
}



//*********************************************************************
// The function deletePerson takes all the contacts' array locations  *
// and the total number of contacts as parameters.  It then searches  *
// for a person, and deletes their information from the arrays.       *
//*********************************************************************
void deletePerson (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
					char email[][SIZE], int &totalContacts)
{
	int location;  //Allows the user to search for a contact
	char deletePerson[4];

	location = findContact (lastName, totalContacts);  // Finds the contact the user wants to delete

	if (location != -1)
	{
		displayContact (firstName, lastName, phone, email, location);  // Displays the contact

		cout << "\n\nDo you wish to delete this contact?  (Y/N):  ";

		cin >> deletePerson;  // If the user says yes then it deletes them

		if (deletePerson[0] == 'Y' || deletePerson[0] == 'y')
		{
			deleteContact (firstName, lastName, phone, email, totalContacts, location);
		}
	}

}



//****************************************************************
// The function findAndDisplayPerson takes all the contacts'     *
// info and the total number of contacts as parameters.          *
// It then allows the user to search for a person by last name,  *
// and diplsays the contact.  It also allows the user            *
// to delete the contact or send an email to the contact.        *
//****************************************************************
void findAndDisplayPerson (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
							char email[][SIZE], int &totalContacts)
{
	int location;	// Allows the user to search for a contact
	char sendEmail[4];
		
	location = findContact (lastName, totalContacts);  // Finds the contact
	
	displayContact (firstName, lastName, phone, email, location);  // Displays the contact
	
	// Allows the user to delete the contact or email the contact
	deleteOrEmail (firstName, lastName, phone, email, totalContacts, location);

}



//***********************************************************
// The function listAllContacts takes all of the contacts'  *
// array locations and the total number of current          *
// contacts as parameters.  It then displays all of the     *
// contacts in the contact list, allows the user to select  *
// a contact, and then allows the user to delete the        *
// contact or send an email to the contact.                 *
//***********************************************************
void listAllPeople (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
					char email[][SIZE], int &totalContacts)
{
	int contact;  // Allows the user to select a contact
	
	system ("cls");  // Clears the screen

	////////////// Displays all the Contacts //////////////////////////////////////////
	
	for (int location = 0; location < totalContacts; location++)
	{
		cout << location + 1 << ".\t" << firstName[location] << " " << lastName[location] << endl;
		cout << "\t\t" << phone[location] << endl;
		cout << "\t\t" << email[location] << "\n\n";
	}
	
	cout << "To select a person, enter the number (without the period) of the person.\n";
	cout << "To exit the list, enter 0.\n\n";
	cout << "Contact #:  ";

	cin >> contact;

	contact--;

	// Allows the user to delete or email the contact
	deleteOrEmail (firstName, lastName, phone, email, totalContacts, contact);
	
}



//*****************************************************
// The function saveList takes all of the contacts'   *
// info and the total number of current contacts      *
// as parameters.  It then saves them in a text file  *
// named "book.txt" to be read from and used later.   *
//*****************************************************
void saveList (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
				char email[][SIZE], int totalContacts)
{
	ofstream contactList;

	contactList.open ("book.txt");  // Opens the file the information will be saved to

	// Fills the file with each contacts' info on one line, each item separated by a space.
	for (int person = 0; person < totalContacts; person++)
	{
		contactList << firstName[person] << " " << lastName[person] << " " << phone[person]
					<< " " << email[person] << endl;
	}

	contactList.close();

	system ("cls");
	cout << "Contact list saved.\n\n";  // Displays a confirmation message
	system ("pause");

}



//***********************************************************************
// The function exitProgram takes a bool as a parameter that controls   *
// the while loop of the main function.  It causes the program to end.  *
//***********************************************************************
bool exitProgram ()
{
	// Returns false to set the varialbe in the main while loop to false
	// and displays a message to let the user know that it is exiting
	system ("cls");
	cout << "Exiting\n\n";
	return false;
}



//**********************************************************************
// The function invalidInput displays a message letting the user know  *
// that he or she entered a character that does not do anything.       *
//**********************************************************************
void invalidInput ()
{
	system ("cls");
	cout << "Invalid input, try again\n\n";
	system ("pause");
}



//*************************************************************
// The function findContact takes the lastName array and the  *
// total number of contacts as parameters.  It then returns   *
// the location of the contact.                               *
//*************************************************************
int findContact (char lastName[][SIZE], int totalContacts)
{
	char searchName[32];  // Allows the user to search for a contact by last name
	int location = -1;

	system ("cls");

	cout << "Enter the last name of the contact:  ";

	cin >> searchName;

	for (int i = 0; i < totalContacts; i++)  // Searches for the last name entered
	{
		if (strcmp (lastName[i], searchName) == 0)
		{
			location = i;
		}
		
	}

	if (location == -1)  // Displays an error message if the last name is not found
	{
		cout << "\nThere is no contact found with that last name.\n\n";
		system ("pause");
	}
	
	return location;
}



//*********************************************************************
// The function displayContact takes all the contacts' information,   *
// the location of one contact and the total number of contacts as    *
// parameters.  It then displays the contact found at that location.  *
//*********************************************************************
void displayContact (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
						char email[][SIZE], int location)
{
	if (location > -1 && location < 100)  // Checks to make sure the location is valid
	{
		system ("cls");

		cout << "Name :  " << firstName[location] << " " << lastName[location];
		cout << "\nPhone:  " << phone[location];
		cout << "\nEmail:  " << email[location];
	}

}



//********************************************************************
// The function deleteContact takes all the contacts' information,   *
// the location of one contact and the total number of contacts as   *
// parameters.  It then deletes the contact found at that location,  *
//  and moves everybody else in the list below it up.                *
//********************************************************************
void deleteContact (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
					char email [][SIZE], int &totalContacts, int location)
{
	// Starts at the given location of the contact to be deleted and copies
	// over the contact.  Also makes sure that the location given is valid.
	if (location > -1 && location < totalContacts)
	{
		for (location; location < totalContacts; location++)
		{
			strcpy (firstName[location], firstName[location + 1]);
			strcpy (lastName[location], lastName[location + 1]);
			strcpy (phone[location], phone[location + 1]);
			strcpy (email[location], email[location + 1]);
		}

		totalContacts--;  // There is one less contact

		system ("cls");
		cout << "Contact Deleted\n\n";  // Displays a confirmation message
		system ("pause");
	}

}



//*************************************************************
// The function emailContact takes the email array's address  *
// from one given contact as a parameter.  It then opens up   *
// Outlook with the email address in the To: box.             *
//*************************************************************
void emailContact (char email[])
{
	char cmdline[SIZE] =
		"\"C:/Program Files/Microsoft Office/Office12/Outlook.exe\" /c ipm.note /m ";

	strcat (cmdline, email);  // Adds the email address to the line so Outlook
							  // will start up with the address in the To:  box
	system (cmdline);
}



//************************************************************************
// The function deleteOrEmail takes all the contacts' information,       *
// the location of one contact and the total number of contacts as       *
// parameters.  It then allows the user to delete or email the contact.  *
//************************************************************************
void deleteOrEmail (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
					char email[][SIZE], int &totalContacts, int location)
{
	char person[7];
	
	if (location > -1 && location < 100)  // Checks to make sure the input is valid
	{
		displayContact (firstName, lastName, phone, email, location);

		cout << "\n\nTo delete this person, enter - D\n";
		cout << "To email this person, enter - M\n";
		cout << "To exit back to the main menu, enter - E\n\n";
		cout << "What do you wish to do?:  ";

		cin >> person;

		switch (person[0])
		{
		case 'D':	// delete the contact
		case 'd':	deleteContact (firstName, lastName, phone, email, totalContacts, location);
					break;
		case 'M':	// email the contact
		case 'm':	emailContact (email[location]);
					break;
		case 'E':	// drop out to the main menu
		case 'e':	break;

		default:	// Display an invalid input message
					invalidInput ();
					break;
		}
	}

}



//***********************************************************
// The function alphabetizeContacts takes all of the        *
// contacts' info and the total number of current contacts  *
// as parameters.  It performs a bubble sort on all of      *
// the contacts to organize the contacts by last name.      *
//***********************************************************
void alphabetizeContacts (char firstName[][SIZE], char lastName[][SIZE], char phone[][SIZE],
							char email[][SIZE], int totalContacts)
{
	bool swap;		// lets the computer know when it is done sorting

	char temporary[SIZE];	// creates a temporary holding space for each array
							// so it can be safely moved to another location
							// without being overridden

	do
	{
		swap = false;		// If none of the names need to be sorted, then it is done

		for (int name = 0; name < (totalContacts - 1); name++)
		{
			if (strcmp (lastName[name], lastName[name + 1]) > 0)
			{
				strcpy (temporary, lastName[name]);
				strcpy (lastName[name], lastName[name + 1]);
				strcpy (lastName[name + 1], temporary);

				strcpy (temporary, firstName[name]);
				strcpy (firstName[name], firstName[name + 1]);
				strcpy (firstName[name + 1], temporary);

				strcpy (temporary, phone[name]);
				strcpy (phone[name], phone[name + 1]);
				strcpy (phone[name + 1], temporary);

				strcpy (temporary, email[name]);
				strcpy (email[name], email[name + 1]);
				strcpy (email[name + 1], temporary);
				
				swap = true;	// If the names had to be sorted, it's not done
			}
		}
	} while (swap);

}