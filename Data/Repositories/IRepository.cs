using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IRepository<T> where T : IdObject
    {
        void Save(T entity);
        void Save(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteById(Guid id);
        T Get(Guid id);
        IEnumerable<T> Get(IEnumerable<Guid> ids);
        int GetTotalRecords();
        void Delete(IEnumerable<T> entities);
    }
}