using System.Linq.Expressions;

namespace Hotel_Restaurant_Reservation.Application.Abstractions.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<TEntity?> GetByIdAsync(Guid id);

        public Task<IEnumerable<TEntity>?> GetAllAsync();

        /// <summary>
        /// Returns the base IQueryable for the entity.
        /// This is used for building more complex queries (e.g., with pagination)
        /// before executing them against the database.
        /// </summary>
        /// <returns>An IQueryable of the entity.</returns>
        IQueryable<TEntity> GetAllQuery();

        public Task<TEntity> AddAsync(TEntity entity);

        public Task<TEntity?> UpdateAsync(Guid id, TEntity entity);

        public Task<TEntity?> RemoveAsync(Guid id);

        public Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        public Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        public Task SaveChangesAsync();

        public Task<IEnumerable<TEntity>> Take(int number);
    }
}
