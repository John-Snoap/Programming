/* John Snoap
 * Assignment 2
 * Invoice Test
 * Object Oriented Programming
 * September 11, 2013
 * https://drive.google.com/a/oc.edu/folderview?id=0B2KfHJWwUz_sZG5MY2J0Y1RtYnc&usp=sharing
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invoice
{
    class InvoiceTest
    {
        static void Main(string[] args)
        {
            Invoice goodTest1 = new Invoice("1", "First Good Test");  // create Invoice object using default constructor
            Invoice goodTest2 = new Invoice("2", "Second Good Test", 10.0M);  // create Invoice object using first alternate constructor
            Invoice goodTest3 = new Invoice("3", "Third Good Test", 500, 5);  // create Invoice object using second alternate constructor
            Invoice errorTest1 = new Invoice("4", "First Error Test", -50);  // create an error to make sure error checking works
            Invoice errorTest2 = new Invoice("5", "Second Error Test", 50, -20);  // create an error to make sure error checking works
            Invoice errorTest3 = new Invoice("6", "Thrid Error Test", -50, -20);  // create an error to make sure error checking works

            // test set UnitPrice and set Quantity
            goodTest1.DisplayInvoice();  // display
            goodTest1.UnitPrice = 20.0M;  // set UnitPrice
            goodTest1.DisplayInvoice();  // display
            goodTest1.Quantity = 1;  // set Quantity
            goodTest1.DisplayInvoice();  // display

            // test get Quantity and get UnitPrice
            goodTest2.DisplayInvoice();  // display
            goodTest2.Quantity = goodTest1.Quantity;  // get Quantity1 and set Quantity2
            goodTest2.DisplayInvoice();  // display
            goodTest2.UnitPrice = goodTest1.UnitPrice;  // get UnitPrice1 and set UnitPrice2
            goodTest2.DisplayInvoice();  // display

            // show the thrid good test
            goodTest3.DisplayInvoice();  // display

            // test the error case for setting UnitPrice and Quantity
            errorTest1.DisplayInvoice();  // display
            errorTest1.Quantity = -10;  // set Quantity (create an error)
            errorTest1.UnitPrice = -5.00M;  // set UnitPrice (create an error)
            errorTest1.DisplayInvoice();  // display

            // test set UnitPrice and set Quantity
            errorTest1.Quantity = 10;  // set Quantity (no error)
            errorTest1.UnitPrice = 5.00M;  // set UnitPrice (no error)

            // test set PartNumber and set PartDescription
            errorTest1.PartNumber = "7";  // set PartNumber
            errorTest1.PartDescription = "First Error Test Fixed";  // set PartDescription
            errorTest1.DisplayInvoice();  // display

            errorTest2.DisplayInvoice();  // display

            // test set UnitPrice and set Quantity
            errorTest2.Quantity = 10;  // set Quantity (no error)
            errorTest2.UnitPrice = 5.00M;  // set UnitPrice (no error)

            // test set PartNumber and set PartDescription
            errorTest2.PartNumber = "8";  // set PartNumber
            errorTest2.PartDescription = "Second Error Test Fixed";  // set PartDescription
            errorTest2.DisplayInvoice();  // display

            errorTest3.DisplayInvoice();  // display

            // test set UnitPrice and set Quantity
            errorTest3.Quantity = 10;  // set Quantity (no error)
            errorTest3.UnitPrice = 5.00M;  // set UnitPrice (no error)

            // test set PartNumber and set PartDescription
            errorTest3.PartNumber = "9";  // set PartNumber
            errorTest3.PartDescription = "Third Error Test Fixed";  // set PartDescription
            errorTest3.DisplayInvoice();  // display

            // test every get (I know this will work because it works inside my class)
            Console.WriteLine("\nPart Number:  {0}", goodTest1.PartNumber);
            Console.WriteLine("Description:  {0}", goodTest1.PartDescription);
            Console.WriteLine("Unit Price:  {0:C}", goodTest1.UnitPrice);
            Console.WriteLine("Quantity:  {0}", goodTest1.Quantity);
            Console.WriteLine("Invoice Amount:  {0:C}\n", goodTest1.GetInvoiceAmount());
        } // end Main
    } // end InvoiceTest Class
} // end Invoice namespace
