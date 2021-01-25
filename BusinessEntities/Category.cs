using System;

namespace BusinessEntities
{
    public class Category : IdObject
    {
        public string Name { get; private set; }
        public bool Active { get; private set;}

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided.");
            }
            Name = name;
        }

        public void SetActive(bool active)
        {
            Active = active;
        }
    }
}
