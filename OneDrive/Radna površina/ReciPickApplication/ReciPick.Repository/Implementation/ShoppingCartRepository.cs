using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReciPick.Domain.DomainModels;
using ReciPick.Repository.Interface;

namespace ReciPick.Repository.Implementation
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ShoppingCart> GetCartByUserIdAsync(string userId)
        {
            var cart = await _dbSet
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    Items = new List<ShoppingCartItem>()
                };
                await CreateAsync(cart);
            }

            return cart;
        }

        public async Task<ShoppingCart> GetCartWithItemsAsync(string userId)
        {
            var cart = await _dbSet
                .Include(sc => sc.Items)
                .ThenInclude(sci => sci.Ingredient)
                .FirstOrDefaultAsync(sc => sc.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    Items = new List<ShoppingCartItem>()
                };
                await CreateAsync(cart);
            }

            return cart;
        }

        public async Task AddItemToCartAsync(string userId, Guid ingredientId, decimal amount, string unit)
        {
            var cart = await GetCartByUserIdAsync(userId);

            var existingItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(sci => sci.ShoppingCartId == cart.Id && sci.IngredientId == ingredientId);

            if (existingItem != null)
            {
                existingItem.Amount += amount;
                _context.ShoppingCartItems.Update(existingItem);
            }
            else
            {
                var newItem = new ShoppingCartItem
                {
                    ShoppingCartId = cart.Id,
                    IngredientId = ingredientId,
                    Amount = amount,
                    Unit = unit
                };
                _context.ShoppingCartItems.Add(newItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await GetCartWithItemsAsync(userId);
            if (cart.Items != null && cart.Items.Any())
            {
                _context.ShoppingCartItems.RemoveRange(cart.Items);
                await _context.SaveChangesAsync();
            }
        }
    }
}