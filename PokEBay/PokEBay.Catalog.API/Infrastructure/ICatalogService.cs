using PokEBay.Catalog.API.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokEBay.Catalog.API.Infrastructure
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogItemDto>> GetCatalogItemsAsync();
        Task AddITemToCatalogAsync(CatalogItemDto catalogItem);
        Task DeleteItemFromCatalogAsync(Guid id);
    }
}
