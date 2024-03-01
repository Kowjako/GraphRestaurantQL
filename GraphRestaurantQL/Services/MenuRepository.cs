using GraphRestaurantQL.Data;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphRestaurantQL.Services
{
    public class MenuRepository : IMenuRepository
    {
        private readonly GraphQLDbContext _ctx;

        public MenuRepository(GraphQLDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Menu> AddMenu(Menu menu)
        {
            _ctx.Menus.Add(menu);
            await _ctx.SaveChangesAsync();

            return menu;
        }

        public async Task DeleteMenu(int id)
        {
            var menu = await _ctx.Menus.FindAsync(id);

            if (menu != null)
            {
                _ctx.Menus.Remove(menu);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<Menu>> GetAll()
        {
            return await _ctx.Menus.ToListAsync();
        }

        public async Task<Menu?> GetMenuById(int id)
        {
            return await _ctx.Menus.FindAsync(id);
        }

        public async Task<Menu> UpdateMenu(int id, Menu menu)
        {
            var menuDb = await _ctx.Menus.FindAsync(id);

            if (menuDb != null)
            {
                menuDb.Name = menu.Name;
                menuDb.Description = menu.Description;
                menuDb.Price = menu.Price;
                await _ctx.SaveChangesAsync();
            }

            return menu;
        }
    }
}
