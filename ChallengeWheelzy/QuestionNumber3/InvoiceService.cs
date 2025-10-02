using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChallengeWheelzy.QuestionNumber3
{
    public class InvoiceService
    {
        private readonly DemoDbContext dbContext;

        public InvoiceService(DemoDbContext context)
        {
            dbContext = context;
        }

        // metodo que actualiza el balance de los clientes segun las facturas
        // antes el ejemplo hacia SaveChanges() en cada loop de forma ineficiente
        // aca lo hago todo en memoria y guardo al final
        public async Task UpdateCustomersBalanceByInvoicesAsync(List<Invoice> invoices)
        {
            var customerIds = invoices
                .Where(i => i.CustomerId.HasValue)
                .Select(i => i.CustomerId!.Value)
                .Distinct()
                .ToList();

            var customers = await dbContext.Customers
                .Where(c => customerIds.Contains(c.CustomerId))
                .ToDictionaryAsync(c => c.CustomerId);

            foreach (var invoice in invoices)
            {
                if (invoice.CustomerId.HasValue && customers.TryGetValue(invoice.CustomerId.Value, out var customer))
                {
                    customer.Balance -= invoice.Total;
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
