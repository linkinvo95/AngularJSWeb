using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Worksheet
{
  [AutoRegister]
  public class GetWorksheetService : IGetWorksheetService
  {
    private readonly IWorksheetRepository _worksheetRepository;

    public GetWorksheetService(IWorksheetRepository worksheetRepository)
    {
      _worksheetRepository = worksheetRepository;
    }


    public BusinessEntities.Worksheet GetWorksheet(Guid id)
    {
      return _worksheetRepository.Get(id);
    }

        public IEnumerable<BusinessEntities.Worksheet> GetWorksheets(Guid? userId, Guid? projectId, int skip, int take)
        {
            return _worksheetRepository.GetWorksheets(userId, projectId, skip, take);
        }
    }
}
