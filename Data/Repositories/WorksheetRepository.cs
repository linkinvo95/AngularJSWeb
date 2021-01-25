using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class WorksheetRepository : Repository<Worksheet>, IWorksheetRepository
    {
        private readonly IDocumentSession _documentSession;

        public WorksheetRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

       
        public IEnumerable<Worksheet> GetWorksheets(Guid? userId, Guid? projectId, int skip, int take)
        {
            var query = _documentSession.Advanced.DocumentQuery<Worksheet, WorksheetListIndex>();
            var hasFirstParameter = false;

            if (userId.HasValue)
            {
                query = query.WhereEquals("UserId", userId);
                hasFirstParameter = true;
            }

            if (projectId.HasValue)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("ProjectId", projectId);
            }
            return query
                .Skip(skip)
                .Take(take);
        }
    }
}