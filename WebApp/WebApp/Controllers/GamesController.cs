using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mapper;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly ILogger<GamesController> _logger;

        public GamesController(AppDbContext context, ILogger<GamesController> logger) 
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> Get()
        {
            _logger.LogInformation("Get all games");
            return await _context.Games.Select(game => GameMapper.Map(game)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> Get(int id)
        {
            var result = await _context.Games.FindAsync(id);
            if (result == null) return NotFound();
            return GameMapper.Map(result);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> Post([FromBody] GameCreate game)
        {
            var addedGame = _context.Games.Add(GameMapper.Map(game)).Entity;
            await _context.SaveChangesAsync();
            var returnedGame = GameMapper.Map(addedGame);
            return Created("", returnedGame);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> Put(int id, [FromBody] GameUpdate game)
        {
            if (id != game.Id) return BadRequest();
            _context.Games.Update(GameMapper.Map(game));
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return NotFound();
            _context.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
