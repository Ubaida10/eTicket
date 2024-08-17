using eTicket.Models.Entity_Classes;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Actor());
    }
    
    public IActionResult Create(Actor actor)
    {
        ActorRepository actors = new ActorRepository();
        actors.AddActor(actor);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        ActorRepository actorRepo = new ActorRepository();
        var actor = actorRepo.GetActorById(id);
        if (actor == null)
        {
            return RedirectToAction("ActorNotFound");
        }
        return View(actor);
    }

    [HttpPost]
    public IActionResult Edit(Actor actor)
    {
        ActorRepository actorRepo = new ActorRepository();
        actorRepo.UpdateActor(actor);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        Actor actor = new Actor();
        ActorRepository actorRepository = new ActorRepository();
        actor = actorRepository.GetActorById(id);
        return View(actor);
    }
    
    public IActionResult Delete(int id)
    {
        ActorRepository actorRepository = new ActorRepository();
        actorRepository.DeleteActor(id);
        return RedirectToAction("Index");
    }
    
    public IActionResult ActorNotFound()
    {
        return View();
    }
}