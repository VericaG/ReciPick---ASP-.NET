using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;

namespace ReciPick.Service.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string userId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task<Order> GetOrderByIdAsync(Guid orderId);
    }
}