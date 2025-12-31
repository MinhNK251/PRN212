namespace Bai3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lastNames = new List<string>
            {
                "Nguyen", "Tran", "Le", "Pham", "Hoang", "Huynh", "Phan", "Vu", "Vo", "Dang",
                "Bui", "Do", "Ho", "Ngo", "Duong", "Ly", "Luong", "Cao", "Trieu", "Chu"
            };

            List<string> middleNames = new List<string>
            {
                "Van", "Thi", "Huu", "Minh", "Quoc", "Ngoc", "Dinh", "Thanh", "Gia", "Phu",
                "Thu", "Thao", "Bao", "Hanh", "Khanh", "Quy", "Dang", "Kim", "Lan", "Hoa",
                "Mai", "Quynh", "Duc", "Phuc", "Bich", "Anh", "Tuan", "Chau", "Hong", "Thuong",
                "Manh", "Xuan", "Linh", "Trung", "Nam", "Nhat", "Son", "Long", "Hieu", "Tam",
                "Han", "Diep", "Lam", "Vy", "Duong", "Hanh", "Thien", "Cuong", "Tuyet", "Hoa"
            };

            List<string> firstNames = new List<string>
            {
                "An", "Binh", "Cuong", "Dung", "Em", "Giang", "Hanh", "Hung", "Huyen", "Khoa",
                "Lam", "Lien", "Linh", "Mai", "Minh", "Nam", "Nga", "Ngoc", "Nhi", "Nhung",
                "Oanh", "Phat", "Phuc", "Phuong", "Quyen", "Quynh", "Son", "Tai", "Thao", "Thien",
                "Thinh", "Tien", "Toan", "Trang", "Trinh", "Truc", "Tuan", "Tuyet", "Van", "Vy",
                "Xuan", "Yen", "Bao", "Cao", "Dao", "Diem", "Duc", "Hoa", "Huy", "Khanh",
                "Kiet", "Lam", "Long", "Manh", "Nghia", "Phong", "Quoc", "Sang", "Tan", "Thang",
                "Thanh", "Tri", "Trung", "Tu", "Vinh", "Viet", "Anh", "Hieu", "Hao", "Hai",
                "Kieu", "Le", "My", "Nguyen", "Phu", "Quang", "Tam", "Thuy", "Tien", "To",
                "Tram", "Triet", "Truong", "Tuyet", "Viet", "Vu", "Xuan", "Y"
            };

            List<string> fullNames = new List<string>();
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                string fullName = $"{lastNames[random.Next(lastNames.Count)]} " +
                                  $"{middleNames[random.Next(middleNames.Count)]} " +
                                  $"{firstNames[random.Next(firstNames.Count)]}";
                fullNames.Add(fullName);
            }
            fullNames.Sort();
            Console.WriteLine("100 random generated Full Names:");
            for (int i = 0; i < 100; i++)
            {
                int count = i+1;
                Console.WriteLine($"{count}. {fullNames[i]}");
            }
            Console.Write("\nPlease enter the Page Index: ");
            int pageIndex;
            while (!int.TryParse(Console.ReadLine(), out pageIndex) || pageIndex < 1)
            {
                Console.WriteLine("Invalid input. Please enter a valid Page Index (>= 1): ");
            }

            Console.Write("\nPlease enter the Page Size: ");
            int pageSize;
            while (!int.TryParse(Console.ReadLine(), out pageSize) || pageSize < 1)
            {
                Console.Write("Invalid input. Please enter a valid Page Size (>= 1): ");
            }

            int startIndex = (pageIndex - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, fullNames.Count);

            if (startIndex < fullNames.Count)
            {
                Console.WriteLine($"\nFull names on Page {pageIndex} of size {pageSize}:");
                for (int i = startIndex; i < endIndex; i++)
                {
                    Console.WriteLine($"{++startIndex}. {fullNames[i]}");
                }
            }
            else
            {
                Console.WriteLine("Page Index out of range.");
            }
        }
    }
}
