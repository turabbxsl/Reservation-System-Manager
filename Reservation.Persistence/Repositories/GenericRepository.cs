using Microsoft.EntityFrameworkCore;
using Reservation.Application.Interfaces;
using Reservation.Domain.Entities;
using Reservation.Persistence.Context;
using System.Linq.Expressions;

namespace Reservation.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ReservationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ReservationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #region Get

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public async Task<List<Service>> FindWithIncludeAsync(Expression<Func<Service, bool>> predicate)
        {
            return await _context.Services
                .Include(s => s.Specialty)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        #endregion



        #region Post

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Remove(T entity) => _dbSet.Remove(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        #endregion


    }
}
