using System;

namespace PokEBay.UI.Infrastructure.Basket.DTO
{
    public class BasketItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
