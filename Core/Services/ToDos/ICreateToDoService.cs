using System;
using BusinessEntities;

namespace Core.Services.ToDos
{
    public interface ICreateToDoService
    {
        ToDo Create(Guid todoId, string description);
    }
}