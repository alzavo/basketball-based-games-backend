using Entity = Domain.App.PlayedGame;
using Dto = PublicApi.DTO.v1.PlayedGame;

namespace DAL.Repositories
{
    public interface IPlayedGamesRepository : IRepository<int, Entity, Dto>
    {
        Task<IEnumerable<Dto>> GetAllDetailedAsync(int userId);
        Task<Dto?> GetOneDetailedAsync(int id, int userId);
    }
}
