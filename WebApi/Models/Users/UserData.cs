using BusinessEntities;

namespace WebApi.Models.Users
{
    public class UserData : IdObjectData
    {
        public UserData(User user) : base(user)
        {
            Email = user.Email;
            Name = user.Name;
            Type = user.Type;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public UserTypes Type { get; set; }
    }
}