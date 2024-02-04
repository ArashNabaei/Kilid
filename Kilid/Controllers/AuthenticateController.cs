using Kilid.Interfaces;
using Kilid.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kilid.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        private readonly IAuthService _authService;

        private readonly IUserRepository _userRepository;

        public AuthenticateController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var user = await _authService.SignUpAsync(request.PhoneNumber);

            if (user == null)
            {
                return Conflict("User already exists.");
            }

            // You may want to return additional information or a token for successful sign-up.

            return Ok(user);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            var user = await _authService.SignInAsync(request.PhoneNumber, request.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // You may want to return additional information or a token for successful sign-in.

            return Ok(user);
        }

        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            // Retrieve the user Id from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return Unauthorized("User not authenticated.");
            }

            // Use the retrieved userId to fetch the user's information
            var userInfo = await _userRepository.GetUserById(userId.Value);

            if (userInfo == null)
            {
                return NotFound("User not found.");
            }

            return Ok(userInfo);
        }

    }


    public class SignUpRequest
    {
        public string PhoneNumber { get; set; }
        // Other properties as needed
    }

    public class SignInRequest
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
