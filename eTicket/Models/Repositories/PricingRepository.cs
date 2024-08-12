using eTicket.Models.Interfaces;

namespace eTicket.Models.Repositories;

public class PricingRepository : IPricingRepository
{
    public decimal GetPrice(int movieId, DateTime showTime)
    {
        throw new NotImplementedException();
    }
}