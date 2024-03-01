using GraphQL;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Mutations
{
    public class MenuMutations : ObjectGraphType
    {
        public MenuMutations(IMenuRepository menuRepo)
        {
            // Here we define like in REST "endpoints"
            Field<MenuType>("add")
                .Arguments(new QueryArguments(new QueryArgument<MenuInputType>() { Name = "menu" }))
                .ResolveAsync(async ctx =>
            {
                // here its like automapping since properties from menuInputType and menu are the same
                var dto = ctx.GetArgument<Menu>("name");
                await menuRepo.AddMenu(dto);
                return dto;
            });

            Field<MenuType>("update")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" },
                                              new QueryArgument<MenuInputType>() { Name = "menu" }))
                .ResolveAsync(async ctx =>
                {
                    // here its like automapping since properties from menuInputType and menu are the same
                    var dto = ctx.GetArgument<Menu>("name");
                    var id = ctx.GetArgument<int>("id");
                    await menuRepo.UpdateMenu(id, dto);
                    return dto;
                });

            Field<StringGraphType>("delete")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" }))
                .ResolveAsync(async ctx =>
                 {
                     var id = ctx.GetArgument<int>("id");
                     await menuRepo.DeleteMenu(id);
                     return "Deleted successfully";
                 });
        }
    }
}
