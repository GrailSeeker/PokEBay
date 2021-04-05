using PokEBay.Notifications.API.Infrastructure.DTO;
using System.Threading.Tasks;

namespace PokEBay.Notifications.API.Infrastructure
{
    public interface INotificationService
    {
        Task NotifyAsync(OrderDto orderDto);
    }
}
