using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IActorService
    {
        // Get all Actors
        Task<IEnumerable<Actor>> GetAllAsync(CancellationToken ct);


        // Get Actor by Id
        Task<Actor?> GetByIdAsync(int id, CancellationToken ct);


        // Add new Actor
        Task AddAsync(Actor actor, CancellationToken ct);


        // Update existing Actor
        Task UpdateAsync(int id, Actor updatedActor, CancellationToken ct);


        // Delete Actor by Id
        Task DeleteAsync(int id, CancellationToken ct);


        // I Pass The Id In Update Method To Ensure This Actor Exists In The Database Before Updating
        // Interfaces define a public contract, not private behavior.
        // I`m Make the Method AddAsync , UpdateAsync And DeleteAsync ? 
        // Beacause These Methods Are Responsible For Modifying The Data In The Database.
    }
}
