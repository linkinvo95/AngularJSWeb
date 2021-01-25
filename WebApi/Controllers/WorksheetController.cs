using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Core.Services.Worksheet;
using Data.Repositories;
using WebApi.Models.Worksheet;

namespace WebApi.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  [RoutePrefix("worksheets")]
  public class WorksheetController : BaseApiController
  {
    private readonly ICreateWorksheetService _createWorksheetService;
    private readonly IGetWorksheetService _getWorksheetService;
    private readonly IProjectRepository _projectRepository;
    private readonly IUpdateWorksheetService _updateWorksheetService;
    private readonly IUserRepository _userRepository;
    private readonly IWorksheetRepository _worksheetRepository;

    public WorksheetController(ICreateWorksheetService createWorksheetService, IUpdateWorksheetService updateWorksheetService, IWorksheetRepository worksheetRepository, IUserRepository userRepository, IProjectRepository projectRepository, IGetWorksheetService getWorksheetService)
    {
      _createWorksheetService = createWorksheetService;
      _updateWorksheetService = updateWorksheetService;
      _worksheetRepository = worksheetRepository;
      _userRepository = userRepository;
      _projectRepository = projectRepository;
      _getWorksheetService = getWorksheetService;
    }

    [Route("create/{worksheetId:guid}")]
    [HttpPost]
    public HttpResponseMessage CreateWorksheet(Guid worksheetId, [FromBody] WorksheetModel model)
    {
      var user = _userRepository.Get(model.User);
      if (user == null)
      {
        return DoesNotExist();
      }
      var project = model.Project.HasValue
        ? _projectRepository.Get(model.Project.Value)
        : null;
      var worksheet = _createWorksheetService.Create(worksheetId, user, project, model.Time, model.Date, model.Description);
      return Found(new WorksheetData(worksheet));
    }

    [Route("update/{worksheetId:guid}")]
    [HttpPost]
    public HttpResponseMessage UpdateWorksheet(Guid worksheetId, [FromBody] WorksheetModel model)
    {
      var worksheet = _worksheetRepository.Get(worksheetId);
      if (worksheet == null)
      {
        return DoesNotExist();
      }
      var user = _userRepository.Get(model.User);
      if (user == null)
      {
        return DoesNotExist();
      }
      var project = model.Project.HasValue
        ? _projectRepository.Get(model.Project.Value)
        : null;
      _updateWorksheetService.Update(worksheet, user, project, model.Time, model.Date, model.Description);
      return Found(new WorksheetData(worksheet));
    }

    [Route("delete/{worksheetId:guid}")]
    [HttpDelete]
    public HttpResponseMessage DeleteWorksheet(Guid worksheetId)
    {
      var worksheet = _worksheetRepository.Get(worksheetId);
      if (worksheet == null)
      {
        return DoesNotExist();
      }
      _worksheetRepository.Delete(worksheet);
      return Found();
    }

    [Route("{worksheetId:guid}")]
    [HttpGet]
    public HttpResponseMessage GetWorksheet(Guid worksheetId)
    {
      var worksheet = _getWorksheetService.GetWorksheet(worksheetId);
      return Found(new WorksheetData(worksheet));
    }
       
        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetWorksheets(int skip, int take, Guid? userId = null, Guid? projectId = null)
        {
            var worksheets = _getWorksheetService.GetWorksheets(userId, projectId, skip, take);
            var data = worksheets.Select(w => new WorksheetData(w));
            return Found(data);
        }
  }
}
