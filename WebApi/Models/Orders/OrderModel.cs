using System;
using System.Collections.Generic;
using BusinessEntities;
using WebApi.Models.OrderItem;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public Guid User { get; set; }
        public IEnumerable<OrderItemModel> Items { get; set; }
    }
}