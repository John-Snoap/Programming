// John Snoap
// Lab 9
// 11/16/2010


#include <iostream>
#include <fstream>
using namespace std;

int main()
{
	char weatherInformation[3][30],
		 summerMonth[3][7] = {"June", "July", "August"};
	int month,
		day,
		rain,
		mostRain = 0,
		sunnyDays = 0,
		rainyDays = 0,
		cloudyDays = 0,
		totalSunnyDays = 0,
		totalRainyDays = 0,
		totalCloudyDays = 0;
		
		
	
	ifstream inputFile ("RainOrShine.dat");

	
	if (!inputFile)
	{
		cout << "Error opening file.\n\n";
	}

	for (month = 0; month < 3 && inputFile; month++)
	{
		for (day = 0; day < 30 && inputFile; day++)
		{
			inputFile >> weatherInformation[month][day];
		}

	}



	for (month = 0; month < 3; month++)
	{
		sunnyDays = 0;
		rainyDays = 0;
		cloudyDays = 0;

		for (day = 0; day < 30; day++)
		{
			switch (weatherInformation[month][day])
			{
				case 'S':	sunnyDays += 1;
					break;
				case 'R':	rainyDays += 1;
					break;
				default:	cloudyDays += 1;
					break;
			}

			
		}



		totalSunnyDays += sunnyDays;
		totalRainyDays += rainyDays;
		totalCloudyDays += cloudyDays;

		cout << summerMonth[month] << " had " << sunnyDays << " sunny days.\n";
		cout << summerMonth[month] << " had " << rainyDays << " rainy days.\n";
		cout << summerMonth[month] << " had " << cloudyDays << " cloudy days.\n\n";

		
		if (rainyDays > mostRain)
		{
			mostRain = rainyDays;
			rain = month;
		}
		
	}

	cout << "There were " << totalSunnyDays << " sunny days total.\n";
	cout << "There were " << totalRainyDays << " rainy days total.\n";
	cout << "There were " << totalCloudyDays << " cloudy days total.\n\n";

	cout << summerMonth[rain] << " had the most rainy days.\n\n";
	

	return 0;
}