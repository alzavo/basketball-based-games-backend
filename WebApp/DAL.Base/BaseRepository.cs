using Domain;
using Mapping;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base
{
    public class BaseRepository<TKey, TEntity, TDto> : IRepository<TKey, TEntity, TDto>
        where TKey : IEquatable<TKey>
        where TEntity : class
        where TDto : class
    {
        protected readonly DbContext DbContext;
        protected readonly IMapper<TEntity, TDto> Mapper;

        public BaseRepository(DbContext dbContext, IMapper<TEntity, TDto> mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().Select(entity => Mapper.Map(entity)).ToListAsync();
        }

        public async Task<TDto?> GetOneAsync(TKey id)
        {
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var entity = await DbContext.Set<TEntity>().FindAsync(id);
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            if (entity != null) {
                return Mapper.Map(entity);
            }

            return null;
        }

        public void Add(TDto dto)
        {
            DbContext.Set<TEntity>().Add(Mapper.Map(dto));
        }

        public void Delete(TDto dto)
        {
            DbContext.Set<TEntity>().Remove(Mapper.Map(dto));
        }

        public void Edit(TDto dto)
        {
            DbContext.Set<TEntity>().Update(Mapper.Map(dto));
        }
    }
}
