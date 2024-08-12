using System.ComponentModel.DataAnnotations;

namespace eTicket.Data;

public class NewMovie
{
    public int Id { get; set; }

    [Display(Name = "Movie name")]
    [Required(ErrorMessage = "Name is required")]
    public string Title { get; set; }

    [Display(Name = "Movie description")]
    [Required(ErrorMessage = "Description is required")]
    public string Synopsis { get; set; }

    [Display(Name = "Price in $")]
    [Required(ErrorMessage = "Price is required")]
    public double Price { get; set; }

    [Display(Name = "Movie poster URL")]
    [Required(ErrorMessage = "Movie poster URL is required")]
    public string ImageUrl { get; set; }

    [Display(Name = "Movie start date")]
    [Required(ErrorMessage = "Start date is required")]
    public DateTime ReleaseDate { get; set; }

    
    [Display(Name = "Select a category")]
    [Required(ErrorMessage = "Movie category is required")]
    public Genre Genre { get; set; }

    //Relationships
    [Display(Name = "Select actor")]
    [Required(ErrorMessage = "Movie actor is required")]
    public int ActorId { get; set; }

    [Display(Name = "Select a cinema")]
    [Required(ErrorMessage = "Movie cinema is required")]
    public int CinemaId { get; set; }

    [Display(Name = "Select a producer")]
    [Required(ErrorMessage = "Movie producer is required")]
    public int ProducerId { get; set; }
}