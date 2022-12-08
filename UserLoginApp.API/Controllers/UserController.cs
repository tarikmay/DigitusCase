using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using UserLoginApp.Business.Attributes;
using UserLoginApp.Business.Helper.Claims;
using UserLoginApp.Business.Interfaces;
using UserLoginApp.DTO.DTOs.UserDTOs;
using UserLoginApp.Entities.Concrete;
using UserLoginApp.Entities.Concrete.Reponse;
using UserLoginApp.Entities.Conrete;

namespace UserLoginApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IRequestTimeService _requestTimeService;

        public UserController(IRequestTimeService requestTimeService,IMemoryCache memoryCache,IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _requestTimeService=requestTimeService;
        }

        [HttpGet("Users")]
        [PermissionAttr("Admin")]
        public IEnumerable<User> Users()
        {
            return _userService.GetAll();
        }
        [HttpGet("{id}")]
        public User GetUserById(string id)
        {
            return _userService.Get(x=>x.Id==id);
        }
        [Authorize]
        [HttpGet("GetUser")]
        public User GetUser()
        {
            return _userService.Get(x => x.Id == ClaimManager.GetClaim("UserId"));
        }
        [HttpPost("Confirm/{key}")]
        public ConfirmMailResponse Confirm(string key)
        {
            return _userService.ConfirmUser(key);
        }

        [HttpPost("Login")]
        public Token Login(LoginDto login)
        {
            return _userService.Login(login);
        }

        [HttpPost("Register")]
        public User Register(UserRegisterDto userRegister)
        {
            return _userService.Register(_mapper.Map<User>(userRegister));
        }
        [HttpPost("ForgotPassword")]
        public ForgotPasswordResponse ForgotPassword(ForgotPasswordDto forgotPassword)
        {
            return _userService.ForgotPassword(forgotPassword);
        }
        [HttpPost("NewPassword")]
        public NewPasswordResponse NewPassword(NewPasswordDto newPasswordDto)
        {
            return _userService.NewPassword(newPasswordDto);
        }
        [HttpGet("OnlineUser")]
        [PermissionAttr("Admin")]
        public List<OnlineUser> OnlineUsers()
        {
            _memoryCache.TryGetValue<List<OnlineUser>>("Onlines", out List<OnlineUser> list);
            return list;
        }

        [HttpGet("RegisteredUsers")]
        [PermissionAttr("Admin")]
        public IEnumerable<User> RegisteredUsers()
        {
            return _userService.GetRegisteredUsers();
        }

        [HttpGet("RegisteredUsersByDay")]
        [PermissionAttr("Admin")]
        public IEnumerable<User> RegisteredUsersByDay(int day)
        {
            return _userService.GetRegisteredUsersByDay(day);
        }

        [HttpGet("GetNotConfirmedUsersByDay")]
        [PermissionAttr("Admin")]
        public IEnumerable<User> GetNotConfirmedUsersByDay(int day)
        {
            return _userService.GetNotConfirmedUsersByDay(day);
        }
        [HttpGet("GetUsersRequestCompleteTimeByDate")]
        [PermissionAttr("Admin")]
        public IEnumerable<RequestTime> GetUsersRequestCompleteTimeByDate(string date)
        {
            return _requestTimeService.GetUsersRequestCompleteTimeByDate(date);
        }



    }
}
