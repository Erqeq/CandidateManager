using CandidateManager.Application.DTOs;
using CandidateManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CandidateManager.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidatesController : ControllerBase
{
    private readonly ICandidateService _candidateService;

    public CandidatesController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpGet(Name = "GetAllCandidates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CandidateDto>>> GetBooks()
    {
        var candidates = await _candidateService.GetAllAsync();
        return Ok(candidates);
    }
}
