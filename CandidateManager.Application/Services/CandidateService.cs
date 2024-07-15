using CandidateManager.Application.DTOs;
using CandidateManager.Application.Interfaces;
using AutoMapper;
using CandidateManager.Domain.Interfaces.Repositories;
using CandidateManager.Domain.Entities;

namespace CandidateManager.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IBaseRepository<Candidate> _repository;
        private readonly IMapper _mapper;

        public CandidateService(IBaseRepository<Candidate> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CandidateDto>> GetAllAsync()
        {
            var candidates = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }

        public async Task<CandidateDto> GetByIdAsync(int id)
        {
            var candidate = await _repository.GetByIdAsync(id);
            return _mapper.Map<CandidateDto>(candidate);
        }

        public async Task CreateAsync(CandidateDto candidateDto)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);
            await _repository.CreateAsync(candidate);
        }

        public async Task<bool> UpdateAsync(CandidateDto candidateDto)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);
            return await _repository.UpdateAsync(candidate);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
