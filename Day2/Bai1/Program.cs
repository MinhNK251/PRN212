namespace Bai1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Quadratic Equation: ax^2 + bx + c = 0"); 
            Console.Write("Input a: ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input b: ");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input c: ");
            int c = Convert.ToInt32(Console.ReadLine());
            EquaticEquation(a, b, c);
            Console.WriteLine();
        }

        public static void EquaticEquation(int a, int b, int c)
        {
            double x1, x2, delta = b*b - 4*a*c;
            if (delta < 0)
            {
                Console.WriteLine("No Solution");
            }
            else if (delta == 0)
            {
                x1 = -b / (2.0 * a);
                Console.WriteLine("x1 = x2 = "+ x1);
            }
            else
            {
                x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                Console.WriteLine("x1 = {0}; x2 = {1}", x1,x2);
            }
        }
    }
}
