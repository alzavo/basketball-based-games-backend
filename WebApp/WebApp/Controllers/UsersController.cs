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
    [Route("api/[controller]")]
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
        [AllowAnonymous]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.Select(user => UserMapper.Map(user)).ToListAsync();
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
