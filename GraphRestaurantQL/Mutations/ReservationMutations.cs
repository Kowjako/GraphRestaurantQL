using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Mutations
{
    public class ReservationMutations : ObjectGraphType
    {
        public ReservationMutations()
        {
            // Here we define like in REST "endpoints"
            Field<ReservationType, Reservation>("add")
                .Argument<ReservationInputType>("reservation")
                .ResolveScopedAsync(async ctx =>
            {
                // here its like automapping since properties from menuInputType and menu are the same
                var dto = ctx.GetArgument<Reservation>("reservation");

                var resRepo = ctx.RequestServices!.GetRequiredService<IReservationRepository>();
                await resRepo.AddReservation(dto);
                return dto;
            });
        }
    }
}
