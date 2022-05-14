using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokEBay.Notifications.API.Infrastructure.DTO;
using System.Threading.Tasks;

namespace PokEBay.Notifications.API.Infrastructure
{
    public class NotificationService : INotificationService
    {
        DaprClient _daprClient;
        IConfiguration _configuration;
        ILogger<NotificationService> _logger;

        public NotificationService(DaprClient daprClient, IConfiguration configuration, ILogger<NotificationService> logger)
        {
            _daprClient = daprClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task NotifyAsync(OrderDto orderDto)
        {
            // Send notification here.
            await Task.Run(() => _logger.LogInformation($"***Your dummy email.*** \n Order# {orderDto.Id} was successfully placed. \n *****"));
        }
    }
}
