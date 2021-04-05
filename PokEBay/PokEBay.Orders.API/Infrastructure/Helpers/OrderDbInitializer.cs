namespace PokEBay.Orders.API.Infrastructure.Helpers
{
    public class OrderDbInitializer
    {
        public static void Initialize(OrderContext orderContext)
        {
            orderContext.Database.EnsureCreated();
        }
    }
}
