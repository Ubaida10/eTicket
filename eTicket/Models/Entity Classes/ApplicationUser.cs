using System.ComponentModel.DataAnnotations;

namespace eTicket.Models.Entity_Classes;

public class ApplicationUser
{
    [Display(Name = "Full name")]
    public string Name { get; set; }
}