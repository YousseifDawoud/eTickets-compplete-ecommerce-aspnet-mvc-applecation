using eTickets.Data.Persistence;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers;

public class ActorController : Controller
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
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Post : Actor/Create
    [HttpPost]
    public async Task<IActionResult> Create(Actor actor)
    {
        if (!ModelState.IsValid)
        {
            return View(actor);
        }
        await _service.AddAsync(actor, HttpContext.RequestAborted);
        return RedirectToAction(nameof(Index));
    }

    // Get : Actor/Details/{id}
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var actor = await _service.GetByIdAsync(id, HttpContext.RequestAborted);
        if (actor is null) return  NotFound();
        return View(actor);
    }

    // GET: Actor/Edit/id
    public async Task<IActionResult> Edit(int id)
    {
        var actor = await _service.GetByIdAsync(id, HttpContext.RequestAborted);

        if (actor is null)
            return NotFound();

        return View(actor);
    }

    // POST: Actor/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Actor actor)
    {
        if (id != actor.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(actor);

        await _service.UpdateAsync(id, actor, HttpContext.RequestAborted);

        return RedirectToAction(nameof(Index));
    }
}