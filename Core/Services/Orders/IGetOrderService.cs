using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Repositories;
using JetBrains.Annotations;

namespace Core.Services.Orders
{
   public interface IGetOrderService
    {
        Order GetOrder(Guid id);
        IEnumerable<Order> GetOrders(Guid? userId, string number, DateTime? dateFrom, DateTime? dateTo, int skip, int take);
    }
}
