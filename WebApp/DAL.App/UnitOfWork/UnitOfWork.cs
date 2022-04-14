using DAL.App.EF;
using DAL.App.Repositories;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1.Mapper;

namespace DAL.App.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context, UserManager<Domain.App.User> userManager)
        {
            _context = context;
            Games = new GamesRepository(_context, new GameMapper());
            Users = new UsersRepository(_context, new UserMapper(), userManager);
            Friendships = new FriendshipsRepository(_context, new FriendshipMapper());
            PlayedGames = new PlayedGamesRepository(_context, new PlayedGameMapper());
        }

        public IGamesRepository Games { get; set; }
        public IUsersRepository Users { get; set; }
        public IFriendshipsRepository Friendships { get; set; }
        public IPlayedGamesRepository PlayedGames { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
