using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IDocumentSession _documentSession;

        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }
        public IEnumerable<Product> GetProducts(string name, Guid? categoryId, decimal? priceFrom, decimal? priceTo, int? quantityFrom, int? quantityTo, int skip, int take)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product,
                ProductListIndex>();
            var hasFirstParameter = false;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where($"Name:*{name}*");
                hasFirstParameter = true;
            }
            if (categoryId.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("CategoryId", categoryId.Value);
                hasFirstParameter = true;
            }
            if (priceFrom.HasValue || priceTo.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                var s = $"Price_Range:[Dx{priceFrom.GetValueOrDefault(decimal.MinValue)} TO Dx{priceTo.GetValueOrDefault(decimal.MaxValue)}]";
                query = query.Where(s);
                hasFirstParameter = true;
            }
            if (quantityFrom.HasValue || quantityTo.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.Where($"Quantity_Range:[Ix{quantityFrom.GetValueOrDefault(int.MinValue)} TO Ix{quantityTo.GetValueOrDefault(int.MaxValue)}]");
            }
            return query
                .OrderBy(q => q.Name)
                .Skip(skip)
                .Take(take);
        }
    }
}