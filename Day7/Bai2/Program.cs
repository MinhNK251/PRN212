using System;
using System.Collections.Generic;
using System.Linq;

namespace Bai2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                numbers.Add(random.Next());
            }

            Console.WriteLine("Please enter a number:");
            int N;
            while (!int.TryParse(Console.ReadLine(), out N))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer:");
            }

            numbers = numbers.Where(x => x >= N).ToList();

            double average = numbers.Count > 0 ? numbers.Average() : 0;

            Console.WriteLine($"List of 1000 random numbers greater than or equal to {N}:");
            numbers.ForEach(Console.WriteLine);
            Console.WriteLine($"Average of the list: {average}");
        }
    }
}
