using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokEBay.Orders.API.Domain.Entities;
using PokEBay.Orders.API.Infrastructure.DTO;
using PokEBay.Orders.API.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PokEBay.Orders.API.Infrastructure
{
    public class OrderService : IOrderService
    {
        IOrderContext _orderContext;
        IMapper _mapper;

        public OrderService(IOrderContext orderContext, OrderContext seedContext, IMapper mapper)
        {
            _orderContext = orderContext;
            _mapper = mapper;

            // Only for seeding.
            OrderDbInitializer.Initialize(seedContext);
        }

        public async Task<OrderDto> CreateOrderAsync(IEnumerable<OrderItemDto> orderItemsDto)
        {
            var orderItems = new List<OrderItem>();

            foreach (var item in orderItemsDto)
            {
                var orderItem = new OrderItem
                {
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    PictureUri = item.PictureUri,
                    Category = item.Category,
                    Type = item.Type,
                    Weaknesses = item.Weaknesses
                };

                orderItems.Add(orderItem);
            }

            // TODO:
            // var mappedOrderItems = _mapper.Map<List<OrderItem>>(orderItemsDto);

            var order = new Order
            {
                OrderItems = orderItems,
            };

            var result = _orderContext.Orders.Add(order);
            await _orderContext.SaveChangesAsync(new CancellationToken());

            var orderDto = new OrderDto
            {
                Id = result.Entity.Id,
                OrderItems = orderItemsDto
            };

            return orderDto;
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersAsync()
        {
            var result = await _orderContext.Orders
                                            .Include(o => o.OrderItems)
                                            .ToListAsync();

            if (result.Count == 0)
            {
                return null;
            }

            var orderDto = new List<OrderDto>();

            foreach (var order in result)
            {
                var orderObj = new OrderDto
                {
                    Id = order.Id
                };

                var orderItemsDto = new List<OrderItemDto>();
                foreach (var item in order.OrderItems)
                {
                    var orderItem = new OrderItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        PictureUri = item.PictureUri,
                        Category = item.Category,
                        Type=item.Type,
                        Weaknesses = item.Weaknesses
                    };

                    orderItemsDto.Add(orderItem);
                }

                orderObj.OrderItems = orderItemsDto;
                orderObj.CreatedOn = order.CreatedOn;

                orderDto.Add(orderObj);
            }

            // TODO:
            // return _mapper.Map < IEnumerable<OrderDto>(result);

            return orderDto;
        }
    }
}
