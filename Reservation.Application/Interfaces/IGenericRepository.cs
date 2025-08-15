using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Reservation.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> FindWithIncludeAsync(
     Expression<Func<T, bool>> predicate,
     Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task AddAsync(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);

        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyAsync(Expression<Func<T,bool>> predicate);
    }
}
