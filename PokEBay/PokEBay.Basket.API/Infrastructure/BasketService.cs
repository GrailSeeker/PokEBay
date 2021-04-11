using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PokEBay.Basket.API.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokEBay.Basket.API.Infrastructure
{
    public class BasketService : IBasketService
    {
        DaprClient _daprClient;
        IConfiguration _cofiguration;

        public BasketService(DaprClient daprClient, IConfiguration configuration)
        {
            _daprClient = daprClient;
            _cofiguration = configuration;
        }

        public async Task<IEnumerable<BasketDto>> GetItemsFromBasketAsync()
        {
            var serializedbasketItems = await _daprClient.GetStateAsync<string>(
                                                                                _cofiguration["DaprConfiguration:StateStore"],
                                                                                _cofiguration["DaprConfiguration:StateStoreKey"]
                                                                               );

            if (string.IsNullOrEmpty(serializedbasketItems))
            {
                return null;
            }

            var deserializedBasketItems = JsonConvert.DeserializeObject<IEnumerable<BasketDto>>(serializedbasketItems);

            return deserializedBasketItems;
        }

        public async Task AddToBasketAsync(BasketDto basketDto)
        {
            var basketItems = new List<BasketDto>();
            basketDto.CreatedOn = DateTime.Now;
            basketItems.Add(basketDto);

            var serializedbasketItems = await _daprClient.GetStateAsync<string>(
                                                                                _cofiguration["DaprConfiguration:StateStore"],
                                                                                _cofiguration["DaprConfiguration:StateStoreKey"]
                                                                               );
            if (!string.IsNullOrEmpty(serializedbasketItems))
            {
                var deserializedBasketItems = JsonConvert.DeserializeObject<IEnumerable<BasketDto>>(serializedbasketItems);

                foreach (var item in deserializedBasketItems)
                {
                    basketItems.Add(item);
                }
            }

            var serializedBasket = JsonConvert.SerializeObject(basketItems);

            await _daprClient.SaveStateAsync(
                                             _cofiguration["DaprConfiguration:StateStore"],
                                             _cofiguration["DaprConfiguration:StateStoreKey"],
                                             serializedBasket
                                            );

            return;
        }

        public async Task ClearBasketAsync()
        {
            await _daprClient.DeleteStateAsync(
                                               _cofiguration["DaprConfiguration:StateStore"],
                                               _cofiguration["DaprConfiguration:StateStoreKey"]
                                              );
        }
    }
}
