using System;
using BusinessEntities;

namespace Core.Services.Worksheet
{
   public interface ICreateWorksheetService
    {
        BusinessEntities.Worksheet Create(Guid id, User user, Project project, TimeSpan time, DateTime date, string description);
    }
}
