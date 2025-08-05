using Hotel_Restaurant_Reservation.Application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_Restaurant_Reservation.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly HotelRestaurantDbContext _hotelRestaurantDbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(HotelRestaurantDbContext hotelRestaurantDbContext)
        {
            _hotelRestaurantDbContext = hotelRestaurantDbContext;
            _dbSet = _hotelRestaurantDbContext.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Implements the method to return the base IQueryable.
        /// </summary>
        public IQueryable<TEntity> GetAllQuery()
        {
            return _dbSet.AsQueryable();
        }

        // --- All other methods (AddAsync, GetByIdAsync, etc.) remain the same ---
        #region Other Methods
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await _hotelRestaurantDbContext.SaveChangesAsync();
        }

        public async Task<TEntity?> UpdateAsync(Guid id, TEntity entity)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null) return null;
            var properties = _hotelRestaurantDbContext.Entry(existingEntity).Properties.Where(p => !p.Metadata.IsPrimaryKey());
            foreach (var property in properties)
            {
                var newValue = _hotelRestaurantDbContext.Entry(entity).Property(property.Metadata.Name).CurrentValue;
                property.CurrentValue = newValue;
            }
            return existingEntity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return entities;
        }

        public async Task<TEntity?> RemoveAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return null;
            _dbSet.Remove(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> Take(int number)
        {
            return await _dbSet.Take(number).ToListAsync();
        }
        #endregion
    }
}
