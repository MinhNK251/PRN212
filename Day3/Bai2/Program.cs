namespace Bai2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: count_directory <rootDirectory> <-l1|-all>");
                return;
            }
            
            string rootDirectory = args[0];
            string option = args[1];

            if (!Directory.Exists(rootDirectory))
            {
                Console.WriteLine("Folder not existed!");
                return;
            }

            if (option != "-l1" && option != "-all")
            {
                Console.WriteLine("Invalid option! Usage: count_directory <rootDirectory> <-l1|-all>");
                return;
            }

            int count = CountDirectories(rootDirectory, option);

            if (option == "-l1")
            {
                Console.WriteLine($"Number of subdirectory level 1: {count}");
            }
            else if (option == "-all")
            {
                Console.WriteLine($"Total number of subdirectory: {count}");
            }


        }

        static int CountDirectories(string directory, string option)
        {
            int count = 0;

            string[] subDirectories = Directory.GetDirectories(directory);

            foreach (string subDirectory in subDirectories)
            {
                if (option == "-l1")
                {
                    string[] subSubDirectories = Directory.GetDirectories(subDirectory);
                    if (subSubDirectories.Length >= 0)
                    {
                        count++;
                    }
                }
                else if (option == "-all")
                {
                    count++;
                    count += CountDirectories(subDirectory, option);
                }
            }

            return count;
        }
    }
}
