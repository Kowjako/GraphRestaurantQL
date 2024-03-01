using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Query
{
    public class ReservationQuery : ObjectGraphType
    {
        public ReservationQuery(IReservationRepository resRepo)
        {
            // Here we define like in REST "endpoints"
            Field<ListGraphType<ReservationType>>("getAll").ResolveAsync(async ctx =>
            {
                return await resRepo.GetAll();
            });
        }
    }
}
