using Bogus;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class StockSeeder
    {
        public static void Seed(ERPDbContext context)
        {
            if (context.Stocks.Any())
                return;

            var products = context.Products.ToList();

            if (!products.Any())
                return;

            var faker = new Faker("tr");

            var stocks = products.Select(p => new Stock
            {
                ProductID = p.ProductID,
                Quantity = faker.Random.Decimal(0, 500),
                UpdateDate = faker.Date.Between(DateTime.Now.AddMonths(-3), DateTime.Now),
                IsActive = true
            }).ToList();

            context.Stocks.AddRange(stocks);
            context.SaveChanges();

            Console.WriteLine("Stock Seeder çalıştı");
        }
    }
}
