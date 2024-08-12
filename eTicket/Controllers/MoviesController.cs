using eTicket.Data;
using eTicket.Models.Entity_Classes;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTicket.Controllers;

public class MoviesController : Controller
{
    // GET
    public IActionResult Index()
    {
        var moviesRepo = new MovieRepository();
        var movies = moviesRepo.GetAllMovies();
        return View(movies);
    }

    public IActionResult Create()
    {
        var cinemaRepo = new CinemaRepository();
        var producersRepo = new ProducerRepository();
        var actorsRepo = new ActorRepository();
        
        var cinemas = cinemaRepo.GetAllCinemas();
        var producers = producersRepo.GetAllProducers();
        var actors = actorsRepo.GetAllActors();
        
        ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(producers, "Id", "Name");
        ViewBag.Actors = new MultiSelectList(actors, "Id", "Name");
        
        return View();
    }
    
    public IActionResult Details()
    {
        Movie movie = new Movie();
        MovieRepository movieRepository = new MovieRepository();
        movie.Cinema = new Cinema();
        movie.Producer = new Producer();
        
        var cinemaRepo = new CinemaRepository();
        var producersRepo = new ProducerRepository();
        var actorsRepo = new ActorRepository();
        
        movie = movieRepository.GetMovieById(12);
        movie.Cinema = cinemaRepo.GetCinemaById(movie.CinemaId);
        movie.Producer = producersRepo.GetProducerById(movie.ProducerId);
        
        return View(movie);
    }
    
    public IActionResult Edit()
    {
        var cinemaRepo = new CinemaRepository();
        var producersRepo = new ProducerRepository();
        var actorsRepo = new ActorRepository();
        
        var cinemas = cinemaRepo.GetAllCinemas();
        var producers = producersRepo.GetAllProducers();
        var actors = actorsRepo.GetAllActors();
        
        ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(producers, "Id", "Name");
        ViewBag.Actors = new MultiSelectList(actors, "Id", "Name");

        return View();
    }
}