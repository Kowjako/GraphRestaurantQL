using GraphRestaurantQL.Models;

namespace GraphRestaurantQL.Interfaces
{
    public interface IReservationRepository
    {
        Task<IReadOnlyList<Reservation>> GetAll();
        Task<Reservation> AddReservation(Reservation reservation);
    }
}
