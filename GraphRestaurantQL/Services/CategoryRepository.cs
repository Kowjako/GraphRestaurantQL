using GraphRestaurantQL.Data;
using GraphRestaurantQL.Interfaces;
using GraphRestaurantQL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphRestaurantQL.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GraphQLDbContext _ctx;

        public CategoryRepository(GraphQLDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _ctx.Categories.Add(category);
            await _ctx.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _ctx.Categories.FindAsync(id);
            if (category != null)
            {
                _ctx.Categories.Remove(category);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<Category>> GetCategories()
        {
            return await _ctx.Categories.Include(m => m.Menus).ToListAsync();
        }

        public async Task<Category> UpdateCategory(int id, Category category)
        {
            var categoryDb = await _ctx.Categories.FindAsync(id);

            if (categoryDb != null)
            {
                categoryDb.Name = category.Name;
                categoryDb.ImageUrl= category.ImageUrl;
                await _ctx.SaveChangesAsync();
            }

            return categoryDb;
        }
    }
}
