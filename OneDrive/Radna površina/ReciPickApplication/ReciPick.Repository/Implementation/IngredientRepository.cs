using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReciPick.Domain.DomainModels;
using ReciPick.Repository.Interface;

namespace ReciPick.Repository.Implementation
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByRecipeIdAsync(Guid recipeId)
        {
            return await _dbSet
                .Where(i => i.RecipeId == recipeId)
                .ToListAsync();
        }
    }
}