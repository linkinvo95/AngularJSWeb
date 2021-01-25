using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Category = product.Category != null ? new IdNameData(product.Category.Id, product.Category.Name) : null;
            Price = product.Price;
            Quantity = product.Quantity;
        }

        public string Name { get; set; }
        public IdNameData Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}