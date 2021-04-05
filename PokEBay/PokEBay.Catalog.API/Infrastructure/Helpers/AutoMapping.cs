using AutoMapper;
using PokEBay.Catalog.API.Domain.Entities;
using PokEBay.Catalog.API.Infrastructure.DTO;

namespace PokEBay.Catalog.API.Infrastructure.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CatalogItem, CatalogItemDto>().ReverseMap();
        }
    }
}
