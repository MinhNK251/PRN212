namespace Bai1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var products = new List<Product>
        {
            new Product { Name = "Laptop", Category = "Electronics", Price = 1200m, Stock = 10 },
            new Product { Name = "Smartphone", Category = "Electronics", Price = 800m, Stock = 15 },
            new Product { Name = "TV", Category = "Electronics", Price = 1500m, Stock = 5 },
            new Product { Name = "Refrigerator", Category = "Appliances", Price = 700m, Stock = 8 },
            new Product { Name = "Microwave", Category = "Appliances", Price = 300m, Stock = 20 },
            new Product { Name = "Tablet", Category = "Electronics", Price = 400m, Stock = 12 },
            new Product { Name = "Headphones", Category = "Electronics", Price = 150m, Stock = 30 },
            new Product { Name = "Camera", Category = "Electronics", Price = 550m, Stock = 7 },
            new Product { Name = "Washing Machine", Category = "Appliances", Price = 600m, Stock = 4 },
            new Product { Name = "Speaker", Category = "Electronics", Price = 200m, Stock = 25 }
        };

            var filteredProducts = products
                .Where(p => p.Category == "Electronics" && p.Price > 500)
                .OrderByDescending(p => p.Price)
                .ToList();

            int count = 1;
            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"{count++}. Name: {product.Name}\n   Category: {product.Category}\n   Price: {product.Price}\n   Stock: {product.Stock}\n");
            }
        }
    }
}
