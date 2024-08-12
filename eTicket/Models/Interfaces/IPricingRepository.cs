namespace eTicket.Models.Interfaces;

public interface IPricingRepository
{
    decimal GetPrice(int movieId, DateTime showTime);
}