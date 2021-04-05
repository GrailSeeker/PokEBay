using Microsoft.EntityFrameworkCore;
using PokEBay.Orders.API.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokEBay.Orders.API.Infrastructure
{
    public class OrderContext : DbContext, IOrderContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrderContext(DbContextOptions<OrderContext> options)
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
            modelBuilder.Entity<Order>()
                        .HasMany(o => o.OrderItems);

            // Required to pick the entity configurations automatically.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderContext).Assembly);
        }
    }
}
