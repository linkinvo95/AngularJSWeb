using BusinessEntities;

namespace WebApi.Models.Projects
{
  public class ProjectData : IdObjectData
  {
    public ProjectData(Project project) : base(project)
    {
      Name = project.Name;
    }

    public string Name { get; set; }
  }
}
