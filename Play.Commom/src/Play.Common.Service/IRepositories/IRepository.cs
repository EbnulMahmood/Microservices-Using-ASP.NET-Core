using Play.Common.Service.IEntities;
using System.Linq.Expressions;

namespace Play.Common.Service.IRepositories
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<IReadOnlyCollection<TEntity>> LoadAsync();
        Task<IReadOnlyCollection<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter);
        Task CreateAsync(TEntity entityToCreate);
        Task UpdateAsync(TEntity entityToUpdate);
        Task DeleteAsync(Guid id);
    }
}
