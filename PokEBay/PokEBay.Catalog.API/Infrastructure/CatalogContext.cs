using Microsoft.EntityFrameworkCore;
using PokEBay.Catalog.API.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokEBay.Catalog.API.Infrastructure
{
    public class CatalogContext : DbContext, ICatalogContext
    {
        public DbSet<CatalogItem> CatalogItems { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "GrailSeeker";
                        entry.Entity.CreatedOn = DateTime.Now; ;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = "GrailSeeker";
                        entry.Entity.LastModifiedOn = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Required to pick the entity configurations automatically.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }
    }
}
