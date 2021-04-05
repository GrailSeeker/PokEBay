using Microsoft.EntityFrameworkCore;
using PokEBay.Catalog.API.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PokEBay.Catalog.API.Infrastructure
{
    public interface ICatalogContext
    {
        DbSet<CatalogItem> CatalogItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
