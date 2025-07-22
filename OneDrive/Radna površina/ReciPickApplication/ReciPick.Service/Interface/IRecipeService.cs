using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;

namespace ReciPick.Service.Interface
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(Guid id);
        Task<Recipe> CreateRecipeAsync(Recipe recipe);
        Task<Recipe> UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(Guid id);
        Task<Recipe> GetRecipeWithIngredientsAsync(Guid id);
        Task AddIngredientToRecipeAsync(Guid recipeId, Ingredient ingredient);
        Task AddRecipeToCartAsync(Guid recipeId, string userId);
    }
}