using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eTicket.Models;
using eTicket.Models.Entity_Classes;
using eTicket.Models.Repositories;

namespace eTicket.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        MovieRepository moviesRepository = new MovieRepository();
        var movies = moviesRepository.GetAllMovies();
        return View(movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}