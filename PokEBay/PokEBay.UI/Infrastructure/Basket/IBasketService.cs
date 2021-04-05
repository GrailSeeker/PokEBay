using PokEBay.UI.Infrastructure.Basket.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokEBay.UI.Infrastructure.Basket
{
    public interface IBasketService
    {
        Task AddToBasketAsync(BasketItemDto basketItemDto);
        Task<IEnumerable<BasketItemDto>> GetItemsFromBasketAsync();
        Task ClearBasketAsync();
    }
}
