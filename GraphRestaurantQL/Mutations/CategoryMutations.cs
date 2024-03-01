using GraphQL;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Mutations
{
    public class CategoryMutations : ObjectGraphType
    {
        public CategoryMutations(ICategoryRepository catRepo)
        {
            // Here we define like in REST "endpoints"
            Field<MenuType>("add")
                .Arguments(new QueryArguments(new QueryArgument<CategoryInputType>() { Name = "category" }))
                .ResolveAsync(async ctx =>
            {
                // here its like automapping since properties from menuInputType and menu are the same
                var dto = ctx.GetArgument<Category>("category");
                await catRepo.AddCategory(dto);
                return dto; // it will be automapped to MenuType
            });

            Field<MenuType>("update")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" },
                                              new QueryArgument<CategoryInputType>() { Name = "category" }))
                .ResolveAsync(async ctx =>
                {
                    // here its like automapping since properties from menuInputType and menu are the same
                    var dto = ctx.GetArgument<Category>("category");
                    var id = ctx.GetArgument<int>("id");
                    await catRepo.UpdateCategory(id, dto);
                    return dto;
                });

            Field<StringGraphType>("delete")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" }))
                .ResolveAsync(async ctx =>
                 {
                     var id = ctx.GetArgument<int>("id");
                     await catRepo.DeleteCategory(id);
                     return "Deleted successfully";
                 });
        }
    }
}
