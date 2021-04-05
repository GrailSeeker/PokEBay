using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokEBay.Catalog.API.Domain.Entities;
using PokEBay.Catalog.API.Infrastructure.DTO;
using PokEBay.Catalog.API.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PokEBay.Catalog.API.Infrastructure
{
    public class CatalogService : ICatalogService
    {
        ICatalogContext _catalogContext;
        IMapper _mapper;

        public CatalogService(ICatalogContext catalogContext, CatalogContext seedContext, IMapper mapper)
        {
            _catalogContext = catalogContext;
            _mapper = mapper;

            // Only for seeding.
            CatalogDbInitializer.Initialize(seedContext);
        }

        public async Task<IEnumerable<CatalogItemDto>> GetCatalogItemsAsync()
        {
            var result = await _catalogContext.CatalogItems.ToListAsync();

            return _mapper.Map<IEnumerable<CatalogItemDto>>(result);
        }

        public async Task AddITemToCatalogAsync(CatalogItemDto catalogItem)
        {
            var itemToAdd = _mapper.Map<CatalogItem>(catalogItem);

            var result = _catalogContext.CatalogItems.Add(itemToAdd);
            await _catalogContext.SaveChangesAsync(new CancellationToken());

            return;
        }

        public async Task DeleteItemFromCatalogAsync(Guid id)
        {
            var itemToRemove = _catalogContext.CatalogItems.Where(ci => ci.Id == id).FirstOrDefault();

            if (itemToRemove == null)
            {
                throw new Exception($"Item with id {id} does not exist in the catalog.");
            }

            var result = _catalogContext.CatalogItems.Remove(itemToRemove);
            await _catalogContext.SaveChangesAsync(new CancellationToken());

            return;
        }
    }
}
