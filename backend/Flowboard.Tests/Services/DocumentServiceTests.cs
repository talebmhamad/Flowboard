using Flowboard.Application.DTOs;
using FluentAssertions;
using Flowboard.Tests.Helpers;
using System.Net;
using System.Text;

namespace Flowboard.Tests.Services
{
    public class DocumentServiceTests
    {
        [Fact]
        public async Task SaveDocumentAsync_Should_Return_Result_When_Response_Is_Success()
        {
            // Arrange

            var expectedResult =
                "{ \"success\": true }";

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,

                    Content = new StringContent(
                        expectedResult,
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
                new DocumentService(httpClient);

            var request =
                new SaveDocumentDto
                {
                    DocumentTypeId = 1,
                    FormData = "{ \"name\": \"mhamad\" }",
                    WorkflowId = 10
                };

            // Act

            var result =
                await service.SaveDocumentAsync(request);

            // Assert

            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task SaveDocumentAsync_Should_Throw_Exception_When_Response_Is_Failure()
        {
            // Arrange

            var errorMessage =
                "Portal Internal Server Error";

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode =
                        HttpStatusCode.InternalServerError,

                    Content = new StringContent(
                        errorMessage,
                        Encoding.UTF8,
                        "text/plain")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new DocumentService(httpClient);

            var request =
                new SaveDocumentDto
                {
                    DocumentTypeId = 1,
                    FormData = "{ \"name\": \"mhamad\" }",
                    WorkflowId = 10
                };

            // Act

            Func<Task> act = async () =>
                await service.SaveDocumentAsync(request);

            // Assert

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("*Portal API Error*");
        }

        [Fact]
        public async Task GetDocumentById_Should_Return_Result_When_Response_Is_Success()
        {
            // Arrange

            var expectedResult =
                "{ \"id\": 1, \"title\": \"Document\" }";

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,

                    Content = new StringContent(
                        expectedResult,
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
                new DocumentService(httpClient);

            // Act

            var result =
                await service.GetDocumentById(1);

            // Assert

            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task GetDocumentById_Should_Throw_Exception_When_Response_Is_Failure()
        {
            // Arrange

            var errorMessage =
                "Document Not Found";

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,

                    Content = new StringContent(
                        errorMessage,
                        Encoding.UTF8,
                        "text/plain")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new DocumentService(httpClient);

            // Act

            Func<Task> act = async () =>
                await service.GetDocumentById(1);

            // Assert

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("*Portal API Error*");
        }
    }
}