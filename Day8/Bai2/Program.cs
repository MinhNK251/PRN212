namespace Bai2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a string: ");
            string input = Console.ReadLine();

            string reversedString = ReverseStringUsingStack(input);

            Console.WriteLine($"Reversed string: {reversedString}");
        }

        static string ReverseStringUsingStack(string input)
        {
            Stack<char> stack = new Stack<char>();

            // Push each character of the string onto the stack
            foreach (char c in input)
            {
                stack.Push(c);
            }

            // Pop each character from the stack and build the reversed string
            char[] reversedChars = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                reversedChars[i] = stack.Pop();
            }

            return new string(reversedChars);
        }
    }
}
