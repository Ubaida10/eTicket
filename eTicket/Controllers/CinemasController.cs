using eTicket.Models.Entity_Classes;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eTicket.Controllers;

public class CinemasController : Controller
{
    // GET
    public IActionResult Index()
    {
        CinemaRepository cinemasData = new CinemaRepository();
        var cinemas = cinemasData.GetAllCinemas();
        return View(cinemas);
    }
    
    public IActionResult Create()
    {
        return View(new Cinema());
    }
    
    public IActionResult Delete()
    {
        return View(new Cinema());
    }
    
    public IActionResult Edit()
    {
        return View(new Cinema());
    }
    
    public IActionResult Details()
    {
        Cinema cinema = new Cinema();
        CinemaRepository cinemaRepository = new CinemaRepository();
        cinema = cinemaRepository.GetCinemaById(1);
        return View(cinema);
    }
}