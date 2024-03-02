using GraphQL;
using GraphQL.Types;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Type;

namespace GraphRestaurantQL.Query
{
    // In the REST its like a controller for Menu's but only for reading
    public class MenuQuery : ObjectGraphType
    {
        public MenuQuery(IMenuRepository menuRepo)
        {
            Description = "Menu query entrypoint";

            // Here we define like in REST "endpoints"
            Field<ListGraphType<MenuType>>("getAll").Description("Retrieve all menus").ResolveAsync(async ctx =>
            {
                return await menuRepo.GetAll();
            });

            Field<MenuType>("getById")
                .Description("Get menu by id")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "menuId" }))
                .ResolveAsync(async ctx =>
                {
                    return await menuRepo.GetMenuById(ctx.GetArgument<int>("menuId"));
                });
        }
    }
}
