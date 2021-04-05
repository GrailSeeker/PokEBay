using System;
using System.Collections.Generic;
using System.Linq;

namespace PokEBay.Notifications.API.Infrastructure.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }

        public OrderDto()
        {
            OrderItems = new List<OrderItemDto>();
        }

        public decimal GetTotal()
        {
            var result = OrderItems.Sum(o => o.Price);

            return result < 0 ? 0 : result;
        }
    }
}
