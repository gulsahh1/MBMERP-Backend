using Bogus;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class SaleDetailSeeder
    {
        public static void Seed(ERPDbContext context)
        {
            if (context.SaleDetails.Any())
                return;

            var sales = context.Sales.ToList();
            var products = context.Products.ToList();

            if (!sales.Any() || !products.Any())
                return;

            var detailList = new List<SaleDetail>();

            var faker = new Faker("tr");

            foreach (var sale in sales)
            {
                // Her satışa 1-5 ürün
                var itemCount = faker.Random.Int(1, 5);

                for (int i = 0; i < itemCount; i++)
                {
                    var product = faker.PickRandom(products);

                    var quantity = faker.Random.Decimal(1, 10);

                    var detail = new SaleDetail
                    {
                        SaleID = sale.SaleID,
                        ProductID = product.ProductID,
                        Quantity = quantity,
                        UnitPrice = product.Price, // satış fiyatı
                        IsActive = true
                    };

                    detailList.Add(detail);
                }
            }

            context.SaleDetails.AddRange(detailList);
            context.SaveChanges();

            Console.WriteLine("SaleDetail Seeder çalıştı");

            // 🔥 Total hesaplama (çok kritik)
            var updatedSales = context.Sales
                .ToList();

            var saleDetails = context.SaleDetails.ToList();

            foreach (var sale in updatedSales)
            {
                sale.TotalAmount = saleDetails
                    .Where(x => x.SaleID == sale.SaleID)
                    .Sum(x => x.Quantity * x.UnitPrice);
            }

            context.SaveChanges();
        }
    }
}
