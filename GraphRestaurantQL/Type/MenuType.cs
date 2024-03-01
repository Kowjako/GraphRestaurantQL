using GraphQL.Types;
using GraphRestaurantQL.Models;

namespace GraphRestaurantQL.Type
{
    // Needed for GraphQL type system, to allow autocomplete etc.
    // So its a bridge between model and GraphQL type
    // for each model we need such bridge

    // So in rest api its something like DTO, but in graphQL its separate graph object
    // but it stand as DTO
    public class MenuType : ObjectGraphType<Menu>
    {
        public MenuType()
        {
            Description = "ABC";

            // Here we provide which properties will be available for the GraphQL
            // from our model, here we allow all
            Field(m => m.Id).Description("id field");
            Field(m => m.Name).Description("id field");
            Field(m => m.Description).Description("id field");
            Field(m => m.Price).Description("id field");
            Field(m => m.ImageUrl).Description("image");
            Field(m => m.CategoryId);
        }
    }
}
