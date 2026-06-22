using Flowboard.Application.DTOs.Document;
using Flowboard.Intalio.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Flowboard.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class AdminDocumentControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IDocumentIntalioService> _serviceMock;

        public AdminDocumentControllerTests(WebApplicationFactory<Program> factory)
        {
            _serviceMock = new Mock<IDocumentIntalioService>();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IDocumentIntalioService));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton(_serviceMock.Object);
                });
            });
        }

        private HttpClient CreateClient() => _factory.CreateClient();

        // Happy path
        [Fact]
        public async Task GetTrackingByTask_WithExistingTaskId_ReturnsOkWithDocument()
        {
            // Arrange
            var fakeDocument = new DocumentDetailsDto
            {
                Id = 1,
            };

            _serviceMock
                .Setup(s => s.GetDocumentByTaskIdAsync(42))
                .ReturnsAsync(fakeDocument);

            // Act
            var response = await CreateClient()
                .GetAsync("/api/admin/document/by-task/42");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            body.GetProperty("id").GetInt32().Should().Be(1);
        }

        // Sad paths
        [Fact]
        public async Task GetTrackingByTask_WithNonExistingTaskId_ReturnsNotFound()
        {
            // Arrange
            _serviceMock!
                .Setup(s => s.GetDocumentByTaskIdAsync(999))!
                .ReturnsAsync((DocumentDetailsDto?)null);

            // Act
            var response = await CreateClient()
                .GetAsync("/api/admin/document/by-task/999");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        // Edge cases — invalid route values
        [Theory]
        [InlineData("/api/admin/document/by-task/0")]    // zero id
        [InlineData("/api/admin/document/by-task/-1")]   // negative id
        public async Task GetTrackingByTask_WithInvalidTaskId_ReturnsNotFound(string url)
        {
            // Arrange — service returns null for any invalid id
            _serviceMock!
                .Setup(s => s.GetDocumentByTaskIdAsync(It.Is<int>(id => id <= 0)))!
                .ReturnsAsync((DocumentDetailsDto?)null);

            // Act
            var response = await CreateClient().GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetTrackingByTask_WithNonNumericTaskId_ReturnsBadRequest()
        {
            // Arrange — "abc" cannot be bound to int, ASP.NET rejects it
            // Act
            var response = await CreateClient()
                .GetAsync("/api/admin/document/by-task/abc");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        // Service fault — unexpected exception should not leak a 200
        [Fact]
        public async Task GetTrackingByTask_WhenServiceThrows_ReturnsInternalServerError()
        {
            // Arrange
            _serviceMock
                .Setup(s => s.GetDocumentByTaskIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Unexpected DB failure"));

            // Act
            var response = await CreateClient()
                .GetAsync("/api/admin/document/by-task/1");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}