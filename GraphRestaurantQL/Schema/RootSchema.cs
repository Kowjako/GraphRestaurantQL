using GraphRestaurantQL.Mutations;
using GraphRestaurantQL.Query;

namespace GraphRestaurantQL.Schema
{
    // root -> category / menu / query -> category (few fields) + menu (few fields) + query (few fields)

    public class RootSchema : GraphQL.Types.Schema
    {
        public RootSchema(IServiceProvider sp) : base(sp)
        {
            Query = sp.GetRequiredService<RootQuery>();
            Mutation = sp.GetRequiredService<RootMutation>();
        }
    }
}
