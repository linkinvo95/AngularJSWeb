using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Core.Services.ToDos;
using Data.Repositories;
using WebApi.Models.ToDo;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("todo")]
    public class ToDoController : BaseApiController
    {
        private readonly ICreateToDoService _createToDoService;
        private readonly IToDoRepository _toDoRepository;
        private readonly IUpdateToDoService _updateToDoService;

        public ToDoController(ICreateToDoService createToDoService, IToDoRepository toDoRepository, IUpdateToDoService updateToDoService)
        {
            _createToDoService = createToDoService;
            _toDoRepository = toDoRepository;
            _updateToDoService = updateToDoService;
        }

        [Route("create/{todoId:guid}")]
        [HttpPost]
        public HttpResponseMessage CreateToDo(Guid todoId, [FromBody] ToDoModel model)
        {
            var toDo = _createToDoService.Create(todoId, model.Description);
            return Found(new ToDoData(toDo));
        }

        [Route("update/{todoId:guid}")]
        [HttpPost]
        public HttpResponseMessage UpdateToDo(Guid todoId, [FromBody] ToDoModel model)
        {
            var toDo = _toDoRepository.Get(todoId);
            if (toDo == null)
            {
                return DoesNotExist();
            }
            _updateToDoService.Update(toDo, model.Description);
            return Found(new ToDoData(toDo));
        }

        [Route("completed/{todoId:guid}")]
        [HttpPost]
        public HttpResponseMessage CompleteToDo(Guid todoId, [FromBody] TodoCompletedModel model)
        {
            var toDo = _toDoRepository.Get(todoId);
            if (toDo == null)
            {
                return DoesNotExist();
            }
            toDo.SetCompleted(model.Completed);
            return Found(new ToDoData(toDo));
        }

        [Route("delete/{todoId:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeleteToDo(Guid todoId)
        {
            var toDo = _toDoRepository.Get(todoId);
            if (toDo == null)
            {
                return DoesNotExist();
            }
            _toDoRepository.Delete(toDo);
            return Found();
        }

        [Route("get/{todoId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetToDo(Guid todoId)
        {
            var toDo = _toDoRepository.Get(todoId);
            return toDo == null
                ? DoesNotExist()
                : Found(new ToDoData(toDo));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetToDos(string description = null)
        {
            var toDos = _toDoRepository.Get(description)
                .Select(q => new ToDoData(q))
                .ToList();
            return Found(toDos);
        }
    }
}