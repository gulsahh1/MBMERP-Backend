using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class CategorySeeder
    {
        public static void Seed(ERPDbContext context )
        {
            //if (context.Categories.Any())
            //{
            //    var categories = new List<Category>
            //    {
            //        new Category { CategoryName = "T-Shirt" },
            //        new Category { CategoryName = "Gömlek" },
            //        new Category { CategoryName = "Pantolon" },
            //        new Category { CategoryName = "Elbise" },
            //        new Category { CategoryName = "Sweatshirt" },
            //        new Category { CategoryName = "Mont & Ceket" },
            //        new Category { CategoryName = "Spor Giyim" },
            //        new Category { CategoryName = "Çocuk Giyim" },
            //        new Category { CategoryName = "Denim Ürünler" }
                  
            //    };
            //    context.Categories.AddRange(categories);
            //    context.SaveChanges();

            //    Console.WriteLine("Categories seeded successfully.");   
            //}


        }
    }
}
