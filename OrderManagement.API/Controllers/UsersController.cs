using Microsoft.AspNetCore.Mvc;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.DTO.Authenticate;
using OrderManagement.Common.DTO.User;
using OrderManagement.Common.Helpers;
using Serilog;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBusinessEngine _userBusinessEngine;

        public UsersController(IUserBusinessEngine userBusinessEngine)
        {
            _userBusinessEngine = userBusinessEngine;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userBusinessEngine.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(UserCreateDto userCreateDto)
        {
            var response = _userBusinessEngine.CreateUser(userCreateDto);
            //if (!response.Status)
            //{
            //    return BadRequest(response);
            //}

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            var userIdInt = Convert.ToInt32(userId);
            var response = _userBusinessEngine.GetById(userIdInt);

            //if (!response.Status)
            //{
            //    BadRequest(response);
            //}

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{userid}")]
        public IActionResult GetUserById(int userid)
        {
            var response = _userBusinessEngine.GetById(userid);
            //if (!response.Status)
            //{
            //    return BadRequest(response);
            //}

            return Ok(response);
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            Log.Information("Getting all users.");

            var users = _userBusinessEngine.GetAll();
            if (users.Count == 0)
                throw new KeyNotFoundException("Users not found. Try again");

            Log.Information($"users: {users}");
            return Ok(users);
        }
    }
}
