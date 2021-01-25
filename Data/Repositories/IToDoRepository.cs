using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IToDoRepository : IRepository<ToDo>
    {
        IEnumerable<ToDo> Get(string description);
    }
}