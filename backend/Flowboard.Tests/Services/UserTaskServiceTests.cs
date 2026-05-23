using Flowboard.Infrastructure.Services;

using FluentAssertions;

using Flowboard.Tests.Helpers;

using System.Net;
using System.Text;

namespace Flowboard.Tests.Services
{
    public class UserTaskServiceTests
    {
        [Fact]
        public async Task GetTaskDetails_Should_Return_Result_When_Response_Is_Valid()
        {
            // Arrange

            var expectedResult =
                "{ \"taskId\": 1, \"status\": \"Open\" }";

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
                new UserTaskService(httpClient);

            // Act

            var result =
                await service.GetTaskDetails(1);

            // Assert

            result.Should().Be(expectedResult);
        }

        [Fact]
        public async Task GetTaskDetails_Should_Throw_Exception_When_Response_Is_Failure()
        {
            // Arrange

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,

                    Content = new StringContent(
                        "Task Not Found")
                });

            var httpClient =
                new HttpClient(handler)
                {
                    BaseAddress =
                        new Uri("https://fake-url.com/")
                };

            var service =
                new UserTaskService(httpClient);

            // Act

            Func<Task> act = async () =>
                await service.GetTaskDetails(1);

            // Assert

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("*Portal API Error*");
        }
    }
}