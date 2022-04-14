using Entity = Domain.App.Friendship;
using Dto = PublicApi.DTO.v1.Friendship;

namespace DAL.Repositories
{
    public interface IFriendshipsRepository : IRepository<int, Entity, Dto>
    {
        Task<IEnumerable<Dto>> GetAllDetailedAsync(int userId);
        Task<Dto?> GetOneDetailedAsync(int id);
    }
}
