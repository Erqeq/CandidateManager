using System.Linq.Expressions;
using CandidateManager.Domain.Interfaces.Repositories;
using CandidateManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CandidateManager.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("Entity is null");
        }

        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentException("Entity is null");
        }

        _dbSet.Update(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteByEmailAsync(string email)
    {
        var entity = await _dbSet.FindAsync(email);
        if (entity == null)
        {
            return false;
        }

        _dbSet.Remove(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }
}
