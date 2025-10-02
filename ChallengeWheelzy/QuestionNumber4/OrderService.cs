using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChallengeWheelzy.QuestionNumber4
{
    public class OrderService
    {
        private readonly OrdersDbContext dbContext;

        public OrderService(OrdersDbContext context)
        {
            dbContext = context;
        }

        // metodo para traer las ordenes aplicando filtros opcionales
        // la idea es que si el parametro viene null no filtro por ese campo
        public async Task<List<OrderDTO>> GetOrders(
            DateTime? dateFrom,
            DateTime? dateTo,
            List<int>? customerIds,
            List<int>? statusIds,
            bool? isActive)
        {
            var query = dbContext.Orders.AsQueryable();

            if (dateFrom.HasValue)
                query = query.Where(o => o.OrderDate >= dateFrom.Value);

            if (dateTo.HasValue)
                query = query.Where(o => o.OrderDate <= dateTo.Value);

            if (customerIds != null && customerIds.Any())
                query = query.Where(o => customerIds.Contains(o.CustomerId));

            if (statusIds != null && statusIds.Any())
                query = query.Where(o => statusIds.Contains(o.StatusId));

            if (isActive.HasValue)
                query = query.Where(o => o.IsActive == isActive.Value);

            return await query
                .Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    StatusId = o.StatusId,
                    OrderDate = o.OrderDate,
                    IsActive = o.IsActive
                })
                .ToListAsync();
        }
    }
}
