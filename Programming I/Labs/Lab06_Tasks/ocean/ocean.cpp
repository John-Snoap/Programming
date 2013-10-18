// =============================================================================
// John Snoap
// 10/14/2010
// Lab 6
// =============================================================================

#include <iostream>
#include <iomanip>
#include <cmath>
#include <fstream>
#include <cstring>
#include <cstdlib>
#include <ctime>
using namespace std;

int main()
{
	 
	for (double rise = 1.5, years = 1;  years <= 25;  rise += 1.5, years++)
	{
		cout << "The ocean will rise " << rise << " millimeters " << years << " years\n";
	}



	return 0;
}



