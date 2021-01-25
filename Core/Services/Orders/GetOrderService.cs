using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Core.Services.Orders
{
    [AutoRegister]
   public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order GetOrder(Guid id)
        {
            return _orderRepository.Get(id);
        }
      public IEnumerable<Order> GetOrders(Guid? userId, string number, DateTime? dateFrom, DateTime? dateTo, int skip, int take)
        {
            return _orderRepository.GetOrders(userId, number, dateFrom, dateTo, skip, take);
        }
    }
}
