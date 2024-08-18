using eTicket.Data;
using eTicket.Models.Entity_Classes;
using eTicket.Models.Interfaces;
using eTicket.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTicket.Controllers;

public class MoviesController : Controller
{
    private readonly IMovieRepository _movieRepository;
    private readonly IProducerRepository _producerRepository;
    private readonly IActorRepository _actorRepository;
    private readonly ICinemaRepository _cinemaRepository;

    public MoviesController(IMovieRepository movieRepository, IActorRepository actorRepository, ICinemaRepository cinemaRepository, IProducerRepository producerRepository)
    {
        _movieRepository = movieRepository;
        _actorRepository = actorRepository;
        _cinemaRepository = cinemaRepository;
        _producerRepository = producerRepository;
    }

    // GET
    public IActionResult Index()
    {
        var movies = _movieRepository.GetAllMovies();
        return View(movies);
    }

    // GET: Movies/Create
    [HttpGet]
    public IActionResult Create()
    {
        var cinemas = _cinemaRepository.GetAllCinemas();
        var producers = _producerRepository.GetAllProducers();
        var actors = _actorRepository.GetAllActors();
        
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
            _movieRepository.AddMovie(movie);
            return RedirectToAction("Index");
        }
        
        var cinemas = _cinemaRepository.GetAllCinemas();
        var producers = _producerRepository.GetAllProducers();
        var actors = _actorRepository.GetAllActors();
        
        ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
        ViewBag.Producers = new SelectList(producers, "Id", "Name");
        ViewBag.Actors = new MultiSelectList(actors, "Id", "Name");

        return View(movie);
    }
    
    public IActionResult Details(int id)
    {
        Movie movie = new Movie();
        movie.Cinema = new Cinema();
        movie.Producer = new Producer();

        movie = _movieRepository.GetMovieById(id);
        movie.Cinema = _cinemaRepository.GetCinemaById(movie.CinemaId);
        movie.Producer = _producerRepository.GetProducerById(movie.ProducerId);
        
        return View(movie);
    }

    public IActionResult Search(string searchString)
    {
        Movie movie = new Movie();
        movie.Cinema = new Cinema();
        movie.Producer = new Producer();
        
        
        movie = _movieRepository.GetMovieByName(searchString);
        if (movie == null)
        {
            return RedirectToAction("MovieNotFound");
        }
        movie.Cinema = _cinemaRepository.GetCinemaById(movie.CinemaId);
        movie.Producer = _producerRepository.GetProducerById(movie.ProducerId);
        

        return View("Details", movie);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var movie = _movieRepository.GetMovieById(id);

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
        
        ViewBag.Cinemas = new SelectList(_cinemaRepository.GetAllCinemas(), "Id", "Name", movie.CinemaId);
        ViewBag.Producers = new SelectList(_producerRepository.GetAllProducers(), "Id", "Name", movie.ProducerId);
        ViewBag.Actors = new SelectList(_actorRepository.GetAllActors(), "Id", "Name", movie.ActorId);

        return View(newMovie);
    }

    [HttpPost]
    public IActionResult Edit(NewMovie movie)
    {
        if (ModelState.IsValid)
        {
            movie.Duration = new TimeSpan(movie.Hours, movie.Minutes, 0);
            _movieRepository.UpdateMovie(movie);
            return RedirectToAction("Index");
        }
        
        ViewBag.Cinemas = new SelectList(_cinemaRepository.GetAllCinemas(), "Id", "Name", movie.CinemaId);
        ViewBag.Producers = new SelectList(_producerRepository.GetAllProducers(), "Id", "Name", movie.ProducerId);
        ViewBag.Actors = new SelectList(_actorRepository.GetAllActors(), "Id", "Name", movie.ActorId);

        return View(movie);
    }

    public IActionResult Delete(int id)
    {
        _movieRepository.DeleteMovie(id);
        return RedirectToAction("Index");
    }

    public IActionResult MovieNotFound()
    {
        return View();
    }
}