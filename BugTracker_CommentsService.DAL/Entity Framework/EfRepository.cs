using BugTracker_CommentsService.DAL.Abstractions;
using BugTracker_CommentsService.Domain;
using Microsoft.EntityFrameworkCore;

namespace BugTracker_CommentsService.DAL.Entity_Framework
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private DbContext _dbContext;

        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext
                .Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext
                .Set<T>()
                .AddAsync(entity);
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }
            _dbContext.Set<T>().Remove(entity);
            return true;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
