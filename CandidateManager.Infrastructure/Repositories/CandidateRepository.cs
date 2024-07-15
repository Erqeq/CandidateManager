using CandidateManager.Domain.Entities;
using CandidateManager.Domain.Interfaces.Repositories;
using CandidateManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CandidateManager.Infrastructure.Repositories;

public class CandidateRepository(AppDbContext context) : ICandidateRepository
{
    public async Task<IEnumerable<Candidate>> GetBooksAsync()
    {
        return await context.Candidates.ToListAsync();
    }

    public async Task<Candidate> GetBookByIdAsync(int id)
    {
        return await context.Candidates.FindAsync(id);
    }

    public async Task CreateBookAsync(Candidate candidate)
    {
        await context.Candidates.AddAsync(candidate);
        await context.SaveChangesAsync();
    }

    public async Task<bool> UpdateBookAsync(Candidate candidate)
    {
        context.Candidates.Update(candidate);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var candidate = await context.Candidates.FindAsync(id);

        if(candidate == null)
        {
            return false;
        }

        context.Candidates.Remove(candidate);
        return await context.SaveChangesAsync() > 0;
    }
}
