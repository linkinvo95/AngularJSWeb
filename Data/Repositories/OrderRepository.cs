using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IDocumentSession _documentSession;

        public OrderRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Order> GetOrders(Guid? userId, string number, DateTime? dateFrom, DateTime? dateTo, int skip, int take)
        {
            var query = _documentSession.Advanced.DocumentQuery<Order, OrderListIndex>();
            var hasFirstParameter = false;

            if (userId.HasValue)
            {
                query = query.WhereEquals("UserId", userId);
                hasFirstParameter = true;
            }
            if (!string.IsNullOrEmpty(number))
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.Where($"Number:*{number}*");
                hasFirstParameter = true;
            }
            if (dateFrom.HasValue || dateTo.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }

                query = query.Where( $"Date: [{dateFrom.GetValueOrDefault(DateTime.MinValue).ToString("yyyy-MM-ddTHH:mm:ss.fffffff")} TO {dateTo.GetValueOrDefault(DateTime.MaxValue).ToString("yyyy-MM-ddTHH:mm:ss.fffffff")}]");               
            }
            
            
            return query
                .Skip(skip)
                .Take(take);
        }
    }
}
