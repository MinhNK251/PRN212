namespace Bai3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input a: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input b: ");
            int b = Convert.ToInt32(Console.ReadLine());
            GreatestCommonDivisor(a, b);
            Console.WriteLine();
        }
        public static void GreatestCommonDivisor(int a, int b)
        {
            int remainder;
            while (b != 0)
            {
                remainder = a % b;
                a = b;
                b = remainder;
            }
            Console.WriteLine("Greatest common divisor: "+ a);
        }
    }
}
