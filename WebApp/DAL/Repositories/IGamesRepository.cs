using Entity = Domain.App.Game;
using Dto = PublicApi.DTO.v1.Game;

namespace DAL.Repositories
{
    public interface IGamesRepository : IRepository<int, Entity, Dto>
    {
        // custom methods
    }
}
