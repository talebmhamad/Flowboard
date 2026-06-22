using Flowboard.Application.DTOs;
using Flowboard.Infrastructure.Services;

using FluentAssertions;

using Flowboard.Tests.Helpers;

using System.Net;
using System.Text;

namespace Flowboard.Tests.Services
{
    public class WorkflowServiceTests
    {
        [Fact]
        public async Task GetWorkflowsAsync_Should_Return_Workflow_List_When_Response_Is_Valid()
        {
            // Arrange

            var json =
                """
                [
                    {
                        "id": 1,
                        "name": "Instructor Release"
                    },
                    {
                        "id": 2,
                        "name": "Leave Request"
                    }
                ]
                """;

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,

                    Content = new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new WorkflowService(httpClient);

            // Act

            var result =
                await service.GetWorkflowsAsync();

            // Assert

            result.Should().NotBeNull();

            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetWorkflowsAsync_Should_Throw_Exception_When_Response_Is_Failure()
        {
            // Arrange

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode =
                        HttpStatusCode.InternalServerError,

                    Content = new StringContent(
                        "Server Error")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new WorkflowService(httpClient);

            // Act

            Func<Task> act = async () =>
                await service.GetWorkflowsAsync();

            // Assert

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("*Portal API Error*");
        }

        [Fact]
        public async Task GetWorkflowsAsync_Should_Return_Empty_List_When_Response_Is_Empty()
        {
            // Arrange

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,

                    Content = new StringContent("")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new WorkflowService(httpClient);

            // Act

            var result =
                await service.GetWorkflowsAsync();

            // Assert

            result.Should().NotBeNull();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetWorkflowFormAsync_Should_Return_Form_When_Response_Is_Valid()
        {
            // Arrange

            var formJson =
                """
                {
                    "components": []
                }
                """;

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,

                    Content = new StringContent(
                        formJson,
                        Encoding.UTF8,
                        "application/json")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new WorkflowService(httpClient);

            // Act

            var result =
                await service.GetWorkflowFormAsync(1);

            // Assert

            result.Should().Be(formJson);
        }

        [Fact]
        public async Task GetWorkflowFormAsync_Should_Return_Empty_Object_When_Response_Is_Empty()
        {
            // Arrange

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,

                    Content = new StringContent("")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new WorkflowService(httpClient);

            // Act

            var result =
                await service.GetWorkflowFormAsync(1);

            // Assert

            result.Should().Be("{}");
        }

        [Fact]
        public async Task GetWorkflowFormAsync_Should_Throw_Exception_When_Response_Is_Failure()
        {
            // Arrange

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,

                    Content = new StringContent(
                        "Bad Request")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new WorkflowService(httpClient);

            // Act

            Func<Task> act = async () =>
                await service.GetWorkflowFormAsync(1);

            // Assert

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("*Portal API Error*");
        }
    }
}