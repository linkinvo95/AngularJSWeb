using System.Collections.Generic;
using BusinessEntities;


namespace Data.Repositories
{
   public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> Get(string name, bool? active, int skip, int take);
    }
}
