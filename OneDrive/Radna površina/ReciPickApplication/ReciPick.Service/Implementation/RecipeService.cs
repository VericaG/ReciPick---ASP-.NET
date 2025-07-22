using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;
using ReciPick.Repository.Interface;
using ReciPick.Service.Interface;

namespace ReciPick.Service.Implementation
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public RecipeService(IRecipeRepository recipeRepository,
                           IIngredientRepository ingredientRepository,
                           IShoppingCartRepository shoppingCartRepository)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _recipeRepository.GetRecipesWithIngredientsAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(Guid id)
        {
            return await _recipeRepository.GetByIdAsync(id);
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            return await _recipeRepository.CreateAsync(recipe);
        }

        public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
        {
            return await _recipeRepository.UpdateAsync(recipe);
        }

        public async Task DeleteRecipeAsync(Guid id)
        {
            await _recipeRepository.DeleteAsync(id);
        }

        public async Task<Recipe> GetRecipeWithIngredientsAsync(Guid id)
        {
            return await _recipeRepository.GetRecipeWithIngredientsAsync(id);
        }

        public async Task AddIngredientToRecipeAsync(Guid recipeId, Ingredient ingredient)
        {
            ingredient.RecipeId = recipeId;
            await _ingredientRepository.CreateAsync(ingredient);
        }

        public async Task AddRecipeToCartAsync(Guid recipeId, string userId)
        {
            var recipe = await _recipeRepository.GetRecipeWithIngredientsAsync(recipeId);
            if (recipe?.Ingredients != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    await _shoppingCartRepository.AddItemToCartAsync(
                        userId,
                        ingredient.Id,
                        ingredient.Amount ?? 1,
                        ingredient.Unit ?? "piece");
                }
            }
        }
    }
}