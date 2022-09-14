using Hotel_listing.Application.Utilities;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace Hotel_listing.Application.Common
{
    public static class LoggerConfig
    {
        public static void LoggerConnector(WebApplicationBuilder builder)
        {
            //TODO dynamic Application name
            var logsPath = Utils.GenerateBasePath("\\Hotel_listing.Application\\Logs\\Log_.txt");
            builder.Host.UseSerilog((_, lc) =>lc
                .WriteTo.File(
                    path:logsPath,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: LogEventLevel.Information
                ).WriteTo.Console()
            );
        }
    }
}