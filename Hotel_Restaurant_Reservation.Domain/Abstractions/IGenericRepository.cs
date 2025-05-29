using System.Linq.Expressions;

namespace Hotel_Restaurant_Reservation.Domain.Abstractions;

public interface IGenericRepository<TEntity> where TEntity : class
{
    public Task<TEntity?> GetByIdAsync(Guid id);

    public Task<IEnumerable<TEntity>?> GetAllAsync();

    public Task<TEntity> AddAsync(TEntity entity);

    public Task<TEntity?> UpdateAsync(Guid id, TEntity entity);



    public TEntity? GetById(Guid id);

    public IEnumerable<TEntity>? GetAll();

    public TEntity Add(TEntity entity);

    public TEntity? Update(Guid id, TEntity entity);


    public TEntity Remove(TEntity entity);

    public Task SaveChangesAsync();

    public Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);


    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);


    public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

    public Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);


    public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
}
