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

    /// <summary>
    /// Get all candidates   
    /// </summary>
    /// <returns>List of candidates</returns>
    [HttpGet(Name = "GetAllCandidates")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CandidateDto>>> GetCandidates()
    {
        var candidates = await _candidateService.GetAllAsync();
        return Ok(candidates);
    }

    /// <summary>
    /// Create or update a candidate
    /// </summary>
    /// <param name="candidateDto">Candidate data</param>
    /// <returns>Created or updated candidate</returns>
    [HttpPost(Name = "CreateOrUpdateCandidate")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrUpdateCandidate([FromBody] CandidateDto candidateDto)
    {
        if (candidateDto == null)
        {
            return BadRequest("Candidate data is null");
        }

        try
        {
            var emailExists  = await _candidateService.IsEmailRegisteredAsync(candidateDto.Email);

            if (emailExists)
            {
                await _candidateService.UpdateAsync(candidateDto);
                return NoContent();
            }

            var createdCandidate = await _candidateService.CreateAsync(candidateDto);
            return StatusCode(StatusCodes.Status201Created, createdCandidate);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete a candidate by email
    /// </summary>
    /// <param name="id">Candidate email</param>
    [HttpDelete("{email}", Name = "DeleteCandidate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCandidate(string email)
    {
        var deletedCandidate = await _candidateService.DeleteByEmailAsync(email);
        if (!deletedCandidate)
        {
            return NotFound();
        }

        return NoContent();
    }
}
