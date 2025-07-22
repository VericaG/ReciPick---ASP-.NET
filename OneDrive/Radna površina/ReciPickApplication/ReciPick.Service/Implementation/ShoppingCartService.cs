using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;
using ReciPick.Repository.Interface;
using ReciPick.Service.Interface;

namespace ReciPick.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<ShoppingCart> GetCartByUserIdAsync(string userId)
        {
            return await _shoppingCartRepository.GetCartWithItemsAsync(userId);
        }

        public async Task AddItemToCartAsync(string userId, Guid ingredientId, decimal amount, string unit)
        {
            await _shoppingCartRepository.AddItemToCartAsync(userId, ingredientId, amount, unit);
        }

        public async Task RemoveItemFromCartAsync(Guid itemId)
        {
           
            throw new NotImplementedException();
        }

        public async Task ClearCartAsync(string userId)
        {
            await _shoppingCartRepository.ClearCartAsync(userId);
        }

        public async Task<decimal> GetCartTotalAsync(string userId)
        {
            var cart = await _shoppingCartRepository.GetCartWithItemsAsync(userId);
            return cart.Items?.Sum(item => (item.Amount ?? 0) * (item.Ingredient?.Price ?? 0)) ?? 0;
        }
    }
}