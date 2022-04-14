using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamesController : ControllerBase
    {

        private readonly ILogger<GamesController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GamesController(ILogger<GamesController> logger, IUnitOfWork unitOfWork) 
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Game>>> Get()
        {
            return Ok(await _unitOfWork.Games.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game?>> Get(int id)
        {
            var dto = await _unitOfWork.Games.GetOneAsync(id);
            if (dto != null) return Ok(dto);
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Post([FromBody] Game game)
        {
            _unitOfWork.Games.Add(game);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> Put(int id, [FromBody] Game game)
        {
            if (id != game.Id) return BadRequest();
            _unitOfWork.Games.Edit(game);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _unitOfWork.Games.GetOneAsync(id);
            if (dto == null) return NotFound();
            _unitOfWork.Games.Delete(dto);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
