using System;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        public Guid Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}