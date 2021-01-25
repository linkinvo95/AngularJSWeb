using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order, IEnumerable<OrderItemBase> items);
    }
}
