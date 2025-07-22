using System;
namespace ReciPick.Domain.DomainModels
{
    public class ShoppingCartItem : BaseEntity
    {
        public Guid? IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public Guid? ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public decimal? Amount { get; set; }
        public string? Unit { get; set; }
    }
}