using GraphQL.Types;

namespace GraphRestaurantQL.Type
{
    // Defining like a DTO to create an object (Creation = mutation)
    public class CategoryInputType : InputObjectGraphType
    {
        public CategoryInputType()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("imageUrl");
        }
    }
}
