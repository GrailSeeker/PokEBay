using System;

namespace PokEBay.Orders.API.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
