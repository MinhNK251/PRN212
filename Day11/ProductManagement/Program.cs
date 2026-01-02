using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Models;
using System;
using System.IO;
using System.Linq;

namespace ProductManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Bai 1:");
            using (var context = new Prn212Day11Context())
            {
                var electronicsProducts = from p in context.Products
                                          join c in context.Categories on p.CategoryId equals c.CategoryId
                                          where c.CategoryName == "Electronics"
                                          select p;

                foreach (var product in electronicsProducts)
                {
                    Console.WriteLine($"Product: {product.ProductName}, Price: {product.Price}");
                }
            }

            Console.WriteLine($"\nBai 2:");
            using (var context = new Prn212Day11Context())
            {
                var orderSummary = context.OrderDetails
                    .Where(od => od.OrderId == 1)
                    .GroupBy(od => od.OrderId)
                    .Select(g => new
                    {
                        TotalQuantity = g.Sum(od => od.Quantity),
                        TotalValue = g.Sum(od => od.Quantity * od.UnitPrice * (1 - od.Discount / 100))
                    })
                    .FirstOrDefault();

                if (orderSummary != null)
                {
                    Console.WriteLine($"OrderID: 1, Total Quantity: {orderSummary.TotalQuantity}, Total Value: {orderSummary.TotalValue}");
                }
            }

            Console.WriteLine($"\nBai 3:");
            using (var context = new Prn212Day11Context())
            {
                var orders = from o in context.Orders
                             orderby o.OrderDate descending
                             select new
                             {
                                 o.OrderId,
                                 o.Status,
                                 o.OrderDate
                             };

                foreach (var order in orders)
                {
                    Console.WriteLine($"OrderID: {order.OrderId}, Status: {order.Status}, OrderDate: {order.OrderDate}");
                }
            }

            Console.WriteLine($"\nBai 4:");
            using (var context = new Prn212Day11Context())
            {
                var bookProducts = context.Products
                    .Where(p => p.ProductName.Contains("Book"));

                foreach (var product in bookProducts)
                {
                    Console.WriteLine($"Product: {product.ProductName}, Price: {product.Price}");
                }
            }

            Console.WriteLine($"\nBai 5:");
            using (var context = new Prn212Day11Context())
            {
                var categoryProductTotals = context.Products
                    .GroupBy(p => p.CategoryId)
                    .Select(g => new
                    {
                        CategoryId = g.Key,
                        TotalQuantity = g.Sum(p => p.Quantity)
                    })
                    .ToList();

                var result = categoryProductTotals
                    .Join(context.Categories,
                          g => g.CategoryId,
                          c => c.CategoryId,
                          (g, c) => new
                          {
                              c.CategoryName,
                              g.TotalQuantity
                          })
                    .ToList();

                foreach (var item in result)
                {
                    Console.WriteLine($"Category: {item.CategoryName}, Total Quantity: {item.TotalQuantity}");
                }
            }
        }
    }
}
