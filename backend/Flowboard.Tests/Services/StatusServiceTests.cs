using FluentAssertions;
using Flowboard.Tests.Helpers;
using System.Net;
using System.Text;

namespace Flowboard.Tests.Services
{
    public class StatusServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Should_Return_Status_List_When_Response_Is_Valid()
        {
            // Arrange

            var json =
                """
                [
                    {
                        "id": 1,
                        "name": "Open"
                    },
                    {
                        "id": 2,
                        "name": "Closed"
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
                new StatusService(httpClient);

            // Act

            var result =
                await service.GetAllAsync();

            // Assert

            result.Should().NotBeNull();

            result.Should().HaveCount(2);

            result[0].Id.Should().Be(1);

            result[1].Id.Should().Be(2);
        }

        [Fact]
        public async Task GetAllAsync_Should_Throw_Exception_When_Response_Is_Failure()
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
                new StatusService(httpClient);

            // Act

            Func<Task> act = async () =>
                await service.GetAllAsync();

            // Assert

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("*Failed to fetch statuses*");
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_Empty_List_When_Response_Is_Null()
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
                new StatusService(httpClient);

            // Act

            var result =
                await service.GetAllAsync();

            // Assert

            result.Should().NotBeNull();

            result.Should().BeEmpty();
        }
    }
}