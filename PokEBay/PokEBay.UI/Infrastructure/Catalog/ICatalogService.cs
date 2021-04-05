using PokEBay.UI.Infrastructure.Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokEBay.UI.Infrastructure.Catalog
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogItemDto>> GetCatalogItemsAsync();
        Task AddITemToCatalogAsync(CatalogItemDto catalogItem);
        Task DeleteItemFromCatalogAsync(Guid id);
    }
}
