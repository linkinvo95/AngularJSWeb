using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.OrderItem
{
    public class OrderItemModel
    {
        public Guid Id { get; set; }
        public Guid Product { get; set; }
        public int Quantity { get; set;}
    }
}