using System;

namespace PokEBay.Catalog.API.Infrastructure.DTO
{
    public class CatalogItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
