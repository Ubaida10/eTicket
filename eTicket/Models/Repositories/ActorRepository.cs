using eTicket.Data;
using eTicket.Models.Entity_Classes;
using eTicket.Models.Interfaces;
using Microsoft.Data.SqlClient;

namespace eTicket.Models.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly string _connectionString;

    public ActorRepository()
    {
        _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eTicketProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    }
    public void AddActor(Actor actor)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Actors(ProfilePictureUrl, Name, DateOfBirth, Biography) VALUES (@ProfilePicture, @FullName, @DateOfBirth, @Biography); SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProfilePicture", actor.ProfilePictureUrl);
            command.Parameters.AddWithValue("@FullName", actor.Name);
            command.Parameters.AddWithValue("@DateOfBirth", actor.DateOfBirth);
            command.Parameters.AddWithValue("@Biography", actor.Biography);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public Actor GetActorById(int id)
    {
        Actor actor = new Actor();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Actors WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                actor.Id = reader.GetInt32(0);
                actor.ProfilePictureUrl = reader.GetString(1);
                actor.Name = reader.GetString(2);
                actor.DateOfBirth = reader.GetDateTime(3);
                actor.Biography = reader.GetString(4);
            }
            connection.Close();
        }
        return actor;
    }

    public IEnumerable<Movie> GetMoviesByActor(int id)
    {
        List<Movie> movies = new List<Movie>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"SELECT m.* FROM Movies m JOIN MovieActors ma ON m.Id = ma.MovieId WHERE ma.ActorId = @ActorId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ActorId", id);
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
                    RottenTomatoScore = reader.GetDouble(7),
                    Genre = (Genre)reader.GetInt32(8)
                };
                movies.Add(m);
            }
            connection.Close();
        }
        return movies;
    }

    public IEnumerable<Actor> GetAllActors()
    {
        List<Actor> actor = new List<Actor>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Actors";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Actor a = new Actor
                {
                    Id = reader.GetInt32(0),
                    ProfilePictureUrl = reader.GetString(1),
                    Name = reader.GetString(2),
                    DateOfBirth = reader.GetDateTime(3),
                    Biography = reader.GetString(4)
                };
                actor.Add(a);
            }
            connection.Close();
        }
        return actor;
    }

    public void UpdateActor(Actor actor)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Actors SET ProfilePictureUrl = @ProfilePicture, Name = @FullName, DateOfBirth = @DateOfBirth, Biography = @Biography WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", actor.Id);
            command.Parameters.AddWithValue("@ProfilePicture", actor.ProfilePictureUrl);
            command.Parameters.AddWithValue("@FullName", actor.Name);
            command.Parameters.AddWithValue("@DateOfBirth", actor.DateOfBirth);
            command.Parameters.AddWithValue("@Biography", actor.Biography);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void DeleteActor(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM Actors WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}