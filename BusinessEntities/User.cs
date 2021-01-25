using System;

namespace BusinessEntities
{
    public class User : IdObject
    {
        private string _email;
        private string _name;
        private UserTypes _types = UserTypes.Employee;

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public UserTypes Type
        {
            get => _types;
            set => _types = value;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided.");
            }
            _name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("Email was not provided.");
            }
            _email = email;
        }

        public void SetType(UserTypes type)
        {
            _types = type;
        }
    }
}