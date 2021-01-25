using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Core.Services.Product;
using Data.Repositories;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IUpdateProductService _updateProductService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGetProductService _getProductService;

        public ProductController(ICreateProductService createProductService, IUpdateProductService updateProductService, ICategoryRepository categoryRepository, IProductRepository productRepository, IGetProductService getProductService)
        {
            _createProductService = createProductService;
            _updateProductService = updateProductService;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _getProductService = getProductService;
        }

        [Route("create/{productId:guid}")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(productId);
            if (product != null)
            {
                throw new Exception("Product already exists.");
            }
            var category = _categoryRepository.Get(model.Category);
            if (category == null)
            {
                return DoesNotExist();
            }
              product = _createProductService.Create(productId,model.Name, category, model.Price, model.Quantity);
            return Found(new ProductData(product));
        }

        [Route("delete/{productId:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            _productRepository.Delete(product);
            return Found();
        }

        [Route("update/{productId:guid}")]
        [HttpPost]

        public HttpResponseMessage UpdateProduct(Guid productId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            var category = _categoryRepository.Get(model.Category);
            if (category == null)
            {
                return DoesNotExist();
            }
            _updateProductService.Update(product, category, model.Name, model.Price, model.Quantity);
            return Found(new ProductData(product));
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            return Found(new ProductData(product));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetProducts(int skip, int take, string name = null, Guid? categoryId = null, decimal? priceFrom = null, decimal? priceTo = null, int? quantityFrom = null, int? quantityTo = null, bool? hasAvailableQuantity = null)
        {
            var products = _getProductService.GetProducts(categoryId, name, priceFrom, priceTo, quantityFrom, quantityTo, hasAvailableQuantity, skip, take);
            var data = products.Select(product => new ProductData(product));
            return Found(data);
        }
    }
}
