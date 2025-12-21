using eTickets.Data.Persistence;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ActorService: IActorService
    {
        // Dependency Injection
        private readonly AppDbContext _context;
        public ActorService(AppDbContext context)
        {
            _context =  context;
        }

        // CRUD Operations



        // Get all actors from the database asynchronously
        public async Task<IEnumerable<Actor>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Actors
                                 .AsNoTracking()
                                 .ToListAsync(ct);
        }


        // Get a single actor by ID from the database asynchronously
        public async Task<Actor?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Actors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, ct);
        }


        // Create a new actor to the database asynchronously
        public async Task AddAsync(Actor actor, CancellationToken ct )
        {
            ArgumentNullException.ThrowIfNull(actor);

            await _context.Actors.AddAsync(actor, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(int id, Actor updatedActor, CancellationToken ct)
        {
            // Validate input
            ArgumentNullException.ThrowIfNull(updatedActor);

            // Check if the actor exists
            var existingActor = await _context.Actors
                .FirstOrDefaultAsync(a => a.Id == id, ct);

            if (existingActor is null)
                throw new KeyNotFoundException($"Actor with ID {id} not found.");

            // Update actor properties
            existingActor.FullName = updatedActor.FullName;
            existingActor.Bio = updatedActor.Bio;
            existingActor.ProfilePictureURL = updatedActor.ProfilePictureURL;

            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            // Check if the actor exists
            var actor = await _context.Actors
                .FirstOrDefaultAsync(a => a.Id == id, ct);

            if (actor is null)
                throw new KeyNotFoundException($"Actor with ID {id} not found.");

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync(ct);
        }
    }
}
