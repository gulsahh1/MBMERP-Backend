using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public class CustomerSeeder
    {
        public static void Seed(ERPDbContext context)
        {
            if (context.Customers.Any())
               return;

                var customerFaker = new Bogus.Faker<Customer>("tr")
                    .RuleFor(c => c.CustomerName, f => f.Person.FullName)
                    .RuleFor(c => c.Email, f => f.Internet.Email())
                    .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address, f => f.Address.FullAddress())
                    .RuleFor(c => c.Balance, f => f.Finance.Amount(0, 10000))
                 .RuleFor(c => c.CustomerType, f => f.PickRandom<CustomerType>());

                var customers = customerFaker.Generate(50);

                context.Customers.AddRange(customers);
            Console.WriteLine("Customer Seeder çalıştı");
            context.SaveChanges();
            
        }
    }
}
