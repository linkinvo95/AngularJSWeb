using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class ToDoRepository : Repository<ToDo>, IToDoRepository
    {
        private readonly IDocumentSession _documentSession;

        public ToDoRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<ToDo> Get(string description)
        {
            return _documentSession.Advanced.DocumentQuery<ToDo, ToDoListIndex>()
                .Where($"Description:*{description}*")
                .ToList();
        }
    }
}