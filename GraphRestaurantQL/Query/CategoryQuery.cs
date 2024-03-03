using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Query
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery()
        {
            // Here we define like in REST "endpoints"
            Field<ListGraphType<CategoryType>, IReadOnlyList<Category>>("getAll")
                .ResolveScopedAsync(async ctx =>
            {
                var catRepo = ctx.RequestServices!.GetRequiredService<ICategoryRepository>();
                return await catRepo.GetCategories();
            });
        }
    }
}
