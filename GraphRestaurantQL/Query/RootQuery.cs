using GraphQL;
using GraphQL.Types;

namespace GraphRestaurantQL.Query
{
    // Aggregator for all Queries so we have single enty point for all queries
    [Authorize]
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Field<MenuQuery>("menu").Resolve(ctx => new {});
            Field<CategoryQuery>("category").Resolve(ctx => new {});
            Field<ReservationQuery>("reservation").Resolve(ctx => new {});
        }
    }
}
