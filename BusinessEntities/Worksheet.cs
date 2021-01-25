using System;

namespace BusinessEntities
{
    public class Worksheet : IdObject
    {
        public User User { get; set; }
        public Project Project { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set;}

        public void Set(User user, Project project, TimeSpan time, DateTime date, string description)
        {
            User = user;
            Project = project;
            Time = time;
            Date = date;
            Description = description;
        }
    }
}
