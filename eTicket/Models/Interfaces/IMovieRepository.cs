using eTicket.Models.Entity_Classes;

namespace eTicket.Models.Interfaces;

public interface IMovieRepository
{
    //Create
    void AddMovie(Movie movie);

    //Read
    Movie GetMovieById(int id);
    IEnumerable<Movie> GetAllMovies();
    
    //Update
    void UpdateMovie(Movie movie);
    
    //Delete
    void DeleteMovie(Movie movie);
}