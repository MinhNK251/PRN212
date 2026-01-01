using System.Collections.Concurrent;

namespace Bai3
{
    class Program
    {
        static readonly ConcurrentQueue<int> queue = new ConcurrentQueue<int>();
        static readonly AutoResetEvent queueEvent = new AutoResetEvent(false);
        static readonly Random random = new Random();

        static void Main(string[] args)
        {
            Console.Write("Enter the number of random numbers to generate: ");
            int numberCount;
            while (!int.TryParse(Console.ReadLine(), out numberCount) || numberCount <= 0)
            {
                Console.Write("Invalid input. Please enter a positive number: ");
            }

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Task generateTask = Task.Run(() => GenerateNumbers(numberCount, token), token);
            Task processTask = Task.Run(() => ProcessNumbers(token), token);

            generateTask.Wait();
            cts.Cancel();

            processTask.Wait();

            Console.WriteLine("\nFinished processing all numbers.");
        }

        static void GenerateNumbers(int count, CancellationToken token)
        {
            int index = 1;
            for (int i = 0; i < count; i++)
            {
                if (token.IsCancellationRequested)
                    break;

                int number = random.Next(1, 10000);
                queue.Enqueue(number);
                Console.WriteLine($"\n{index++}. Random Generated Number: {number}");
                queueEvent.Set();
                Thread.Sleep(300);
            }
        }

        static void ProcessNumbers(CancellationToken token)
        {
            while (!token.IsCancellationRequested || !queue.IsEmpty)
            {
                queueEvent.WaitOne(100);
                if (queue.TryDequeue(out int number))
                {
                    Console.WriteLine($"   {number} is prime: {IsPrime(number)}");
                    Console.WriteLine($"   {number} is perfect square: {IsPerfectSquare(number)}");
                }
            }
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        static bool IsPerfectSquare(int number)
        {
            int sqrt = (int)Math.Sqrt(number);
            return sqrt * sqrt == number;
        }
    }
}
