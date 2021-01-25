using BusinessEntities;

namespace Core.Services.Projects
{
   public interface IUpdateProjectService
   {
       void Update(Project project, string name);
   }
}
