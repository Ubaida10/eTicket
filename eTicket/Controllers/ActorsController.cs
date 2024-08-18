using eTicket.Models.Entity_Classes;
using eTicket.Models.Interfaces;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTicket.Controllers;

public class ActorsController : Controller
{
    private readonly IActorRepository _actorRepository;
    
    public ActorsController(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }
    // GET
    public IActionResult Index()         //Default ActionResult
    {
        var acts = _actorRepository.GetAllActors();
        return View(acts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Actor());
    }
    
    public IActionResult Create(Actor actor)
    {
        _actorRepository.AddActor(actor);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var actor = _actorRepository.GetActorById(id);
        if (actor == null)
        {
            return RedirectToAction("ActorNotFound");
        }
        return View(actor);
    }

    [HttpPost]
    public IActionResult Edit(Actor actor)
    {
        _actorRepository.UpdateActor(actor);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        Actor actor = new Actor();
        actor = _actorRepository.GetActorById(id);
        return View(actor);
    }
    
    public IActionResult Delete(int id)
    {
        _actorRepository.DeleteActor(id);
        return RedirectToAction("Index");
    }
    
    public IActionResult ActorNotFound()
    {
        return View();
    }
}