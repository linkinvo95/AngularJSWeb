using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BusinessEntities;
using Core.Services.Orders;
using Core.Services.Product;
using Data.Repositories;
using WebApi.Models.OrderItem;
using WebApi.Models.Orders;
using Core.Factories;

namespace WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderItemService _createOrderItemService;
        private readonly ICreateOrderService _createOrderService;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IGetProductService _getProductService;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIdObjectFactory<OrderItemBase> _orderItemBaseFactory;

        public OrderController(ICreateOrderService createOrderService, IGetOrderService getOrderService, IUserRepository userRepository, ICreateOrderItemService createOrderItemService, IGetProductService getProductService, IOrderRepository orderRepository, IUpdateOrderService updateOrderService, IIdObjectFactory<OrderItemBase> orderItemBaseFactory)
        {
            _createOrderService = createOrderService;
            _getOrderService = getOrderService;
            _userRepository = userRepository;
            _createOrderItemService = createOrderItemService;
            _getProductService = getProductService;
            _orderRepository = orderRepository;
            _updateOrderService = updateOrderService;
            _orderItemBaseFactory = orderItemBaseFactory;
        }


        [Route("create/{orderId:guid}")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order != null)
            {
                throw new Exception("Order already exists.");
            }
            var user = _userRepository.Get(model.User);
            if (user == null)
            {
                return DoesNotExist();
            }

            var items = new List<OrderItem>();
            foreach (var orderItemModel in model.Items)
            {
                var product = _getProductService.GetProduct(orderItemModel.Product);
                var item = _createOrderItemService.Create(orderItemModel.Id, product, orderItemModel.Quantity);
                items.Add(item);
            }


            order = _createOrderService.Create(orderId, user, items);

            return Found(new OrderData(order));
        }

        [Route("update/{orderId:guid}")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);
           
                if (order == null)
                {
                    return DoesNotExist();
                }
                var items = new List<OrderItemBase>();
                foreach (var orderItemModel in model.Items)
                {
                    var product = _getProductService.GetProduct(orderItemModel.Product);
                    var newItem = _orderItemBaseFactory.Create(orderItemModel.Id);
                    newItem.Product = product;
                    newItem.Quantity = orderItemModel.Quantity;
                    items.Add(newItem);
                }

                _updateOrderService.Update(order, items);

                return Found(new OrderData(order));
            
        }

        [Route("delete/{orderId:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _orderRepository.Delete(order);
            return Found();
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int skip, int take, Guid? userId = null, string number = null, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var orders = _getOrderService.GetOrders(userId, number,dateFrom, dateTo, skip, take);
            var data = orders.Select(order => new OrderData(order));
            return Found(data);
        }
    }
}