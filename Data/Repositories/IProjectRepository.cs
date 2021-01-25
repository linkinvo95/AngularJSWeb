using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> Get(string name, int skip, int take);
    }
}