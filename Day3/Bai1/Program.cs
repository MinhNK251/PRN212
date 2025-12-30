namespace Bai1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            string result = "Invalid";
            if (args.Length == 2)
            {
                try
                {
                    int num1 = Convert.ToInt32(args[0]);
                    int num2 = Convert.ToInt32(args[1]);
                    sum = num1 + num2;
                    result = $"{num1} + {num2} = {sum}";
                }
                catch (FormatException)
                {
                    result = "Invalid";
                }
            }
            Console.WriteLine(result);
        }
    }
}
