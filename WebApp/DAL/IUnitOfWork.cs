using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork 
    {
        IGamesRepository Games { get; }
        IUsersRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
