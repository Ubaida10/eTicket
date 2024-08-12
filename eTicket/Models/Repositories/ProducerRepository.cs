using eTicket.Data;
using eTicket.Models.Entity_Classes;
using eTicket.Models.Interfaces;
using Microsoft.Data.SqlClient;

namespace eTicket.Models.Repositories;

public class ProducerRepository : IProducerRepository
{ 
    private readonly string _connectionString;

    public ProducerRepository()
    {
        _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eTicketProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    }
    public void AddProducer(Producer producer)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        string query = "INSERT INTO Producers(ProfilePictureUrl, Name, DateOfBirth, Biography) VALUES (@ProfilePicture, @Name, @DateOfBirth, @Biography); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(query, connection);
        
        command.Parameters.AddWithValue("@ProfilePicture", producer.ProfilePictureUrl);
        command.Parameters.AddWithValue("@Name", producer.Name);
        command.Parameters.AddWithValue("@DateOfBirth", producer.DateOfBirth);
        command.Parameters.AddWithValue("@Biography", producer.Biography);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public Producer GetProducerById(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        Producer producer = new Producer();
        string query = "SELECT * FROM Producers WHERE Id = @Id";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            producer.Id = reader.GetInt32(0);
            producer.ProfilePictureUrl = reader.GetString(1);
            producer.Name = reader.GetString(2);
            producer.DateOfBirth = reader.GetDateTime(3);
            producer.Biography = reader.GetString(4);
        }
        reader.Close();
        connection.Close();
        return producer;
    }

    public IEnumerable<Producer> GetAllProducers()
    {
        List<Producer> producers = new List<Producer>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM Producers";
        SqlCommand command = new SqlCommand(query, connection);
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Producer producer = new Producer
            {
                Id = reader.GetInt32(0),
                ProfilePictureUrl = reader.GetString(1),
                Name = reader.GetString(2),
                DateOfBirth = reader.GetDateTime(3),
                Biography = reader.GetString(4)
            };
            producers.Add(producer);
        }
        reader.Close();
        return producers;
    }

    public IEnumerable<Movie> GetAllMoviesByProducer(int id)
    {
        List<Movie> movies = new List<Movie>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query =
                @"SELECT m.* FROM Movies m JOIN MovieActors ma ON m.Id = ma.MovieId WHERE ma.MovieId = @ProducerId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProducerId", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Movie movie = new Movie
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Synopsis = reader.GetString(2),
                    Duration = reader.GetTimeSpan(3),
                    ReleaseDate = reader.GetDateTime(4),
                    Price = reader.GetDecimal(5),
                    ImageUrl = reader.GetString(6),
                    RottenTomatoScore = reader.GetDouble(7),
                    Genre = (Genre)reader.GetInt32(8)
                };
                movies.Add(movie);
            }
        }
        return movies;
    }

    public void UpdateProducer(Producer producer)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        string query = "UPDATE Producers SET ProfilePictureUrl = @ProfilePicture, Name = @Name, DateOfBirth = @DateOfBirth, Biography = @Biography WHERE Id = @Id";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", producer.Id);
        command.Parameters.AddWithValue("@ProfilePicture", producer.ProfilePictureUrl);
        command.Parameters.AddWithValue("@Name", producer.Name);
        command.Parameters.AddWithValue("@DateOfBirth", producer.DateOfBirth);
        command.Parameters.AddWithValue("@Biography", producer.Biography);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void DeleteProducer(Producer producer)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        string query = "DELETE FROM Producers WHERE Id = @Id";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", producer.Id);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}