using Microsoft.EntityFrameworkCore;
using PokEBay.Orders.API.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PokEBay.Orders.API.Infrastructure
{
    public interface IOrderContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
