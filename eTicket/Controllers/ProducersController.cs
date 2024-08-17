using eTicket.Models.Entity_Classes;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eTicket.Controllers;

public class ProducersController : Controller
{
    // GET
    public IActionResult Index()
    {
        ProducerRepository producerRepository = new ProducerRepository();
        var producers = producerRepository.GetAllProducers();
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
        ProducerRepository producerRepository = new ProducerRepository();
        producerRepository.AddProducer(producer);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int id)
    {
        ProducerRepository producerRepo = new ProducerRepository();
        var producer = producerRepo.GetProducerById(id);
        if (producer== null)
        {
            return RedirectToAction("ProducerNotFound");
        }
        return View(producer);
    }
    
    [HttpPost]
    public IActionResult Edit(Producer producer)
    {
        ProducerRepository producerRepo = new ProducerRepository();
        producerRepo.UpdateProducer(producer);
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        ProducerRepository producerRepo = new ProducerRepository();
        Producer producer = new Producer();
        producer = producerRepo.GetProducerById(id);
        return View(producer);
    }
    
    public IActionResult Delete(int id)
    {
        ProducerRepository producerRepo = new ProducerRepository();
        producerRepo.DeleteProducer(id);
        return RedirectToAction("Index");
    }
}