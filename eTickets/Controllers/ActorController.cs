using eTickets.Data.Persistence;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ActorController: Controller
    {
        // Dependency Injection
        private readonly IActorService _service;
        public ActorController(IActorService service)
        {
            _service = service;  
        }


        // GET: /Actor
        public async Task<IActionResult> Index()
        {
            var actors = await _service.GetAllAsync(HttpContext.RequestAborted);
            return View(actors);
        }

        // Get: Actor/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}