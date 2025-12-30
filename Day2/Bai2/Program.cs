namespace Bai2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input n: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input k: ");
            int k = Convert.ToInt32(Console.ReadLine());
            CalculateCombination(n, k);
            Console.WriteLine();
        }

        public static long Factorial(int number)
        {
            long factorial = 1;
            for (int i = 2; i <= number; i++)
                factorial *= i;
            return factorial;
        }

        public static void CalculateCombination(int n, int k)
        {
            if (k > n)
                throw new ArgumentException("Invalid input: k cannot be greater than n.");
            long kFactorial = Factorial(k);
            long nFactorial = Factorial(n);
            long nMinusKFactorial = Factorial(n - k);
            long combination = nFactorial / (kFactorial * nMinusKFactorial);
            Console.WriteLine("C(n, k): " + combination);
        }
    }
}
