using System;
namespace ReciPick.Domain.DomainModels
{
    public class Ingredient : BaseEntity
    {
        public string? Name { get; set; }
        public decimal? Amount { get; set; }
        public string? Unit { get; set; }
        public decimal? Price { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
