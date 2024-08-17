using eTicket.Data;
using eTicket.Models.Entity_Classes;
using eTicket.Models.Interfaces;
using Microsoft.Data.SqlClient;

namespace eTicket.Models.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly string _connectionString;

    public MovieRepository()
    {
        _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eTicketProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    }

    public void AddMovie(NewMovie movie)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Movies(Title, Synopsis, Duration, ReleaseDate, Price, ImageUrl, RottenTomatoScore, Genre, ProducerID, CinemaID) VALUES (@Title, @Synopsis, @Duration, @ReleaseDate, @Price, @ImageUrl, @RottenTomatoScore, @Genre, @ProducerID, @CinemaID);SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Title", movie.Title);
            command.Parameters.AddWithValue("@Synopsis", movie.Synopsis);
            command.Parameters.AddWithValue("@Duration", movie.Duration);
            command.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
            command.Parameters.AddWithValue("@Price", movie.Price);
            command.Parameters.AddWithValue("@ImageUrl", movie.ImageUrl);
            command.Parameters.AddWithValue("@RottenTomatoScore", movie.RottenTomatoScore);
            command.Parameters.AddWithValue("@Genre", movie.Genre);
            command.Parameters.AddWithValue("@ProducerID", movie.ProducerId);
            command.Parameters.AddWithValue("@CinemaID", movie.CinemaId);
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public Movie GetMovieById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            Movie movie = new Movie();
            string query = @"
        SELECT m.Id, m.Title, m.Synopsis, m.Duration, m.ReleaseDate, m.Price, m.ImageUrl, 
               m.RottenTomatoScore, m.Genre, m.CinemaId, m.ProducerId, 
               c.Name AS CinemaName, p.Name AS ProducerName
        FROM Movies m
        INNER JOIN Cinemas c ON m.CinemaId = c.Id
        INNER JOIN Producers p ON m.ProducerId = p.Id
        WHERE m.Id = @Id";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            string act;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                movie.Id = reader.GetInt32(0);
                movie.Title = reader.GetString(1);
                movie.Synopsis = reader.GetString(2);
                movie.Duration = reader.GetTimeSpan(3);
                movie.ReleaseDate = reader.GetDateTime(4);
                movie.Price = reader.GetDecimal(5);
                movie.ImageUrl = reader.GetString(6);
                movie.RottenTomatoScore = reader.GetInt32(7);
                movie.Genre = (Genre)reader.GetInt32(8);
                movie.CinemaId = reader.GetInt32(9);
                movie.ProducerId = reader.GetInt32(10);
            }
            reader.Close();
            return movie;
        }
    }

    public Movie GetMovieByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Search name cannot be null or empty", nameof(name));
        }

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            Movie movie = null; // Use null initially if no movie is found
            string query = @"
            SELECT m.Id, m.Title, m.Synopsis, m.Duration, m.ReleaseDate, m.Price, m.ImageUrl, 
                   m.RottenTomatoScore, m.Genre, m.CinemaId, m.ProducerId, 
                   c.Name AS CinemaName, p.Name AS ProducerName
            FROM Movies m
            INNER JOIN Cinemas c ON m.CinemaId = c.Id
            INNER JOIN Producers p ON m.ProducerId = p.Id
            WHERE m.Title = @name";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        movie = new Movie
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Synopsis = reader.GetString(2),
                            Duration = reader.GetTimeSpan(3),
                            ReleaseDate = reader.GetDateTime(4),
                            Price = reader.GetDecimal(5),
                            ImageUrl = reader.GetString(6),
                            RottenTomatoScore = reader.GetInt32(7),
                            Genre = (Genre)reader.GetInt32(8),
                            CinemaId = reader.GetInt32(9),
                            ProducerId = reader.GetInt32(10)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return movie;
        }
    }



    public IEnumerable<Movie> GetAllMovies()
    {
        List<Movie> movies = new List<Movie>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Movies";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Movie m = new Movie
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Synopsis = reader.GetString(2),
                    Duration = reader.GetTimeSpan(3),
                    ReleaseDate = reader.GetDateTime(4),
                    Price = reader.GetDecimal(5),
                    ImageUrl = reader.GetString(6),
                    RottenTomatoScore = reader.GetInt32(7),
                    Genre = (Genre)reader.GetInt32(8),
                    ProducerId = reader.GetInt32(9),
                    CinemaId = reader.GetInt32(10)
                };
                movies.Add(m);
            }
            reader.Close();
            connection.Close();
            return movies;
        }
    }

    public void UpdateMovie(NewMovie movie)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"UPDATE Movies SET Title = @Title, Synopsis = @Synopsis, Duration = @Duration, ReleaseDate = @ReleaseDate, Price = @Price, ImageUrl = @ImageUrl, RottenTomatoScore = @RottenTomatoScore, Genre = @Genre, ProducerID = @ProducerID, CinemaID = @CinemaID, ActorID = @ActorID WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", movie.Id);
            command.Parameters.AddWithValue("@Title", movie.Title);
            command.Parameters.AddWithValue("@Synopsis", movie.Synopsis);
            command.Parameters.AddWithValue("@Duration", movie.Duration);
            command.Parameters.AddWithValue("@ReleaseDate", movie.ReleaseDate);
            command.Parameters.AddWithValue("@Price", movie.Price);
            command.Parameters.AddWithValue("@ImageUrl", movie.ImageUrl);
            command.Parameters.AddWithValue("@RottenTomatoScore", movie.RottenTomatoScore);
            command.Parameters.AddWithValue("@Genre", movie.Genre);
            command.Parameters.AddWithValue("@ProducerID", movie.ProducerId);
            command.Parameters.AddWithValue("@CinemaID", movie.CinemaId);
            command.Parameters.AddWithValue("@ActorID", movie.ActorId); 
            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void DeleteMovie(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"DELETE FROM Movies WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}