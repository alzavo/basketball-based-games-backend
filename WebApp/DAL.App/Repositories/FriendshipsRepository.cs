using DAL.Base;
using Entity = Domain.App.Friendship;
using Dto = PublicApi.DTO.v1.Friendship;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Mapping;
using DAL.App.EF;

namespace DAL.App.Repositories
{
    internal class FriendshipsRepository : BaseRepository<int, Entity, Dto>, IFriendshipsRepository
    {
        public FriendshipsRepository(AppDbContext dbContext, IMapper<Entity, Dto> mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<Dto>> GetAllDetailedAsync(int userId)
        {
            return await DbContext.Set<Entity>()
                .Where(f => f.UserId.Equals(userId))
                .Include(f => f.User)
                .Include(f => f.Friend)
                .Select(f => Mapper.Map(f))
                .ToListAsync();
        }

        public async Task<Dto?> GetOneDetailedAsync(int id)
        {
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var dto = await DbContext.Set<Entity>()
                .Include(f => f.User)
                .Include(f => f.Friend)
                .FirstOrDefaultAsync(user => user.Id.Equals(id));
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            if (dto != null) return Mapper.Map(dto);
            return null;
        }
    }
}
