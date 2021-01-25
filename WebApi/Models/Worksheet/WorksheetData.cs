using Raven.Client.Linq.Indexing;
using System;
using WebApi.Models.Projects;
using WebApi.Models.Users;

namespace WebApi.Models.Worksheet
{
    public class WorksheetData : IdObjectData
    {
        public WorksheetData(BusinessEntities.Worksheet worksheet) : base(worksheet)
        {
            User = new IdNameData(worksheet.User.Id, worksheet.User.Name);
            Project = worksheet.Project != null ? new IdNameData(worksheet.Project.Id, worksheet.Project.Name) : null;
            Time = worksheet.Time;
            Date = worksheet.Date;
            Description = worksheet.Description;
        }

        public IdNameData User { get; set; }
        public IdNameData Project { get; set; }
        public string Description { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
    }
}
