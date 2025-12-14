using eTickets.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ActorController(AppDbContext context) : Controller
    {
        
        private readonly AppDbContext _context = context;

        // GET: /Actor
        public async Task<IActionResult> Index()
        {
            var actors = await _context.Actors
                                       .AsNoTracking()
                                       .ToListAsync();
            return View(actors);
        }
    }
}
