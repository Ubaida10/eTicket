using System.Data;
using eTicket.Data;
using Microsoft.Data.SqlClient;

namespace eTicket.Models.Repositories;

public class UserRepository
{
    private readonly string _connectionString;
    public UserRepository()
    {
        _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eTicketProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    }
    public DataTable GetUsers()
    {
        DataTable usersTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Id, Email, Password FROM Admin";
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.Fill(usersTable);
        }
        return usersTable;
    }

    public void AddUser(Register register)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string insertQuery = "INSERT INTO Admin (Email, Password) VALUES (@Email, @Password)";
            
            SqlCommand command = new SqlCommand(insertQuery, connection);
            
            
            command.Parameters.AddWithValue("@Email", register.EmailAddress);
            command.Parameters.AddWithValue("@Password", register.Password);
            
            command.ExecuteNonQuery();
        }
    }
}