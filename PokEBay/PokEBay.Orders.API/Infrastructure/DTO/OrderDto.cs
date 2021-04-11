using System;
using System.Collections.Generic;

namespace PokEBay.Orders.API.Infrastructure.DTO
{
    public class OrderDto : AuditableDto
    {
        public Guid Id { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }

        public OrderDto()
        {
            OrderItems = new List<OrderItemDto>();
        }
    }
}
