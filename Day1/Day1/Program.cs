namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            obj.sum();
            obj.sumOdd();
            obj.CalculateCombination(10, 8);
            obj.CompareThreeNumbers(11, 4, 8);
            obj.CalculateASCIISum("FptUniversity");
            Console.WriteLine();
        }

        public void sum()
        {
            int sum = 0;
            for (int i = 1; i <= 100; i++)
                sum = sum + i;
            Console.WriteLine("Sum from 1 to 100: " + sum);
        }

        public void sumOdd()
        {
            int sum = 0;
            for (int i = 1; i <= 10000; i++)
                if (i % 2 != 0)
                    sum = sum + i;
            Console.WriteLine("Sum odd from 1 to 10000: " + sum);
        }

        public static long Factorial(int number)
        {
            int factorial = 1;
            for (int i = 2; i <= number; i++)
                factorial *= i;
            return factorial;
        }

        public void CalculateCombination(int n, int k)
        {
            if (k > n)
                throw new ArgumentException("Invalid input: k cannot be greater than n.");
            long kFactorial = Factorial(k);
            long nFactorial = Factorial(n);
            long nMinusKFactorial = Factorial(n - k);
            long combination = nFactorial / (kFactorial * nMinusKFactorial);
            Console.WriteLine("Combination nCk: " + combination);
        }

        public void CompareThreeNumbers(int num1, int num2, int num3)
        {
            int[] numbers = { num1, num2, num3 };
            int temp;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = 0; j < numbers.Length - i - 1; j++)
                {
                    if (numbers[j] < numbers[j + 1])
                    {
                        temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
            Console.WriteLine("Compare 3 numbers: " + numbers[0] + "> " + numbers[1] + "> " + numbers[2]);
        }

        public void CalculateASCIISum(string sentence)
        {
            int sum = 0;
            foreach (char c in sentence)
                sum += (int)c;
            Console.WriteLine("Sum of ASCII of " + sentence + ": " + sum);
        }
    }
}