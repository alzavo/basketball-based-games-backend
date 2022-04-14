using Entity = Domain.App.User;
using Dto = PublicApi.DTO.v1.User;

namespace DAL.Repositories
{
    public interface IUsersRepository : IRepository<int, Entity, Dto>
    {
        Task<Dto?> GetOneDetailedAsync(int id);
        Task<IEnumerable<Dto>> GetAllBySearchPhraseAsync(string phrace, int userId);

        Task DeleteAsync(int id);
    }
}
