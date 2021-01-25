using System.Linq;
using BusinessEntities;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class ProjectListIndex : AbstractIndexCreationTask<Project>
    {
        public ProjectListIndex()
        {
            Map = projects => from project in projects
                select new
                {
                    project.Name
                };
        }
    }
}