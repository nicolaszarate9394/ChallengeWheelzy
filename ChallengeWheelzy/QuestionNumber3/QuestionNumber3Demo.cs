using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChallengeWheelzy.QuestionNumber3
{
    public static class QuestionNumber3Demo
    {
        public static async Task Run()
        {
            var options = new DbContextOptionsBuilder<DemoDbContext>()
                .UseInMemoryDatabase(databaseName: "DemoDb")
                .Options;

            using var context = new DemoDbContext(options);

            // seed de clientes
            context.Customers.Add(new Customer { CustomerId = 1, Balance = 1000 });
            context.Customers.Add(new Customer { CustomerId = 2, Balance = 500 });
            await context.SaveChangesAsync();

            var invoices = new List<Invoice>
            {
                new Invoice { CustomerId = 1, Total = 200 },
                new Invoice { CustomerId = 2, Total = 100 }
            };

            var service = new InvoiceService(context);
            await service.UpdateCustomersBalanceByInvoicesAsync(invoices);

            foreach (var customer in context.Customers)
            {
                Console.WriteLine($"Customer {customer.CustomerId} Balance: {customer.Balance}");
            }
        }
    }
}
