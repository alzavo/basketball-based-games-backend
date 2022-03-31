using Domain.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;

namespace WebApp.Controllers.Identity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AccountController> logger, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<JwtResponse>> Register([FromBody] Register dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user != null)
            {
                _logger.LogWarning("User {UserName} already registered!", dto.UserName);
                return BadRequest(new Message("User already registered!"));
            }

            user = new User()
            {
                UserName = dto.UserName,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded) 
            {
                _logger.LogInformation("User {UserName} created a new account with password!", dto.UserName);
                var appUser = await _userManager.FindByNameAsync(user.UserName);
                if (appUser != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                    var jwt = Extension.Base.IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );

                    _logger.LogInformation("User {UserName} token is ready", dto.UserName);
                    return Ok(new JwtResponse()
                    {
                        Token = jwt,
                        UserName = appUser.UserName
                    });
                }

                _logger.LogWarning("User {UserName} not found after creation!", dto.UserName);
                return BadRequest(new Message("User not found after creation!"));
            }

            _logger.LogWarning("Failed to create User {UserName}!", dto.UserName);
            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new Message() { Messages = errors });
        }

        [HttpPost]
        public async Task<ActionResult<JwtResponse>> LogIn([FromBody] LogIn dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                _logger.LogWarning("WebApi login failed. User {User} not found!", dto.UserName);
                return NotFound(new Message("User or Password problem!"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                var jwt = Extension.Base.IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                );

                _logger.LogInformation("WebApi login. User {User}", dto.UserName);
                return Ok(new JwtResponse()
                {
                    Token = jwt,
                    UserName = user.UserName
                });
            }

            _logger.LogWarning("WebApi login failed. User {User} - bad password!", dto.UserName);
            return NotFound(new Message("User or Password problem!"));
        }
    }
}
