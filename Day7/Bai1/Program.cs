namespace Bai1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> primeList = new List<int>();
            List<int> uniqueList = new List<int>();
            Random random = new Random();

            Console.WriteLine("Prime list:");
            while (primeList.Count < 1000000)
            {
                primeList.Add(random.Next());
            }
            primeList = primeList.Where(IsPrime).ToList();
            primeList.ForEach(Console.WriteLine);

            Console.WriteLine("Unique Prime list:");
            while (uniqueList.Count < 10000)
            {
                int number = random.Next(1, 100000);
                if (IsPrime(number) & !primeList.Contains(number))
                    uniqueList.Add(number);
            }
            uniqueList.ForEach(Console.WriteLine);
        }

        static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
