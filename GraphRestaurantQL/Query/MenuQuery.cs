using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Query
{
    // In the REST its like a controller for Menu's but only for reading
    public class MenuQuery : ObjectGraphType
    {
        public MenuQuery()
        {
            Description = "Menu query entrypoint";

            // Here we define like in REST "endpoints"
            Field<ListGraphType<MenuType>, IReadOnlyList<Menu>>("getAll")
                .Description("Retrieve all menus")
                .ResolveScopedAsync(async ctx =>
            {
                var menuRepo = ctx.RequestServices!.GetRequiredService<IMenuRepository>();
                return await menuRepo.GetAll();
            });

            Field<MenuType, Menu>("getById")
                .Description("Get menu by id")
                .Argument<IntGraphType>("menuId")
                .ResolveScopedAsync(async ctx =>
                {
                    var menuRepo = ctx.RequestServices!.GetRequiredService<IMenuRepository>();
                    return await menuRepo.GetMenuById(ctx.GetArgument<int>("menuId"));
                });
        }
    }
}
