using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork 
    {
        IGamesRepository Games { get; }
        IUsersRepository Users { get; }
        IFriendshipsRepository Friendships { get; }
        IPlayedGamesRepository PlayedGames { get; }
        Task<int> SaveChangesAsync();
    }
}
