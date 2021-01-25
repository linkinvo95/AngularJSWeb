namespace WebApi.Models.ToDo
{
    public class ToDoData : IdObjectData
    {
        public ToDoData(BusinessEntities.ToDo toDo) : base(toDo)
        {
            Description = toDo.Description;
            Completed = toDo.Completed;
        }

        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}