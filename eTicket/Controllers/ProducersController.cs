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
    
    public IActionResult Create()
    {
        return View(new Producer());
    }

    public IActionResult Delete()
    {
        return View(new Producer());
    }
    
    public IActionResult Edit()
    {
        return View(new Producer());
    }

    public IActionResult Details()
    {
        ProducerRepository producerRepo = new ProducerRepository();
        Producer producer = new Producer();
        producer = producerRepo.GetProducerById(2);
        return View(producer);
    }
}