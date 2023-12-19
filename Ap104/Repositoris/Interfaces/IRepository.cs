using System.Linq.Expressions;

namespace Ap104.Repositoris.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAllAsync(
            Expression<Func<T,bool>>? expression=null,
            Expression<Func<T,object>>? orderExpression=null,
            bool isDescending=false,
            int skip=0,
            int take=0,
            bool isTracking=false,
            params string[] includes
            );
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
