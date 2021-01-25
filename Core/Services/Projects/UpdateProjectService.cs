using BusinessEntities;
using Common;

namespace Core.Services.Projects
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProjectService : IUpdateProjectService
    {
        public void Update(Project project, string name)
        {
            project.SetName(name);
        }
    }
}