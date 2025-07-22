using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;
using ReciPick.Repository.Interface;
using ReciPick.Service.Interface;

namespace ReciPick.Service.Implementation
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            return await _ingredientRepository.GetAllAsync();
        }

        public async Task<Ingredient> GetIngredientByIdAsync(Guid id)
        {
            return await _ingredientRepository.GetByIdAsync(id);
        }

        public async Task<Ingredient> CreateIngredientAsync(Ingredient ingredient)
        {
            return await _ingredientRepository.CreateAsync(ingredient);
        }

        public async Task<Ingredient> UpdateIngredientAsync(Ingredient ingredient)
        {
            return await _ingredientRepository.UpdateAsync(ingredient);
        }

        public async Task DeleteIngredientAsync(Guid id)
        {
            await _ingredientRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByRecipeIdAsync(Guid recipeId)
        {
            return await _ingredientRepository.GetIngredientsByRecipeIdAsync(recipeId);
        }
    }
}