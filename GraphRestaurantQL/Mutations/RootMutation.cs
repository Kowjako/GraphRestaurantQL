using GraphQL;
using GraphQL.Types;

namespace GraphRestaurantQL.Mutations
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            Field<CategoryMutations>("category").Authorize().Resolve(_ => new { });
            Field<MenuMutations>("menu").Authorize().Resolve(_ => new { });
            Field<ReservationMutations>("reservation").Authorize().Resolve(_ => new { });

            // Disable authentication for auth mutations
            Field<AuthMutations>("auth").AllowAnonymous().Resolve(_ => new { });
        }
    }
}
