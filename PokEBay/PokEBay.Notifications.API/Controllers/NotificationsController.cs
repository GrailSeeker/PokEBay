using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokEBay.Notifications.API.Infrastructure;
using PokEBay.Notifications.API.Infrastructure.DTO;
using System.Diagnostics;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokEBay.Notifications.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {        
        ILogger<NotificationsController> _logger;
        INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService, ILogger<NotificationsController> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        // POST /<NotificationsController>
        [Topic("pubsub", "newordercreatedevent")]
        [HttpPost]
        public async Task<ActionResult> NotifyAsync(OrderDto orderDto)
        {
            //_logger.LogInformation("Email sent to customer with order details.", orderDto);
            //Debug.WriteLine($"Your order is placed with the following details: {orderDto}");

           await _notificationService.NotifyAsync(orderDto);

            return Ok();
        }
    }
}
