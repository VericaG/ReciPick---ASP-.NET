using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;

namespace ReciPick.Service.Interface
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartByUserIdAsync(string userId);
        Task AddItemToCartAsync(string userId, Guid ingredientId, decimal amount, string unit);
        Task RemoveItemFromCartAsync(Guid itemId);
        Task ClearCartAsync(string userId);
        Task<decimal> GetCartTotalAsync(string userId);
    }
}