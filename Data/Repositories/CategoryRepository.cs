using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
   public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IDocumentSession _documentSession;

        public CategoryRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Category> Get(string name, bool? active, int skip, int take)
        {
            var query = _documentSession.Advanced.DocumentQuery<Category,
                CategoryListIndex>();
            var hasFirstParameter = false;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where($"Name:*{name}*");
                hasFirstParameter = true;
            }

            if (active.HasValue)
             {
        if (hasFirstParameter)
        {
          query = query.AndAlso();
        }
        query = query.WhereEquals("Active", (bool) active);
      }

            return query
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}
