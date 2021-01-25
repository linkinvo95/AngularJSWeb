using System;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Product
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IIdObjectFactory<BusinessEntities.Product> _productFactory;
        private readonly IProductRepository _productRepository;
        private readonly IUpdateProductService _updateProductService;

        public CreateProductService(IIdObjectFactory<BusinessEntities.Product> productFactory, IProductRepository productRepository, IUpdateProductService updateProductService)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
            _updateProductService = updateProductService;
        }

        public BusinessEntities.Product Create(Guid id, string name, BusinessEntities.Category category, decimal price, int quantity)
        {
            var product = _productFactory.Create(id);
            _updateProductService.Update(product, category, name, price, quantity);
            _productRepository.Save(product);
            return product;
        }
    }
}
