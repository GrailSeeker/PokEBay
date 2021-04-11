using System;

namespace PokEBay.Catalog.API.Domain.Entities
{
    public class CatalogItem : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string Category { get; set; }

        public string Type { get; set; }

        public string Weaknesses { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
