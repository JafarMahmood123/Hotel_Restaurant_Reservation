﻿namespace Hotel_Restaurant_Reservation.Domain.Abstractions;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>?> GetAllAsync();

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);



    TEntity? GetById(Guid id);

    IEnumerable<TEntity>? GetAll();

    TEntity Add(TEntity entity);

    TEntity Update(TEntity entity);

    TEntity Remove(TEntity entity);

}
