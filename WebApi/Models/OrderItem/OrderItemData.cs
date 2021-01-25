namespace WebApi.Models.OrderItem
{
    public class OrderItemData : IdObjectData
    {
        public OrderItemData(BusinessEntities.OrderItem orderItem) : base(orderItem)
        {
            Product = orderItem.Product != null ? new IdNameData(orderItem.Product.Id, orderItem.Product.Name) : null;
            Price = orderItem.Price;
            Quantity = orderItem.Quantity;
        }

        public IdNameData Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}