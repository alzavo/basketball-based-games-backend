namespace DAL
{
    public interface IRepository<TKey, TEntity, TDto>
        where TKey : IEquatable<TKey>
        where TEntity : class
        where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();

        Task<TDto?> GetOneAsync(TKey id);

        void Add(TDto dto);

        void Edit(TDto dto);

        void Delete(TDto dto);
    }
}
