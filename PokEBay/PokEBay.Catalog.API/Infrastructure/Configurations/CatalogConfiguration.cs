using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokEBay.Catalog.API.Domain.Entities;

namespace PokEBay.Catalog.API.Infrastructure.Configurations
{
    public class CatalogConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(s => s.Name)
                .IsRequired();

            builder.Property(s => s.Description)
                .IsRequired();

            builder.Property(s => s.Price)
                .IsRequired();
        }
    }
}
