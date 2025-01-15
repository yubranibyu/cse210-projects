using System;

class Program
{
    static void Main(string[] args)
    {
         Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        List<int> numbers = new List<int>();
        int input;

        // Solicitar números al usuario y agregarlos a la lista
        do
        {
            Console.Write("Enter number: ");
            input = int.Parse(Console.ReadLine());
            if (input != 0)
            {
                numbers.Add(input);
            }
        } while (input != 0);

        // Verificar que la lista no esté vacía antes de realizar cálculos
        if (numbers.Count > 0)
        {
            // Calcular la suma
            int sum = numbers.Sum();
            Console.WriteLine($"The sum is: {sum}");

            // Calcular el promedio
            double average = numbers.Average();
            Console.WriteLine($"The average is: {average}");

            // Encontrar el número más grande
            int max = numbers.Max();
            Console.WriteLine($"The largest number is: {max}");

            // Stretch Challenge: Encontrar el número positivo más pequeño
            int smallestPositive = numbers.Where(n => n > 0).DefaultIfEmpty(int.MaxValue).Min();
            if (smallestPositive != int.MaxValue)
            {
                Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            }
            else
            {
                Console.WriteLine("No positive numbers were entered.");
            }

            // Stretch Challenge: Ordenar y mostrar la lista
            numbers.Sort();
            Console.WriteLine("The sorted list is:");
            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}