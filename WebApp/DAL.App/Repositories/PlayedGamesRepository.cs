using Entity = Domain.App.PlayedGame;
using Dto = PublicApi.DTO.v1.PlayedGame;
using DAL.Base;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Mapping;
using DAL.App.EF;

namespace DAL.App.Repositories
{
    public class PlayedGamesRepository : BaseRepository<int, Entity, Dto>, IPlayedGamesRepository
    {
        public PlayedGamesRepository(AppDbContext dbContext, IMapper<Entity, Dto> mapper) : base(dbContext, mapper)
        {
        }

        public async Task<IEnumerable<Dto>> GetAllDetailedAsync(int userId)
        {
            return await DbContext.Set<Entity>()
                .Where(pg => pg.UserId.Equals(userId))
                .Include(pg => pg.User)
                .Include(pg => pg.Game)
                .Select(pg => Mapper.Map(pg))
                .ToListAsync();
        }

        public async Task<Dto?> GetOneDetailedAsync(int id, int userId)
        {
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var dto = await DbContext.Set<Entity>()
                .Where(pg => pg.UserId.Equals(userId))
                .Include(pg => pg.User)
                .Include(pg => pg.Game)
                .FirstOrDefaultAsync(pg => pg.Id.Equals(id));
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            if (dto != null) return Mapper.Map(dto);
            return null;
        }
    }
}
