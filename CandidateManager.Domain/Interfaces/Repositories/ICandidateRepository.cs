using CandidateManager.Domain.Entities;

namespace CandidateManager.Domain.Interfaces.Repositories;

public interface ICandidateRepository
{
    Task<IEnumerable<Candidate>> GetBooksAsync();
    Task<Candidate> GetBookByIdAsync(int id);
    Task CreateBookAsync(Candidate candidate);
    Task<bool> UpdateBookAsync(Candidate candidate);
    Task<bool> DeleteBookAsync(int id);
}
