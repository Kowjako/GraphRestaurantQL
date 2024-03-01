using GraphQL.Types;

namespace GraphRestaurantQL.Mutations
{
    public class RootMutation : ObjectGraphType
    {
        public RootMutation()
        {
            Field<CategoryMutations>("category").Resolve(_ => new { });
            Field<MenuMutations>("menu").Resolve(_ => new { });
            Field<ReservationMutations>("reservation").Resolve(_ => new { });
            Field<AuthMutations>("auth").Resolve(_ => new { });
        }
    }
}
