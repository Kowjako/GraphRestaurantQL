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
            Description = "Main entry point to access all project queries";

            Field<MenuQuery>("menu")
                .Description("Graph node related to Menu queries")
                .Resolve(ctx => new {});

            Field<CategoryQuery>("category")
                .Description("Graph node related to category queries")
                .Resolve(ctx => new {});

            Field<ReservationQuery>("reservation")
                .Description("Graph node related to reservation queries")
                .Resolve(ctx => new {});
        }
    }
}
