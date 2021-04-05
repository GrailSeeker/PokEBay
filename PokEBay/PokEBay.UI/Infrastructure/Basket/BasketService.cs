using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokEBay.UI.Infrastructure.Basket.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PokEBay.UI.Infrastructure.Basket
{
    public class BasketService : IBasketService
    {        
        const string _basketApiDefaultMethod = "basket";

        DaprClient _daprClient;
        ILogger<BasketService> _logger;
        IConfiguration _configuration;

        public BasketService(DaprClient daprClient, ILogger<BasketService> logger, IConfiguration configuration)
        {
            _daprClient = daprClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<BasketItemDto>> GetItemsFromBasketAsync()
        {
            try
            {
                var request = _daprClient.CreateInvokeMethodRequest(
                                                                    HttpMethod.Get, 
                                                                    _configuration["DaprConfiguration:AppId_Basket"],
                                                                    _basketApiDefaultMethod
                                                                   );
                var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("Failed to get items from the basket.", response);
                    throw new Exception($"No item(s) found in the basket.");
                }

                var result = await response.Content.ReadFromJsonAsync<IEnumerable<BasketItemDto>>();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddToBasketAsync(BasketItemDto basketItemDto)
        {
            try
            {
                var request = _daprClient.CreateInvokeMethodRequest(
                                                                    HttpMethod.Post,
                                                                    _configuration["DaprConfiguration:AppId_Basket"], 
                                                                    _basketApiDefaultMethod,
                                                                    basketItemDto
                                                                   );
                var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("Failed to add items to the basket.", response);
                    throw new Exception($"Failed to add items to the basket. Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            _logger.LogInformation("An item was added to the basket.", basketItemDto);
        }

        public async Task ClearBasketAsync()
        {
            try
            {
                var request = _daprClient.CreateInvokeMethodRequest(
                                                                    HttpMethod.Delete,
                                                                    _configuration["DaprConfiguration:AppId_Basket"],
                                                                    _basketApiDefaultMethod
                                                                   );
                var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("Failed to clear items from the basket.", response);
                    throw new Exception($"Failed to clear items from the basket. Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            _logger.LogInformation("Basket was cleared.");
        }
    }
}
