using System;

namespace BusinessEntities
{
    public class OrderItem : OrderItemBase
    {       
        public decimal Price { get; set; }
       
        public void Set(Product product, int quantity)
        {
            UpdateQuantity(product, quantity);          
            Product = product;            
            Price = product.Price;            
        }
        
        public void UpdateQuantity(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException("The product was not proided.");
            }  
            var adjustedQuantity = quantity - Quantity;
            if (adjustedQuantity > 0 && !product.HasAvailableQuantity(quantity))
            {
                throw new Exception($"The product '{product.Name}' quantity is unavailable.");
            }
            product.AdjustQuantity(-1 * quantity);
            Quantity = quantity;
        }

        public decimal GetTotal() => Quantity * Price;
    }
}