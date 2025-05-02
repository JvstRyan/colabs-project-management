using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Colabs.ProjectManagement.Persistence.Repositories
{
    public class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ColabsDbContext _dbContext;

        public BaseRepository(ColabsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
           return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
           _dbContext.Entry(entity).State = EntityState.Modified;
           await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
           var entity = await GetByIdAsync(id, cancellationToken);
           if (entity != null)
           {
               _dbContext.Set<T>().Remove(entity);
               await _dbContext.SaveChangesAsync(cancellationToken);
           }
        }
        
    }
}
