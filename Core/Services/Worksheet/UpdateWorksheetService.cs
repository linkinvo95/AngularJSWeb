using System;
using BusinessEntities;
using Common;

namespace Core.Services.Worksheet
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateWorksheetService : IUpdateWorksheetService
    {
        public void Update(BusinessEntities.Worksheet worksheet, User user, Project project, TimeSpan time, DateTime date, string description)
        {
            worksheet.Set(user, project, time, date, description);
        }
    }
}
