using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Core.Services.Projects;
using Data.Repositories;
using WebApi.Models.Projects;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
  [RoutePrefix("projects")]
  public class ProjectController : BaseApiController
  {
    private readonly ICreateProjectService _createProjectService;
    private readonly IProjectRepository _projectRepository;
    private readonly IUpdateProjectService _updateProjectService;

    public ProjectController(ICreateProjectService createProjectService, IProjectRepository projectRepository, IUpdateProjectService updateProjectService)
    {
      _createProjectService = createProjectService;
      _projectRepository = projectRepository;
      _updateProjectService = updateProjectService;
    }

    [Route("create/{projectId:guid}")]
    [HttpPost]
    public HttpResponseMessage CreateProject(Guid projectId, [FromBody] ProjectModel model)
    {
      var project = _createProjectService.Create(projectId, model.Name);
      return Found(new ProjectData(project));
    }

    [Route("update/{projectId:guid}")]
    [HttpPost]
    public HttpResponseMessage UpdateProject(Guid projectId, [FromBody] ProjectModel model)
    {
      var project = _projectRepository.Get(projectId);
      if (project == null)
      {
        return DoesNotExist();
      }
      _updateProjectService.Update(project, model.Name);
      return Found(new ProjectData(project));
    }


    [Route("delete/{projectId:guid}")]
    [HttpDelete]
    public HttpResponseMessage DeleteProject(Guid projectId)
    {
      var project = _projectRepository.Get(projectId);
      if (project == null)
      {
        return DoesNotExist();
      }
      _projectRepository.Delete(project);
      return Found();
    }

    [Route("list")]
    [HttpGet]
    public HttpResponseMessage GetProjects(int skip, int take, string name = null)
    {
      var projects = _projectRepository.Get(name, skip, take)
        .Take(take)
        .Skip(skip)
        .ToList();
      var data = projects.Select(project => new ProjectData(project));
      return Found(data);
    }
  }
}
