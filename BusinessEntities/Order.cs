using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities
{
   public class Order : IdObject
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public List<OrderItem> Items { get; set; }

        public void Set(User user, IEnumerable<OrderItem> items )
        {
            if (user == null)
            {
                throw new ArgumentNullException("user was not provided");
            }
            Date = DateTime.Now;
            Number = $"Order- {Date.Ticks}";
            
            User = user;
            Items = new List<OrderItem>(items);
        }

        public void AddItem(OrderItem item)
        {
            if (Items.Any(q=>q.Id == item.Id))
            {
                throw new ArgumentException("The item with the same ID already exists.");
            }
            Items.Add(item);
        }

        public decimal GetTotal() => Items.Sum(q => q.GetTotal());
    }
}
