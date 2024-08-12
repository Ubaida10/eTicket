using eTicket.Models.Entity_Classes;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eTicket.Controllers;

public class ActorsController : Controller
{
    // GET
    public IActionResult Index()         //Default ActionResult
    {
        ActorRepository actors = new ActorRepository();
        var acts = actors.GetAllActors();
        return View(acts);
    }

    public IActionResult Create()
    {
        return View(new Actor());
    }
    
    public IActionResult Delete(string name)
    {
        return View(new Actor());
    }
    
    public IActionResult Edit(string name)
    {
        return View(new Actor());
    }

    public IActionResult Details()
    {
        Actor actor = new Actor();
        ActorRepository actorRepository = new ActorRepository();
        actor = actorRepository.GetActorById(2);
        return View(actor);
    }
}