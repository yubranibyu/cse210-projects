using System;

class Program
{
    static void Main(string[] args)
    {
        string playAgain = "yes";

        while (playAgain.ToLower() == "yes")
        {
           
            Random random = new Random();
            int magicNumber = random.Next(1, 101);

            Console.WriteLine("¡Guess the magic number! ");

            int guess = 0; 
            int attempts = 0; 

            
            while (guess != magicNumber)
            {
               
                Console.Write("¿What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                attempts++; 

                
                if (guess < magicNumber)
                {
                    Console.WriteLine("Higger");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"¡Congratulation you guessed in {attempts} attemps!");
                }
            }

           
            Console.Write("¿Do you wanna play again (yes/no): ");
            playAgain = Console.ReadLine();
        }

        Console.WriteLine("¡Thanks for playing!");
    }
}