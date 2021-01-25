using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProducts(string name, Guid? categoryId, decimal? priceFrom, decimal? priceTo, int? quantityFrom, int? quantityTo, int skip, int take);
    }
}
