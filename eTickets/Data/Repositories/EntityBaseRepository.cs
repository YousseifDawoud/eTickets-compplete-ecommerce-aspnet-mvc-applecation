using eTickets.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eTickets.Data.Repositories;

public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
{
    // Dependency Injection of AppDbContext And DbSet<T>
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet; // Represents the collection of entities of type T in the context
    public EntityBaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    // -------------------- READ -------------------- //
    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct)
        => await _dbSet
                .AsNoTracking() 
                .ToListAsync(ct);

    // GetAll with include properties
    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct,
        params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        foreach (var includeProperty in includeProperties)
            query = query.Include(includeProperty);

        return await query.ToListAsync(ct);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct)
        => await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, ct);

    // GetById with include properties
    public async Task<T?> GetByIdAsync(int id,CancellationToken ct,
        params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        foreach (var includeProperty in includeProperties)
            query = query.Include(includeProperty);
        
        return await query.FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    /* -------------------- WRITE -------------------- */

    public async Task AddAsync(T entity, CancellationToken ct)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _dbSet.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(int id,T entity, CancellationToken ct)
    {
        // Ensure the entity Argument is not null
        ArgumentNullException.ThrowIfNull(entity);

        // Check if the entity exists
        var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id, ct);

        // If not found, throw an exception
        if ( existingEntity is null)
            throw new KeyNotFoundException($"{typeof(T).Name} with ID {id} not found.");

        // Attach the entity and set its state to Modified
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        // Find the entity by ID first
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id, ct);

        //  If not found, throw an exception
        if (entity is null)
            throw new KeyNotFoundException($"{typeof(T).Name} with ID {id} not found.");

        // Remove the entity and save changes
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
