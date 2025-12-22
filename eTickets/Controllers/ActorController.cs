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
        if (actor is null)
            return  View("NotFound");

        return View(actor);
    }



    // GET: Actor/Edit/id
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var actor = await _service.GetByIdAsync(id, HttpContext.RequestAborted);

        if (actor is null)
            return View("NotFound");

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



    // GET: Actors/Delete/id
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var actor = await _service.GetByIdAsync(id, HttpContext.RequestAborted);

        if (actor is null)
            return View("NotFound");

        return View(actor);
    }

    // Post: Actor/Delete/id
    [HttpPost , ActionName(nameof(Delete))]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Get the actor by id
        var actor = await _service.GetByIdAsync(id, HttpContext.RequestAborted);

        // If actor is null return NotFound
        if (actor is null) 
            View("NotFound");

        // Call the service to delete the actor
        await _service.DeleteAsync(id, HttpContext.RequestAborted);

        return RedirectToAction(nameof(Index));
    }
}