using Flowboard.Intalio.Context;
using Flowboard.Intalio.Interfaces;
using Flowboard.Intalio.Repositories;
using Flowboard.Intalio.Services;
using Flowboard.Intalio.Services.Interfaces;
using Intalio.Case.Portal.Core.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flowboard.Intalio.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIntalioInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<IAMContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("IAMConnection")));

            services.AddDbContext<CasePortalContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("CaseConnection")));

            services.AddScoped<UserRepository>();
            services.AddScoped<IUserExtensionService, UserExtensionService>();

            services.AddScoped<TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}