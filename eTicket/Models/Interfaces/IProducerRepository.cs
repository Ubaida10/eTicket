using eTicket.Models.Entity_Classes;

namespace eTicket.Models.Interfaces;

public interface IProducerRepository
{
    //Create
    void AddProducer(Producer producer);

    //Read
    Producer GetProducerById(int id);
    IEnumerable<Producer> GetAllProducers();
    IEnumerable<Movie> GetAllMoviesByProducer(int id);
    
    //Update
    void UpdateProducer(Producer producer);
    
    //Delete
    void DeleteProducer(int id);
}