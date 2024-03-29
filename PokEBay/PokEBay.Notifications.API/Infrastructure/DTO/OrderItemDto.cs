﻿using System;

namespace PokEBay.Notifications.API.Infrastructure.DTO
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUri { get; set; }
    }
}
