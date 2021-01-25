using System.Linq;
using BusinessEntities;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class ToDoListIndex : AbstractIndexCreationTask<ToDo>
    {
        public ToDoListIndex()
        {
            Map = todos => from todo in todos
                select new
                {
                    todo.Description
                };
        }
    }
}