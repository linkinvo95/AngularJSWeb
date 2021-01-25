using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly IDocumentSession _documentSession;

        public ProjectRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Project> Get(string name, int skip, int take)
        {
            var query = _documentSession.Advanced.DocumentQuery<Project, ProjectListIndex>().Where($"Name:*{name}*");
            return query
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}
