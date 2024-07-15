using AutoMapper;
using CandidateManager.Application.DTOs;
using CandidateManager.Domain.Entities;

namespace CandidateManager.Application.Mappings;

public class CandidateMappingProfile : Profile
{
	public CandidateMappingProfile()
	{
        CreateMap<Candidate, CandidateDto>().ReverseMap();
    }
}
