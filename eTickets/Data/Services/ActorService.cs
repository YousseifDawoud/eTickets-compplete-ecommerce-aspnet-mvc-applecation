using eTickets.Data.Persistence;
using eTickets.Data.Repositories;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services;

public class ActorService : EntityBaseRepository<Actor>, IActorService
{
    // Dependency Injection of AppDbContext
    public ActorService(AppDbContext context) : base(context) { }



     

}
