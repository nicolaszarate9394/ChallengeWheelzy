using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChallengeWheelzy.QuestionNumber4
{
    public static class QuestionNumber4Demo
    {
        public static async Task Run()
        {
            var options = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseInMemoryDatabase("OrdersDb")
                .Options;

            using var context = new OrdersDbContext(options);

            // dats de ejemplo
            context.Orders.AddRange(new List<Order>
            {
                new Order { OrderId = 1, CustomerId = 101, StatusId = 1, OrderDate = DateTime.UtcNow.AddDays(-5), IsActive = true },
                new Order { OrderId = 2, CustomerId = 102, StatusId = 2, OrderDate = DateTime.UtcNow.AddDays(-3), IsActive = false },
                new Order { OrderId = 3, CustomerId = 101, StatusId = 1, OrderDate = DateTime.UtcNow.AddDays(-1), IsActive = true },
            });
            await context.SaveChangesAsync();

            var service = new OrderService(context);

            // probamos varios filtros
            var result1 = await service.GetOrders(null, null, null, null, null);
            Console.WriteLine($"Total orders: {result1.Count}");

            var result2 = await service.GetOrders(DateTime.UtcNow.AddDays(-4), null, null, null, null);
            Console.WriteLine($"Orders desde hace 4 días: {result2.Count}");

            var result3 = await service.GetOrders(null, null, new List<int> { 101 }, null, true);
            Console.WriteLine($"Órdenes activas del cliente 101: {result3.Count}");
        }
    }
}
