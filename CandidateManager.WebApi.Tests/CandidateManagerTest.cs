using Moq;
using Microsoft.AspNetCore.Mvc;
using CandidateManager.WebApi.Controllers;
using CandidateManager.Application.Interfaces;
using CandidateManager.Application.DTOs;

public class CandidatesControllerTests
{
    private readonly Mock<ICandidateService> _mockCandidateService;
    private readonly CandidatesController _controller;

    public CandidatesControllerTests()
    {
        _mockCandidateService = new Mock<ICandidateService>();
        _controller = new CandidatesController(_mockCandidateService.Object);
    }

    [Fact]
    public async Task GetCandidates_ReturnsOkResult_WithListOfCandidates()
    {
        // Arrange
        var candidates = new List<CandidateDto>
        {
            new CandidateDto { Email = "test1@test.com", FirstName = "John", LastName = "Doe" },
            new CandidateDto { Email = "test2@test.com", FirstName = "Jane", LastName = "Doe" }
        };
        _mockCandidateService.Setup(service => service.GetAllAsync())
            .ReturnsAsync(candidates);

        // Act
        var result = await _controller.GetCandidates();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnCandidates = Assert.IsType<List<CandidateDto>>(okResult.Value);
        Assert.Equal(2, returnCandidates.Count);
    }

    [Fact]
    public async Task CreateOrUpdateCandidate_ReturnsCreatedAtActionResult_WhenCandidateIsCreated()
    {
        // Arrange
        var candidate = new CandidateDto { Email = "test1@test.com" };
        _mockCandidateService.Setup(service => service.IsEmailRegisteredAsync(candidate.Email))
            .ReturnsAsync(false);
        _mockCandidateService.Setup(service => service.CreateAsync(candidate))
            .ReturnsAsync(candidate);

        // Act
        var result = await _controller.CreateOrUpdateCandidate(candidate);

        // Assert
        var createdAtActionResult = Assert.IsType<ObjectResult>(result);
        var returnCandidate = Assert.IsType<CandidateDto>(createdAtActionResult.Value);
        Assert.Equal(candidate.Email, returnCandidate.Email);
    }

    [Fact]
    public async Task CreateOrUpdateCandidate_ReturnsNoContentResult_WhenCandidateIsUpdated()
    {
        // Arrange
        var candidate = new CandidateDto { Email = "test1@test.com" };
        _mockCandidateService.Setup(service => service.IsEmailRegisteredAsync(candidate.Email))
            .ReturnsAsync(true);
        _mockCandidateService.Setup(service => service.UpdateAsync(candidate))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.CreateOrUpdateCandidate(candidate);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteCandidate_ReturnsNoContentResult_WhenCandidateIsDeleted()
    {
        // Arrange
        _mockCandidateService.Setup(service => service.DeleteByEmailAsync("test1@test.com"))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteCandidate("test1@test.com");

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteCandidate_ReturnsNotFoundResult_WhenCandidateDoesNotExist()
    {
        // Arrange
        _mockCandidateService.Setup(service => service.DeleteByEmailAsync("test1@test.com"))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteCandidate("test1@test.com");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
