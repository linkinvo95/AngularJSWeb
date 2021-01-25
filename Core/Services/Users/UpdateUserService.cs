using BusinessEntities;
using Common;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateUserService : IUpdateUserService
    {
        public void Update(User user, string name, string email, UserTypes type)
        {
            user.SetName(name);
            user.SetEmail(email);
            user.SetType(type);
        }
    }
}