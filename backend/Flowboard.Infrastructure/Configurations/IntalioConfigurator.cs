using Intalio.Core;
using Microsoft.Extensions.Configuration;

namespace Flowboard.Infrastructure.Configurations
{
    public static class IntalioConfigurator
    {
        public static void Configure(IConfiguration configuration)
        {
            Intalio.Case.Core.Configuration.DbConnectionString =
                configuration.GetConnectionString("CaseConnection");

            Intalio.Case.Core.Configuration.DatabaseType =
                configuration.GetValue<DatabaseType>(
                    "DatabaseType",
                    DatabaseType.MSSQL
                );
        }
    }
}