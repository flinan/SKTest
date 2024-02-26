using Microsoft.AspNetCore.Mvc;
using SKTest.Server.Dtos;
using SKTest.Server.Interfaces;

namespace SKTest.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<UserDto> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpGet("DeleteUser")]
        public void DeleteUser(int userId)
        {
            _userService.DeleteUser(userId);
        }

        [HttpPost("UpdateUser")]
        public void UpdateUser([FromBody] UserDto user)
        {
            _userService.UpdateUser(user);
        }


        public class EmailRequest
        {
            public required string email { get; set; }
            public int userId { get; set; }
        }

        [HttpPost("IsEmailUsedByAnotherUser")]
        public bool IsEmailUsedByAnotherUser([FromBody] EmailRequest request)
        {
            return _userService.IsEmailUsedByAnotherUser(request.email, request.userId);
        }
    }
}
