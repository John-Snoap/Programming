/*
 * https://docs.google.com/a/oc.edu/document/d/1ERC6Woxp0wJ4z-bSjxSnd4AV4P0mPMH9uKwSqsJ0qeE/edit?usp=sharing
 * John Snoap
 * Assignment 1
 * September 4, 2013
 * Object Oriented Programming
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BodyMassIndexCalculator
{
    class BodyMassIndex
    {
        static void Main(string[] args)
        {
            // declare variables
            double  weightInPounds; // the user's weight in pounds
            double  heightInInches; // the user's height in inches
            double  bodyMassIndex;  // the user's body mass index
            string  doOver;         // lets the user decide to do the program over again or not

            do
            {
                // state the title of program
                Console.WriteLine("\tThe Body Mass Index (BMI) Calculator\n"); // introductory statement

                // ask the user for his or her weight
                Console.Write("Please enter your weight in pounds (lbs):  "); // input weight statement
                weightInPounds = Convert.ToDouble(Console.ReadLine()); // store weight variable

                // ask the user for his or her height
                Console.Write("Please enter your height in inches (in):  "); // input height statement
                heightInInches = Convert.ToDouble(Console.ReadLine()); // store height variable

                // calculate the user's Body Mass Index
                bodyMassIndex = (weightInPounds * 703) / (heightInInches * heightInInches); // calculate the user's BMI

                // display the user's BMI and his or her BMI status
                Console.WriteLine("\nYour BMI is:\t{0:F1}", bodyMassIndex);
                Console.Write("You are ");

                // determine and display the user's BMI status; the values are so precise to account for rounding error
                if (bodyMassIndex < 18.45)
                    Console.WriteLine("underweight\n");
                else if (bodyMassIndex < 24.95)
                    Console.WriteLine("normal\n");
                else if (bodyMassIndex < 29.95)
                    Console.WriteLine("overweight\n");
                else
                    Console.WriteLine("obese\n");

                // display the official BMI status values for the user to easily see
                Console.WriteLine("\tBMI VALUES");
                Console.WriteLine("Underweight:\tless than 18.5");
                Console.WriteLine("Normal:\t\tbetween 18.5 and 24.9");
                Console.WriteLine("Overweight:\tbetween 25 and 29.9");
                Console.WriteLine("Obese:\t\t30 or greater\n");

                // ask the user if he or she wants to go again
                Console.Write("Do you want to do this over again? (Y/N):  "); // input do over statement
                doOver = Console.ReadLine(); // store do over variable
                Console.Clear(); // clear the screen

            } while (doOver[0] == 'Y' || doOver[0] == 'y'); // a 'Y' or a 'y' will restart the program for the user

            // tell the user bye-bye
            Console.WriteLine("Bye-bye!\n"); // end statement

        } // end Main
    } // end class BodyMassIndex
} // end namespace BodyMassIndexCalculator
