using Flowboard.Application.Interfaces;
using Flowboard.Infrastructure.Services;
using Flowboard.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//  Services

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    //  Add JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token like: Bearer YOUR_TOKEN"
    });

    //  Apply it globally
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

//  IAM Settings
builder.Services.Configure<IamSettings>(
    builder.Configuration.GetSection("IAM"));

builder.Services.Configure<PortalSettings>(
    builder.Configuration.GetSection("Portal"));

builder.Services.AddHttpClient<IUserTaskService, UserTaskService>((sp, client) =>
{
    var portalSettings = sp.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<PortalSettings>>().Value;

    client.BaseAddress = new Uri(portalSettings.BaseUrl);
});

//  JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:4000"; 
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            RoleClaimType = "role",  
            NameClaimType = "sub"
        };
    });

//  Authorization
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  Enable CORS
app.UseCors("AllowAll");

app.UseAuthentication(); 
app.UseAuthorization();

//  Map Controllers
app.MapControllers();

app.Run();