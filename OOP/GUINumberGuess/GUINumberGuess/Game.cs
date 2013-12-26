/* John Snoap
 * Assignment 4
 * Number Game
 * Object Oriented Programming
 * September 23, 2013
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGame
{
    class Game
    {
        private const int MAX = 10000; // the max value (inclusive) the game will play
        private const int MIN = 1; // the min value (inclusive) the game will play
        private int myNumber; // the games random number
        private int guessCount; // the current guess count
        private Random number = new Random(); // a random class for the game to use

        // property GetGuessCount allows the user to know how many guesses have been attempted
        public int GetGuessCount
        {
            get
            {
                return guessCount;
            } // end get
        } // end property GetGuessCount

        // constructor
        // allow the user to specify the range of the game
        public Game()
        {
            NumberGameReset();
        } // end constructor NumberGame

        // method GuessMyNumber allows user to guess the game's number
        // and returns a positive number if the guess was too high,
        // a negative number if the guess was too low,
        // and a zero if the guess was correct
        public int GuessMyNumber(int userGuess)
        {
            int positionFromMyNumber; // variable to hold the position the user guess is from my number

            guessCount++; // increment the guess count

            positionFromMyNumber = userGuess - myNumber; // calculate the position the user guess is from my number (high = pos) (low = neg)

            return positionFromMyNumber; // return the position the user guess is from my number
        } // end method GuessMyNumber

        // method NumberGameReset resets the game for a new runthrough
        public void NumberGameReset()
        {
            myNumber = number.Next(MIN, (MAX + 1)); // get a new random integer between the min and the max (both inclusive)
            guessCount = 0; // set the guess count to 0
        } // end method NumberGameReset
    } // end class NumberGame
} // end namespace NumberGame
