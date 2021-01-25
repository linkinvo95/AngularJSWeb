using System;

namespace WebApi.Models.Worksheet
{
    public class WorksheetModel
    {
        public Guid User { get; set; }
        public Guid? Project { get; set; }
        public string Description { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
    }
}