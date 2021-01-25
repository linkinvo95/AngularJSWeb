namespace BusinessEntities
{
    public class ToDo : IdObject
    {
        private bool _completed;
        private string _description;

        public bool Completed
        {
            get => _completed;
            private set => _completed = value;
        }

        public string Description
        {
            get => _description;
            private set => _description = value;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public void SetCompleted(bool completed)
        {
            _completed = completed;
        }
    }
}