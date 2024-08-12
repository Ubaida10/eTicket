using System.Collections;
using eTicket.Models.Entity_Classes;

namespace eTicket.Models.Interfaces;

public interface IActorRepository
{
    //Create
    void AddActor(Actor actor);

    //Read
    Actor GetActorById(int id);
    IEnumerable<Actor> GetAllActors();
    IEnumerable<Movie>GetMoviesByActor(int id);
    
    //Update
    void UpdateActor(Actor actor);
    
    //Delete
    void DeleteActor(int id);
}