using GraphQL.Types;

namespace GraphRestaurantQL.Type
{
    // Defining like a DTO to create an object (Creation = mutation)
    public class MenuInputType : InputObjectGraphType
    {
        public MenuInputType()
        {
            Field<IntGraphType>("id");
            Field<StringGraphType>("name");
            Field<StringGraphType>("description");
            Field<FloatGraphType>("price");
            Field<StringGraphType>("imageUrl");
            Field<IntGraphType>("categoryId");
        }
    }
}
