using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using Mapper = PublicApi.DTO.v1.Mapper.FriendshipMapper;
using Extension.Base;


namespace WebApp.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FriendshipsController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly ILogger<FriendshipsController> _logger;

        public FriendshipsController(AppDbContext context, ILogger<FriendshipsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Friendship>> Get()
        {
            return await _context.Friendships
                .Where(f => f.UserId.Equals(User.GetUserId()))
                .Include(f => f.User)
                .Include(f => f.Friend)
                .Select(f => Mapper.Map(f)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Friendship>> Get(int id)
        {
            var result = await _context.Friendships
                .Include(f => f.User)
                .Include(f => f.Friend)
                .FirstAsync(f => f.Id.Equals(id));

            if (result == null) return NotFound();
            return Mapper.Map(result);
        }

        [HttpPost]
        public async Task<ActionResult<Friendship>> Post([FromBody] FriendshipCreate friendship)
        {
            var addedFriendship = _context.Friendships.Add(Mapper.Map(friendship)).Entity;
            await _context.SaveChangesAsync();
            return Created("", addedFriendship);
        }

        [HttpPut]
        public async Task<ActionResult<Friendship>> Put(int id, [FromBody] FriendshipUpdate friendship)
        {
            if (id != friendship.Id) return BadRequest();
            _context.Friendships.Update(Mapper.Map(friendship));
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var friendship = await _context.Friendships.FindAsync(id);
            if (friendship == null) return NotFound();
            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
