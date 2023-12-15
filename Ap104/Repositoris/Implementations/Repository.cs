
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ap104.Repositoris.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _db;
        public Repository(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Category category)
        {
            await _db.AddAsync(category);
        }
        public void Delete(Category category)
        {
            _db.Categories.Remove(category);
        }
        public async Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>>? exp = null, params string[] includes)
        {
            var que = _db.Categories.AsQueryable();
            if (exp != null) { que = que.Where(exp); }

            if (includes != null)
            {
                for (var i = 0; i < includes.Length; i++)
                {
                    que = que.Include(includes[i]);
                }
            }
            return que;
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            Category category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async void Update(Category category)
        {
            _db.Categories.Update(category);
        }
    }
}
