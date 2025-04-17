using Hotel_Restaurant_Reservation.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

    public virtual TEntity Add(TEntity entity)
    {
        _dbSet.Add(entity);

        _hotelRestaurantDbContext.SaveChanges();

        return entity;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
         await _dbSet.AddAsync(entity);

        _hotelRestaurantDbContext.SaveChanges();

        return entity;
    }

    public virtual IEnumerable<TEntity>? GetAll()
    {
        return _dbSet.ToList();
    }

    public virtual async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual TEntity? GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public TEntity Remove(TEntity entity)
    {
        _dbSet.Remove(entity);

        _hotelRestaurantDbContext.SaveChanges();

        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        var oldEntity = _dbSet.Find(entity);

        if (oldEntity != null)
        {
            oldEntity = entity;

            _hotelRestaurantDbContext.SaveChanges();
        }

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var oldEntity = await _dbSet.FindAsync(entity);

        if (oldEntity != null)
        {
            oldEntity = entity;

            _hotelRestaurantDbContext.SaveChanges();
        }

        return entity;
    }
}
