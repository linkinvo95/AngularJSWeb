using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Worksheet
{
  public interface IGetWorksheetService
  {
    BusinessEntities.Worksheet GetWorksheet(Guid id);
    IEnumerable<BusinessEntities.Worksheet> GetWorksheets(Guid? userId, Guid? projectId, int skip, int take);
  }
}
