namespace Bai2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var orders = new List<Order>
        {
            new Order { OrderId = 1, CustomerId = 101, OrderValue = 150.00m },
            new Order { OrderId = 2, CustomerId = 102, OrderValue = 205.00m },
            new Order { OrderId = 3, CustomerId = 101, OrderValue = 250.00m },
            new Order { OrderId = 4, CustomerId = 103, OrderValue = 30.00m },
            new Order { OrderId = 5, CustomerId = 102, OrderValue = 100.00m },
            new Order { OrderId = 6, CustomerId = 101, OrderValue = 150.00m },
            new Order { OrderId = 7, CustomerId = 104, OrderValue = 170.00m },
            new Order { OrderId = 8, CustomerId = 101, OrderValue = 250.00m },
            new Order { OrderId = 9, CustomerId = 103, OrderValue = 310.00m },
            new Order { OrderId = 10, CustomerId = 104, OrderValue = 80.00m }
        };

            var customerOrderValues = orders
                .GroupBy(o => o.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalOrderValue = g.Sum(o => o.OrderValue),
                    TotalOrder = g.Count()
                })
                .ToList();

            int count = 1;
            foreach (var customer in customerOrderValues)
            {
                Console.WriteLine($"{count++}. CustomerId: {customer.CustomerId}\n   Total Order: {customer.TotalOrder}\n   Total Order Value: {customer.TotalOrderValue}\n");
            }
        }
    }
}
