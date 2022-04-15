using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using Extension.Base;
using DAL;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlayedGamesController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PlayedGamesController> _logger;

        public PlayedGamesController(IUnitOfWork unitOfWork, ILogger<PlayedGamesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayedGame>>> Get()
        {
            return Ok(await _unitOfWork.PlayedGames.GetAllDetailedAsync(User.GetUserId()!.Value));
        }

        [HttpPost]
        public async Task<ActionResult<PlayedGame>> Post([FromBody] PlayedGameAllUsers dto)
        {
            foreach (var game in dto.PlayedGames) 
            {
                _unitOfWork.PlayedGames.Add(game);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var game = await _unitOfWork.PlayedGames.GetOneAsync(id);
            if (game == null) return NotFound();
            _unitOfWork.PlayedGames.Delete(game);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
