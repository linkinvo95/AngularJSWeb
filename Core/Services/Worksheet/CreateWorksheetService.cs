using System;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Worksheet
{
    [AutoRegister]
    public class CreateWorksheetService : ICreateWorksheetService
    {
        private readonly IUpdateWorksheetService _updateWorksheetService;
        private readonly IIdObjectFactory<BusinessEntities.Worksheet> _worksheetFactory;
        private readonly IWorksheetRepository _worksheetRepository;

        public CreateWorksheetService(IUpdateWorksheetService updateWorksheetService, IWorksheetRepository worksheetRepository, IIdObjectFactory<BusinessEntities.Worksheet> worksheetFactory)
        {
            _updateWorksheetService = updateWorksheetService;
            _worksheetRepository = worksheetRepository;
            _worksheetFactory = worksheetFactory;
        }

        public BusinessEntities.Worksheet Create(Guid id, User user, Project project, TimeSpan time, DateTime date, string description)
        {
            var worksheet = _worksheetFactory.Create(id);
            _updateWorksheetService.Update(worksheet, user, project, time, date, description);
            _worksheetRepository.Save(worksheet);
            return worksheet;
        }
    }
}
