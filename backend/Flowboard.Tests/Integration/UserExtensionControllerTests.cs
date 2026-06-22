using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using Flowboard.Intalio.Interfaces;
using Moq;
using Xunit;

namespace Flowboard.API.Tests.Integration;

public class UserExtensionControllerIntegrationTests : IDisposable
{
    private readonly TestWebApplicationFactory _factory;
    private readonly Mock<IUserExtensionService> _userServiceMock;

    public UserExtensionControllerIntegrationTests()
    {
        _userServiceMock = new Mock<IUserExtensionService>();
        _factory = new TestWebApplicationFactory(_userServiceMock);
    }

    public void Dispose() => _factory.Dispose();

    private HttpClient CreateAuthenticatedClient()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Test", "anything");
        return client;
    }

    private HttpClient CreateUnauthenticatedClient() => _factory.CreateClient();


    // GET /api/userextension/GetByManager?ManagerId={id}
    [Fact]
    public async Task GetByManager_ReturnsOk_WithEmployeeList()
    {
        // Arrange
        var expected = new List<UserLookupDto>
        {
            new UserLookupDto { Id = 1, Name = "Alice Johnson" },
            new UserLookupDto { Id = 2, Name = "Bob Smith"     }
        };

        _userServiceMock
            .Setup(s => s.GetEmployeesByManagerIdAsync(10))
            .ReturnsAsync(expected);

        var client = CreateAuthenticatedClient();

        // Act
        var response = await client.GetAsync("/api/userextension/GetByManager?ManagerId=10");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<List<UserLookupDto>>();

        Assert.NotNull(body);
        Assert.Equal(2, body.Count);
        Assert.Equal("Alice Johnson", body[0].Name);
    }

    [Fact]
    public async Task GetByManager_ReturnsOk_WithEmptyList_WhenManagerHasNoEmployees()
    {
        // Arrange
        _userServiceMock
            .Setup(s => s.GetEmployeesByManagerIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<UserLookupDto>());

        var client = CreateAuthenticatedClient();

        // Act
        var response = await client.GetAsync("/api/userextension/GetByManager?ManagerId=99");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<List<UserLookupDto>>();

        Assert.NotNull(body);
        Assert.Empty(body);
    }

    [Fact]
    public async Task GetByManager_Returns500_WhenServiceThrows()
    {
        // Arrange
        _userServiceMock
            .Setup(s => s.GetEmployeesByManagerIdAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Database unavailable"));

        var client = CreateAuthenticatedClient();

        // Act
        var response = await client.GetAsync("/api/userextension/GetByManager?ManagerId=1");

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    // Query parameter / model binding checks
    [Fact]
    public async Task GetByManager_CallsServiceWithZero_WhenManagerIdIsMissing()
    {
        // Arrange — missing ManagerId binds as 0 (default int)
        _userServiceMock
            .Setup(s => s.GetEmployeesByManagerIdAsync(0))
            .ReturnsAsync(new List<UserLookupDto>());

        var client = CreateAuthenticatedClient();

        // Act
        var response = await client.GetAsync("/api/userextension/GetByManager");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _userServiceMock.Verify(s => s.GetEmployeesByManagerIdAsync(0), Times.Once);
    }

    [Fact]
    public async Task GetByManager_ReturnsBadRequest_WhenManagerIdIsNotAnInteger()
    {
        // Arrange
        var client = CreateAuthenticatedClient();

        // Act
        var response = await client.GetAsync("/api/userextension/GetByManager?ManagerId=abc");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // Route / method contract checks
    [Fact]
    public async Task GetByManager_OnlyAcceptsGetRequests()
    {
        // Arrange
        _userServiceMock
            .Setup(s => s.GetEmployeesByManagerIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<UserLookupDto>());

        var client = CreateAuthenticatedClient();

        // Act
        var postResponse = await client.PostAsync("/api/userextension/GetByManager?ManagerId=1", null);
        var deleteResponse = await client.DeleteAsync("/api/userextension/GetByManager?ManagerId=1");

        // Assert
        Assert.Equal(HttpStatusCode.MethodNotAllowed, postResponse.StatusCode);
        Assert.Equal(HttpStatusCode.MethodNotAllowed, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task GetByManager_ServiceCalledWithCorrectManagerId()
    {
        // Arrange
        const int managerId = 5;

        _userServiceMock
            .Setup(s => s.GetEmployeesByManagerIdAsync(managerId))
            .ReturnsAsync(new List<UserLookupDto>());

        var client = CreateAuthenticatedClient();

        // Act
        await client.GetAsync($"/api/userextension/GetByManager?ManagerId={managerId}");

        // Assert
        _userServiceMock.Verify(s => s.GetEmployeesByManagerIdAsync(managerId), Times.Once);
    }
}