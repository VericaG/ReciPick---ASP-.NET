using System;
using System.Collections.Generic;
using ReciPick.Domain.Identity;
namespace ReciPick.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string? UserId { get; set; }
        public ReciPickUser? User { get; set; }
        public ICollection<ShoppingCartItem>? Items { get; set; }
    }
}
