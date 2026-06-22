using Flowboard.Application.DTOs;
using Flowboard.Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net;

namespace Flowboard.Tests.Integration
{
    [Trait("Category", "Integration")]
    public class LookupControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ILookupService> _lookupServiceMock = new();

        public LookupControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton(_lookupServiceMock.Object);

                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
                });
            });
        }

        private HttpClient AuthClient()
        {
            var client = _factory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Test-UserId", "1");
            return client;
        }

        // Happy path
        [Fact]
        public async Task GetLookupItemsByName_WithValidParams_ReturnsOk()
        {
            _lookupServiceMock
                .Setup(s => s.GetLookupItemsByNameAsync("Status", 1))
                .ReturnsAsync(new List<LookupItemDto> { new LookupItemDto { Id = 1, Text = "TEST" } });

            var response = await AuthClient()
                .GetAsync("/api/lookup/GetLookupItemsByName?name=Status&language=1");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        // Authorization
        [Fact]
        public async Task GetLookupItemsByName_WithoutAuth_ReturnsUnauthorized()
        {
            var response = await _factory.CreateClient()
                .GetAsync("/api/lookup/GetLookupItemsByName?name=Status&language=1");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        // Missing params
        [Fact]
        public async Task GetLookupItemsByName_WithMissingName_ReturnsBadRequest()
        {
            var response = await AuthClient()
                .GetAsync("/api/lookup/GetLookupItemsByName?language=1");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetLookupItemsByName_WithMissingLanguage_ReturnsBadRequest()
        {
            var response = await AuthClient()
                .GetAsync("/api/lookup/GetLookupItemsByName?name=Status");

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}