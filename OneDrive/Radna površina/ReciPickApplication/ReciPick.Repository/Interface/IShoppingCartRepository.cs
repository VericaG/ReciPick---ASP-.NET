using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;

namespace ReciPick.Repository.Interface
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        Task<ShoppingCart> GetCartByUserIdAsync(string userId);
        Task<ShoppingCart> GetCartWithItemsAsync(string userId);
        Task AddItemToCartAsync(string userId, Guid ingredientId, decimal amount, string unit);
        Task ClearCartAsync(string userId);
    }
}