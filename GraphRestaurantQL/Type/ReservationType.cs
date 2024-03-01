using GraphQL.Types;
using GraphRestaurantQL.Models;

namespace GraphRestaurantQL.Type
{
    public class ReservationType : ObjectGraphType<Reservation>
    {
        public ReservationType()
        {
            Field(m => m.Id).Description("id field");
            Field(m => m.ReservationDate).Description("date");
            Field(m => m.CustomerName);
            Field(m => m.Email).Description("id field");
            Field(m => m.PartySize).Description("image");
            Field(m => m.SpecialRequest);
            Field(m => m.PhoneNumber);
        }
    }
}
