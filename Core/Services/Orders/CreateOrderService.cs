using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IIdObjectFactory<Order> _orderFactory;
        
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(IOrderRepository orderRepository, IIdObjectFactory<Order> orderFactory)
        {
            _orderRepository = orderRepository;
            _orderFactory = orderFactory;
        }

        public Order Create(Guid id, User user, IEnumerable<OrderItem> items)
        {
            var order = _orderFactory.Create(id);
            order.Set(user, items);
            _orderRepository.Save(order);

            return order;
        }
    }
}