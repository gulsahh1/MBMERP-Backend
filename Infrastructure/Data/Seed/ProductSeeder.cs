using Bogus;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class ProductSeeder
    {
        public static void Seed(ERPDbContext context)
        {
            if (context.Products.Any())
                return;

            var random = new Random();

            var categoryProducts = new Dictionary<int, List<string>>
            {
                { 2, new List<string>
                    {
                        "Basic Beyaz T-Shirt",
                        "Oversize Siyah T-Shirt",
                        "Slim Fit T-Shirt"
                    }
                },

                { 3, new List<string>
                    {
                        "Keten Gömlek",
                        "Oduncu Gömlek",
                        "Slim Fit Beyaz Gömlek"
                    }
                },

                { 4, new List<string>
                    {
                        "Kargo Pantolon",
                        "Slim Fit Pantolon",
                        "Jogger Pantolon"
                    }
                },

                { 5, new List<string>
                    {
                        "Yazlık Elbise",
                        "Midi Elbise",
                        "Çiçek Desenli Elbise"
                    }
                },

                { 6, new List<string>
                    {
                        "Kapüşonlu Sweatshirt",
                        "Oversize Sweat",
                        "Basic Sweatshirt"
                    }
                },

                { 7, new List<string>
                    {
                        "Deri Ceket",
                        "Şişme Mont",
                        "Kot Ceket"
                    }
                },

                { 8, new List<string>
                    {
                        "Eşofman Takımı",
                        "Spor Tayt",
                        "Antrenman T-Shirt"
                    }
                },

                { 9, new List<string>
                    {
                        "Çocuk Sweatshirt",
                        "Çocuk Eşofman",
                        "Çocuk T-Shirt"
                    }
                },

                { 10, new List<string>
                    {
                        "Mom Jean",
                        "Slim Jean",
                        "Regular Fit Kot Pantolon"
                    }
                }
            };

            var faker = new Faker<Product>("tr")
                .RuleFor(p => p.CategoryID,
                    f => f.Random.Int(2, 10))

                .RuleFor(p => p.ProductName,
                    (f, p) =>
                    {
                        return f.PickRandom(
                            categoryProducts[p.CategoryID]
                        );
                    })

                .RuleFor(p => p.Price,
                    f => f.Random.Decimal(250, 5000))

                .RuleFor(p => p.CostPrice,
                    (f, p) =>
                    {
                        return p.Price - f.Random.Decimal(50, 300);
                    })

                .RuleFor(p => p.StockQuantity,
                    f => f.Random.Int(5, 500))

                .RuleFor(p => p.Unit,
                    f => "Adet")

                .RuleFor(p => p.Description,
                    (f, p) =>
                    {
                        return $"{p.ProductName} kaliteli kumaştan üretilmiştir.";
                    })

                .RuleFor(p => p.isActive,
                    f => true)

                .RuleFor(p => p.CreatedDate,
                    f => f.Date.Past(1));

            var products = faker.Generate(25);

            context.Products.AddRange(products);

            Console.WriteLine("Product Seeder çalıştı");

            context.SaveChanges();
        }
    }
}
