using System;
using System.Collections.Generic;
using ReciPick.Domain.Identity;
namespace ReciPick.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string? UserId { get; set; }
        public ReciPickUser? User { get; set; }
        public DateTime? DateOrdered { get; set; }
        public decimal? TotalAmount { get; set; }
        public ICollection<OrderItem>? Items { get; set; } = new List<OrderItem>();
    }
}