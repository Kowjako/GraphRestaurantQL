using GraphQL;
using GraphQL.Types;

namespace GraphRestaurantQL.Query
{
    // Aggregator for all Queries so we have single enty point for all queries
    public class RootQuery : ObjectGraphType
    {
        public RootQuery()
        {
            Description = "Main entry point to access all project queries";

            Field<MenuQuery>("menu")
                .Authorize()
                .Description("Graph node related to Menu queries")
                .Resolve(ctx => new {});

            Field<CategoryQuery>("category")
                .Authorize()
                .Description("Graph node related to category queries")
                .Resolve(ctx => new {});

            Field<ReservationQuery>("reservation")
                .Authorize()
                .Description("Graph node related to reservation queries")
                .Resolve(ctx => new {});
        }
    }
}
