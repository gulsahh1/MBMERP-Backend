using Bogus;
using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class OrderSeeder
    {
        public static void Seed(ERPDbContext context)
        {
            if (context.Orders.Any())
                return;

            var customerIds = context.Customers.Select(x => x.CustomerID).ToList();

            if (!customerIds.Any())
                return;

            var orderFaker = new Faker<Order>("tr")
                .RuleFor(o => o.CustomerID, f => f.PickRandom(customerIds))
                .RuleFor(o => o.OrderDate, f => f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now))
                .RuleFor(o => o.OrderNumber, f => $"SIP-{f.Random.Number(10000, 99999)}")
                .RuleFor(o => o.TotalAmount, f => 0) // sonra detail'den hesaplayacağız
                .RuleFor(o => o.OrderStatus, f => f.PickRandom<OrderStatus>())
                .RuleFor(o => o.Description, f => f.Lorem.Sentence())
                .RuleFor(o => o.IsActive, true);

            var orders = orderFaker.Generate(100);

            context.Orders.AddRange(orders);
            context.SaveChanges();

            Console.WriteLine("Order Seeder çalıştı");
        }
    }
}
