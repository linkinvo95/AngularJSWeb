using System;
using System.Collections.Generic;

namespace Core.Services.Product
{
    public interface IGetProductService
    {
        BusinessEntities.Product GetProduct(Guid id);
        IEnumerable<BusinessEntities.Product> GetProducts(Guid? categoryId, string name, decimal? priceFrom, decimal? priceTo, int? quantityFrom, int? quantityTo, bool? hasAvailableQuantity, int skip, int take);
    }
}