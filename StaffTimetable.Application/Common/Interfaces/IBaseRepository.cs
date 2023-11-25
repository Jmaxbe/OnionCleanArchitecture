﻿using System.Linq.Expressions;
using StaffTimetable.Domain.Common;

namespace StaffTimetable.Application.Common.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Finds an entity with the given primary key values
    /// </summary>
    /// <param name="id">primary key value</param>
    /// <param name="cancellationToken">A CancellationToken</param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<T> Get();
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}