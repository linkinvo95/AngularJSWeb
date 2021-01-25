using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BusinessEntities;
using Core.Services.Users;
using Data.Repositories;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IUpdateUserService _updateUserService;
        private readonly IUserRepository _userRepository;

        public UserController(ICreateUserService createUserService, IUserRepository userRepository, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _userRepository = userRepository;
            _updateUserService = updateUserService;
        }

        [Route("create/{userId:guid}")]
        [HttpPost]
        public HttpResponseMessage CreateUser(Guid userId, [FromBody] UserModel model)
        {
            var user = _userRepository.Get(userId);
            if (user != null)
            {
                return Error("User already exists");
            }
            user = _createUserService.Create(userId, model.Name, model.Email, model.Type);
            return Found(new UserData(user));
        }

        [Route("update/{userId:guid}")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            _updateUserService.Update(user, model.Name, model.Email, model.Type);
            return Found(new UserData(user));
        }

        [Route("delete/{userId:guid}")]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(Guid userId)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
            {
                return DoesNotExist();
            }
            _userRepository.Delete(user);
            return Found();
        }

        [Route("{userId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetUser(Guid userId)
        {
            var user = _userRepository.Get(userId);
            return user == null
                ? DoesNotExist()
                : Found(new UserData(user));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetUsers(int skip, int take, string name = null, string email = null, UserTypes? userType = null)
        {
            var users = _userRepository.Get(name, email, userType, skip, take);
            var data = users.Select(user => new UserData(user));
            return Found(data);
        }
    }
}