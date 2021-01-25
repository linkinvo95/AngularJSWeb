using System;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.ToDos
{
    [AutoRegister]
    public class CreateToDoService : ICreateToDoService
    {
        private readonly IIdObjectFactory<ToDo> _toDoFactory;
        private readonly IToDoRepository _toDoRepository;
        private readonly IUpdateToDoService _updateToDoService;

        public CreateToDoService(IIdObjectFactory<ToDo> doFactory, IToDoRepository doRepository, IUpdateToDoService updateToDoService)
        {
            _toDoFactory = doFactory;
            _toDoRepository = doRepository;
            _updateToDoService = updateToDoService;
        }

        public ToDo Create(Guid todoId, string description)
        {
            var toDo = _toDoFactory.Create(todoId);
            _updateToDoService.Update(toDo, description);
            _toDoRepository.Save(toDo);
            return toDo;
        }
    }
}