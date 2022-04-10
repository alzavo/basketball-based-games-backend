using DAL.App.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mapper;
using Extension.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(AppDbContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Select(user => UserMapper.Map(user)).ToListAsync();
        }

        [HttpGet("{name}")]
        public async Task<IEnumerable<User>> GetAllByName(string name)
        {
            var userId = User.GetUserId()!.Value;
            return await _context.Users
                .Where(user => !user.Id.Equals(userId))
                .Where(user => user.UserName.Contains(name))
                .Include(user => user.FriendshipsWithUser)
                .Where(user => !user.FriendshipsWithUser!.Any(friendship => friendship.UserId.Equals(userId)))
                .Select(user => UserMapper.Map(user))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            if (user.Id != User.GetUserId()!.Value) return BadRequest();
            return UserMapper.Map(user);
        }


        // add scoped for repository in program.cs, that creates every time new scoped instance (DI for AppDbContext)
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UserUpdate userUpdate)
        {
            if (userUpdate.Id != id) return BadRequest();

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            if (user.Id != User.GetUserId()!.Value) return BadRequest();

            _context.Users.Update(UserMapper.Map(userUpdate));
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            if (user.Id != User.GetUserId()!.Value) return BadRequest();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
