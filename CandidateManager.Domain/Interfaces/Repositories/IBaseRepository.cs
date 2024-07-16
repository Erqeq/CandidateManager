using System.Linq.Expressions;

namespace CandidateManager.Domain.Interfaces.Repositories;

public interface IBaseRepository<IEntity>
{
    Task<IEnumerable<IEntity>> GetAllAsync();
    Task<IEntity> CreateAsync(IEntity entity);
    Task<bool> UpdateAsync(IEntity entity);
    Task<bool> DeleteByEmailAsync(string email);
    Task<IEntity?> FindAsync(Expression<Func<IEntity, bool>> predicate);
}
