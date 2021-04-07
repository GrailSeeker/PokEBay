using PokEBay.Notifications.API.Infrastructure.DTO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PokEBay.Notifications.API.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public NotificationService()
        {

        }

        public async Task NotifyAsync(OrderDto orderDto)
        {
            // TODO:
            // Send Email here
            await Task.Run(() => Debug.WriteLine($"Your order is successfully placed: {orderDto}"));
        }
    }
}
