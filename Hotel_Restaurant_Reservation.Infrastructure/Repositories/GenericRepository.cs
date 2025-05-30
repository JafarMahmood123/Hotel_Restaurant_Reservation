using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_Restaurant_Reservation.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly HotelRestaurantDbContext _hotelRestaurantDbContext;

    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(HotelRestaurantDbContext hotelRestaurantDbContext)
    {
        _hotelRestaurantDbContext = hotelRestaurantDbContext;

        _dbSet = _hotelRestaurantDbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
         await _dbSet.AddAsync(entity);

        return entity;
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        var filteredDbSet = _dbSet.Where(predicate);

        return filteredDbSet;
    }

    public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
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

        // 1. Get existing entity
        var existingEntity = await _dbSet.FindAsync(id);

        if (existingEntity == null) 
            return null;

        // 2. Get all properties EXCEPT the primary key
        var properties = _hotelRestaurantDbContext.Entry(existingEntity).Properties
            .Where(p => !p.Metadata.IsPrimaryKey());

        // 3. Update only non-key properties
        foreach (var property in properties)
        {
            var newValue = _hotelRestaurantDbContext.Entry(entity).Property(property.Metadata.Name).CurrentValue;
            property.CurrentValue = newValue;
        }

        //_dbSet.Update(existingEntity);

        //existingEntity = entity;

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

        if(entity == null) return null;

        _dbSet.Remove(entity);

        return entity;
    }
}
