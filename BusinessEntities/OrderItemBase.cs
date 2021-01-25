using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class OrderItemBase : IdObject
    {
         public int Quantity { get; set; }
         public Product Product { get; set; }
    }
}
