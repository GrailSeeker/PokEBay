using PokEBay.UI.Infrastructure.Orders.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokEBay.UI.Infrastructure.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersAsync();
        Task CreateOrderAsync(IEnumerable<OrderItemDto> orderItems);
    }
}
