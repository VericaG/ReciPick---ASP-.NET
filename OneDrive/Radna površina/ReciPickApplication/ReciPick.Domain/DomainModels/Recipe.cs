using System;
using System.Collections.Generic;
namespace ReciPick.Domain.DomainModels
{
    public class Recipe : BaseEntity
    {
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public int? Calories { get; set; }
        public virtual ICollection<Ingredient>? Ingredients { get; set; }
    }
}