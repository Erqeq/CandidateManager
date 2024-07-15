namespace CandidateManager.Domain.Interfaces.Repositories;

public interface IBaseRepository<IEntity>
{
    Task<IEnumerable<IEntity>> GetAllAsync();
    Task<IEntity> GetByIdAsync(int id);
    Task CreateAsync(IEntity entity);
    Task<bool> UpdateAsync(IEntity entity);
    Task<bool> DeleteAsync(int id);
}
