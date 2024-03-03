using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Query
{
    public class ReservationQuery : ObjectGraphType
    {
        public ReservationQuery()
        {
            // Here we define like in REST "endpoints"
            Field<ListGraphType<ReservationType>, IReadOnlyList<Reservation>>("getAll")
                .ResolveScopedAsync(async ctx =>
            {
                var resRepo = ctx.RequestServices!.GetRequiredService<IReservationRepository>();
                return await resRepo.GetAll();
            });
        }
    }
}
