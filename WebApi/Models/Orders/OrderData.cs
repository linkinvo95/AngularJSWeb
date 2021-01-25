using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using WebApi.Models.OrderItem;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            Number = order.Number;
            Date = order.Date;
            User = order.User != null ? new IdNameData(order.User.Id, order.User.Name) : null;
            TotalPrice = order.GetTotal();
            Items = order.Items.Select(q => new OrderItemData(q));
        }

        public string Number { get; set; }
        public DateTime Date { get; set; }
        public IdNameData User { get; set; }
        public IEnumerable<OrderItemData> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}