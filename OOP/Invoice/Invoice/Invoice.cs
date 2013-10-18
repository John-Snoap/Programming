/* John Snoap
 * Assignment 2
 * Invoice Class
 * Object Oriented Programming
 * September 11, 2013
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invoice
{
    class Invoice
    {
        // DATA and PROPERTIES

        private decimal unitPrice;  // price per part
        private int quantity;  // the amount or number of the part
        public string PartNumber { get; set; }  // auto-implemented property PartNumber
        public string PartDescription { get; set; }  // auto-implemented property PartDescription

        public decimal UnitPrice
        {
            get
            {
                return unitPrice;
            } // end get
            set
            {
                if (value >= 0)  // make sure the value is non-negative
                    unitPrice = value;
                else
                {
                    Console.WriteLine("\nInvalid Change Of Unit Price\n");
                    Console.WriteLine("The unit price for invoice number {0}, {1}", PartNumber, PartDescription);
                    Console.WriteLine("has been left unchanged\n");
                }
            } // end set
        } // end property UnitPrice

        public int Quantity
        {
            get
            {
                return quantity;
            } // end get
            set
            {
                if (value >= 0)  // make sure the value is non-negative
                    quantity = value;
                else
                {
                    Console.WriteLine("\nInvalid Change Of Quantity\n");
                    Console.WriteLine("The quantity for invoice number {0}, {1}", PartNumber, PartDescription);
                    Console.WriteLine("has been left unchanged\n");
                }
            } // end set
        } // end property Quantity


        // CONSTRUCTORS

        // default constructor
        // part number must be given
        // part description must be given
        public Invoice(string itemNumber, string itemDescription)
        {
            PartNumber = itemNumber;  // set PartNumber to itemNumber
            PartDescription = itemDescription;  // set PartDescription to itemDescription
            // the default unitPrice is $0.0M because the default value for a decimal variable is $0.0M
            // the default quantity is 0 because the default value for an int is 0
        } // end default constructor

        // first alternate constructor
        // part number must be given
        // part description must be given
        // unit price must be given
        public Invoice(string itemNumber, string itemDescription, decimal itemPrice)
        {
            PartNumber = itemNumber;  // set PartNumber to itemNumber
            PartDescription = itemDescription;  // set PartDescription to itemDescription

            if (itemPrice >= 0)  // make sure the value is non-negative
                unitPrice = itemPrice;  // set unitPrice to itemPrice
            else
            {
                // the default unitPrice is $0.0M because the default value for a decimal variable is $0.0M
                Console.WriteLine("\nInvalid Unit Price Initialization\n");
                Console.WriteLine("The unit price for invoice number {0}, {1}", PartNumber, PartDescription);
                Console.WriteLine("has been set to $0.00\n");
            }

            // the default quantity is 0 because the default value for an int is 0
        } // end first alternate constructor

        // second (final) alternate constructor
        // part number must be given
        // part description must be given
        // unit price must be given
        // quantity of part must be given
        public Invoice(string itemNumber, string itemDescription, decimal itemPrice, int itemQuantity)
        {
            PartNumber = itemNumber;  // set PartNumber to itemNumber
            PartDescription = itemDescription;  // set PartDescription to description

            if (itemPrice >= 0)  // make sure the value is non-negative
                unitPrice = itemPrice;  // set unitPrice to itemPrice
            else
            {
                // the default unitPrice is $0.0M because the default value for a decimal variable is $0.0M
                Console.WriteLine("\nInvalid Unit Price Initialization\n");
                Console.WriteLine("The unit price for invoice number {0}, {1}", PartNumber, PartDescription);
                Console.WriteLine("has been set to $0.00\n");
            }

            if (itemQuantity >= 0)  // make sure the value is non-negative
                quantity = itemQuantity;  // set quantity to itemQuantity
            else
            {
                // the default quantity is 0 because the default value for an int is 0
                Console.WriteLine("\nInvalid Quantity Initialization\n");
                Console.WriteLine("The quantity for invoice number {0}, {1}", PartNumber, PartDescription);
                Console.WriteLine("has been set to 0\n");
            }
        } // end second (final) alternate constructor


        // METHODS

        public decimal GetInvoiceAmount()
        {
            return (unitPrice * quantity);  // the invoice amount is equal to the unitPrice times the quantity
        } // end GetInvoiceAmount Method

        public void DisplayInvoice()
        {
            Console.WriteLine("\nPart Number:  {0}", PartNumber);
            Console.WriteLine("Description:  {0}", PartDescription);
            Console.WriteLine("Unit Price:  {0:C}", UnitPrice);
            Console.WriteLine("Quantity:  {0}", Quantity);
            Console.WriteLine("Invoice Amount:  {0:C}\n", GetInvoiceAmount());
        } // end displayInvoice Method
    } // end Invoice Class
} // end Invoice namespace
