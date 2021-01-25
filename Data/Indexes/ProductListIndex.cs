using System;
using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
namespace Data.Indexes
{
   public class ProductListIndex : AbstractIndexCreationTask<Product>
    {
        public ProductListIndex()
        {
            Map = products => from product in products select new
            {
                CategoryId = product.Category.Id,
                product.Name,
                product.Price,
                product.Quantity
            };

        }
    }
}
