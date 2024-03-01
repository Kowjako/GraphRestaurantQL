using GraphQL.Types;
using GraphRestaurantQL.Models;

namespace GraphRestaurantQL.Type
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType()
        {
            Field(_ => _.Id);
            Field(_ => _.Name);
            Field(_ => _.ImageUrl);
            Field(_ => _.Menus);
        }
    }
}
