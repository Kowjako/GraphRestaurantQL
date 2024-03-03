using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Mutations
{
    public class MenuMutations : ObjectGraphType
    {
        public MenuMutations()
        {
            // Here we define like in REST "endpoints"
            Field<MenuType, Menu>("add")
                .Arguments(new QueryArguments(new QueryArgument<MenuInputType>() { Name = "menu" }))
                .ResolveScopedAsync(async ctx =>
            {
                // here its like automapping since properties from menuInputType and menu are the same
                var dto = ctx.GetArgument<Menu>("name");
                var menuRepo = ctx.RequestServices!.GetRequiredService<IMenuRepository>();
                await menuRepo.AddMenu(dto);
                return dto;
            });

            Field<MenuType, Menu>("update")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" },
                                              new QueryArgument<MenuInputType>() { Name = "menu" }))
                .ResolveScopedAsync(async ctx =>
                {
                    // here its like automapping since properties from menuInputType and menu are the same
                    var dto = ctx.GetArgument<Menu>("name");
                    var id = ctx.GetArgument<int>("id");
                    var menuRepo = ctx.RequestServices!.GetRequiredService<IMenuRepository>();
                    await menuRepo.UpdateMenu(id, dto);
                    return dto;
                });

            Field<StringGraphType, string>("delete")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "id" }))
                .ResolveScopedAsync(async ctx =>
                 {
                     var id = ctx.GetArgument<int>("id");
                     var menuRepo = ctx.RequestServices!.GetRequiredService<IMenuRepository>();
                     await menuRepo.DeleteMenu(id);
                     return "Deleted successfully";
                 });
        }
    }
}
