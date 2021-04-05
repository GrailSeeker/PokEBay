using PokEBay.Basket.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokEBay.Basket.API.Infrastructure
{
    public interface IBasketService
    {
        Task AddToBasketAsync(BasketDto basketDto);
        Task<IEnumerable<BasketDto>> GetItemsFromBasketAsync();
        Task ClearBasketAsync();
    }
}
