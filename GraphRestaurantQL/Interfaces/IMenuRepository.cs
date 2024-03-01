using GraphRestaurantQL.Models;

namespace GraphRestaurantQL.Interfaces
{
    public interface IMenuRepository
    {
        Task<IReadOnlyList<Menu>> GetAll();
        Task<Menu?> GetMenuById(int id);
        Task<Menu> AddMenu(Menu menu);
        Task<Menu> UpdateMenu(int id, Menu menu);
        Task DeleteMenu(int id);
    }
}
