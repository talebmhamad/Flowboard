using Flowboard.Infrastructure.Services;
using FluentAssertions;
using Flowboard.Tests.Helpers;
using System.Net;
using System.Text;

namespace Flowboard.Tests.Services
{
    public class UserSummaryServiceTests
    {
        [Fact]
        public async Task GetSummary_Should_Return_All_Counts_When_Responses_Are_Valid()
        {
            // Arrange

            var json =
                """
                {
                    "today": 5,
                    "total": 20
                }
                """;

            var handler =
                new DynamicFakeHttpMessageHandler(request =>
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,

                        Content = new StringContent(
                            json,
                            Encoding.UTF8,
                            "application/json")
                    };
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new UserSummaryService(httpClient);

            // Act

            var result =
                await service.GetSummary();

            // Assert

            result.Should().NotBeNull();

            result.Draft.Total.Should().Be(20);

            result.Inbox.Total.Should().Be(20);

            result.Completed.Total.Should().Be(20);

            result.MyRequests.Total.Should().Be(20);

            result.Closed.Total.Should().Be(20);
        }

        [Fact]
        public async Task GetSummary_Should_Return_Empty_Count_When_Request_Fails()
        {
            // Arrange

            var handler =
                new DynamicFakeHttpMessageHandler(request =>
                {
                    if (request.RequestUri!
                        .ToString()
                        .Contains("GetInboxCounts"))
                    {
                        return new HttpResponseMessage
                        {
                            StatusCode =
                                HttpStatusCode.InternalServerError
                        };
                    }

                    var json =
                        """
                        {
                            "today": 5,
                            "total": 20
                        }
                        """;

                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,

                        Content = new StringContent(
                            json,
                            Encoding.UTF8,
                            "application/json")
                    };
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new UserSummaryService(httpClient);

            // Act

            var result =
                await service.GetSummary();

            // Assert

            result.Should().NotBeNull();

            result.Draft.Total.Should().Be(20);

            result.Inbox.Total.Should().Be(0);

            result.Completed.Total.Should().Be(20);

            result.MyRequests.Total.Should().Be(20);

            result.Closed.Total.Should().Be(20);
        }

        [Fact]
        public async Task GetSummary_Should_Return_Empty_Count_When_Response_Is_Null()
        {
            // Arrange

            var handler =
                new DynamicFakeHttpMessageHandler(request =>
                {
                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,

                        Content = new StringContent(
                            "null",
                            Encoding.UTF8,
                            "application/json")
                    };
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new UserSummaryService(httpClient);

            // Act

            var result =
                await service.GetSummary();

            // Assert

            result.Should().NotBeNull();

            result.Draft.Total.Should().Be(0);

            result.Inbox.Total.Should().Be(0);

            result.Completed.Total.Should().Be(0);

            result.MyRequests.Total.Should().Be(0);

            result.Closed.Total.Should().Be(0);
        }
    }
}