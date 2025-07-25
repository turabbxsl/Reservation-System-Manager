using Reservation.Domain.Entities;
using System.Linq.Expressions;

namespace Reservation.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task<List<Service>> FindWithIncludeAsync(Expression<Func<Service, bool>> predicate);

        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
