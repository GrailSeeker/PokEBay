using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokEBay.UI.Infrastructure.Orders.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PokEBay.UI.Infrastructure.Orders
{
    public class OrderService : IOrderService
    {
        const string _ordersApiDefaultMethod = "orders";

        DaprClient _daprClient;
        ILogger<OrderService> _logger;
        IConfiguration _configuration;

        public OrderService(DaprClient daprClient, ILogger<OrderService> logger, IConfiguration configuration)
        {
            _daprClient = daprClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            try
            {
                var request = _daprClient.CreateInvokeMethodRequest(
                                                                    HttpMethod.Get,
                                                                    _configuration["DaprConfiguration:AppId_Orders"],
                                                                    _ordersApiDefaultMethod
                                                                   );
                var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("Failed to get order details.", response);
                    throw new Exception($"Failed to get order details: \n {response.StatusCode} : {response.ReasonPhrase}");
                }

                var result = await response.Content?.ReadFromJsonAsync<IEnumerable<OrderDto>>();

                _logger.LogInformation("Successfully fetched order details.", result);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task CreateOrderAsync(IEnumerable<OrderItemDto> orderItems)
        {
            try
            {
                var request = _daprClient.CreateInvokeMethodRequest(
                                                                    HttpMethod.Post,
                                                                    _configuration["DaprConfiguration:AppId_Orders"],
                                                                    _ordersApiDefaultMethod, 
                                                                    orderItems
                                                                   );
                var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("Failed to create order.", response);
                    throw new Exception($"Failed to create order: \n {response.StatusCode} : {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            _logger.LogInformation("An order was created.", orderItems);
        }
    }
}
