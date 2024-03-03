using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Mutations
{
    public class CategoryMutations : ObjectGraphType
    {
        public CategoryMutations()
        {
            // Here we define like in REST "endpoints"
            Field<CategoryType, Category>("add")
                .Arguments(new QueryArguments(new QueryArgument<CategoryInputType>() { Name = "category" }))
                .ResolveScopedAsync(async ctx =>
            {
                // here its like automapping since properties from menuInputType and menu are the same
                var dto = ctx.GetArgument<Category>("category");
                var catRepo = ctx.RequestServices!.GetRequiredService<ICategoryRepository>();
                await catRepo.AddCategory(dto);
                return dto; // it will be automapped to MenuType
            });

            Field<CategoryType, Category>("update")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" },
                                              new QueryArgument<CategoryInputType>() { Name = "category" }))
                .ResolveScopedAsync(async ctx =>
                {
                    // here its like automapping since properties from menuInputType and menu are the same
                    var dto = ctx.GetArgument<Category>("category");
                    var id = ctx.GetArgument<int>("id");

                    var catRepo = ctx.RequestServices!.GetRequiredService<ICategoryRepository>();
                    return await catRepo.UpdateCategory(id, dto);
                });

            Field<StringGraphType, string>("delete")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" }))
                .ResolveScopedAsync(async ctx =>
                 {
                     var id = ctx.GetArgument<int>("id");

                     var catRepo = ctx.RequestServices!.GetRequiredService<ICategoryRepository>();
                     await catRepo.DeleteCategory(id);
                     return "Deleted successfully";
                 });
        }
    }
}
