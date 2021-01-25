using BusinessEntities;

namespace WebApi.Models.Users
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public UserTypes Type { get; set; }
    }
}