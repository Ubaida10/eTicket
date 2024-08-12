using System.ComponentModel.DataAnnotations;
using eTicket.Models.Entity_Classes;

namespace eTicket.Data;

public class Cart
{
    public string CartId { get; set; }
    [Required]
    public ApplicationUser User { get; set; }
    public List<ShoppingCartItem> Items { get; set; }
    public int quantity { get; set; }
    public decimal Total { get; set; }
    public DateTime DateAdded { get; set; }
}