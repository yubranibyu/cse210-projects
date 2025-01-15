using System;

class Program
{ 
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string usergrade = Console.ReadLine();
        int x = int.Parse(usergrade);
        string letter = "";
        string gradeSign = "";
        if (letter != "F")
        {
            int lastDigit = x % 10;

            if (lastDigit >= 7)
            {
                gradeSign = "+";
            }
            else if (lastDigit < 3)
            {
                gradeSign = "-";
            }
        }
        if (x >= 90)
        {
            letter = "A";
            Console.WriteLine($"Your grade is {gradeSign}{letter}");
        }
        else if (x >= 80)
        {   
            letter = "B";
            Console.WriteLine($"Your grade is {gradeSign}{letter}");
        }
        else if (x >= 70)
        {   
            letter = "C";
            Console.WriteLine($"Your grade is {gradeSign}{letter}");
        }
        else if (x >= 60)
        {   
            letter = "D";
            Console.WriteLine($"Your grade is {gradeSign}{letter}");
        }
        else if (x < 60)
        {   
            letter = "F";
            Console.WriteLine($"Your grade is {gradeSign}{letter}");
        }

        if (x >= 70) 
        {   

            Console.WriteLine("Congratulations you pass the course :) ! ");
        }
        else if (x < 60)
        {
            Console.WriteLine("Sorry you didn't pass the course :( !)");
        }
        
    }  
  
}