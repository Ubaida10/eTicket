using System.ComponentModel.DataAnnotations;

namespace eTicket.Models.Entity_Classes;

public class Producer
{
    [Key]
    public int Id { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Biography { get; set; }
}