using eTickets.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MovieController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        // GET: /Movie
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var movies = await _context.Movies
                                       .AsNoTracking()
                                       .Include(c => c.Cinema)
                                       .Include(p => p.Producer)
                                       .OrderBy(m => m.Name)
                                       .ToListAsync(ct);

            return View(movies);
        }
    }
}
