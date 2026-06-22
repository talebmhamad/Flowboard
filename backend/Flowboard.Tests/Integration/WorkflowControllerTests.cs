using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;
using System.Net.Http.Json;

namespace Flowboard.API.Tests.Integration;

[Trait("Category", "Integration")]
public class WorkflowControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IWorkflowService> _workflowServiceMock;

    public WorkflowControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _workflowServiceMock = new Mock<IWorkflowService>();

        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace the real service with the mock
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(IWorkflowService));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddSingleton(_workflowServiceMock.Object);
            });
        });
    }

    // GET /api/workflow
    [Fact]
    public async Task GetWorkflows_ReturnsOk_WithWorkflowList()
    {
        // Arrange
        var expected = new List<WorkflowDto>
    {
        new WorkflowDto { Id = 1, Name = "Invoice Approval" },
        new WorkflowDto { Id = 2, Name = "Leave Request"    }
    };

        _workflowServiceMock
            .Setup(s => s.GetWorkflowsAsync())
            .ReturnsAsync(expected);

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/workflow");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<List<WorkflowDto>>();

        Assert.NotNull(body);
        Assert.Equal(2, body.Count);
        Assert.Equal("Invoice Approval", body[0].Name);
    }

    [Fact]
    public async Task GetWorkflows_ReturnsOk_WithEmptyList_WhenNoWorkflowsExist()
    {
        // Arrange
        _workflowServiceMock
            .Setup(s => s.GetWorkflowsAsync())
            .ReturnsAsync(new List<WorkflowDto>());

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/workflow");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadFromJsonAsync<List<WorkflowDto>>();

        Assert.NotNull(body);
        Assert.Empty(body);
    }

    [Fact]
    public async Task GetWorkflows_Returns500_WhenServiceThrows()
    {
        // Arrange
        _workflowServiceMock
            .Setup(s => s.GetWorkflowsAsync())
            .ThrowsAsync(new Exception("Database unavailable"));

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/workflow");

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    // GET /api/workflow/form/{documentTypeId}
    [Fact]
    public async Task GetForm_ReturnsOk_WithFormData_ForValidDocumentTypeId()
    {
        // Arrange
        const int documentTypeId = 42;
        var expectedForm = """{"documentTypeId": 42, "fields": ["FieldA", "FieldB"]}""";

        _workflowServiceMock
            .Setup(s => s.GetWorkflowFormAsync(documentTypeId))
            .ReturnsAsync(expectedForm);

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/workflow/form/{documentTypeId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();

        Assert.NotNull(body);
        Assert.Equal(expectedForm, body);
    }

    [Fact]
    public async Task GetForm_ReturnsBadRequest_ForNonIntegerDocumentTypeId()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act — pass a non-integer segment so model binding fails
        var response = await client.GetAsync("/api/workflow/form/not-a-number");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetForm_ReturnsNoContent_WhenFormDoesNotExist()
    {
        // Arrange — simulate service returning null for an unknown document type
        _workflowServiceMock!
            .Setup(s => s.GetWorkflowFormAsync(It.IsAny<int>()))!
            .ReturnsAsync((string?)null);

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/workflow/form/999");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task GetForm_Returns500_WhenServiceThrows()
    {
        // Arrange
        _workflowServiceMock
            .Setup(s => s.GetWorkflowFormAsync(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Unexpected error"));

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/workflow/form/1");

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    // Route / method contract checks
    [Theory]
    [InlineData("/api/workflow")]
    [InlineData("/api/workflow/form/1")]
    public async Task Endpoints_OnlyAcceptGetRequests(string url)
    {
        // Arrange
        _workflowServiceMock
            .Setup(s => s.GetWorkflowsAsync())
            .ReturnsAsync(new List<WorkflowDto>());

        _workflowServiceMock!
            .Setup(s => s.GetWorkflowFormAsync(It.IsAny<int>()))!
            .ReturnsAsync((string?)null);

        var client = _factory.CreateClient();

        // Act
        var postResponse = await client.PostAsync(url, null);
        var deleteResponse = await client.DeleteAsync(url);

        // Assert — non-GET verbs should be rejected
        Assert.Equal(HttpStatusCode.MethodNotAllowed, postResponse.StatusCode);
        Assert.Equal(HttpStatusCode.MethodNotAllowed, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task GetWorkflows_ServiceCalledExactlyOnce()
    {
        // Arrange
        _workflowServiceMock
            .Setup(s => s.GetWorkflowsAsync())
            .ReturnsAsync(new List<WorkflowDto>());

        var client = _factory.CreateClient();

        // Act
        await client.GetAsync("/api/workflow");

        // Assert
        _workflowServiceMock.Verify(s => s.GetWorkflowsAsync(), Times.Once);
    }

    [Fact]
    public async Task GetForm_ServiceCalledWithCorrectDocumentTypeId()
    {
        // Arrange
        const int documentTypeId = 7;

        _workflowServiceMock!
            .Setup(s => s.GetWorkflowFormAsync(documentTypeId))!
            .ReturnsAsync((string?)null);

        var client = _factory.CreateClient();

        // Act
        await client.GetAsync($"/api/workflow/form/{documentTypeId}");

        // Assert
        _workflowServiceMock.Verify(s => s.GetWorkflowFormAsync(documentTypeId), Times.Once);
    }


}