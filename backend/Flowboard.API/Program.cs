using Flowboard.Infrastructure.Services;
using Flowboard.Infrastructure.Settings;
using Flowboard.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//  Services 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<IamSettings>(
    builder.Configuration.GetSection("IAM"));

builder.Services.AddHttpClient<IAuthService, AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:5178") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

//  Middleware 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  Enable CORS (must be before authorization/endpoints)
app.UseCors("AllowReact");

app.UseAuthorization();

app.MapControllers();

app.Run();