using BusinessEntities;

namespace Core.Services.ToDos
{
    public interface IUpdateToDoService
    {
        void Update(ToDo toDo, string description);
    }
}