using Flowboard.Infrastructure.Services;
using Flowboard.Infrastructure.Settings;
using Flowboard.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;

namespace Flowboard.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task LoginAsync_Should_Return_Token_When_Response_Is_Valid()
        {
            // Arrange

            var responseJson =
                @"{ ""access_token"": ""test-token"" }";

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        responseJson,
                        Encoding.UTF8,
                        "application/json")
                });

            var httpClient = new HttpClient(handler);

            var settings = Options.Create(new IamSettings
            {
                Url = "https://fake-url.com",
                ClientId = "client-id",
                ClientSecret = "secret"
            });

            var authService =
                new AuthService(httpClient, settings);

            // Act

            var result =
                await authService.LoginAsync(
                    "admin",
                    "123");

            // Assert

            result.Should().Be("wrong-token");
        }

        [Fact]
        public async Task LoginAsync_Should_Throw_UnauthorizedException_When_Response_Is_Invalid()
        {
            // Arrange

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("Invalid credentials")
                });

            var httpClient = new HttpClient(handler);

            var settings = Options.Create(new IamSettings
            {
                Url = "https://fake-url.com",
                ClientId = "client-id",
                ClientSecret = "secret"
            });

            var authService =
                new AuthService(httpClient, settings);

            // Act

            Func<Task> act = async () =>
                await authService.LoginAsync("admin", "wrong-password");

            // Assert

            await act.Should()
                .ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("*Login failed*");
        }

        [Fact]
        public async Task LoginAsync_Should_Throw_Exception_When_Token_Is_Missing()
        {
            // Arrange

            var responseJson =
                @"{ ""name"": ""mhamad"" }";

            var handler = new FakeHttpMessageHandler(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        responseJson,
                        Encoding.UTF8,
                        "application/json")
                });

            var httpClient = new HttpClient(handler);

            var settings = Options.Create(new IamSettings
            {
                Url = "https://fake-url.com",
                ClientId = "client-id",
                ClientSecret = "secret"
            });

            var authService =
                new AuthService(httpClient, settings);

            // Act

            Func<Task> act = async () =>
                await authService.LoginAsync(
                    "admin",
                    "123");

            // Assert

            await act.Should()
                .ThrowAsync<Exception>()
                .WithMessage("*Token not found*");
        }
    }
}