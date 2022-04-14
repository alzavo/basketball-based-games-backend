using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using Extension.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DAL;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<Domain.App.User> _userManager;

        public UsersController(IUnitOfWork unitOfWork, ILogger<UsersController> logger, UserManager<Domain.App.User> userManager)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        [HttpGet("{phrase}")]
        public async Task<IEnumerable<User>> GetAll(string phrase)
        {
            return await _unitOfWork.Users.GetAllBySearchPhraseAsync(phrase, User.GetUserId()!.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            if (id != User.GetUserId()!.Value) return BadRequest();
            var user = await _unitOfWork.Users.GetOneDetailedAsync(id);
            if (user != null) return Ok(user);
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User dto) 
        {
            if (!id.Equals(dto.Id) || !id.Equals(User.GetUserId()!.Value)) return BadRequest();

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (await _userManager.FindByNameAsync(dto.UserName) != null) return BadRequest();

            user.UserName = dto.UserName;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id != User.GetUserId()!.Value) return BadRequest();
            await _unitOfWork.Users.DeleteAsync(id);
            return NoContent();
        }
    }
}
