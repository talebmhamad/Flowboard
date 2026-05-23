using Flowboard.Application.Services;
using FluentAssertions;
using Flowboard.Tests.Helpers;
using System.Net;
using System.Text;

namespace Flowboard.Tests.Services
{
    public class LookupServiceTests
    {
        [Fact]
        public async Task GetLookupItemsByNameAsync_Should_Return_List_When_Response_Is_Valid()
        {
            // Arrange

            var json =
                """
                [
                    {
                        "id": 1,
                        "text": "Manager"
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
                new LookupService(httpClient);

            // Act

            var result =
                await service.GetLookupItemsByNameAsync(
                    "roles",
                    1);

            // Assert

            result.Should().NotBeNull();

            result.Should().HaveCount(1);

            result[0].Id.Should().Be(1);

            result[0].Text.Should().Be("Manager");
        }

        [Fact]
        public async Task GetLookupItemsByNameAsync_Should_Throw_Exception_When_Response_Is_Failure()
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
                new LookupService(httpClient);

            // Act

            Func<Task> act = async () =>
                await service.GetLookupItemsByNameAsync(
                    "roles",
                    1);

            // Assert

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("*Failed to fetch lookup items*");
        }

        [Fact]
        public async Task GetLookupItemsByNameAsync_Should_Return_Empty_List_When_Response_Is_Null()
        {
            // Arrange

            var json = "null";

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
                new LookupService(httpClient);

            // Act

            var result =
                await service.GetLookupItemsByNameAsync(
                    "roles",
                    1);

            // Assert

            result.Should().NotBeNull();

            result.Should().BeEmpty();
        }
    }
}