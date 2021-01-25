using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderItemService
    {
        OrderItem Create(Guid id, BusinessEntities.Product product, int quantity);
    }
}
