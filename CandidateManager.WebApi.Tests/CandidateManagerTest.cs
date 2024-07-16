using CandidateManager.Application.DTOs;
using CandidateManager.Application.Interfaces;
using CandidateManager.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace CandidateManager.Tests
{
    public class CandidatesControllerTests
    {
        private readonly Mock<ICandidateService> _candidateServiceMock;
        private readonly IMemoryCache _memoryCache;
        private readonly CandidatesController _controller;

        public CandidatesControllerTests()
        {
            _candidateServiceMock = new Mock<ICandidateService>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _controller = new CandidatesController(_candidateServiceMock.Object, _memoryCache);
        }

        [Fact]
        public async Task GetCandidates_ReturnsOkResult_WithListOfCandidates()
        {
            // Arrange
            var candidates = new List<CandidateDto>
            {
                new CandidateDto { FirstName = "Test", Email = "Test@test.com" },
                new CandidateDto { FirstName = "Test2", Email = "Test2@test.com" }
            };
            _candidateServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(candidates);

            // Act
            var result = await _controller.GetCandidates();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CandidateDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCandidates_ReturnsCachedData()
        {
            // Arrange
            var candidates = new List<CandidateDto>
            {
                new CandidateDto { FirstName = "John", Email = "john@test.com" }
            };
            _memoryCache.Set("candidatesList", candidates);

            // Act
            var result = await _controller.GetCandidates();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<CandidateDto>>(okResult.Value);
            Assert.Single(returnValue);
            _candidateServiceMock.Verify(service => service.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateOrUpdateCandidate_ReturnsBadRequest_WhenCandidateDtoIsNull()
        {
            // Act
            var result = await _controller.CreateOrUpdateCandidate(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Candidate data is null", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateOrUpdateCandidate_CreatesNewCandidate_WhenEmailNotRegistered()
        {
            // Arrange
            var candidateDto = new CandidateDto { FirstName = "John", Email = "john@test.com" };
            _candidateServiceMock.Setup(service => service.IsEmailRegisteredAsync(candidateDto.Email)).ReturnsAsync(false);
            _candidateServiceMock.Setup(service => service.CreateAsync(candidateDto)).ReturnsAsync(candidateDto);

            // Act
            var result = await _controller.CreateOrUpdateCandidate(candidateDto);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            var returnValue = Assert.IsType<CandidateDto>(createdResult.Value);
            Assert.Equal(candidateDto.Email, returnValue.Email);
            Assert.False(_memoryCache.TryGetValue("candidatesList", out _));
        }

        [Fact]
        public async Task CreateOrUpdateCandidate_UpdatesExistingCandidate_WhenEmailRegistered()
        {
            // Arrange
            var candidateDto = new CandidateDto { FirstName = "John", Email = "john@test.com" };
            _candidateServiceMock.Setup(service => service.IsEmailRegisteredAsync(candidateDto.Email)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateOrUpdateCandidate(candidateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _candidateServiceMock.Verify(service => service.UpdateAsync(candidateDto), Times.Once);
            Assert.False(_memoryCache.TryGetValue("candidatesList", out _));
        }

        [Fact]
        public async Task DeleteCandidate_ReturnsNotFound_WhenCandidateNotFound()
        {
            // Arrange
            var email = "test@test1.com";
            _candidateServiceMock.Setup(service => service.DeleteByEmailAsync(email)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCandidate(email);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteCandidate_ReturnsNoContent_WhenCandidateDeleted()
        {
            // Arrange
            var email = "test@test.com";
            _candidateServiceMock.Setup(service => service.DeleteByEmailAsync(email)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCandidate(email);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.False(_memoryCache.TryGetValue("candidatesList", out _));
        }
    }
}
