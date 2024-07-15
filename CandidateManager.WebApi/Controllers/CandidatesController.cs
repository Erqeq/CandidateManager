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
    /// Get a candidate by id
    /// </summary>
    /// <param name="id">Candidate id</param>
    /// <returns>Candidate detail</returns>
    [HttpGet("{id}", Name = "GetCandidateById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CandidateDto>> GetCandidateById(int id)
    {
        var candidate = await _candidateService.GetByIdAsync(id);
        if (candidate is null)
        {
            return NotFound();
        }
        return Ok(candidate);
    }

    /// <summary>
    /// Create a new candidate
    /// </summary>
    /// <param name="candidateDto">Candidate data</param>
    /// <returns>Created candidate</returns>
    [HttpPost(Name = "CreateCandidate")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CandidateDto>> CreateCandidate(CandidateDto candidateDto)
    {
        try
        {
            var createdCandidate = await _candidateService.CreateAsync(candidateDto);

            return CreatedAtAction(nameof(GetCandidateById),
                new { id = createdCandidate.Id }, createdCandidate);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Update a candidate
    /// </summary>
    /// <param name="id">Candidate Id</param>
    /// <param name="candidateDto">Options of a candidate</param>
    /// <returns></returns>
    [HttpPut("{id}", Name = "UpdateCandidate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCandidate(int id, [FromBody] CandidateDto candidateDto)
    {
        if (id != candidateDto.Id)
        {
            return BadRequest();
        }

        var updatedCandidate = await _candidateService.UpdateAsync(candidateDto);
        if (!updatedCandidate)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete a candidate by id
    /// </summary>
    /// <param name="id">Candidate Id</param>
    [HttpDelete("{id}", Name = "DeleteCandidate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCandidate(int id)
    {
        var deletedCandidate = await _candidateService.DeleteAsync(id);
        if (!deletedCandidate)
        {
            return NotFound();
        }

        return NoContent();
    }
}
