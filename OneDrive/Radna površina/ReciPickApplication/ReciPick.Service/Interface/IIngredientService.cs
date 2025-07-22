using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;

namespace ReciPick.Service.Interface
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetAllIngredientsAsync();
        Task<Ingredient> GetIngredientByIdAsync(Guid id);
        Task<Ingredient> CreateIngredientAsync(Ingredient ingredient);
        Task<Ingredient> UpdateIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(Guid id);
        Task<IEnumerable<Ingredient>> GetIngredientsByRecipeIdAsync(Guid recipeId);
    }
}