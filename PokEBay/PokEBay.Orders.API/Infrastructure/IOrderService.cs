using PokEBay.Orders.API.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokEBay.Orders.API.Infrastructure
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersAsync();
        Task<OrderDto> CreateOrderAsync(IEnumerable<OrderItemDto> orderItems);
    }
}
