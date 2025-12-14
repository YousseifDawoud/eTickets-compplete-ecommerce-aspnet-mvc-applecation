using eTickets.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemaController : Controller
    {
        private readonly AppDbContext _context;

        public CinemaController(AppDbContext context)
        {
            _context = context;
        }

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
