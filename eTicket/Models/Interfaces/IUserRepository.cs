using System.Data;
using eTicket.Data;

namespace eTicket.Models.Interfaces;

public interface IUserRepository
{
    public DataTable GetUsers();
    public void AddUser(Register register);
    public void Edit(Register register);
    public void Delete(string id);
}