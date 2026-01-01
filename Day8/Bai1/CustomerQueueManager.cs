namespace Bai1
{
    public class CustomerQueueManager
    {
        private Queue<Customer> queue;

        public CustomerQueueManager()
        {
            queue = new Queue<Customer>();
        }

        public void AddCustomer()
        {
            Console.Write("\nEnter phone number: ");
            int phoneNumber;
            bool flag = true;
            do
            {
                while (!int.TryParse(Console.ReadLine(), out phoneNumber))
                {
                    Console.Write("Invalid phone numbers. Please enter phone n: ");
                }
                if (queue.Any(c => c.PhoneNumber == phoneNumber))
                {
                    Console.WriteLine($"Customer with phone number {phoneNumber} already exists in the queue. Cannot add duplicate customer.");
                    Console.Write($"Please re-enter phone number: ");
                }
                else
                {
                    flag = false;
                }
            } while (flag);
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter gender (1-Male, 0-Female): ");
            int gender;
            while (!int.TryParse(Console.ReadLine(), out gender) || (gender != 1 && gender != 0))
            {
                Console.Write("Invalid input. Please enter 1 for Male or 0 for Female:");
            }
            Customer newCustomer = new Customer(phoneNumber, name, gender);
            queue.Enqueue(newCustomer);
            Console.WriteLine($"\nAdded new customer: {newCustomer}");
            Console.WriteLine($"Press Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        public void ProcessCustomer()
        {
            if (queue.Count > 0)
            {
                Customer customer = queue.Dequeue();
                Console.WriteLine($"\nProcessed customer (dequeue): {customer}");
            }
            else
            {
                Console.WriteLine("\nNo customers in the queue.");
            }
            Console.WriteLine($"Press Enter to continue"); 
            Console.ReadLine();
            Console.Clear();
        }

        public void PrintQueue()
        {
            if (queue.Count > 0)
            {
                Console.WriteLine("\nCustomer queue:");
                int i = 1;
                foreach (var customer in queue)
                {
                    Console.WriteLine($"{i}. {customer}");
                    i++;
                }
            }
            else
            {
                Console.WriteLine("\nThe queue is empty.");
            }
            Console.WriteLine($"Press Enter to continue"); 
            Console.ReadLine();
            Console.Clear();
        }
    }
}
