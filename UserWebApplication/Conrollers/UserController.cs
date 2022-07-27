using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserWebApplication.Model;
using UserWebApplication.Repository;
using UserWebApplication.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserWebApplication.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<UserModel> _signInManager;


        public UserController(
            SignInManager<UserModel> signInManager,
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("new")]
        public async Task<ActionResult<UserWithToken>> Register(UserModel user)
        {
            var newUser = new UserModel
            {
                Username = user.Username,
                Email = user.Email,
                Fullname = user.Fullname,
                Password = user.Password
            };

            var result = await _userRepository.CreateNewUser(newUser);

            if (result.Succeeded)
            {
                newUser = await _userRepository.GetUserByUsername(user.Username);

                UserWithToken userWithToken = new UserWithToken()
                {
                    UserId = newUser.UserId,
                    Username = newUser.Username,
                    Email = newUser.Email,
                    Fullname = newUser.Fullname,
                    Password = user.Password,
                    Token = _tokenService.CreateToken(newUser)
                };

                return Ok(userWithToken);
            }

            return BadRequest("Something went wrong, try again.");
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserWithToken>> Login(UserLogin userLogin)
        {
            var user = await _userRepository.GetUserByUsername(userLogin.Username);

            if (user != null)
            {

                // TODO proof if the password is correct
                var result = await _signInManager.CheckPasswordSignInAsync(
                    user,
                    userLogin.Password, false);

                if (result.Succeeded)
                {
                    UserWithToken userWithToken = new UserWithToken
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        Email = user.Email,
                        Fullname = user.Fullname,
                        Password = user.Password,
                        Token = _tokenService.CreateToken(user)

                    };

                    return Ok(userWithToken);
                }
            }
            return BadRequest("Loging is not valid, try again.");
        }
    }
}

