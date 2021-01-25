using System;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Projects
{
  [AutoRegister]
  public class CreateProjectService : ICreateProjectService
  {
    private readonly IIdObjectFactory<Project> _projectFactory;
    private readonly IProjectRepository _projectRepository;
    private readonly IUpdateProjectService _updateProjectService;

    public CreateProjectService(IUpdateProjectService updateProjectService, IIdObjectFactory<Project> userFactory,
                                IProjectRepository userRepository)
    {
      _updateProjectService = updateProjectService;
      _projectFactory = userFactory;
      _projectRepository = userRepository;
    }

    public Project Create(Guid id, string name)
    {
      var project = _projectFactory.Create(id);
      _updateProjectService.Update(project, name);
      _projectRepository.Save(project);
      return project;
    }
  }
}
