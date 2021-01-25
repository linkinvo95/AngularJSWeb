using System;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        public string Name { get; private set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public void Set(string name, Category category, decimal price, int quantity)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided.");
            }
            Category = category;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void AdjustQuantity(int quantity) => Quantity = Quantity + quantity;
        public bool HasAvailableQuantity(int quantity) => Quantity >= quantity;
    }
}