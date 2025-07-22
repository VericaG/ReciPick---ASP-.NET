using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReciPick.Domain.DomainModels;
using ReciPick.Repository.Interface;

namespace ReciPick.Repository.Implementation
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _dbSet
                .Where(o => o.UserId == userId)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Ingredient)
                .OrderByDescending(o => o.DateOrdered)
                .ToListAsync();
        }

        public async Task<Order> GetOrderWithItemsAsync(Guid orderId)
        {
            return await _dbSet
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Ingredient)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}