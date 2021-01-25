using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IWorksheetRepository : IRepository<Worksheet>
    {
        IEnumerable<Worksheet> GetWorksheets(Guid? userId, Guid? projectId, int skip, int take);
    }
}