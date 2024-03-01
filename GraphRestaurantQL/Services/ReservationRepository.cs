using GraphRestaurantQL.Data;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphRestaurantQL.Services
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly GraphQLDbContext _ctx;

        public ReservationRepository(GraphQLDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            _ctx.Reservations.Add(reservation);
            await _ctx.SaveChangesAsync();
            return reservation;
        }

        public async Task<IReadOnlyList<Reservation>> GetAll()
        {
            return await _ctx.Reservations.ToListAsync();
        }
    }
}
