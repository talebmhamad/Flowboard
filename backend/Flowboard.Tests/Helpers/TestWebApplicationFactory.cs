using Flowboard.Intalio.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Mock<IUserExtensionService> _userServiceMock;

    public TestWebApplicationFactory(Mock<IUserExtensionService> userServiceMock)
    {
        _userServiceMock = userServiceMock;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(IUserExtensionService));

            if (descriptor != null)
                services.Remove(descriptor);

            services.AddSingleton(_userServiceMock.Object);

            services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
        });
    }
}