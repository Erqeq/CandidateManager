using CandidateManager.Application.DTOs;

namespace CandidateManager.Application.Interfaces;

public interface ICandidateService
{
    Task<IEnumerable<CandidateDto>> GetAllAsync();
    Task<CandidateDto> CreateAsync(CandidateDto entity);
    Task<bool> UpdateAsync(CandidateDto entity);
    Task<bool> DeleteByEmailAsync(string email);
    Task<bool> IsEmailRegisteredAsync(string email);
}
