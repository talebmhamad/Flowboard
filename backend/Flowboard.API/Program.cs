using Flowboard.Application.Interfaces;
using Flowboard.Application.Services;
using Flowboard.Infrastructure.Handlers;
using Flowboard.Infrastructure.Services;
using Flowboard.Infrastructure.Settings;
using Flowboard.Intalio.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
var iamSettings = builder.Configuration.GetSection("IAM").Get<IamSettings>()!;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token like: Bearer YOUR_TOKEN"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.Configure<IamSettings>(
    builder.Configuration.GetSection("IAM"));

builder.Services.Configure<PortalSettings>(
    builder.Configuration.GetSection("Portal"));

builder.Services.AddHttpClient<IWorkflowService, WorkflowService>((sp, client) =>
{
    var portalSettings = sp
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<PortalSettings>>()
        .Value;

    client.BaseAddress = new Uri(portalSettings.BaseUrl);
})
.AddHttpMessageHandler<AuthTokenHandler>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = iamSettings.Url;
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = "IdentityServerApi",

            ValidateIssuer = true,
            ValidIssuer = iamSettings.Url,

            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            NameClaimType = "sub"
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddIntalioInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthTokenHandler>();


// User Tasks Service
builder.Services.AddHttpClient<IUserTaskService, UserTaskService>((sp, client) =>
{
    var portalSettings = sp
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<PortalSettings>>()
        .Value;

    client.BaseAddress = new Uri(portalSettings.BaseUrl);
})
.AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<IDocumentService, DocumentService>((sp, client) =>
{
    var portalSettings = sp
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<PortalSettings>>()
        .Value;

    client.BaseAddress = new Uri(portalSettings.BaseUrl);
})
.AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<IStatusService, StatusService>((sp, client) =>
{
    var portalSettings = sp
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<PortalSettings>>()
        .Value;

    client.BaseAddress = new Uri(portalSettings.BaseUrl);
})
.AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<ILookupService, LookupService>((sp, client) =>
{
    var portalSettings = sp
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<PortalSettings>>()
        .Value;

    client.BaseAddress = new Uri(portalSettings.BaseUrl);
})
.AddHttpMessageHandler<AuthTokenHandler>();


// User Summary Service
builder.Services.AddHttpClient<IUserSummaryService, UserSummaryService>((sp, client) =>
{
    var portalSettings = sp
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<PortalSettings>>()
        .Value;

    client.BaseAddress = new Uri(portalSettings.BaseUrl);
})
.AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddHttpClient<IAuthService, AuthService>((sp, client) =>
{
    var iam = sp
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<IamSettings>>()
        .Value;

    client.BaseAddress = new Uri(iam.Url);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

IntalioConfigurator.Configure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

