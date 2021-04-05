using System;
using System.Collections.Generic;

namespace PokEBay.Orders.API.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public Guid Id { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
