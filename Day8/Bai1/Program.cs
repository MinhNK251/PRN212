namespace Bai1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerQueueManager manager = new CustomerQueueManager();

            while (true)
            {
                Console.WriteLine("----------- Menu -----------");
                Console.WriteLine("1. Add new customer to queue");
                Console.WriteLine("2. Process a customer (dequeue)");
                Console.WriteLine("3. Print customer queue");
                Console.WriteLine("4. Exit program");
                Console.Write("Your option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.AddCustomer();
                        break;
                    case "2":
                        manager.ProcessCustomer();
                        break;
                    case "3":
                        manager.PrintQueue();
                        break;
                    case "4":
                        Console.WriteLine("\nGoodbye!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid option. Please select again.");
                        Console.WriteLine($"Press Enter to continue");
                        Console.ReadLine();
                        break;
                }
                Console.Clear();
            }
        }
    }
}