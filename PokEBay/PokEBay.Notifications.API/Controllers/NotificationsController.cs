using Dapr;
using Microsoft.AspNetCore.Mvc;
using PokEBay.Notifications.API.Infrastructure;
using PokEBay.Notifications.API.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PokEBay.Notifications.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {        
        INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // POST /<NotificationsController>
        [Topic("pubsub", "newordercreatedevent")]
        [HttpPost]
        public async Task<ActionResult> NotifyAsync(OrderDto orderDto)
        {
            try
            {
                await _notificationService.NotifyAsync(orderDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} \n {ex.InnerException?.Message}");
            }
        }
    }
}
