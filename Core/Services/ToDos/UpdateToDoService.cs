using System;
using BusinessEntities;
using Common;

namespace Core.Services.ToDos
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateToDoService : IUpdateToDoService
    {
        public void Update(ToDo toDo, string description)
        {
            toDo.SetDescription(description);
        }
    }
}