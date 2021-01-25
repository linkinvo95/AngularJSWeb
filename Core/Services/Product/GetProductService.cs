using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Core.Factories;
using Data.Repositories;


namespace Core.Services.Product
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _productRepository;

        public GetProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        } 

        public BusinessEntities.Product GetProduct(Guid id)
        {
            return _productRepository.Get(id);
        }

        public IEnumerable<BusinessEntities.Product> GetProducts(Guid? categoryId, string name, decimal? priceFrom, decimal? priceTo, int? quantityFrom, int? quantityTo,bool? hasAvailableQuantity, int skip, int take)
        {
            if (hasAvailableQuantity.HasValue && !quantityFrom.HasValue && !quantityTo.HasValue)
            {
                quantityFrom = 1;
            }
            return _productRepository.GetProducts(name, categoryId, priceFrom, priceTo, quantityFrom, quantityTo, skip, take);
        }
    }
}
