using AutoMapper;
using PokEBay.Orders.API.Domain.Entities;
using PokEBay.Orders.API.Infrastructure.DTO;
using System.Collections.Generic;

namespace PokEBay.Orders.API.Infrastructure.Helpers
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<IEnumerable<Order>, IEnumerable<OrderDto>>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<IEnumerable<OrderItem>, IEnumerable<OrderItemDto>>().ReverseMap();
            CreateMap<List<OrderItem>, IEnumerable<OrderItemDto>>();
            CreateMap<List<OrderItem>, IEnumerable<OrderItemDto>>().ReverseMap();
        }
    }
}
