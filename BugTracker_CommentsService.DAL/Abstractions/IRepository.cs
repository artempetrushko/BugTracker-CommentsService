using BugTracker_CommentsService.Domain;

namespace BugTracker_CommentsService.DAL.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        Task<bool> RemoveAsync(Guid id);

        void Update(T entity);

        Task SaveChangesAsync();
    }
}
