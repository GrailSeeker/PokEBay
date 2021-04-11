using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PokEBay.Orders.API.Infrastructure;
using PokEBay.Orders.API.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PokEBay.Orders.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        DaprClient _daprClient;
        IOrderService _orderService;
        IConfiguration _configuration;

        public OrdersController(DaprClient daprClient, IOrderService orderService, IConfiguration configuration)
        {
            _orderService = orderService;
            _daprClient = daprClient;
            _configuration = configuration;
        }

        // GET: /<OrdersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersAsync()
        {
            try
            {
                var items = await _orderService.GetOrdersAsync();                

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} \n {ex.InnerException.Message}");
            }
        }

        // POST /<OrdersController>
        [HttpPost]
        public async Task<ActionResult> CreateOrderAsync([FromBody] IEnumerable<OrderItemDto> orderItems)
        {
            try
            {
                var order = await _orderService.CreateOrderAsync(orderItems);

                await _daprClient.PublishEventAsync<object>(
                                                               _configuration["DaprConfiguration:PubSubComponent"],
                                                               _configuration["DaprConfiguration:NotificationTopic"],
                                                               order
                                                             );

                return Created("Item added to catalog.", orderItems);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message} \n {ex.InnerException?.Message}");
                return BadRequest($"{ex.Message} \n {ex.InnerException.Message}");
            }
        }
    }
}
