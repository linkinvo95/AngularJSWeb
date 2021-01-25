using System;
using BusinessEntities;
using Common;
using Core.Factories;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class CreateOrderItemsService : ICreateOrderItemService
    {
        private readonly IIdObjectFactory<OrderItem> _factory;

        public CreateOrderItemsService(IIdObjectFactory<OrderItem> factory)
        {
            _factory = factory;
        }

        public OrderItem Create(Guid id, BusinessEntities.Product product, int quantity)
        {
            var item = _factory.Create(id);
            item.Set(product, quantity);
            return item;
        }
    }
}