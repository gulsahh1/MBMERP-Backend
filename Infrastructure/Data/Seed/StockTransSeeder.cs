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
    public class StockTransSeeder
    {
        public static void Seed(ERPDbContext context)
        {
            if (context.StockTransactions.Any())
                return;

            var products = context.Products.ToList();

            if (!products.Any())
                return;

            var faker = new Faker("tr");

            var transactions = new List<StockTransaction>();

            foreach (var product in products)
            {
                // Her ürün için 5-20 arası işlem üret
                var count = faker.Random.Int(5, 20);

                for (int i = 0; i < count; i++)
                {
                    var type = faker.PickRandom<TransactionType>();

                    decimal qty = faker.Random.Decimal(1, 50);

                    // Sale ise negatif gibi düşün (çıkış)
                    if (type == TransactionType.Sale)
                        qty = -qty;

                    // Adjustment bazen negatif bazen pozitif
                    if (type == TransactionType.Adjustment)
                        qty = faker.Random.Bool() ? qty : -qty;

                    transactions.Add(new StockTransaction
                    {
                        ProductID = product.ProductID,
                        Quantity = qty,
                        TransactionType = type,
                        Date = faker.Date.Between(DateTime.Now.AddMonths(-6), DateTime.Now),
                        IsActive = true
                    });
                }
            }

            context.StockTransactions.AddRange(transactions);
            context.SaveChanges();

            Console.WriteLine("StockTransaction Seeder çalıştı");
        }
    }
}
