using System;
using System.Linq;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
  public class WorksheetListIndex : AbstractIndexCreationTask<Worksheet>
  {
    public WorksheetListIndex() 
    {
      Map = worksheets => from worksheet in worksheets
        select new
        {
          UserId = worksheet.User.Id,
          ProjectId = worksheet.Project != null ? worksheet.Project.Id : (Guid?) null
        };
      Index(x => x.User.Id, FieldIndexing.NotAnalyzed);
      Index(x => x.Project.Id, FieldIndexing.NotAnalyzed);

    }
  }
}
