using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Student
    {
        public String name { get; set; }
        public String gpa { get; set; }

        public Student (String n, String g) 
        {
            name = n;
            gpa = g;
        }



        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            students.Add(new Student("Bob", "3.6"));
            students.Add(new Student("Sue", "2.4"));
            students.Add(new Student("Jim", "3.1"));
            students.Add(new Student("Betty", "4.0"));
            displayStudent(students);
            students.Insert(1, new Student("Fred", "2.0"));
            displayStudent(students);
            Console.Write("Enter a student name to delete: ");
            String input;
            input = Console.ReadLine();
            int index = -1;
            for (int i = 0; i < students.Count; i++)
            {
                if (input.Equals(students[i].name))
                {
                    index = i;
                }
            }
            if(index != -1)
                students.RemoveAt(index);
            displayStudent(students);
            students.Clear();
            displayStudent(students);
        }


        public static void displayStudent(List<Student> list)
        {

            foreach (Student student in list)
            {
                Console.WriteLine(student.name + " " + student.gpa);
            }
            Console.WriteLine();
        }



    }
}