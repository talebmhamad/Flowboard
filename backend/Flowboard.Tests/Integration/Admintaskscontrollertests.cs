using FluentAssertions;
using Flowboard.Application.DTOs;
using Flowboard.Intalio.Interfaces;
using Flowboard.Intalio.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Flowboard.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class AdminTasksControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IUserExtensionService> _userServiceMock = new();
        private readonly Mock<ITaskService> _taskServiceMock = new();

        public AdminTasksControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_userServiceMock.Object);
                    services.AddSingleton(_taskServiceMock.Object);

                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
                });
            });
        }

        private HttpClient AdminClient(int userId = 1)
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Test-UserId", userId.ToString());
            client.DefaultRequestHeaders.Add("X-Test-Role", "Administrator");
            return client;
        }

        // Happy path
        [Fact]
        public async Task GetInboxTasks_AsAdmin_ReturnsOk()
        {
            _userServiceMock
                .Setup(s => s.GetEmployeesByManagerIdAsync(1))
                .ReturnsAsync(new List<UserLookupDto> { new() { Id = 10 } });

            _taskServiceMock
                .Setup(s => s.GetInboxTasksAsync(
                    It.IsAny<List<long>>(),
                    It.IsAny<TaskRequestDto>()))
                .ReturnsAsync(new DataTableResponse<InboxTaskDto>
                {
                    Data = new List<InboxTaskDto>
                    {
            new InboxTaskDto
            {
                Id = 1,
                ReferenceNumber = "REF-001",
                DocumentType = "Bug Report"
            }
                    }
                });

            var response = await AdminClient(userId: 1)
                .PostAsJsonAsync("/api/admin/tasks/inbox", new TaskRequestDto());

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // Authorization
        [Fact]
        public async Task GetInboxTasks_WithoutAuth_ReturnsUnauthorized()
        {
            var response = await _factory.CreateClient()
                .PostAsJsonAsync("/api/admin/tasks/inbox", new TaskRequestDto());

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetInboxTasks_WithNonAdminRole_ReturnsForbidden()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Test-UserId", "1");
            client.DefaultRequestHeaders.Add("X-Test-Role", "Employee");

            var response = await client
                .PostAsJsonAsync("/api/admin/tasks/inbox", new TaskRequestDto());

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }

    // Fake auth handler — reads headers instead of validating a real JWT
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler( IOptionsMonitor<AuthenticationSchemeOptions> options,ILoggerFactory logger, UrlEncoder encoder)
            : base(options, logger, encoder) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("X-Test-UserId", out var userId))
                return Task.FromResult(AuthenticateResult.Fail("No user"));

            var role = Request.Headers.TryGetValue("X-Test-Role", out var r) ? r.ToString() : "Employee";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var ticket = new AuthenticationTicket(
                new ClaimsPrincipal(new ClaimsIdentity(claims, "Test")), "Test");

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

}