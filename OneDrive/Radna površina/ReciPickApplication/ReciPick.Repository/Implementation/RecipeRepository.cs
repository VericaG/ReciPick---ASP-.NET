using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReciPick.Domain.DomainModels;
using ReciPick.Repository.Interface;

namespace ReciPick.Repository.Implementation
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Recipe> GetRecipeWithIngredientsAsync(Guid id)
        {
            return await _dbSet
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Recipe>> GetRecipesWithIngredientsAsync()
        {
            return await _dbSet
                .Include(r => r.Ingredients)
                .ToListAsync();
        }
    }
}