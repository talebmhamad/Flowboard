using Microsoft.Extensions.Configuration;

namespace Flowboard.Intalio.Configurations
{
    public static class IntalioConfigurator
    {
        public static void Configure(IConfiguration configuration)
        {
            // CASE
            global::Intalio.Case.Core.Configuration.DbConnectionString =
                configuration.GetConnectionString("CaseConnection");

            global::Intalio.Case.Core.Configuration.DatabaseType =
                configuration.GetValue<global::Intalio.Core.DatabaseType>(
                    "DatabaseType",
                    global::Intalio.Core.DatabaseType.MSSQL
                );

        }
    }
}