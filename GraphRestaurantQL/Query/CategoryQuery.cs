using System.Security.Claims;
using GraphQL;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Query
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(ICategoryRepository catRepo)
        {
            // Here we define like in REST "endpoints"
            Field<ListGraphType<CategoryType>>("getAll").ResolveAsync(async ctx =>
            {
                return await catRepo.GetCategories();
            });
        }
    }
}
