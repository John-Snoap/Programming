/* https://drive.google.com/a/oc.edu/folderview?id=0B2KfHJWwUz_sMVBmR0lVUXIxYlE&usp=sharing
 * Joshua Mullen and John Snoap
 * Assignment 5
 * Letter Set
 * Object Oriented Programming
 * October 4, 2013
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Program_5
{
    class LetterSet
    {
        static void Main(string[] args) // LetterSet Tester
        {
            LetterSet test1 = new LetterSet();
            LetterSet test2 = new LetterSet("God said let there be light!");
            LetterSet test3 = new LetterSet();
            LetterSet test4 = new LetterSet(test2);
            LetterSet test5 = new LetterSet("abcdefghijklmnop");
            LetterSet test6 = new LetterSet("hijklmnopqrstuv");
            LetterSet test7 = new LetterSet("qrstuvwxyz");
            LetterSet test8 = new LetterSet(test5);

            Console.WriteLine(test1);
            Console.WriteLine(test2);
            Console.WriteLine(test3);
            Console.WriteLine(test4);
            Console.WriteLine("");

            test1.Insert("Z");
            test2.Insert("z");

            Console.WriteLine(test1);
            Console.WriteLine(test2);
            Console.WriteLine(test3);
            Console.WriteLine(test4);
            Console.WriteLine("");

            test1.Remove("rz");
            test2.Remove("rZ");

            Console.WriteLine(test1);
            Console.WriteLine(test2);
            Console.WriteLine(test3);
            Console.WriteLine(test4);
            Console.WriteLine("");

            test1 = test2.Copy();
            test3 = test2.Copy();

            Console.WriteLine(test1);
            Console.WriteLine(test2);
            Console.WriteLine(test3);
            Console.WriteLine(test4);
            Console.WriteLine("");

            Console.WriteLine("\nNow we shall test the unions and intersections!\n\nIntersect 5 and 6:");
            Console.WriteLine(test5.Intersect(test6));
            Console.WriteLine("\nIntersect 6 and 7");
            Console.WriteLine(test6.Intersect(test7));
            test5 = test8.Union(test6).Union(test7);
            Console.WriteLine("\nUnion 6, 7, and 8; store value in 5 and make sure 8 did not change:");
            Console.WriteLine(test5);
            Console.WriteLine(test8);
            Console.WriteLine("");
        } // end Main LetterSet Tester

        // enumeration for the class (the letters of the alphabet)
        private enum Letter { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };

        // member variables for the class
        private bool[] letterIsSet; // array of bools to tell which letters are set
        private int totalNumberOfPossibleLetters = (Enum.GetNames(typeof(Letter))).Length; // in case the enumeration must be larger later

        // default constructor
        public LetterSet()
        {
            letterIsSet = SetAllLettersToFalse();
        } // end default constructor

        // constructor with string parameter
        public LetterSet(string letters)
        {
            letterIsSet = SetAllLettersToFalse();
            Insert(letters); // set the appropriate letters to true
        } // end constructor with string parameter

        // constructor that performs a deep copy of another LetterSet object
        public LetterSet(LetterSet LetterSetWeWantToCopy)
        {
            letterIsSet = (LetterSetWeWantToCopy.Copy()).letterIsSet;
        } // end consturctor with LetterSet parameter

        // method SetAllLettersToFalse
        private bool[] SetAllLettersToFalse()
        {
            return new bool[totalNumberOfPossibleLetters]; // bools automatically start out as false
        } // end method SetAllLettersToFalse

        // method Insert
        public void Insert(string letters)
        {
            int numberOfLetter; // number that will correspond to a letter

            foreach(char letter in letters) // go through all the individual characters in the string
            {
                numberOfLetter = ((int)Char.ToUpper(letter) - (int)'A'); // convert character to an int we can use

                if (((numberOfLetter >= 0) && (numberOfLetter < totalNumberOfPossibleLetters)) && !letterIsSet[numberOfLetter])
                {
                    letterIsSet[numberOfLetter] = true; // set the bool of the letter to true
                } // end if
            } // end foreach loop
        } // end method Insert

        // method Remove
        public void Remove(string letters)
        {
            int numberOfLetter; // number that will correspond to a letter

            foreach (char letter in letters) // go through all the individual characters in the string
            {
                numberOfLetter = ((int)Char.ToUpper(letter) - (int)'A'); // convert character to an int we can use

                if (((numberOfLetter >= 0) && (numberOfLetter < totalNumberOfPossibleLetters)) && letterIsSet[numberOfLetter])
                {
                    letterIsSet[numberOfLetter] = false; // set the bool of the letter to false
                } // end if
            } // end foreach loop
        } // end method Remove

        // method Intersect
        public LetterSet Intersect(LetterSet that)
        {
            LetterSet theOther = new LetterSet();

            for (int i = 0; i < totalNumberOfPossibleLetters; i++)
            {
                if (this.letterIsSet[i] && that.letterIsSet[i])
                {
                    theOther.letterIsSet[i] = true;
                } // end if
            } // end for loop

            return theOther;
        } // end method Intersect

        // method Union
        public LetterSet Union(LetterSet that)
        {
            LetterSet theOther = new LetterSet();

            for (int i = 0; i < totalNumberOfPossibleLetters; i++)
            {
                if (this.letterIsSet[i] || that.letterIsSet[i])
                {
                    theOther.letterIsSet[i] = true;
                } // end if
            } // end for loop

            return theOther;
        } // end method Union

        // method Copy
        public LetterSet Copy()
        {
            // use nested shallow copies to achieve a deep copy
            LetterSet other = (LetterSet)MemberwiseClone(); // copy the class (member variable arrays are not real copies)
            other.letterIsSet = (bool[])letterIsSet.Clone(); // copy the member variable array of the class
            return other;
        } // end method Copy

        // override method ToString
        public override string ToString()
        {
            string returnString = "{ "; // start the string with an opening bracket
            Letter letter; // enumeration to get the letter

            for (int i = 0; i < totalNumberOfPossibleLetters; i++) // go through the alphabet
            {
                if (letterIsSet[i]) // see if we have any of that letter
                {
                    letter = (Letter)i; // convert the index to the appropriate letter i.e. 0 will be A, 1 will be B
                    returnString += letter.ToString(); // convert the enum letter to a string i.e. enum A will become "A"
                    returnString += ", "; // add a comma and a space at the end of each letter
                } // end if
            } // end for loop

            if (returnString.Length > 2) // this is to remove the last ", " so there is no trailing comma
            {
                returnString = returnString.Remove((returnString.Length - 2)); // remove the last trailing comma
                returnString += " "; // add a spce for aesthetics
            } // end if

            returnString += "}"; // finish the string with a closing bracket

            return returnString;
        } // end override method ToString
    } // end class LetterSet
} // end namespace Program_5
