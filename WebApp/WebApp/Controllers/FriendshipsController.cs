using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using Extension.Base;
using DAL;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FriendshipsController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<FriendshipsController> _logger;

        public FriendshipsController(IUnitOfWork unitOfWork, ILogger<FriendshipsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friendship>>> Get()
        {
            return Ok(await _unitOfWork.Friendships.GetAllDetailedAsync(User.GetUserId()!.Value));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Friendship>> Get(int id)
        {
            var dto = await _unitOfWork.Friendships.GetOneDetailedAsync(id);
            if (dto != null)
            {
                if (!dto.UserId.Equals(User.GetUserId()!.Value)) return BadRequest();
                return Ok(dto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Friendship>> Post([FromBody] Friendship dto)
        {
            _unitOfWork.Friendships.Add(dto);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _unitOfWork.Friendships.GetOneAsync(id);
            if (dto == null) return NotFound();
            _unitOfWork.Friendships.Delete(dto);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
