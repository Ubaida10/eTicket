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

    // GET: Movies/Create
    [HttpGet]
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

    // POST: Movies/Create
    [HttpPost]
    public IActionResult Create(NewMovie movie)
    {
        if (ModelState.IsValid)
        {
            movie.Duration = new TimeSpan(movie.Hours, movie.Minutes, 0);
            var moviesRepo = new MovieRepository();
            moviesRepo.AddMovie(movie);
            return RedirectToAction("Index");
        }

        var cinemaRepo = new CinemaRepository();
        var producersRepo = new ProducerRepository();
        var actorsRepo = new ActorRepository();
        
        var cinemas = cinemaRepo.GetAllCinemas();
        var producers = producersRepo.GetAllProducers();
        var actors = actorsRepo.GetAllActors();
        
        ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(producers, "Id", "Name");
        ViewBag.Actors = new MultiSelectList(actors, "Id", "Name");

        return View(movie);
    }
    
    public IActionResult Details(int id)
    {
        Movie movie = new Movie();
        MovieRepository movieRepository = new MovieRepository();
        movie.Cinema = new Cinema();
        movie.Producer = new Producer();
        
        var cinemaRepo = new CinemaRepository();
        var producersRepo = new ProducerRepository();
        var actorsRepo = new ActorRepository();
        
        movie = movieRepository.GetMovieById(id);
        movie.Cinema = cinemaRepo.GetCinemaById(movie.CinemaId);
        movie.Producer = producersRepo.GetProducerById(movie.ProducerId);
        
        return View(movie);
    }

    public IActionResult Search(string searchString)
    {
        Movie movie = new Movie();
        MovieRepository movieRepository = new MovieRepository();
        movie.Cinema = new Cinema();
        movie.Producer = new Producer();
        
        var cinemaRepo = new CinemaRepository();
        var producersRepo = new ProducerRepository();
        var actorsRepo = new ActorRepository();
        
        movie = movieRepository.GetMovieByName(searchString);
        if (movie == null)
        {
            return RedirectToAction("MovieNotFound");
        }
        movie.Cinema = cinemaRepo.GetCinemaById(movie.CinemaId);
        movie.Producer = producersRepo.GetProducerById(movie.ProducerId);
        

        return View("Details", movie);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var moviesRepo = new MovieRepository();
        var movie = moviesRepo.GetMovieById(id);

        var newMovie = new NewMovie();
        newMovie.Title = movie.Title;
        newMovie.Synopsis = movie.Synopsis;
        newMovie.Duration = movie.Duration;
        newMovie.ReleaseDate = movie.ReleaseDate;
        newMovie.Price = movie.Price;
        newMovie.ImageUrl = movie.ImageUrl;
        newMovie.RottenTomatoScore = movie.RottenTomatoScore;
        newMovie.Genre = movie.Genre;
        newMovie.ProducerId = movie.ProducerId;
        newMovie.CinemaId = movie.CinemaId;
        newMovie.ActorId = movie.ActorId;
        newMovie.Hours = movie.Duration.Hours;
        newMovie.Minutes = movie.Duration.Minutes;
        if (movie == null)
        {
            return RedirectToAction("MovieNotFound");
        }

        var cinemaRepo = new CinemaRepository();
        var producersRepo = new ProducerRepository();
        var actorsRepo = new ActorRepository();

        ViewBag.Cinemas = new SelectList(cinemaRepo.GetAllCinemas(), "Id", "Name", movie.CinemaId);
        ViewBag.Producers = new SelectList(producersRepo.GetAllProducers(), "Id", "Name", movie.ProducerId);
        ViewBag.Actors = new SelectList(actorsRepo.GetAllActors(), "Id", "Name", movie.ActorId);

        return View(newMovie);
    }

    [HttpPost]
    public IActionResult Edit(NewMovie movie)
    {
        if (ModelState.IsValid)
        {
            movie.Duration = new TimeSpan(movie.Hours, movie.Minutes, 0);
            var moviesRepo = new MovieRepository();
            moviesRepo.UpdateMovie(movie);
            return RedirectToAction("Index");
        }

        var cinemaRepo = new CinemaRepository();
        var producersRepo = new ProducerRepository();
        var actorsRepo = new ActorRepository();

        ViewBag.Cinemas = new SelectList(cinemaRepo.GetAllCinemas(), "Id", "Name", movie.CinemaId);
        ViewBag.Producers = new SelectList(producersRepo.GetAllProducers(), "Id", "Name", movie.ProducerId);
        ViewBag.Actors = new SelectList(actorsRepo.GetAllActors(), "Id", "Name", movie.ActorId);

        return View(movie);
    }

    public IActionResult Delete(int id)
    {
        var moviesRepo = new MovieRepository();
        moviesRepo.DeleteMovie(id);
        return RedirectToAction("Index");
    }

    public IActionResult MovieNotFound()
    {
        return View();
    }
}