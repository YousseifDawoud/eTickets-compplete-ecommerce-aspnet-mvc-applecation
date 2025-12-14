using eTickets.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbContext _context;

        public ProducerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Producer
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var producers = await _context.Producers
                                          .AsNoTracking()
                                          .ToListAsync(ct);

            return View(producers);
        }
    }
}
