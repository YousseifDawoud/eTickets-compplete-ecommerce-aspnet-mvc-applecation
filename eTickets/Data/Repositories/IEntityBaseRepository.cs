using System.Linq.Expressions;

namespace eTickets.Data.Repositories;

public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
{
    // CRUD Operations

    // Get all Entities Async
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct);


    // Get all Entities Async with Include Properties
    Task<IReadOnlyList<T>> GetAllAsync( CancellationToken ct,
        params Expression<Func<T, object>>[] includeProperties);


    // Get Entity by Id Async
    Task<T?> GetByIdAsync(int id, CancellationToken ct);


    // Get Entity by Id Async with Include Properties
    Task<T?> GetByIdAsync( int id, CancellationToken ct,
        params Expression<Func<T, object>>[] includeProperties);


    // Create New Entity Async
    Task AddAsync(T entity, CancellationToken ct);


    // Update Existing Entity Async
    Task UpdateAsync(int id, T entity, CancellationToken ct);


    // Delete Entity by Id Async
    Task DeleteAsync(int id, CancellationToken ct);
}