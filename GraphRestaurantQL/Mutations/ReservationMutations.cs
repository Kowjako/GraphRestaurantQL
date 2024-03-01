using GraphQL;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Mutations
{
    public class ReservationMutations : ObjectGraphType
    {
        public ReservationMutations(IReservationRepository resRepo)
        {
            // Here we define like in REST "endpoints"
            Field<ReservationType>("add")
                .Arguments(new QueryArguments(new QueryArgument<ReservationInputType>() { Name = "reservation" }))
                .ResolveAsync(async ctx =>
            {
                // here its like automapping since properties from menuInputType and menu are the same
                var dto = ctx.GetArgument<Reservation>("reservation");
                await resRepo.AddReservation(dto);
                return dto;
            });
        }
    }
}
