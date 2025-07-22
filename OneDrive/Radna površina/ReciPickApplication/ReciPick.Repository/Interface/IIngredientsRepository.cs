using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;

namespace ReciPick.Repository.Interface
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<IEnumerable<Ingredient>> GetIngredientsByRecipeIdAsync(Guid recipeId);
    }
}