﻿using CandidateManager.Application.DTOs;
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

        public async Task<CandidateDto> CreateAsync(CandidateDto candidateDto)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);
            var createdCandidate = await _repository.CreateAsync(candidate);
            return _mapper.Map<CandidateDto>(createdCandidate);
        }

        public async Task<bool> UpdateAsync(CandidateDto candidateDto)
        {
            var candidate = _mapper.Map<Candidate>(candidateDto);
            return await _repository.UpdateAsync(candidate);
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            return await _repository.DeleteByEmailAsync(email);
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            var candidateEmail = await _repository.FindAsync(c => c.Email == email);
            return candidateEmail != null;
        }
    }
}
