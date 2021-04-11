using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokEBay.UI.Infrastructure.Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PokEBay.UI.Infrastructure.Catalog
{
    public class CatalogService : ICatalogService
    {
        const string _catalogApiDefaultMethod = "catalog";

        DaprClient _daprClient;
        ILogger<CatalogService> _logger;
        IConfiguration _configuration;

        public CatalogService(DaprClient daprClient, ILogger<CatalogService> logger, IConfiguration configuration)
        {
            _daprClient = daprClient;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<CatalogItemDto>> GetCatalogItemsAsync()
        {
            try
            {
                var request = _daprClient.CreateInvokeMethodRequest(
                                                                    HttpMethod.Get,
                                                                    _configuration["DaprConfiguration:AppId_Catalog"],
                                                                    _catalogApiDefaultMethod
                                                                   );
                var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("Failed to get items from the catalog.", response);
                    throw new Exception($"Failed to get items from the catalog: \n {response.StatusCode} : {response.ReasonPhrase}");
                }

                var result = await response.Content?.ReadFromJsonAsync<IEnumerable<CatalogItemDto>>();

                _logger.LogInformation("Successfully fetched items from catalog.", result);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task AddITemToCatalogAsync(CatalogItemDto catalogItemDto)
        {
            try
            {
                var request = _daprClient.CreateInvokeMethodRequest(
                                                                    HttpMethod.Post,
                                                                    _configuration["DaprConfiguration:AppId_Catalog"],
                                                                    _catalogApiDefaultMethod,
                                                                    catalogItemDto
                                                                   );

                var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("Failed to add items to the catalog.", response);
                    throw new Exception($"Failed to add items to the catalog. Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            _logger.LogInformation("An item was added to the basket.", catalogItemDto);
        }

        public async Task DeleteItemFromCatalogAsync(Guid id)
        {
            try
            {
                var request = _daprClient.CreateInvokeMethodRequest(
                                                                    HttpMethod.Delete,
                                                                    _configuration["DaprConfiguration:AppId_Catalog"],
                                                                    $"{_catalogApiDefaultMethod}/{id}"
                                                                   );

                var response = await _daprClient.InvokeMethodWithResponseAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogCritical("Failed to delete item from the catalog with id {id}.", response);
                    throw new Exception($"Failed to delete item from the catalog with id {id}. Please try again.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            _logger.LogInformation("Catalog item with id {id} deleted from the catalog.");
        }
    }
}
