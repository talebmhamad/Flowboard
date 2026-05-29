using FluentAssertions;
using Flowboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Flowboard.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthControllerTests(WebApplicationFactory<Program> factory)
        {
            _authServiceMock = new Mock<IAuthService>();

            // Swap in a stubbed IAuthService so tests are hermetic
            // and don't depend on a real database or external service.
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the real registration and replace with the mock
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IAuthService));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddSingleton(_authServiceMock.Object);
                });
            });
        }

        private HttpClient CreateClient() => _factory.CreateClient();

        // Happy path
        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkWithNonEmptyToken()
        {
            // Arrange
            _authServiceMock
                .Setup(s => s.LoginAsync("admin", "P@$$w0rd1234"))
                .ReturnsAsync("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.sample");

            var request = new { Username = "admin", Password = "P@$$w0rd1234" };

            // Act
            var response = await CreateClient().PostAsJsonAsync("/api/auth/login", request);

            // Assert — status
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Assert — token exists and is a non-empty string (not null / "")
            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            body.TryGetProperty("token", out var tokenElement).Should().BeTrue(
                because: "response body must contain a 'token' property");
            tokenElement.GetString().Should().NotBeNullOrWhiteSpace(
                because: "token must be a non-empty string");
        }

        // Sad paths — authentication failures
        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            _authServiceMock
                .Setup(s => s.LoginAsync("wrong", "wrong"))
                .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials."));

            var request = new { Username = "wrong", Password = "wrong" };

            // Act
            var response = await CreateClient().PostAsJsonAsync("/api/auth/login", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("Admin", "P@$$w0rd1234")]   // username case mismatch
        [InlineData("admin", "p@$$w0rd1234")]   // password case mismatch
        public async Task Login_WithWrongCasing_ReturnsUnauthorized(string username, string password)
        {
            // Arrange
            _authServiceMock
                .Setup(s => s.LoginAsync(username, password))
                .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials."));

            var request = new { Username = username, Password = password };

            // Act
            var response = await CreateClient().PostAsJsonAsync("/api/auth/login", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        // Bad requests — missing / empty fields
        [Theory]
        [InlineData(null, "P@$$w0rd1234")]  // missing username
        [InlineData("admin", null)]            // missing password
        [InlineData("", "P@$$w0rd1234")]  // empty username
        [InlineData("admin", "")]              // empty password
        [InlineData("   ", "P@$$w0rd1234")] // whitespace-only username
        public async Task Login_WithMissingOrEmptyFields_ReturnsBadRequest(string? username, string? password)
        {
            // Arrange — model-binding / validation rejects before hitting the service
            var request = new { Username = username, Password = password };

            // Act
            var response = await CreateClient().PostAsJsonAsync("/api/auth/login", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        // Malformed / unexpected payloads
        [Fact]
        public async Task Login_WithMalformedJson_ReturnsBadRequest()
        {
            // Arrange
            var content = new StringContent(
                "{ this is not valid json }",
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await CreateClient().PostAsync("/api/auth/login", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_WithWrongContentType_ReturnsUnsupportedMediaType()
        {
            // Arrange
            var content = new StringContent(
                "Username=admin&Password=P%40%24%24w0rd1234",
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            // Act
            var response = await CreateClient().PostAsync("/api/auth/login", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        }

        [Fact]
        public async Task Login_WithEmptyBody_ReturnsBadRequest()
        {
            // Arrange
            var content = new StringContent(
                string.Empty,
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await CreateClient().PostAsync("/api/auth/login", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        // Security — special characters & injection attempts
        [Theory]
        [InlineData("admin' OR '1'='1", "anything")]      // SQL injection
        [InlineData("<script>alert(1)</script>", "pass")]  // XSS attempt
        [InlineData("admin\0null", "pass")]                // null-byte injection
        public async Task Login_WithSpecialCharacterPayloads_DoesNotCrash(string username, string password)
        {
            // Arrange — service throws; we just want the app to handle it gracefully
            _authServiceMock
                .Setup(s => s.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials."));

            var request = new { Username = username, Password = password };

            // Act
            var response = await CreateClient().PostAsJsonAsync("/api/auth/login", request);

            // Assert — 400 or 401 are both acceptable; 500 is not
            response.StatusCode.Should().BeOneOf(
                HttpStatusCode.BadRequest,
                HttpStatusCode.Unauthorized);
        }
    }
}