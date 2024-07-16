using CandidateManager.Application.DTOs;

namespace CandidateManager.Application.Interfaces;

public interface ICandidateService
{
    Task<IEnumerable<CandidateDto>> GetAllAsync();
    Task<CandidateDto> GetByIdAsync(int id);
    Task<CandidateDto> CreateAsync(CandidateDto entity);
    Task<bool> UpdateAsync(CandidateDto entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> IsEmailRegisteredAsync(string email);
}
