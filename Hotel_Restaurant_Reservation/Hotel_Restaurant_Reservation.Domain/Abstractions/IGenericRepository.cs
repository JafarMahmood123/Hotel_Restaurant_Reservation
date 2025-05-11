using System.Linq.Expressions;

namespace Hotel_Restaurant_Reservation.Domain.Abstractions;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>?> GetAllAsync();

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity?> UpdateAsync(Guid id, TEntity entity);



    TEntity? GetById(Guid id);

    IEnumerable<TEntity>? GetAll();

    TEntity Add(TEntity entity);

    TEntity? Update(Guid id, TEntity entity);


    TEntity Remove(TEntity entity);

    Task SaveChangesAsync();

    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);


    IEnumerable<TEntity>? Where(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>?> WhereAsync(Expression<Func<TEntity, bool>> predicate);
}
