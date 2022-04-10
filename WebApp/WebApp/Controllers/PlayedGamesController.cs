using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mapper;
using Extension.Base;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PlayedGamesController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly ILogger<PlayedGamesController> _logger;

        public PlayedGamesController(AppDbContext context, ILogger<PlayedGamesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<PlayedGame>> Get()
        {
            return await _context.PlayedGames
                .Where(pg => pg.UserId.Equals(User.GetUserId()!.Value))
                .Include(pg => pg.Game)
                .Include(pg => pg.User)
                .Select(pg => PlayedGameMapper.Map(pg)).ToListAsync();


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlayedGame>> Get(int id)
        {
            var result = await _context.PlayedGames
                .Include(pg => pg.Game)
                .Include(pg => pg.User)
                .FirstAsync(pg => pg.Id.Equals(id));

            if (result == null) return NotFound();
            return PlayedGameMapper.Map(result);
        }

        [HttpPost]
        public async Task<ActionResult<PlayedGame>> Post([FromBody] PlayedGameAllUsers dto)
        {
            foreach (var game in dto.PlayedGames) 
            {
                var addedGame = _context.PlayedGames.Add(PlayedGameMapper.Map(game)).Entity;
            }
            await _context.SaveChangesAsync();

            return Created("", null);
        }

        [HttpPut]
        public async Task<ActionResult<PlayedGame>> Put(int id, [FromBody] PlayedGameUpdate game)
        {
            if (id != game.Id) return BadRequest();
            _context.PlayedGames.Update(PlayedGameMapper.Map(game));
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var game = await _context.PlayedGames.FindAsync(id);
            if (game == null) return NotFound();
            _context.PlayedGames.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
