using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IDocumentSession _documentSession;

        public UserRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<User> Get(string name, string email, UserTypes? userType, int skip, int take)
        {
            var query = _documentSession.Advanced.DocumentQuery<User, UsersListIndex>();

            var hasFirstParameter = false;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where($"Name:*{name}*");
                hasFirstParameter = true;
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where($"Email:*{email}*");
                hasFirstParameter = true;
            }

            if (userType.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("Type", (int) userType);
            }

            return query
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}