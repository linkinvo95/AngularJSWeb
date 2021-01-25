using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class Repository<T> : IRepository<T> where T : IdObject
    {
        private readonly IDocumentSession _documentSession;

        public Repository(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public void Save(T entity)
        {
            _documentSession.Store(entity);
        }

        public void Save(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                return;
            }
            var list = entities.ToList();
            if (!list.Any())
            {
                return;
            }
            foreach (var entity in list)
            {
                Save(entity);
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                return;
            }
            var list = entities.ToList();
            foreach (var entity in list)
            {
                Delete(entity);
            }
        }

        public void Delete(T entity)
        {
            _documentSession.Delete(entity);
        }

        public void DeleteById(Guid id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public T Get(Guid id)
        {
            return _documentSession.Load<T>(id);
        }

        public IEnumerable<T> Get(IEnumerable<Guid> ids)
        {
            return _documentSession.Load<T>(ids.Cast<ValueType>());
        }

        public int GetTotalRecords()
        {
            return _documentSession.Query<T>().Count();
        }

        protected IEnumerable<Guid> GetIds(List<string> ids)
        {
            return ids.Any() ? ids.Select(Guid.Parse) : Enumerable.Empty<Guid>();
        }
    }
}