using System.Linq.Expressions;

namespace Ap104.Repositoris.Interfaces
{
    public interface IRepository
    {
        Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>>? exp = null, params string[] includes);
        Task<Category> GetByIdAsync(int id);
        void Delete(Category category);
        Task AddAsync(Category category);
        void Update(Category category);
        Task SaveChangesAsync();
    }
}
