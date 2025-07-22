using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReciPick.Domain.DomainModels;

namespace ReciPick.Repository.Interface
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<Recipe> GetRecipeWithIngredientsAsync(Guid id);
        Task<IEnumerable<Recipe>> GetRecipesWithIngredientsAsync();
    }
}