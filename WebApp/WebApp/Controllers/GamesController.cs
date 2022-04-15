using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;

namespace WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
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
            return Ok(await _unitOfWork.Games.GetAllDetailedAsync());
        }
    }
}
