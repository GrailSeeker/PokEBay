using System;

namespace PokEBay.Basket.API.DTO
{
    public class BasketDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
