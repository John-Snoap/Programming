/*  Luke Studer, Alexander Orr, John Snoap
 *  OOP In Class Assignment on Arrays
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_InClass1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array1 = new int[10, 20];
            int[][] jag1 = new int[10][];


            // These nested loops set all the values in the array to 0
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++ )
                {
                    array1[i, j] = 0;
                }
            }

            // Print the array to the console
            Console.WriteLine("Looking at the rectangular array contents");
            for (int i = 0; i < array1.GetLength(0); i++)
            {
                for (int j = 0; j < array1.GetLength(1); j++)
                {
                    Console.Write("{0} ", array1[i, j]); // Write each element to the console
                }
                Console.Write("\n");
            }

            // For the jagged array,give the first 5 rows 10 columns and the second 5 rows 20 columns
            for (int i = 0; i < 10; i++)
            {
                if(i < 5)
                {
                    jag1[i] = new int[10];
                }
                else
                {
                    jag1[i] = new int[20];
                }
            }


            
            // Set the jagged array to all 0's
            for (int i = 0; i < jag1.GetLength(0); i++)
            {
                for (int j = 0; j < jag1[i].GetLength(0); j++)
                {
                    jag1[i][j] = 0;
                }
            }

            Console.WriteLine("\nNow looking at the jagged array");
            for (int i = 0; i < jag1.GetLength(0); i++)
            {
                for (int j = 0; j < jag1[i].GetLength(0); j++)
                {

                    Console.Write("{0} ", jag1[i][j]);
                }
                Console.Write("\n");
            }
            Console.WriteLine("\nNow using foreach looop");
            foreach (int[] row in jag1)
            {
                foreach (int column in row)
                {
                    Console.Write("{0} ", column);
                }
                Console.Write("\n");
            }
            Console.WriteLine("");
        }
    }
}
