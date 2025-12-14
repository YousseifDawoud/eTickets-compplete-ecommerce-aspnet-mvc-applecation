using eTickets.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemaController(AppDbContext context) : Controller
    {
        private readonly AppDbContext _context = context;

        // GET: /Cinema
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var cinemas = await _context.Cinemas
                                        .AsNoTracking()
                                        .ToListAsync(ct);

            return View(cinemas);
        }
    }
}
