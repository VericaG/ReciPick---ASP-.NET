using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;
using ReciPick.Repository.Interface;
using ReciPick.Service.Interface;

namespace ReciPick.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderService(IOrderRepository orderRepository,
                          IShoppingCartRepository shoppingCartRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<Order> CreateOrderAsync(string userId)
        {
            var cart = await _shoppingCartRepository.GetCartWithItemsAsync(userId);

            if (cart.Items == null || !cart.Items.Any())
            {
                throw new InvalidOperationException("Cannot create order with empty cart");
            }

            var order = new Order
            {
                UserId = userId,
                DateOrdered = DateTime.UtcNow,
                Items = cart.Items.Select(item => new OrderItem
                {
                    IngredientId = item.IngredientId,
                    Amount = item.Amount,
                    Unit = item.Unit
                }).ToList()
            };

            order.TotalAmount = cart.Items.Sum(item => (item.Amount ?? 0) * (item.Ingredient?.Price ?? 0));

            var createdOrder = await _orderRepository.CreateAsync(order);
            await _shoppingCartRepository.ClearCartAsync(userId);

            return createdOrder;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await _orderRepository.GetOrderWithItemsAsync(orderId);
        }
    }
}