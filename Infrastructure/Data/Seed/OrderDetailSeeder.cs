using Bogus;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class OrderDetailSeeder
    {
        public static void Seed(ERPDbContext context)
        {
            if (context.OrderDetails.Any())
                return;

            var orders = context.Orders.ToList();
            var products = context.Products.ToList();

            if (!orders.Any() || !products.Any())
                return;

            var detailFaker = new Faker<OrderDetail>("tr")
                .RuleFor(od => od.OrderID, f => f.PickRandom(orders).OrderID)
                .RuleFor(od => od.ProductID, f => f.PickRandom(products).ProductID)
                .RuleFor(od => od.Quantity, f => f.Random.Decimal(1, 10))
                .RuleFor(od => od.UnitPrice, f => f.PickRandom(products).Price)
                .RuleFor(od => od.Description, f => f.Lorem.Sentence())
                .RuleFor(od => od.IsActive, true);

            var orderDetails = detailFaker.Generate(300);

            // TotalPrice hesapla
            foreach (var item in orderDetails)
            {
                item.TotalPrice = (item.Quantity * (item.UnitPrice ?? 0));
            }

            context.OrderDetails.AddRange(orderDetails);
            context.SaveChanges();

            Console.WriteLine("OrderDetail Seeder çalıştı");
        }
    }
}
