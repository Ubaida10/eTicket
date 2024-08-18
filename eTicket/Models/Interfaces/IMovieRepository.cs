using eTicket.Data;
using eTicket.Models.Entity_Classes;

namespace eTicket.Models.Interfaces;

public interface IMovieRepository
{
    //Create
    void AddMovie(NewMovie movie);

    //Read
    Movie GetMovieById(int id);
    IEnumerable<Movie> GetAllMovies();
    
    //Update
    void UpdateMovie(NewMovie movie);
    
    //Delete
    void DeleteMovie(int id);
    
    Movie GetMovieByName(string name);
}