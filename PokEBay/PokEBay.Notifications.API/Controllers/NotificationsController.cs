using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokEBay.Notifications.API.Infrastructure;
using PokEBay.Notifications.API.Infrastructure.DTO;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokEBay.Notifications.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        DaprClient _daprClient;
        INotificationService _notificationService;
        ILogger<NotificationsController> _logger;

        public NotificationsController(DaprClient daprClient,INotificationService notificationService, ILogger<NotificationsController> logger)
        {
            _daprClient = daprClient;
            _notificationService = notificationService;
            _logger = logger;
        }

        // POST /<NotificationsController>
        [Topic("pokebay-pubsub", "new-order-created-event")]
        [HttpPost]
        public async Task NotifyAsync([FromBody] OrderDto orderDto)
        {
            await _notificationService.NotifyAsync(orderDto);

            _logger.LogInformation("Email sent to customer with order details.", orderDto);
        }
    }
}
