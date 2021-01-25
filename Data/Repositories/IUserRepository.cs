using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> Get(string name, string email, UserTypes? userType, int skip, int take);
    }
}