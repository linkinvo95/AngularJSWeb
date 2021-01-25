using System;
using System.Linq;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Services.Product;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class UpdateOrderService : IUpdateOrderService
    {
        private readonly IGetProductService _getProductService;
        private readonly ICreateOrderItemService _createOrderItemService;

        public UpdateOrderService( IGetProductService getProductService, ICreateOrderItemService createOrderItemService)
        {
            _getProductService = getProductService;
            _createOrderItemService = createOrderItemService;
        }

        public void Update(Order order, IEnumerable<OrderItemBase> items)
        {
            items = items.Where(q=>q.Quantity > 0).ToList();


            foreach(var item in order.Items)
            {
                var updatedItem = items.FirstOrDefault(q=>q.Id == item.Id);
                if (updatedItem != null)
                {
                    //var product =  _getProductService.GetProduct(item.Product.Id);                    
                    item.UpdateQuantity(updatedItem.Product, updatedItem.Quantity);
                }
            }
            
            foreach(var item in items)
            {
                var existingItem = order.Items.FirstOrDefault(q=>q.Id == item.Id);
                if (existingItem == null)
                {
                    // var product =  _getProductService.GetProduct(item.Product.Id);
                    var newOrderItem = _createOrderItemService.Create(item.Id, item.Product, item.Quantity);
                    order.AddItem(newOrderItem);
                }
            }

            foreach(var item in order.Items.ToArray())
            {
                var existingItem = items.FirstOrDefault(q=>q.Id == item.Id);
                if (existingItem == null)
                {
                    order.Items.Remove(item);
                }
            }

            //var itemsToDelete = order.Items.Where(q=>!items.Any(z=>z.Id == q.Id)).ToList();
            //foreach(var item in itemsToDelete)
            //{
            //    order.Items.Remove(item);
            //}
        }
    }
}
