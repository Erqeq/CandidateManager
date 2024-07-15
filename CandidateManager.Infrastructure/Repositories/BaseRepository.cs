using CandidateManager.Domain.Interfaces.Repositories;
using CandidateManager.Infrastructure.Data;

namespace CandidateManager.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("Entity is null");
        }

        _dbContext.Add(entity);
        _dbContext.SaveChangesAsync();

        return Task.FromResult(entity);
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public Task<TEntity> RemoveAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("Entity is null");
        }

        _dbContext.Remove(entity);
        _dbContext.SaveChangesAsync();

        return Task.FromResult(entity);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("Entity is null");
        }

        _dbContext.Update(entity);
        _dbContext.SaveChangesAsync();

        return Task.FromResult(entity);
    }
}
