using System;
using BusinessEntities;

namespace Core.Services.Worksheet
{
    public interface IUpdateWorksheetService
    {
      void Update(BusinessEntities.Worksheet worksheet, User user, Project project, TimeSpan time, DateTime date, string description);
  }
}
