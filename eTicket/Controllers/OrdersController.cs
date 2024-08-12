using eTicket.Data;
using eTicket.Models.Entity_Classes;
using Microsoft.AspNetCore.Mvc;

namespace eTicket.Controllers;

public class OrdersController: Controller
{
    public IActionResult Index()
    {
        var orders = new List<Order>();
        return View(orders);
    }
    
    public IActionResult OrderCompleted()
    {
        return View();
    }
    
    public IActionResult ShoopingCart()
    {
        Cart cart = new Cart();
        cart.CartId = "1";
        cart.DateAdded = DateTime.Now;
        cart.Items = new List<ShoppingCartItem>
        {
            new ShoppingCartItem { Movie = new Movie { Title = "Shutter Island", Price = 12 }, Amount = 1 },
            new ShoppingCartItem { Movie = new Movie { Title = "The Prestige", Price = 15 }, Amount = 2 },
        };
        cart.Total = cart.Items.Sum(item => item.Movie.Price * item.Amount);
        cart.User = new ApplicationUser();
        cart.User.Name = "Abubakar";
        var orders = new List<Cart>();
        orders.Add(cart);
        return View(orders);
    }
}