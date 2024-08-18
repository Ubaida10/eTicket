using eTicket.Models.Entity_Classes;
using eTicket.Models.Interfaces;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eTicket.Controllers;

public class ProducersController : Controller
{
    private readonly IProducerRepository _producerRepository;
    
    public ProducersController(IProducerRepository producerRepository)
    {
        _producerRepository = producerRepository;
    }
    // GET
    public IActionResult Index()
    {
        var producers = _producerRepository.GetAllProducers();
        return View(producers);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View(new Producer());
    }

    [HttpPost]
    public IActionResult Create(Producer producer)
    {
        _producerRepository.AddProducer(producer);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var producer = _producerRepository.GetProducerById(id);
        if (producer== null)
        {
            return RedirectToAction("ProducerNotFound");
        }
        return View(producer);
    }
    
    [HttpPost]
    public IActionResult Edit(Producer producer)
    {
        
        _producerRepository.UpdateProducer(producer);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        Producer producer = new Producer();
        producer = _producerRepository.GetProducerById(id);
        return View(producer);
    }
    
    public IActionResult Delete(int id)
    {
        _producerRepository.DeleteProducer(id);
        return RedirectToAction("Index");
    }
}