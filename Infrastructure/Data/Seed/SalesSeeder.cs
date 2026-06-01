using Bogus;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class SalesSeeder
    {
        public static void Seed(ERPDbContext context)
        {
            if (context.Sales.Any())
                return;

            var customerIds = context.Customers.Select(x => x.CustomerID).ToList();

            if (!customerIds.Any())
                return;

            var saleFaker = new Faker<Sale>("tr")
                .RuleFor(s => s.CustomerID, f => f.PickRandom(customerIds))
                .RuleFor(s => s.Date, f => f.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now))
                .RuleFor(s => s.TotalAmount, 0) // sonra hesaplanacak
                .RuleFor(s => s.IsActive, true)
                .RuleFor(s => s.SaleDetails, new List<SaleDetail>());

            var sales = saleFaker.Generate(120);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            Console.WriteLine("Sales Seeder çalıştı");
        }
    }
}
