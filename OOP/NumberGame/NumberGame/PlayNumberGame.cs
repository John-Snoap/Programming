/* John Snoap
 * Assignment 3
 * Play The Number Game
 * Object Oriented Programming
 * September 16, 2013
 * Main Folder (all my assignments):  https://drive.google.com/a/oc.edu/folderview?id=0B2KfHJWwUz_sMVBmR0lVUXIxYlE&usp=sharing
 * Sub Folder (just this assignment):  https://drive.google.com/a/oc.edu/folderview?id=0B2KfHJWwUz_saENiaUhCWkJSYkk&usp=sharing
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGame
{
    class PlayNumberGame
    {
        static void Main(string[] args)
        {
            NumberGame trialGame = new NumberGame(); // create a new instance of the number game
            int distanceFromCorrectNumber; // a variable to hold how far away the player is from the correct number
            int previousDistanceFromCorrectNumber; // a variable to hold the previous distance from the correct number
            string redo; // allows the user to play the game again or not

            do
            {
                // tell the player useful information and let the player start playing
                Console.WriteLine("The Number Game\n\nI am thinking of an integer between 1 and 100.\nCan you guess my number?\n");
                Console.Write("Enter your first guess:  ");
                distanceFromCorrectNumber = trialGame.GuessMyNumber(Convert.ToInt32(Console.ReadLine())); // let the user guess
                previousDistanceFromCorrectNumber = distanceFromCorrectNumber; // set the previous distance variable
                Console.WriteLine(""); // add an extra line to the screen

                while (distanceFromCorrectNumber != 0)
                {
                    if (trialGame.GetGuessCount > 1)
                    {
                        if (Math.Abs(distanceFromCorrectNumber) > Math.Abs(previousDistanceFromCorrectNumber))
                            Console.WriteLine("\nYou're getting colder!");
                        else if (Math.Abs(distanceFromCorrectNumber) < Math.Abs(previousDistanceFromCorrectNumber))
                            Console.WriteLine("\nYou're getting warmer!");
                        else
                            // this CAN be a VERY useful hint
                            Console.WriteLine("\nYou are not closer nor are you farther away than you were before!");
                    }

                    if (distanceFromCorrectNumber > 0)
                        Console.Write("Your guess was too high!  Guess again:  ");
                    else
                        Console.Write("Your guess was too low!  Guess again:  ");

                    previousDistanceFromCorrectNumber = distanceFromCorrectNumber; // move distance to previous distance
                    distanceFromCorrectNumber = trialGame.GuessMyNumber(Convert.ToInt32(Console.ReadLine())); // let the user guess
                }

                Console.Clear(); // clear the screen
                Console.WriteLine("Congratulations!!!\nYou guessed my number in {0} tries!!!\nYou Win!!!\n", trialGame.GetGuessCount);
                trialGame.NumberGameReset(); // reset the game
                Console.Write("Would you like to play again?  (Y/N):  "); // ask the player if he or she wants to play again
                redo = Console.ReadLine(); // store the player's input
                Console.Clear(); // clear the screen

            } while (redo[0] == 'Y' || redo[0] == 'y'); // a 'Y' or a 'y' will restart the game for the player

            // tell the player bye-bye
            Console.WriteLine("Bye-bye!\n"); // end statement

        } // end Main
    } // end class PlayNumberGame
} // end namespace NumberGame
