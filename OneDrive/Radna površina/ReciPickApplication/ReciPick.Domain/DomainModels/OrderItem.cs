using System;
namespace ReciPick.Domain.DomainModels
{
    public class OrderItem : BaseEntity
    {
        public Guid? IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
        public decimal? Amount { get; set; }
        public string? Unit { get; set; }
    }
}